using RF.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly UserRoleBusiness UserRoleBusiness;
        protected readonly UserBusiness UserBusiness;

        protected readonly RestaurantBusiness RestaurantBusiness;
        protected readonly RestaurantRegistrationRequestBusiness RestaurantRegistrationRequestBusiness;
        protected readonly ScheduleBusiness ScheduleBusiness;
        protected readonly MenuBusiness MenuBusiness;
        protected readonly MenuItemBusiness MenuItemBusiness;

        public ControllerBase()
        {
            UserRoleBusiness = new UserRoleBusiness();
            UserBusiness = new UserBusiness();
            RestaurantRegistrationRequestBusiness = new RestaurantRegistrationRequestBusiness();
            ScheduleBusiness = new ScheduleBusiness();
            MenuBusiness = new MenuBusiness();
            MenuItemBusiness = new MenuItemBusiness();
        }

    }

    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null)
                return null;

            var attempted = value.AttemptedValue.Replace(",", "."); // convierte comas a puntos

            if (decimal.TryParse(attempted, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
                return parsed;

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "El valor ingresado no es válido.");
            return null;
        }
    }
}