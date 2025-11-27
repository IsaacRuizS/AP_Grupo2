using System.Web;
using System.Web.Mvc;
using RF.Login.Filters;

namespace RF.Login
{
    public class FilterConfig
    {
        //aca manda a autenticar para poder entrar al app
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireLoginAttribute());

            filters.Add(new HandleErrorAttribute());

            // aca aplica un limite de velociadad global para proteger los controladores del trafico excesivo
            filters.Add(new RateLimitFilter
            {
                MaxRequests = 1000, // numero maximo de solicitudes permitidas por intervalo de tiempo
                WindowSeconds = 60 // duracion del tiempo de solicitud en segundos
            });
        }
    }
}
