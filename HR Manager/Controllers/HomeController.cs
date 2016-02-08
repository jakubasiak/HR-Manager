using HR_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return View();
            else
                return View("PublicIndex");
        }

        public ActionResult About()
        {
            return View();
        }

        
    }
}