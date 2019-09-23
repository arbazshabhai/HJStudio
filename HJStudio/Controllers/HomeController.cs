using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Service;
using HJStudio.Models;
namespace HJStudio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetClalendarEvent()
        {
            var list = CommanService.GetCalanderFollowup();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}