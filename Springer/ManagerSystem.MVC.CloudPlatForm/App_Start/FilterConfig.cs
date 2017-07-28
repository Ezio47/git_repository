using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.CloudPlatForm
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
