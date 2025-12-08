using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RF.Login.Models;
using RF.Data;

namespace RF.Login.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private RestaurantFinderEntities db = new RestaurantFinderEntities();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Tu contraseña ha sido cambiada exitosamente."
                : message == ManageMessageId.UpdateProfileSuccess ? "Tu perfil ha sido actualizado exitosamente."
                : message == ManageMessageId.Error ? "Ha ocurrido un error."
                : "";

            // Get the UserDBId claim
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userDbIdClaim = claimsIdentity.FindFirst("UserDbId");
            
            if (userDbIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userDbId = int.Parse(userDbIdClaim.Value);
            
            // Fetch the user from the database
            User user = db.Users.Include(u => u.UserRole).FirstOrDefault(u => u.UserID == userDbId);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //
        // POST: /Manage/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile([Bind(Include = "UserID,FullName,Email,PasswordHash,RoleID,IsActive,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                // Get the current user's ID from claims
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userDbIdClaim = claimsIdentity.FindFirst("UserDBId");
                
                if (userDbIdClaim == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                int userDbId = int.Parse(userDbIdClaim.Value);

                // Ensure the user can only update their own profile
                if (user.UserID != userDbId)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }

                // Update only specific fields
                var existingUser = db.Users.Find(user.UserID);
                if (existingUser != null)
                {
                    existingUser.FullName = user.FullName;
                    existingUser.Email = user.Email;
                    existingUser.UpdatedAt = DateTime.Now;

                    db.Entry(existingUser).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { Message = ManageMessageId.UpdateProfileSuccess });
                }
            }

            return View("Index", user);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (db != null)
                {
                    db.Dispose();
                }
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            UpdateProfileSuccess,
            Error
        }

#endregion
    }
}