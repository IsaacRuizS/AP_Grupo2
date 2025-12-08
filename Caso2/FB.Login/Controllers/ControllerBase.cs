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
        protected readonly UserDetailBusiness UserDetailBusiness;

        public ControllerBase()
        {
            UserDetailBusiness = new UserDetailBusiness();

        }

    }
}