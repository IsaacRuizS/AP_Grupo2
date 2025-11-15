using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RF.Data;

namespace RF.MVC.Controllers
{
    public class RestaurantRegistrationRequestsController : ControllerBase
    {
        // GET: RestaurantRegistrationRequests
        public ActionResult Index()
        {
            var restaurantRegistrationRequests = RestaurantRegistrationRequestBusiness.GetRestaurantRegistrationRequests(0);
            return View(restaurantRegistrationRequests.ToList());
        }

        // GET: RestaurantRegistrationRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantRegistrationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,LegalBusinessId,Email,Latitude,Longitude,IsActive,CreatedAt,UpdatedAt")] RestaurantRegistrationRequest restaurantRegistrationRequest)
        {
            if (ModelState.IsValid)
            {
                restaurantRegistrationRequest.CreatedAt = DateTime.Now;

                RestaurantRegistrationRequestBusiness.SaveOrUpdate(restaurantRegistrationRequest);
                return RedirectToAction("~/Views/Shared/SignUpRequestSended.cshtml");
            }

            return View(restaurantRegistrationRequest);
        }

        // GET: RestaurantRegistrationRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantRegistrationRequest restaurantRegistrationRequest = RestaurantRegistrationRequestBusiness.GetRestaurantRegistrationRequests((int)id).FirstOrDefault();
            if (restaurantRegistrationRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(UserBusiness.GetUsers(0), "UserID", "FullName");
            return View(restaurantRegistrationRequest);
        }

        // POST: RestaurantRegistrationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,LegalBusinessId,Email,Latitude,Longitude,IsActive,CreatedAt,UpdatedAt")] RestaurantRegistrationRequest restaurantRegistrationRequest)
        {
            if (ModelState.IsValid)
            {
                RestaurantRegistrationRequestBusiness.SaveOrUpdate(restaurantRegistrationRequest);
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", restaurantRegistrationRequest.Id);
            return View(restaurantRegistrationRequest);
        }

        // GET: RestaurantRegistrationRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantRegistrationRequest restaurantRegistrationRequest = RestaurantRegistrationRequestBusiness.GetRestaurantRegistrationRequests((int)id).FirstOrDefault();
            if (restaurantRegistrationRequest == null)
            {
                return HttpNotFound();
            }
            return View(restaurantRegistrationRequest);
        }

        // POST: RestaurantRegistrationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = RestaurantRegistrationRequestBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
