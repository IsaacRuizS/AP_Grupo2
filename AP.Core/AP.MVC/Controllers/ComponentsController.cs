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
    public class ComponentsController : Controller
    {
        private readonly ComponentBusiness _componentBusiness;

        public ComponentsController()
        {
            _componentBusiness = new ComponentBusiness();
        }

        // GET: Components
        public ActionResult Index()
        {
            var components = _componentBusiness.GetComponents(0);
            return View(components.ToList());
        }

        // GET: Components/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Components components = (Components)_componentBusiness.GetComponents(id);
            if (components == null)
            {
                return HttpNotFound();
            }
            return View(components);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,content")] Components components)
        {
            if (ModelState.IsValid)
            {
                _componentBusiness.SaveOrUpdate(components);
                return RedirectToAction("Index");
            }

            return View(components);
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Components components = (Components)_componentBusiness.GetComponents((int)id); ;
            if (components == null)
            {
                return HttpNotFound();
            }
            return View(components);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,content")] Components components)
        {
            if (ModelState.IsValid)
            {
                _componentBusiness.SaveOrUpdate(components);
                return RedirectToAction("Index");
            }
            return View(components);
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Components components = (Components)_componentBusiness.GetComponents((int)id);
            if (components == null)
            {
                return HttpNotFound();
            }
            return View(components);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _componentBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
