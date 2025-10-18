using RF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly UserRoleBusiness UserRoleBusiness;
        protected readonly UserBusiness UserBusiness;

        protected readonly RestaurantBusiness RestaurantBusiness;
        protected readonly ScheduleBusiness ScheduleBusiness;
        protected readonly MenuBusiness MenuBusiness;
        protected readonly MenuItemBusiness MenuItemBusiness;

        public ControllerBase()
        {
            UserRoleBusiness = new UserRoleBusiness();
            UserBusiness = new UserBusiness();
            RestaurantBusiness = new RestaurantBusiness();
            ScheduleBusiness = new ScheduleBusiness();
            MenuBusiness = new MenuBusiness();
            MenuItemBusiness = new MenuItemBusiness();
        }

    }
}