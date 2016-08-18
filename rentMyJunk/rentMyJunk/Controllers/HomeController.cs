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
                
                int x = 0;
            }

            return View();
        }
    }
}
