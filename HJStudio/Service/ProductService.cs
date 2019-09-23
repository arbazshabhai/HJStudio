using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Data;
using HJStudio.Models;

namespace HJStudio.Service
{
    public class ProductService
    {
        public static bool AddProduct(ProductModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    ProductMaster prod = new ProductMaster();
                    var temp = context.ProductMasters.Find(model.ProductId);
                    prod = temp == null ? new ProductMaster() : temp;

                    prod.ProductName = model.ProductName;
                    prod.ProductDescription = model.ProductDescription;
                    prod.Amount = model.Amount;
                    
                    if (temp == null)
                    {
                        prod.CreatedBy = "Admin";
                        prod.CreatedDate = DateTime.Now;
                        context.ProductMasters.Add(prod);
                    }
                    else
                    {
                        prod.ModifiedBy = "Admin";
                        prod.ModifiedDate = DateTime.Now;

                    }
                    context.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static List<ProductModel> LoadProductDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = context.ProductMasters.ToList().Select(x => new ProductModel
                    {
                       ProductId = x.ProductId,
                       ProductName = x.ProductName,
                       ProductDescription = x.ProductDescription,
                       Amount = x.Amount,
                       CreatedBy = x.CreatedBy,
                       ModifiedBy = x.ModifiedBy,
                       cdate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                       mdate = x.ModifiedDate != null ? x.ModifiedDate.Value.ToString("dd-MM-yyyy") : "",
                       
                    }).ToList();

                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.ProductName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.ProductDescription).ToList();
                                    break;
                                case 3:
                                    list = list.OrderByDescending(x => x.Amount).ToList();
                                    break;
                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.ProductName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.ProductDescription).ToList();
                                    break;
                                case 3:
                                    list = list.OrderBy(x => x.Amount).ToList();
                                    break;

                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ProductId).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.ProductId != null ? x.ProductId.Equals(Search) : true) ||
                        (x.ProductName != null ? x.ProductName.ToLower().Contains(Search) : true) ||
                        (x.ProductDescription != null ? x.ProductDescription.ToLower().Contains(Search) : true) ||
                        (x.Amount != null ? x.Amount.Equals(Search) : true) 
                        ).ToList();
                    }
                    Total = list != null ? list.Count() : 0;
                    list = list.Skip(startIndex).Take(endIndex).ToList();

                    return list;

                }
            }
            catch (Exception)
            {

                Total = 0;
                return null;
            }
        }

        public static ProductModel getProductbyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.ProductMasters.Where(x => x.ProductId == id).Select(x => new ProductModel
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        ProductDescription = x.ProductDescription,
                        Amount = x.Amount,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        CreatedDate = x.CreatedDate
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static bool DeleteProduct(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    ProductMaster prod = new ProductMaster();
                    var temp = context.ProductMasters.Find(id);
                    prod = temp == null ? new ProductMaster() : temp;


                    if (temp == null)
                        return false;
                    context.ProductMasters.Remove(prod);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}