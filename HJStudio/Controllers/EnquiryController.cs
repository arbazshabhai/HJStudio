using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Service;
using HJStudio.Models;

namespace HJStudio.Controllers
{
    public class EnquiryController : Controller
    {
        // GET: Enquiry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var ClientIdList = CommanService.clientIDList();
            ViewBag.ClientList = new SelectList(ClientIdList, "ClientID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddEdit(EnquiryModel model)
        {
            bool status = EnquiryService.AddEnquiry(model);
            if (status)
                TempData["Success"] = "Enquiry Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetEnquiryDetail(jQueryDataTableParamModel parm)
        {
            int startIndex = parm.iDisplayStart;
            int endIndex = parm.iDisplayStart + parm.iDisplayLength;
            int recordsTotal = 0;

            List<EnquiryModel> list = EnquiryService.LoadEnquiryDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

            return Json(new
            {
                Echo = parm.sEcho,
                iTotalRecord = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                data = list
            });
        }

        public ActionResult Edit(int id)
        {

            return View();
        }

        public ActionResult GetDatabyClientID(int Id)
        {
            ClientModel model = new ClientModel();
            model = ClientService.getClientbyId(Id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddEnquiryFollowUp(int InquiryId)
        {
            EnquiryFollowUpModel model = new EnquiryFollowUpModel();
            model.InquiryId = InquiryId;
            var EnquiryType = (from Enquirytype e in Enum.GetValues(typeof(Enquirytype)) select new { Id = (int)e, Name = e });
            ViewBag.EnquiryType = new SelectList(EnquiryType, "Id", "Name");
            model.EnquiryFollowUpList = EnquiryService.GetInquiryFollowupByInquiryId(InquiryId);
            return PartialView("_AddEnquiryFollowUp", model);
        }
        [HttpPost]
        public ActionResult AddEnquiryFollowUp(EnquiryFollowUpModel model)
        {
            int status = EnquiryService.EnquiryFollowUpModel(model);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}