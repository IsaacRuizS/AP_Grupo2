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
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationBusiness _notificationBusiness;

        public NotificationsController()
        {
            _notificationBusiness = new NotificationBusiness();
        }

        // GET: Notifications
        public ActionResult Index()
        {
            // Obtiene todas las notificaciones
            var notifications = _notificationBusiness.GetNotifications(0);
            return View(notifications.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            // Valida si el id no fue enviado
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca la notificación en la capa de negocio
            Notifications notifications = (Notifications)_notificationBusiness.GetNotifications((int)id);
            if (notifications == null)
            {
                return HttpNotFound();
            }

            return View(notifications);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            // Muestra el formulario de creación
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,message,is_read,created_at")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                // Guarda la nueva notificación
                _notificationBusiness.SaveOrUpdate(notifications);
                return RedirectToAction("Index");
            }

            return View(notifications);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            // Valida si el id no fue enviado
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca la notificación a editar
            Notifications notifications = (Notifications)_notificationBusiness.GetNotifications((int)id);
            if (notifications == null)
            {
                return HttpNotFound();
            }

            return View(notifications);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,message,is_read,created_at")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                // Actualiza la notificación
                _notificationBusiness.SaveOrUpdate(notifications);
                return RedirectToAction("Index");
            }

            return View(notifications);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            // Valida si el id no fue enviado
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca la notificación a eliminar
            Notifications notifications = (Notifications)_notificationBusiness.GetNotifications((int)id);
            if (notifications == null)
            {
                return HttpNotFound();
            }

            return View(notifications);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Elimina la notificación
            _notificationBusiness.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: Notifications/FilterByString/{str}
        public ActionResult FilterByString(string value)
        {
            var notifications = NotificationBusiness.FilterByString(value);
            return View("Index", notifications.ToList());
        }
    }
}
