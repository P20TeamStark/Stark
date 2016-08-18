using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace rentMyJunk.Controllers
{
    public class RequestController : Controller
    {
        private ItemRepository repo = new ItemRepository();
        // GET: Request
        public ActionResult Index(string itemId)
        {
            var item = repo.GetItemAsync(itemId).Result;
            if (!Convert.ToBoolean(item.isAvailable))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var request = new Request
            {
                id = Guid.NewGuid().ToString(),
                itemId = itemId,
                requesterId = User.Identity.Name
            };

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,itemId,requesterId,ownerId,startDate,endDte")] Request request)
        {
            if (ModelState.IsValid)
            {
                // send the request message
                AzureADOAuthGateway azureADOAuthGateway = new AzureADOAuthGateway
                {
                    AuthenticationAuthority = "https://login.windows.net",
                    ClientId = "78316e80-f898-43f3-9823-f652d979dc0f",
                    Key = "+9IaNb9TTwmAkAjayZheZY1ZQJYohWtPCpMrFRmoRcg=",
                    Resource = "https://management.core.windows.net/",
                    TenantId = "4ac2c945-49d5-4b59-8a70-a08dffe43dba"
                };

                string token = azureADOAuthGateway.GetOAuthToken();

            }
            return View(request);
        }
    }
}