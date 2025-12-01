using System.Web;
using System.Web.Mvc;
using CASO2.Login.Filters;

namespace CASO2.Login
{
    public class FilterConfig
    {
        //aca manda a autenticar para poder entrar al app
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireLoginAttribute());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
