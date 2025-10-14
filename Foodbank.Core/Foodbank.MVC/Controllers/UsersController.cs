using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AP.Data;
using AP.Core;

namespace AP.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserBusiness _userBusiness;

        public UsersController()
        {
            _userBusiness = new UserBusiness();
        }

        // GET: Users
        public ActionResult Index()
        {
            // Obtiene todos los usuarios
            var users = _userBusiness.GetUsers(0);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el usuario por id
            Users users = (Users)_userBusiness.GetUsers((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            // Muestra el formulario de creación de usuario
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Email,PasswordHash,CreatedAt,IsActive,LastModified,ModifiedBy,RoleID")] Users users)
        {
            if (ModelState.IsValid)
            {
                // Guarda el nuevo usuario
                _userBusiness.SaveOrUpdate(users);
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el usuario a editar
            Users users = (Users)_userBusiness.GetUsers((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Email,PasswordHash,CreatedAt,IsActive,LastModified,ModifiedBy,RoleID")] Users users)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el usuario
                _userBusiness.SaveOrUpdate(users);
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el usuario a eliminar
            Users users = (Users)_userBusiness.GetUsers((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Elimina el usuario
            _userBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
