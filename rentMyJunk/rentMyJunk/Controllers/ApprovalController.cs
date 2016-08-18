using rentMyJunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rentMyJunk.Controllers
{
    public class ApprovalController : Controller
    {
        private RequestRepository repo = new RequestRepository();
        // GET: Approval
        public ActionResult Index(string requestId)
        {
            var request = repo.GetRequestAsync(requestId).Result;
            return View();
        }
    }
}