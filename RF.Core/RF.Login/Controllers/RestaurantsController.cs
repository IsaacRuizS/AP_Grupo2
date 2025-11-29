using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RF.Data;

namespace RF.Login.Controllers
{
    public class RestaurantsController : ControllerBase
    {
        private RestaurantFinderEntities db = new RestaurantFinderEntities();

        // GET: Restaurants
        public ActionResult Index()
        {
            var claimsIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userDbIdClaim = claimsIdentity.FindFirst("UserDBId");
            int userDbId = int.Parse(userDbIdClaim.Value);


            var restaurants = RestaurantBusiness.GetRestaurantsByUser(userDbId);
            return View(restaurants.ToList());
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(UserBusiness.GetUsers(0), "UserID", "FullName");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantID,UserID,Name,Description,Address,Phone,Email,Website,WazeLink,GoogleMapsLink,Latitude,Longitude,Rating,IsActive,CreatedAt,UpdatedAt,ImageUrl")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                RestaurantBusiness.SaveOrUpdate(restaurant);
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(UserBusiness.GetUsers(0), "UserID", "FullName");
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = RestaurantBusiness.GetRestaurants((int)id).FirstOrDefault();
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(UserBusiness.GetUsers(0), "UserID", "FullName");
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantID,UserID,Name,Description,Address,Phone,Email,Website,WazeLink,GoogleMapsLink,Latitude,Longitude,Rating,IsActive,CreatedAt,UpdatedAt,ImageUrl")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                RestaurantBusiness.SaveOrUpdate(restaurant);
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", restaurant.UserID);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = RestaurantBusiness.GetRestaurants((int)id).FirstOrDefault();
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = RestaurantBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
