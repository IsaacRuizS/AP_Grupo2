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

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRoles((int)id).FirstOrDefault();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRoles((int)id).FirstOrDefault();
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(UserBusiness.GetUsers(0), "UserId", "Username");
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = UserRoleBusiness.GetUserRoles((int)id).FirstOrDefault();
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRoleBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
