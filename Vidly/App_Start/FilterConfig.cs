using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Redirects user to error page when action throws exception
            filters.Add(new HandleErrorAttribute());

            //Applies Authorized Filter to all
            filters.Add(new AuthorizeAttribute());

            //HTTPS enables TLS
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
