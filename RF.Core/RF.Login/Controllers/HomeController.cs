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
            var business = new RestaurantBusiness();
            var restaurants = business.GetRestaurants(0);

            var activeRestaurants = restaurants.Where(r => r.IsActive == true).ToList();

            foreach (var r in activeRestaurants)
            {
                if (r.Menus != null)
                {
                    var activeMenus = r.Menus.Where(m => m.IsActive == true).ToList();

                    foreach (var m in activeMenus)
                    {
                        if (m.MenuItems != null)
                        {
                            m.MenuItems = m.MenuItems.Where(mi => mi.IsActive == true).ToList();
                        }
                    }
                    r.Menus = activeMenus;
                }
            }

            return View(activeRestaurants);
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