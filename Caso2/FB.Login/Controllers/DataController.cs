using System;
using System.Linq;
using System.Web.Mvc;
using FB.Core;
using FB.Data;

namespace FB.Login.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly UserDetailBusiness _business;

        public DataController()
        {
            _business = new UserDetailBusiness();
        }

        // Default -> Paso1
        public ActionResult Index()
        {
            return RedirectToAction("Paso1");
        }

        // Paso1 always available
        public ActionResult Paso1()
        {
            SetStageFlags();

            var items = _business.GetUserDetails(0).ToList();
            ViewBag.Message = TempData["Message"] as string;

            return View("Paso1", items);
        }

        public ActionResult Paso2()
        {
            if (!_business.IsStageCompleted(1))
            {
                TempData["Message"] = "Debe completar paso 1";
                return RedirectToAction("Paso1");
            }

            SetStageFlags();

            var items = _business.GetUserDetails(0).ToList();
            ViewBag.Message = TempData["Message"] as string;

            return View("Paso2", items);
        }

        public ActionResult Paso3()
        {
            if (!_business.IsStageCompleted(2))
            {
                TempData["Message"] = "Debe completar paso 2.";
                return RedirectToAction("Paso2");
            }

            SetStageFlags();

            var items = _business.GetUserDetails(0).ToList();
            ViewBag.Message = TempData["Message"] as string;

            return View("Paso3", items);
        }

        public ActionResult Paso4()
        {
            if (!_business.IsStageCompleted(3))
            {
                TempData["Message"] = "Debe completar paso 3.";
                return RedirectToAction("Paso3");
            }

            SetStageFlags();

            var items = _business.GetUserDetails(0).ToList();
            ViewBag.Message = TempData["Message"] as string;

            return View("Paso4", items);
        }

        public ActionResult Completed()
        {
            SetStageFlags();

            var items = _business.GetUserDetails(0).ToList();
            return View(items);
        }

        // POST: /Data/RunStage - executes the stage transformation and persists changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RunStage(int stage, string returnTo = "Paso1")
        {
            var (success, message) = _business.ExecuteStage(stage);
            TempData["Message"] = message;

            if (success && stage == 4)
            {
                return RedirectToAction("Completed");
            }

            return RedirectToAction(ValidateReturnTo(returnTo));
        }

        private string ValidateReturnTo(string returnTo)
        {
            var allowed = new[] { "Paso1", "Paso2", "Paso3", "Paso4" };
            return allowed.Contains(returnTo) ? returnTo : "Paso1";
        }

        private void SetStageFlags()
        {
            ViewBag.StageCompleted1 = _business.IsStageCompleted(1);
            ViewBag.StageCompleted2 = _business.IsStageCompleted(2);
            ViewBag.StageCompleted3 = _business.IsStageCompleted(3);
            ViewBag.StageCompleted4 = _business.IsStageCompleted(4);
        }
    }
}