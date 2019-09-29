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
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetEmployeeDetail(jQueryDataTableParamModel parm)
        {
            int startIndex = parm.iDisplayStart;
            int endIndex = parm.iDisplayStart + parm.iDisplayLength;
            int recordsTotal = 0;

            List<EmployeeModel> list = EmployeeService.LoadEmpDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

            return Json(new
            {
                Echo = parm.sEcho,
                iTotalRecord = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                data = list
            });
        }

        public ActionResult Add()
        {
            EmployeeModel model = new EmployeeModel();
            if (model.ActionType == "")
                model.ActionType = "Register";

            var UserList = (from UserType e in Enum.GetValues(typeof(UserType)) select new { Id = (int)e, Name = CommanModel.GetEnumDisplayName(e) });
            var wages = (from wagestype e in Enum.GetValues(typeof(wagestype)) select new { Id = (int)e, Name = e });
            ViewBag.Userlist = new SelectList(UserList, "Id", "Name");
            ViewBag.wageslist = new SelectList(wages, "Id","Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEdit(EmployeeModel model)
        {
            //model.AddedBy = HttpContext.Session.GetString("UserName");
            bool EmployeeStatus = EmployeeService.AddEmployee(model);

            if (EmployeeStatus)
                TempData["Success"] = "Employee Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EmployeeModel model = new EmployeeModel();
            model.ActionType = "Update";
            model = EmployeeService.getEventbyId(id);

            var UserList = (from UserType e in Enum.GetValues(typeof(UserType)) select new { Id = (int)e, Name = CommanModel.GetEnumDisplayName(e) });
            var wages = (from wagestype e in Enum.GetValues(typeof(wagestype)) select new { Id = (int)e, Name = e });

            ViewBag.Userlist = new SelectList(UserList, "Id", "Name");
            ViewBag.wageslist = new SelectList(wages, "Id", "Name");

            return View("Add", model);
        }
        


        public ActionResult DeActiveEvent(int Id)
        {
            bool Status = EmployeeService.DeActivateEmployee(Id);

            return Json(Status);
        }
        public ActionResult ActiveEvent(int Id)
        {
            bool Status = EmployeeService.ActivatetEmployee(Id);

            return Json(Status);
        }
        public ActionResult DeleteEvent(int Id)
        {
            bool Status = EmployeeService.DeleteEmployee(Id);

            return Json(Status);
        }
    }
}