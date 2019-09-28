using HJStudio.Models;
using HJStudio.Service;
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

            ViewBag.Userlist = new SelectList(ClientService.getActiveClientList(), "ClientID", "Name");
            model.ProductQuotationList = new List<ProductQuotationModel> { new ProductQuotationModel { ProductList = ProductService.getActiveProductList().Select(x => new SelectListItem { Text = x.ProductName, Value = Convert.ToString(x.ProductId) }).ToList() } };

            return View(model);
        }

        public ActionResult GetClientData(int ClientID)
        {
            return Json(ClientService.getClientbyId(ClientID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeData(int EmployeeID)
        {
            return Json(EmployeeService.getEmployeebyId(EmployeeID), JsonRequestBehavior.AllowGet);
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
            List<SelectListItem> Elist = EmployeeService.getEmployeeList().Select(x => new SelectListItem { Text = x.Name, Value = Convert.ToString(x.EmployeeID) }).ToList();
            for (int i = 1; i <= EventDays; i++)
            {
                model.QuotationDayList.Add(
                    new QuotationDayModel
                    {
                        EventDate = DateTime.Now,
                        IsDelete = i <= ExistDays,
                        EmployeeQuotationDayList = new List<EmployeeQuotationDayModel>() { new EmployeeQuotationDayModel {
                            EmployeeList =Elist,
                        } },

                    }
                    );
            }
            return PartialView("_AddQuotationDays", model);
        }

        public ActionResult AddEmployeeList(int DayId, int EmpId)
        {
            EmpId = EmpId + 1;
            QuotationModel model = new QuotationModel();
            model.QuotationDayList = new List<QuotationDayModel>();
            List<SelectListItem> Elist = EmployeeService.getEmployeeList().Select(x => new SelectListItem { Text = x.Name, Value = Convert.ToString(x.EmployeeID) }).ToList();

            for (int i = 0; i <= DayId; i++)
            {
                var EmployeeQuotationDayList = new List<EmployeeQuotationDayModel>();
                for (int j = 0; j <= EmpId; j++)
                {
                    EmployeeQuotationDayList.Add(new EmployeeQuotationDayModel
                    {
                        EmployeeList = Elist,
                    });
                }
                model.QuotationDayList.Add(
                    new QuotationDayModel
                    {
                        EventDate = DateTime.Now,
                        IsDelete = i <= DayId,
                        EmployeeQuotationDayList = EmployeeQuotationDayList,
                    });
            }
            TempData["DayId"] = DayId;
            TempData["DayEmpId"] = EmpId;
            return PartialView("_AddEmployeeList", model);
        }

        public ActionResult AddProductList(int Id)
        {
            QuotationModel model = new QuotationModel();
            model.ProductQuotationList = new List<ProductQuotationModel>();
            var plist = ProductService.getActiveProductList().Select(x => new SelectListItem { Text = x.ProductName, Value = Convert.ToString(x.ProductId) }).ToList();

            for (int i = 0; i <= Id; i++)
            {
                model.ProductQuotationList.Add(new ProductQuotationModel
                {
                    ProductList = plist,
                    IsDelete = (i != Id)
                });
            }
            return PartialView("_AddProductList", model);
        }

        public ActionResult GetProductData(int ProductID)
        {
            return Json(ProductService.getProductbyId(ProductID), JsonRequestBehavior.AllowGet);
        }

    }
}