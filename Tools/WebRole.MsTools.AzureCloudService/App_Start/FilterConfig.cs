using System.Web;
using System.Web.Mvc;

namespace WebRole.MsTools.AzureCloudService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
