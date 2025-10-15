using FB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FB.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly UserBusiness UserBusiness;
        protected readonly UserRoleBusiness UserRoleBusiness;
        protected readonly RoleBusiness RoleBusiness;

        public ControllerBase()
        {
            UserBusiness = new UserBusiness();
            UserRoleBusiness = new UserRoleBusiness();
            RoleBusiness = new RoleBusiness();
        }

    }
}