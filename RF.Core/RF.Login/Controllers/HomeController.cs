using RF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Login.Controllers
{
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {

        public ActionResult Index()
        {
            var restaurants = RestaurantBusiness.GetRestaurants(0);
            return View(restaurants.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}