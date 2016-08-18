using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

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

            string clientId = "562807cf-3e3d-44cf-9bde-7e541a4f4e0f";
            //string authenticationAuthority = "https://graph.microsoft.io/en-us/docs/authorization/app_authorization";
            string authenticationAuthority = "https://login.microsoftonline.com";
            string key = "bHg8jjk7EsQYfQb78+tv1ow0K7yv7d1v2M3sGEVJBVs=";
            string tenantId = "b1993efb-11a1-42fd-8ec3-26f834594691";

            var request = new Request
            {
                id = Guid.NewGuid().ToString(),
                itemId = itemId,
                requesterId = User.Identity.Name
            };

            if (ModelState.IsValid)
            {
                // send the request message
                AzureADOAuthGateway azureADOAuthGateway = new AzureADOAuthGateway
                {
                    AuthenticationAuthority = authenticationAuthority,
                    ClientId = clientId,
                    Key = key,
                    Resource = "https://graph.microsoft.com",
                    TenantId = tenantId
                };

                string token = azureADOAuthGateway.GetOAuthToken();
                return View(request);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        

        [HttpPost]
        public ActionResult Create(Request request)
        {
            var item = repo.GetItemAsync(request.itemId).Result;

            request.id = Guid.NewGuid().ToString();
            request.startDate = DateTime.Now;
            request.endDate = DateTime.Now.AddDays(7);

            //var requestRepo = new RequestRepository();
            //Task task = requestRepo.SaveRequest(request);
            //task.Wait();

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string message = string.Format(
                    "A request for item <a href=\"https://localhost:44316/Items/Details?id={0}\">{1}</a> has been made by {2} for {3} to {4}.  <a href=\"https://localhost:44316/Approval/Index?requestId={5}\" > Approve</a> or <a href=\"https://localhost:44316/Items/Details?id={5}\" > Deny</a>",
                    request.itemId,
                    item.description,
                    User.Identity.Name,
                    DateTime.Now.ToString(),
                    DateTime.Now.AddDays(7).ToString(),
                    "3c6093eb-d466-4e02-8203-6a4296e54863"
                    );

                var postMessage = new PostMessage { text = message };

                var response = client.PostAsJsonAsync("https://outlook.office365.com/webhook/d7fcb229-db3f-470d-bdc1-24aa147129db@b1993efb-11a1-42fd-8ec3-26f834594691/IncomingWebhook/112c5e35759749a3a2a49742db58246a/374f0237-c207-4417-bbb4-4940eb71685f", postMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    int x = 0;
                }
                else
                {
                    int y = 0;
                }
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}