using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Cloud;

namespace WebRole.MsTools.AzureCloudService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string CloudStatus { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                CloudUtility.InitializeAllServices();
                CloudStatus = "Initialize OK !";
            }
            catch (Exception ex)
            {
                while(ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                CloudStatus = ex.Message;
            }
        }
    }
}
