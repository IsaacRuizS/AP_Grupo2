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
    public class RolesController : Controller
    {
        private readonly RolBusiness _roleBusiness;

        public RolesController()
        {
            _roleBusiness = new RolBusiness();
        }

        // GET: Roles
        public ActionResult Index()
        {
            // Obtiene todos los roles
            var roles = _roleBusiness.GetRols(0);
            return View(roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            // Valida si el id es nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el rol por id
            Roles roles = (Roles)_roleBusiness.GetRols((int)id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            // Muestra el formulario de creación de rol
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                // Guarda el nuevo rol
                _roleBusiness.SaveOrUpdate(roles);
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            // Valida si el id es nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el rol a editar
            Roles roles = (Roles)_roleBusiness.GetRols((int)id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el rol
                _roleBusiness.SaveOrUpdate(roles);
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            // Valida si el id es nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el rol a eliminar
            Roles roles = (Roles)_roleBusiness.GetRols((int)id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Elimina el rol
            _roleBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
