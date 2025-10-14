using AP.Core;
using AP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryBusiness _inventoryBusiness;

        public InventoriesController()
        {
            _inventoryBusiness = new InventoryBusiness();
        }

        // GET: Inventories
        public ActionResult Index()
        {
            var inventories = _inventoryBusiness.GetInventory(0);
            return View(inventories.ToList());
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = (Inventory)_inventoryBusiness.GetInventory((int)id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryID,UnitPrice,UnitsInStock,LastUpdated,ProductId,DateAdded,ModifiedBy")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _inventoryBusiness.SaveOrUpdate(inventory);
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = (Inventory)_inventoryBusiness.GetInventory((int)id); 
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryID,UnitPrice,UnitsInStock,LastUpdated,ProductId,DateAdded,ModifiedBy")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _inventoryBusiness.SaveOrUpdate(inventory);
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = (Inventory)_inventoryBusiness.GetInventory((int)id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _inventoryBusiness.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Inventories/FilterByString/{str}
        public ActionResult FilterByString(string value)
        {
            var inventories = InventoryBusiness.FilterByString(value);
            return View("Index", inventories.ToList());
        }
    }
}
