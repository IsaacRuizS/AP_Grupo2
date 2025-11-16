using FB.Core;
using FB.Data;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FB.MVC.Controllers
{
    [RoutePrefix("UsersDB")]    // ← URL becomes /UsersDB
    public class UsersDBController : ControllerBase
    {
        // GET: UsersDB
        [Route("")]
        public ActionResult Index()
        {
            return View(UserBusiness.GetUsers(0));
        }

        // GET: UsersDB/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = (User)UserBusiness.GetUsers(id.Value).FirstOrDefault();

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // GET: UsersDB/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersDB/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
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

        // GET: UsersDB/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = (User)UserBusiness.GetUsers(id.Value).FirstOrDefault();

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: UsersDB/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit([Bind(Include = "UserId,Username,Email,FullName,IsActive,CreatedAt,LastLogin")] User user)
        {
            if (ModelState.IsValid)
            {
                UserBusiness.SaveOrUpdate(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: UsersDB/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = (User)UserBusiness.GetUsers(id.Value).FirstOrDefault();

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: UsersDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("DeleteConfirmed/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
