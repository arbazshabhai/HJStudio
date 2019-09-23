using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Models;
using HJStudio.Service;

namespace HJStudio.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEdit(ClientModel model)
        {
            //model.AddedBy = HttpContext.Session.GetString("UserName");
            bool ClientStatus = ClientService.AddClient(model);

            if (ClientStatus)
                TempData["Success"] = "Client Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ClientModel model = new ClientModel();
            model = ClientService.getClientbyId(id);
            return View("Add", model);
        }

        [HttpPost]
        public ActionResult GetClientDetail(jQueryDataTableParamModel parm)
        {
            int startIndex = parm.iDisplayStart;
            int endIndex = parm.iDisplayStart + parm.iDisplayLength;
            int recordsTotal = 0;

            List<ClientModel> list = ClientService.LoadClientDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

            return Json(new
            {
                Echo = parm.sEcho,
                iTotalRecord = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                data = list
            });
        }

        public ActionResult DeleteEvent(int Id)
        {
            bool Status = ClientService.DeleteClient(Id);

            return Json(Status);
        }

    }
}