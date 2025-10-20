using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FB.Core;
using FB.Data;

namespace FB.MVC.Controllers
{
    public class FoodItemsController : ControllerBase
    {
        private FoodbankEntities db = new FoodbankEntities();

        // GET: FoodItems
        public ActionResult Index(string roleName)
        {
            ViewBag.RoleList = new SelectList(db.Roles.OrderBy(r => r.RoleName).ToList(), "RoleName", "RoleName", roleName);

            var foodItems = FoodItemBusiness.GetFoodItemsByRole(roleName);

            return View(foodItems.ToList());
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodItem foodItem = (FoodItem)FoodItemBusiness.GetFoodItems((int)id).FirstOrDefault();

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: FoodItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodItemID,Name,Category,Brand,Description,Price,Unit,QuantityInStock,ExpirationDate,IsPerishable,CaloriesPerServing,Ingredients,Barcode,Supplier,DateAdded,IsActive,RoleId")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                foodItem.DateAdded = DateTime.Now;
                FoodItemBusiness.SaveOrUpdate(foodItem);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", foodItem.RoleId);
            return View(foodItem);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodItem foodItem = (FoodItem)FoodItemBusiness.GetFoodItems((int)id).FirstOrDefault();

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", foodItem.RoleId);
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodItemID,Name,Category,Brand,Description,Price,Unit,QuantityInStock,ExpirationDate,IsPerishable,CaloriesPerServing,Ingredients,Barcode,Supplier,DateAdded,IsActive,RoleId")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                FoodItemBusiness.SaveOrUpdate(foodItem);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", foodItem.RoleId);
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodItem foodItem = (FoodItem)FoodItemBusiness.GetFoodItems((int)id).FirstOrDefault();

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = FoodItemBusiness.Delete(id);
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