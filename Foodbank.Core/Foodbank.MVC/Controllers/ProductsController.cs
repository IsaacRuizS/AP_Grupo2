using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using AP.Core;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AP.Data;

namespace AP.MVC.Controllers
{
    public class ProductsController : ControllerBase
    {
        // GET: Products
        public ActionResult Index()
        {
            var products = ProductBusiness.GetProducts(0);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = (Products)ProductBusiness.GetProducts((int)id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(CategoryBusiness.GetCategories(0), "CategoryID", "CategoryName");
            ViewBag.InventoryID = new SelectList(InventoryBusiness.GetInventory(0), "InventoryID", "ModifiedBy");
            ViewBag.SupplierID = new SelectList(SupplierBusiness.GetSuppliers(0), "SupplierID", "SupplierName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,InventoryID,SupplierID,Description,Rating,CategoryID,LastModified,ModifiedBy")] Products products)
        {
            if (ModelState.IsValid)
            {
                ProductBusiness.SaveOrUpdate(products);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(CategoryBusiness.GetCategories(0), "CategoryID", "CategoryName"); 
            ViewBag.InventoryID = new SelectList(InventoryBusiness.GetInventory(0), "InventoryID", "ModifiedBy", products.InventoryID);
            ViewBag.SupplierID = new SelectList(SupplierBusiness.GetSuppliers(0), "SupplierID", "SupplierName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = (Products)ProductBusiness.GetProducts((int)id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(CategoryBusiness.GetCategories(0), "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.InventoryID = new SelectList(InventoryBusiness.GetInventory(0), "InventoryID", "ModifiedBy", products.InventoryID);
            ViewBag.SupplierID = new SelectList(SupplierBusiness.GetSuppliers(0), "SupplierID", "SupplierName", products.SupplierID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,InventoryID,SupplierID,Description,Rating,CategoryID,LastModified,ModifiedBy")] Products products)
        {
            if (ModelState.IsValid)
            {
                ProductBusiness.SaveOrUpdate(products);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(CategoryBusiness.GetCategories(0), "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.InventoryID = new SelectList(InventoryBusiness.GetInventory(0), "InventoryID", "ModifiedBy", products.InventoryID);
            ViewBag.SupplierID = new SelectList(SupplierBusiness.GetSuppliers(0), "SupplierID", "SupplierName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = (Products)ProductBusiness.GetProducts((int)id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBusiness.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Products/FilterByString/{str}
        public ActionResult FilterByString(string value)
        {
            var products = ProductBusiness.FilterByString(value);
            return View("Index", products.ToList());
        }
    }
}
