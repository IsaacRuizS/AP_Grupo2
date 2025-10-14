using AP.Core;
using AP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryBusiness _categoryBusiness;

        public CategoriesController()
        {
            _categoryBusiness = new CategoryBusiness();
        }

        // GET: Categories
        public ActionResult Index()
        {
            var categories = _categoryBusiness.GetCategories(0);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = (Categories)_categoryBusiness.GetCategories((int)id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description,LastModified,ModifiedBy")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _categoryBusiness.SaveOrUpdate(categories);
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = (Categories)_categoryBusiness.GetCategories((int)id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description,LastModified,ModifiedBy")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _categoryBusiness.SaveOrUpdate(categories);
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = (Categories)_categoryBusiness.GetCategories((int)id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryBusiness.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Categories/FilterByString/{str}
        public ActionResult FilterByString(string value)
        {
            var categories = CategoryBusiness.FilterByString(value);
            return View("Index", categories.ToList());
        }

    }
}
