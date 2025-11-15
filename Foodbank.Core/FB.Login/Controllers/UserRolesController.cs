using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FB.Data;

namespace FB.MVC.Controllers
{
    public class UserRolesController : ControllerBase
    {

        // GET: UserRoles
        public ActionResult Index()
        {
            var userRoles = UserRoleBusiness.GetUserRoles(0);
            return View(userRoles.ToList());
        }

        // GET: UserRoles/Details?userId=1&roleId=2
        public ActionResult Details(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRole((int)userId, (int)roleId);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(UserBusiness.GetUsers(0), "UserId", "Username");
            return View();
        }

        // POST: UserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,Description")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                UserRoleBusiness.Save(userRole);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(UserBusiness.GetUsers(0), "UserId", "Username");
            return View(userRole);
        }

        // GET: UserRoles/Edit?userId=1&roleId=2
        public ActionResult Edit(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRole((int)userId, (int)roleId);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(UserBusiness.GetUsers(0), "UserId", "Username");
            return View(userRole);
        }

        // POST: UserRoles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,Description")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                UserRoleBusiness.Update(userRole);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(UserBusiness.GetUsers(0), "UserId", "Username");
            return View(userRole);
        }

        // GET: UserRoles/Delete?userId=1&roleId=2
        public ActionResult Delete(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRole((int)userId, (int)roleId);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userId, int roleId)
        {
            UserRoleBusiness.Delete(userId, roleId);
            return RedirectToAction("Index");
        }
    }
}