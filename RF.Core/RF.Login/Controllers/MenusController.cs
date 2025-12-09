using RF.Core;
using RF.Data;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RF.Login.Controllers
{
    public class MenusController : ControllerBase
    {
        // GET: Menus/Index/{restaurantId}
        public ActionResult Index(int id)
        {
            // Validar que exista el restaurante
            var restaurant = RestaurantBusiness.GetRestaurants(id).FirstOrDefault();
            if (restaurant == null)
                return HttpNotFound();

            ViewBag.RestaurantId = id;
            ViewBag.RestaurantName = restaurant.Name;

            var menus = MenuBusiness.GetMenusByRestaurant(id);

            return View(menus.ToList());
        }

        // GET: Menus/Create/id
        public ActionResult Create(int restaurantId)
        {
            return View(new Menu { RestaurantID = restaurantId });
        }


        // POST: Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuID,RestaurantID,Name,Description,IsActive")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.CreatedAt = DateTime.Now;
                menu.UpdatedAt = DateTime.Now;

                // 🔹 GUARDAR MENÚ
                MenuBusiness.SaveOrUpdate(menu);

                return RedirectToAction("Index", new { id = menu.RestaurantID });
            }

            return View(menu);
        }

        // GET: Menus/Edit/{restaurantId} 
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // 🔹 OBTENER MENÚ
            var menu = MenuBusiness.GetMenus((int)id).FirstOrDefault();
            if (menu == null)
                return HttpNotFound();

            return View(menu);
        }

        // POST: Menus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuID,RestaurantID,Name,Description,IsActive,CreatedAt")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.UpdatedAt = DateTime.Now;

                MenuBusiness.SaveOrUpdate(menu);

                return RedirectToAction("Index", new { id = menu.RestaurantID });
            }

            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var menu = MenuBusiness.GetMenus((int)id).FirstOrDefault();
            if (menu == null)
                return HttpNotFound();

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var menu = MenuBusiness.GetMenus(id).FirstOrDefault();
            if (menu != null)
            {
                MenuBusiness.Delete(id);
            }

            return RedirectToAction("Index", new { id = menu.RestaurantID });
        }


        //MENU ITEMS – PARA EL MODAL

        // Carga la tabla + formulario parcial
        public ActionResult LoadMenuItems(int menuId)
        {
            var menuItemBusiness = new MenuItemBusiness();
            var items = menuItemBusiness.GetItemsByMenu(menuId);

            ViewBag.MenuID = menuId;
            return PartialView("_MenuItemsList", items.ToList());
        }


        // Guardar / modificar ítems
        [HttpPost]
        public ActionResult SaveMenuItem(MenuItem item)
        {
            var menuItemBusiness = new MenuItemBusiness();

            if (item.ItemID == 0)
            {
                item.CreatedAt = DateTime.Now;
                item.UpdatedAt = DateTime.Now;


                menuItemBusiness.SaveOrUpdate(item);
            }
            else
            {
                item.UpdatedAt = DateTime.Now;

                menuItemBusiness.SaveOrUpdate(item);
            }

            var menuBusiness = new MenuBusiness();
            var menu = menuBusiness.GetMenus(item.MenuID).FirstOrDefault();
            return RedirectToAction("Index", new { id = menu.RestaurantID });
        }

        // POST: Menus/DeleteMenuItem
        [HttpPost]
        public ActionResult DeleteMenuItem(int id)
        {
            var menuItemBusiness = new MenuItemBusiness();
            
            // Buscar item
            var item = menuItemBusiness.GetMenuItems(id).FirstOrDefault();

            int menuId = item.MenuID;

            // Eliminar item
            menuItemBusiness.Delete(id);

            var menuBusiness = new MenuBusiness();
            var menu = menuBusiness.GetMenus(item.MenuID).FirstOrDefault();
            return RedirectToAction("Index", new { id = menu.RestaurantID });
        }

    }
}
