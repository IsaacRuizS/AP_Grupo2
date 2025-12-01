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
        // GET: FoodItems
        public ActionResult Index()
        {
            ViewBag.RoleList = new SelectList(RoleBusiness.GetRoles(0).ToList(), "RoleId", "RoleName");
            
            var foodItems = FoodItemBusiness.GetFoodItems(0);

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
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles(0), "RoleId", "RoleName");
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

            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles((int)foodItem.RoleId), "RoleId", "RoleName", foodItem.RoleId);
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
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles((int)foodItem.RoleId), "RoleId", "RoleName", foodItem.RoleId);
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
            ViewBag.RoleId = new SelectList(RoleBusiness.GetRoles((int)foodItem.RoleId), "RoleId", "RoleName", foodItem.RoleId);
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

        public ActionResult FilterData(FilterFoodItemDto filtersInfo)
        {
            //devolver los roles
            ViewBag.RoleList = new SelectList(RoleBusiness.GetRoles(0).ToList(), "RoleId", "RoleName", filtersInfo?.RoleId);

            // Validacion de precios 
            if (filtersInfo.MinPrice.HasValue && filtersInfo.MaxPrice.HasValue)
            {
                if (filtersInfo.MinPrice > filtersInfo.MaxPrice)
                {
                    ModelState.AddModelError("", "El precio mínimo no puede ser mayor que el precio máximo.");
                }
            }

            // Validacion de fechas 
            if (filtersInfo.StartExpirationDate.HasValue && filtersInfo.EndExpirationDate.HasValue)
            {
                if (filtersInfo.StartExpirationDate > filtersInfo.EndExpirationDate)
                {
                    ModelState.AddModelError("", "La fecha inicial no puede ser posterior a la fecha final.");
                }
            }

            if (!ModelState.IsValid)
            {
                IEnumerable<FoodItem> allItems = FoodItemBusiness.GetFoodItems(0);
                return View("Index", allItems.ToList());
            }

            IEnumerable<FoodItem> items = FoodItemBusiness.GetFoodItemsByFilters(filtersInfo);
            return View("Index", items.ToList());
        }

    }
}