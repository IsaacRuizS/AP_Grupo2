using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using RF.Data;
using RF.Core.Helpers;
using System.IO;

namespace RF.Login.Controllers
{
    public class RestaurantsController : ControllerBase
    {
        private RestaurantFinderEntities db = new RestaurantFinderEntities();

        // GET: Restaurants
        public ActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;

            // Obtener el rol del claim estándar
            string role = claims.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Admin")
            {
                var allRestaurants = RestaurantBusiness.GetRestaurants(0);
                return View(allRestaurants.ToList());
            }

            // Si NO es admin → es Restaurant
            var claimId = claims.FindFirst("UserDbId");
            if (claimId == null)
            {
                //HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Restaurants");
            }
            var userDbId = int.Parse(claimId.Value);

            var restaurantsByUser = RestaurantBusiness.GetRestaurantsByUser(userDbId);
            return View(restaurantsByUser.ToList());
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
        public ActionResult Create([Bind(Include = "RestaurantID,UserID,Name,Description,Address,Phone,Email,Website,WazeLink,GoogleMapsLink,Latitude,Longitude,Rating,IsActive,CreatedAt,UpdatedAt,ImageUrl")] Restaurant restaurant, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = FileHelper.GenerateUniqueFileName(imageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/Restaurants"), fileName);
                    imageFile.SaveAs(path);
                    restaurant.ImageUrl = "/Content/Images/Restaurants/" + fileName;
                }

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
        public ActionResult Edit([Bind(Include = "RestaurantID,UserID,Name,Description,Address,Phone,Email,Website,WazeLink,GoogleMapsLink,Latitude,Longitude,Rating,IsActive,CreatedAt,UpdatedAt,ImageUrl")] Restaurant restaurant, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = FileHelper.GenerateUniqueFileName(imageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/Restaurants"), fileName);
                    imageFile.SaveAs(path);
                    restaurant.ImageUrl = "/Content/Images/Restaurants/" + fileName;
                }
                
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
