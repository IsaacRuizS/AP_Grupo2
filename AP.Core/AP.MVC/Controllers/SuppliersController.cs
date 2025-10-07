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
    public class SuppliersController : Controller
    {
        private readonly SupplierBusiness _supplierBusiness;

        public SuppliersController()
        {
            _supplierBusiness = new SupplierBusiness();
        }

        // GET: Suppliers
        public ActionResult Index()
        {
            // Obtiene todos los proveedores
            var suppliers = _supplierBusiness.GetSuppliers(0);
            return View(suppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene el proveedor por id
            Suppliers suppliers = (Suppliers)_supplierBusiness.GetSuppliers((int)id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            return View(suppliers);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            // Muestra el formulario de creación
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,SupplierName,ContactName,ContactTitle,Phone,Address,City,Country,LastModified,ModifiedBy")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                // Guarda el nuevo proveedor
                _supplierBusiness.SaveOrUpdate(suppliers);
                return RedirectToAction("Index");
            }

            // Si hay error de validación, regresa con el modelo cargado
            return View(suppliers);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca el proveedor a editar
            Suppliers suppliers = (Suppliers)_supplierBusiness.GetSuppliers((int)id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            return View(suppliers);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,SupplierName,ContactName,ContactTitle,Phone,Address,City,Country,LastModified,ModifiedBy")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el proveedor
                _supplierBusiness.SaveOrUpdate(suppliers);
                return RedirectToAction("Index");
            }

            return View(suppliers);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca el proveedor a eliminar
            Suppliers suppliers = (Suppliers)_supplierBusiness.GetSuppliers((int)id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            return View(suppliers);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Elimina el proveedor
            _supplierBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
