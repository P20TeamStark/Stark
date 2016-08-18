using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace rentMyJunk.Controllers
{
    public class ApprovalController : Controller
    {
        private RequestRepository repo = new RequestRepository();
        private ItemRepository itemRepo = new ItemRepository();
        // GET: Approval
        public ActionResult Index(string requestId)
        {
            requestId = "3c6093eb-d466-4e02-8203-6a4296e54863";
         //   var request = repo.GetRequestAsync(requestId).Result;
         //   var item = itemRepo.GetItemAsync(request.itemId).Result;

         //   string ownerId = request.ownerId;

            string ownerEmail = "bob@hardnox.onmicrosoft.com";
            string requestorEmail = "bobjac@microsoft.com";


            string clientId = "562807cf-3e3d-44cf-9bde-7e541a4f4e0f";
            //string authenticationAuthority = "https://graph.microsoft.io/en-us/docs/authorization/app_authorization";
            string authenticationAuthority = "https://login.microsoftonline.com";
            string key = "bHg8jjk7EsQYfQb78+tv1ow0K7yv7d1v2M3sGEVJBVs=";
            string tenantId = "b1993efb-11a1-42fd-8ec3-26f834594691";

            //AzureADOAuthGateway azureADOAuthGateway = new AzureADOAuthGateway
            //{
            //    AuthenticationAuthority = authenticationAuthority,
            //    ClientId = clientId,
            //    Key = key,
            //    Resource = "https://graph.microsoft.com",
            //    TenantId = tenantId
            //};

            //string token = azureADOAuthGateway.GetOAuthToken();


            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string message = string.Format(
                    "Your request for item <a href=\"https://localhost:44316/Items/Details?id={0}\">{1}</a> has been approved",
                    Guid.NewGuid().ToString(),
                    "Fancy Expresso Machine",
                    "bobjac@microsoft.com",
                    DateTime.Now.ToString(),
                    DateTime.Now.AddDays(7).ToString()
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


                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
    }
}