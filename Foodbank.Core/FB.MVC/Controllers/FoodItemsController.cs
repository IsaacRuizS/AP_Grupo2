using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FB.Core;
using FB.Data;
using FB.Data.Entities;

namespace FB.MVC.Controllers
{
    public class FoodItemsController : ControllerBase
    {
        // GET: FoodItems
        public ActionResult Index(FoodItemsFilterViewModel filter)
        {
            IEnumerable<Role> roles = RoleBusiness.GetRoles(0);

            ViewBag.RoleList = new SelectList(roles, "RoleName", "RoleName", filter.RoleName);
            
            if (filter.RoleName != null)
                return View(FoodItemBusiness.GetFoodItemsByFilter(filter));

            return View(FoodItemBusiness.GetFoodItems(0));
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
            IEnumerable<Role> roles = RoleBusiness.GetRoles(0);
            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
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

            IEnumerable<Role> roles = RoleBusiness.GetRoles(0);

            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName", foodItem.RoleId);
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

            IEnumerable<Role> roles = RoleBusiness.GetRoles(0);

            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName", foodItem.RoleId);
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

            IEnumerable<Role> roles = RoleBusiness.GetRoles(0);

            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName", foodItem.RoleId);
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

    }
}