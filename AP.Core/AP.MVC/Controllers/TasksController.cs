using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AP.Data;
using AP.Core;

namespace AP.MVC.Controllers
{
    public class TasksController : ControllerBase
    {
        private readonly TaskBusiness _taskBusiness;

        public TasksController()
        {
            _taskBusiness = new TaskBusiness();
        }

        // GET: Tasks
        public ActionResult Index()
        {
            // Obtiene todas las tareas
            var tasks = _taskBusiness.GetTasks(0);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene la tarea por id
            Tasks tasks = (Tasks)_taskBusiness.GetTasks((int)id);
            if (tasks == null)
            {
                return HttpNotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            // Muestra el formulario de creación
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Status,DueDate,CreatedAt,LastModified,ModifiedBy")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                // Guarda la nueva tarea
                _taskBusiness.SaveOrUpdate(tasks);
                return RedirectToAction("Index");
            }

            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene la tarea a editar
            Tasks tasks = (Tasks)_taskBusiness.GetTasks((int)id);
            if (tasks == null)
            {
                return HttpNotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Status,DueDate,CreatedAt,LastModified,ModifiedBy")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                // Actualiza la tarea
                _taskBusiness.SaveOrUpdate(tasks);
                return RedirectToAction("Index");
            }

            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            // Validación de parámetro nulo
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtiene la tarea a eliminar
            Tasks tasks = (Tasks)_taskBusiness.GetTasks((int)id);
            if (tasks == null)
            {
                return HttpNotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Elimina la tarea
            _taskBusiness.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Tasks/FilterByString/{str}
        public ActionResult FilterByString(string value)
        {
            var tasks = TaskBusiness.FilterByString(value);
            return View("Index", tasks.ToList());
        }


    }
}
