using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Models;
using HJStudio.Service;

namespace HJStudio.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GetMenuDetail(DataTableParamModel parm)
        //{
        //    int startIndex = parm.iDisplayStart;
        //    int endIndex = parm.iDisplayStart + parm.iDisplayLength;
        //    int recordsTotal = 0;

        // //   List<MenuModel> list = MenuService.GetMenuList(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

        //    return Json(new
        //    {
        //        Echo = parm.sEcho,
        //        iTotalRecord = recordsTotal,
        //        iTotalDisplayRecords = recordsTotal,
        //       // data = list
        //    });
        //}
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            MenuModel _model = new MenuModel();
            _model.UserId = Id;
          //  ViewBag.MenuTreeModel = MenuService.GetMenuTreeByUser(Id);

            return View(_model);
        }

        [HttpPost]
        public ActionResult Add(MenuModel _model)
        {
            if (_model.UserId > 0)
            {
                if (!string.IsNullOrEmpty(_model.MenuList))
                {
                    _model.MenuIdList = _model.MenuList.Split(',').Select(x => int.Parse(x)).ToList();
                }
             //   int Status = MenuService.AddEditUserMenu(_model);
                //if (Status > 0)
                //{
                //    TempData["Success"] = "User saved successfully.";
                //}
                //else
                //{
                //    TempData["Error"] = "Something went wrong try again Later.";
                //}
            }
            return RedirectToAction("Index");
        }
    }
}