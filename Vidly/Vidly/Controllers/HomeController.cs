using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            //return View();
            //return this.Content("I am Pael!"); 
            //return this.HttpNotFound(); 

            return this.RedirectToAction("Contact", new { age = 29, name = "pael" }); 
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Info(int id)
        {
            return this.Content("Id=" + id); 
        }
    }
}