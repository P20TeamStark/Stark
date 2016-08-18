using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rentMyJunk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            if (Request.IsAuthenticated)
            {
                AzureADOAuthGateway azureADOAuthGateway = new AzureADOAuthGateway
                {
                    AuthenticationAuthority = "https://login.windows.net",
                    ClientId = "78316e80-f898-43f3-9823-f652d979dc0f",
                    Key = "+9IaNb9TTwmAkAjayZheZY1ZQJYohWtPCpMrFRmoRcg=",
                    Resource = "https://management.core.windows.net/",
                    TenantId = "4ac2c945-49d5-4b59-8a70-a08dffe43dba"
                };

                string token = azureADOAuthGateway.GetOAuthToken();
                int x = 0;

                
            }

            return View();
        }
    }
}
