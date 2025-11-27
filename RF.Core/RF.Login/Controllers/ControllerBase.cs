using RF.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Login.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly UserRoleBusiness UserRoleBusiness;
        protected readonly UserBusiness UserBusiness;
        protected readonly RestaurantBusiness RestaurantBusiness;

        public ControllerBase()
        {
            UserRoleBusiness = new UserRoleBusiness();
            UserBusiness = new UserBusiness();
            RestaurantBusiness = new RestaurantBusiness();
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