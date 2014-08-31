using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Cloud;

namespace WebRole.MsTools.AzureCloudService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CloudTest()
        {
            ViewBag.Message = string.Empty;
            try
            {
                var container = CloudUtility.GetAzureBlobContainer("testcontainer");
                var queue = CloudUtility.GetAzureQueue("testqueue");
                ViewBag.Message = container.Name + ":" + container.Uri + " -- " + queue.Name + ":" + queue.Uri;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}