using FB.Core;
using FB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FB.MVC.Controllers
{
    public class UsersController : ControllerBase
    {

        // GET: Users
        public ActionResult Index()
        {
            return View(UserBusiness.GetUsers(0));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = (User)UserBusiness.GetUsers((int)id).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Email,FullName,IsActive,CreatedAt,LastLogin")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                user.LastLogin = DateTime.Now;

                UserBusiness.SaveOrUpdate(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = (User)UserBusiness.GetUsers((int)id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Email,FullName,IsActive,CreatedAt,LastLogin")] User user)
        {
            if (ModelState.IsValid)
            {
                //tomar el created y login del usuario
                /*User baseUser = (User)UserBusiness.GetUsers((int)user.UserId).FirstOrDefault();
                if (baseUser != null)
                {
                    user.CreatedAt = baseUser.CreatedAt;
                    user.LastLogin = baseUser.LastLogin;

                }*/

                UserBusiness.SaveOrUpdate(user);


                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = (User)UserBusiness.GetUsers((int)id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = UserBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
