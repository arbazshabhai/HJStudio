using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Models;
using HJStudio.Service;

namespace HJStudio.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEdit(ExpenseModel model)
        {
            bool status = ExpenseService.AddExpense(model);
            if (status)
                TempData["Success"] = "Expense Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ExpenseModel mdl = new ExpenseModel();
            mdl = ExpenseService.getExpensebyId(id);

            return View("Add",mdl);
        }

        [HttpPost]
        public ActionResult GetExpenseDetail(jQueryDataTableParamModel parm)
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
    }
}