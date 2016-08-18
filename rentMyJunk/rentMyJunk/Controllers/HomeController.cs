using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Microsoft.PowerBI.Api.V1;
using Microsoft.PowerBI.Security;
using Microsoft.Rest;
using rentMyJunk.Models;
using System.Threading.Tasks;
using System.Configuration;

namespace rentMyJunk.Controllers
{
    public class HomeController : Controller
    {

        private readonly string workspaceCollection;
        private readonly string workspaceId;
        private readonly string accessKey;
        private readonly string apiUrl;
        private readonly string reportId;

        /// <summary>
        /// Load web.config keys for workspace and access key configs
        /// </summary>
        public HomeController()
        {
            this.workspaceCollection = ConfigurationManager.AppSettings["powerbi:WorkspaceCollection"];
            this.workspaceId = ConfigurationManager.AppSettings["powerbi:WorkspaceId"];
            this.accessKey = ConfigurationManager.AppSettings["powerbi:AccessKey"];
            this.apiUrl = ConfigurationManager.AppSettings["powerbi:ApiUrl"];
            this.reportId = ConfigurationManager.AppSettings["powerbi:ReportId"];
        }

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

        public async Task<ActionResult> Dashboard()
        {
            using (var client = this.CreatePowerBIClient())
            {
                var reportsResponse = await client.Reports.GetReportsAsync(this.workspaceCollection, this.workspaceId);
                var report = reportsResponse.Value.FirstOrDefault(r => r.Id == reportId);
                var embedToken = PowerBIToken.CreateReportEmbedToken(this.workspaceCollection, this.workspaceId, report.Id);

                var viewModel = new ReportViewModel
                {
                    Report = report,
                    AccessToken = embedToken.Generate(this.accessKey)
                };

                return View(viewModel);
            }
        }

        private IPowerBIClient CreatePowerBIClient()
        {
            var credentials = new TokenCredentials(accessKey, "AppKey");
            var client = new PowerBIClient(credentials)
            {
                BaseUri = new Uri(apiUrl)
            };

            return client;
        }
    }
}
