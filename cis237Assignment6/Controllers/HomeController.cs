using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cis237Assignment6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Beverage App description page.";

            ViewBag.Description = "This application will allow you to view a list of beverages." +
                "\nIt will also allow you to add, delete, and edit beverages on the given list.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Do not hesitate to contact us if you encounter any issues.";

            return View();
        }

      
    }
}