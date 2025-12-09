using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using RF.Data;

namespace RF.Login.Controllers
{
    public class SchedulesController : ControllerBase
    {
        private RestaurantFinderEntities db = new RestaurantFinderEntities();

        // GET: Schedule/Index/{restaurantId}
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int restaurantId = id.Value;
             
            ViewBag.RestaurantId = restaurantId;

            var allSchedules = ScheduleBusiness.GetSchedulesByRestaurant(restaurantId);
            return View(allSchedules.ToList());

        }


        // GET: Schedule/Create/{restaurantId} 
        public ActionResult Create(int id)
        {
            ViewBag.RestaurantId = id;
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule, int RestaurantID)
        {
            schedule.RestaurantID = RestaurantID;
            schedule.CreatedAt = DateTime.Now;
            schedule.UpdatedAt = DateTime.Now;

            ScheduleBusiness.SaveOrUpdate(schedule);

            return RedirectToAction("Index", new { id = RestaurantID });
        }

        // GET: Schedule/Edit/{restaurantId} 
        public ActionResult Edit(int id)
        {
            var schedule = ScheduleBusiness.GetSchedules(id);
            if (schedule == null) return HttpNotFound();

            return View(schedule.First());
        }

        // POST: Schedule/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.UpdatedAt = DateTime.Now;
                ScheduleBusiness.SaveOrUpdate(schedule);
                return RedirectToAction("Index", new { id = schedule.RestaurantID });
            }

            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var schedule = ScheduleBusiness.GetSchedules(id.Value);
            if (schedule == null)
            {
                return HttpNotFound();
            }

            return View(schedule.First());
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var schedule = ScheduleBusiness.GetSchedules(id);

            if (schedule == null)
            {
                return HttpNotFound();
            }

            int restaurantId = schedule.First().RestaurantID;

            bool deleted = ScheduleBusiness.Delete(id);

            // Después de eliminar → volver al Index de horarios del restaurante correspondiente
            return RedirectToAction("Index", new { id = restaurantId });
        }



    }
}
