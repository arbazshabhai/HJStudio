using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Models;
using HJStudio.Service;
	

namespace HJStudio.Controllers
{
    public class EmployeeMasterController : Controller
    {
        // GET: EmployeeMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            if (model.ActionType == "")
                model.ActionType = "Register";

            var UserList = (from UserType e in Enum.GetValues(typeof(UserType)) select new { Id = (int)e, Name = CommanModel.GetEnumDisplayName(e) });

            ViewBag.Userlist = new SelectList(UserList, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public ActionResult AddEmps(EmployeeModel model)
        {
            //model.AddedBy = HttpContext.Session.GetString("UserName");
            bool EmployeeStatus = EmployeeMasterService.AddEmployee(model);

            if (EmployeeStatus)
                TempData["Success"] = "Employee Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("ViewEmployee");
        }

        public ActionResult EditEmployee(int id)
        {
            EmployeeModel model = new EmployeeModel();
            model.ActionType = "Update";
            model = EmployeeMasterService.getEventbyId(id);
            var UserList = (from UserType e in Enum.GetValues(typeof(UserType)) select new { Id = (int)e, Name = CommanModel.GetEnumDisplayName(e) });
            ViewBag.Userlist = new SelectList(UserList, "Id", "Name");           
            return View("AddEmployee", model);
        }

        public ActionResult ViewEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetEmployeeDetail(jQueryDataTableParamModel parm)
        {
            int startIndex = parm.iDisplayStart;
            int endIndex = parm.iDisplayStart + parm.iDisplayLength;
            int recordsTotal = 0;

            List<EmployeeModel> list = EmployeeMasterService.LoadEmpDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

            return Json(new
            {
                Echo = parm.sEcho,
                iTotalRecord = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                data = list
            });
        }

        public ActionResult DeActiveEvent(int Id)
        {
            bool Status = EmployeeMasterService.DeActivateEmployee(Id);

            return Json(Status);
        }
        public ActionResult ActiveEvent(int Id)
        {
            bool Status = EmployeeMasterService.ActivatetEmployee(Id);

            return Json(Status);
        }
        public ActionResult DeleteEvent(int Id)
        {
            bool Status = EmployeeMasterService.DeleteEmployee(Id);

            return Json(Status);
        }
    }
}