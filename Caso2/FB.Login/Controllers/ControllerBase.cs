using CASO2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CASO2.MVC.Controllers
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