using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Login.Controllers
{
    public class RolesController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ManagerPage()
        {
            return View();
        }

        [Authorize(Roles = "Viewer")]
        public ActionResult ViewerPage()
        {
            return View();
        }
    }
}