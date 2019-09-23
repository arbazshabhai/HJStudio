using HJStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HJStudio.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Quotation
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GetEmployeeDetail(jQueryDataTableParamModel parm)
        //{
        //    int startIndex = parm.iDisplayStart;
        //    int endIndex = parm.iDisplayStart + parm.iDisplayLength;
        //    int recordsTotal = 0;

        //    List<EmployeeModel> list = EmployeeService.LoadEmpDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

        //    return Json(new
        //    {
        //        Echo = parm.sEcho,
        //        iTotalRecord = recordsTotal,
        //        iTotalDisplayRecords = recordsTotal,
        //        data = list
        //    });
        //}

        public ActionResult Add()
        {
            QuotationModel model = new QuotationModel();

            ViewBag.Userlist = new SelectList(new List<SelectListItem>(), "Id", "Name");

            return View(model);
        }

        //[HttpPost]
        //public ActionResult AddEdit(EmployeeModel model)
        //{
        //    //model.AddedBy = HttpContext.Session.GetString("UserName");
        //    bool EmployeeStatus = EmployeeService.AddEmployee(model);

        //    if (EmployeeStatus)
        //        TempData["Success"] = "Employee Added Successfully.";
        //    else
        //        TempData["Error"] = "Error, Please Try Again.";

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Edit(int id)
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    model.ActionType = "Update";
        //    model = EmployeeService.getEventbyId(id);
        //    var UserList = (from UserType e in Enum.GetValues(typeof(UserType)) select new { Id = (int)e, Name = CommanModel.GetEnumDisplayName(e) });
        //    ViewBag.Userlist = new SelectList(UserList, "Id", "Name");
        //    return View("Add", model);
        //}

        public ActionResult AddQuotationDays(int EventDays, int ExistDays)
        {
            QuotationModel model = new QuotationModel();
            model.QuotationDayList = new List<QuotationDayModel>();
            for (int i = 1; i <= EventDays; i++)
            {
                model.QuotationDayList.Add(
                    new QuotationDayModel
                    {
                        EventDate = DateTime.Now,
                        IsDelete = i <= ExistDays,
                        EmployeeQuotationDayList = new List<EmployeeQuotationDayModel>() { new EmployeeQuotationDayModel {
                            EmployeeList =new List<SelectListItem>(),
                        } },

                    }
                    );
            }
            return PartialView("_AddQuotationDays", model);
        }

        public ActionResult AddEmployeeList(int EventDays, int ExistDays)
        {
            List<EmployeeQuotationDayModel> model = new List<EmployeeQuotationDayModel>();
            
            return PartialView("_AddEmployeeList", model);
        }

    }
}