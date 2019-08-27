using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeevan.Musku.DateCalculator.Models;
using Jeevan.Musku.DateCalculator.Utilities;

namespace Jeevan.Musku.DateCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(CustomDate model)
        {
            if (ModelState.IsValid)
            {
                CustomDateUtility cdu = new CustomDateUtility();
                ViewBag.message = "Total number of days is " + cdu.CalculateDifferenceInDays(model.FromDate, model.ToDate);
            }
            return View(model);
        }
    }
}