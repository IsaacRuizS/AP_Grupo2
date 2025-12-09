using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using FB.Login.Models;
using FB.Data;
using FB.Core;

namespace FB.Login.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        [AllowAnonymous]
        public ActionResult DebugAutoLogin()
        {
            try
            {
                var userBus = new UserDetailBusiness();

                var user = userBus.GetByEmail("admin@demo.com");

                if (user == null)
                {
                    return Content("El usuario admin@demo.com no existe.");
                }

                var claims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.Name) ? user.Email : user.Name),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true   
                }, identity);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return Content("Ocurrió un error al intentar hacer auto-login.");
            }
        }


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }

        private const string XsrfKey = "XsrfId";

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userBus = new UserDetailBusiness();
                if (!userBus.ValidateCredentials(model.Email, model.Password))
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }

                var user = userBus.GetByEmail(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.Name) ? user.Email : user.Name),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                return RedirectToLocal(returnUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userName = !string.IsNullOrWhiteSpace(model.UserName) ? model.UserName : model.Email;
            var user = new ApplicationUser { UserName = userName, Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Sign in and optionally sync to Foodbank
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                try { UserSync.SyncUserToFoodbank(model.Email, updateLastLogin: true); } catch { }
                return RedirectToAction("Index", "Home");
            }

            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff(FormCollection unused)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);

            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null) { }
            public ChallengeResult(string provider, string redirectUri, string userId) { LoginProvider = provider; RedirectUri = redirectUri; UserId = userId; }
            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null) properties.Dictionary[XsrfKey] = UserId;
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}