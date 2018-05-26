using MvcApplication1.CustomActionFilters;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            
            
            //add the custom action filter as a global filter
            filters.Add(new LogFilterAttribute());
        }
    }
}