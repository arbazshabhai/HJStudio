using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJStudio.Models;
using HJStudio.Service;

namespace HJStudio.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEdit(ProductModel model)
        {
            bool ProductStatus = ProductService.AddProduct(model);

            if (ProductStatus)
                TempData["Success"] = "Product Added Successfully.";
            else
                TempData["Error"] = "Error, Please Try Again.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel();
            model = ProductService.getProductbyId(id);
            return View("Add", model);
        }

        [HttpPost]
        public ActionResult GetProductDetail(jQueryDataTableParamModel parm)
        {
            int startIndex = parm.iDisplayStart;
            int endIndex = parm.iDisplayStart + parm.iDisplayLength;
            int recordsTotal = 0;

            List<ProductModel> list = ProductService.LoadProductDetail(parm.sSearch, startIndex, endIndex, parm.iSortCol_0, parm.sSortDir_0, out recordsTotal);

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
            bool Status = ProductService.DeleteProduct(Id);

            return Json(Status);
        }
    }
}