using AP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly ProductBusiness ProductBusiness;
        protected readonly CategoryBusiness CategoryBusiness;
        protected readonly InventoryBusiness InventoryBusiness;
        protected readonly ComponentBusiness ComponentBusiness;
        protected readonly NotificationBusiness NotificationBusiness;
        protected readonly RolBusiness RolBusiness;
        protected readonly SupplierBusiness SupplierBusiness;
        protected readonly TaskBusiness TaskBusiness;
        protected readonly UserBusiness UserBusiness;

        public ControllerBase()
        {
            ProductBusiness = new ProductBusiness();
            CategoryBusiness = new CategoryBusiness();
            InventoryBusiness = new InventoryBusiness();
            ComponentBusiness = new ComponentBusiness();
            NotificationBusiness = new NotificationBusiness();
            RolBusiness = new RolBusiness();
            SupplierBusiness = new SupplierBusiness();
            TaskBusiness = new TaskBusiness();
            UserBusiness = new UserBusiness();
        }

    }
}