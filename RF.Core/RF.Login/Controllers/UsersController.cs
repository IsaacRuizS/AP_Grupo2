using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RF.Data;
using Microsoft.AspNet.Identity;

namespace RF.Login.Controllers
{
    public class UsersController : Controller
    {
        private RestaurantFinderEntities db = new RestaurantFinderEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserRole);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FullName,Email,PasswordHash,RoleID,IsActive,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                // Hashear la contraseña antes de guardar
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    var hasher = new PasswordHasher();
                    user.PasswordHash = hasher.HashPassword(user.PasswordHash);
                }

                user.CreatedAt = DateTime.Now;
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    // Verificar si es un error de clave unica, osea el UNIQUE del sql
                    var sqlException = ex.InnerException?.InnerException as System.Data.SqlClient.SqlException;
                    if (sqlException != null && (sqlException.Number == 2601 || sqlException.Number == 2627))
                    {
                        if (sqlException.Message.Contains("Email"))
                        {
                            ModelState.AddModelError("Email", "Este correo electrónico ya está registrado.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ya existe un registro con esta información.");
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "Name", user.RoleID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "Name", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FullName,Email,PasswordHash,RoleID,IsActive,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedAt = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "Name", user.RoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
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
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
