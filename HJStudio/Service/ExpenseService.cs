using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Models;
using HJStudio.Data;

namespace HJStudio.Service
{
    public class ExpenseService
    {
        public static bool AddExpense(ExpenseModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Expense exp = new Expense();
                    var temp = context.Expenses.Find(model.ExpenseID);
                    exp = temp == null ? new Expense() : temp;

                    exp.Description = model.Description;
                    exp.EmpName = model.EmpName;
                    exp.BankName = model.BankName;
                    exp.IsBank = model.IsBank;
                    exp.IFSCCode = model.IFSCCode;
                    exp.Amount = model.Amount;

                    if (temp == null)
                    {
                        exp.CreatedBy = "Admin";
                        exp.CreatedDate = DateTime.Now;
                        context.Expenses.Add(exp);
                    }
                    else
                    {
                        exp.Modifiedby = "Admin";
                        exp.ModifiedDate = DateTime.Now;
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

        public static List<ExpenseModel> LoadExpenceDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = context.Expenses.ToList().Select(x => new ExpenseModel
                    {
                        Description = x.Description,
                        EmpName = x.EmpName,
                        BankName = x.BankName,
                        Amount = x.Amount,
                        CreatedBy = x.CreatedBy,
                        cdate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                        
                    }).ToList();

                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.Description).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.Amount).ToList();
                                    break;
                                case 3:
                                    list = list.OrderByDescending(x => x.EmpName).ToList();
                                    break;
                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.Description).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.Amount).ToList();
                                    break;
                                case 3:
                                    list = list.OrderBy(x => x.EmpName).ToList();
                                    break;

                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ExpenseID).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.Description != null ? x.Description.Contains(Search) : true) ||
                        (x.Amount != null ? x.Amount.ToString().Contains(Search) : true) ||
                        (x.CreatedBy != null ? x.CreatedBy.ToLower().Contains(Search) : true) ||
                        (x.EmpName != null ? x.EmpName.ToLower().Contains(Search) : true) ||
                        (x.BankName != null ? x.BankName.ToLower().Contains(Search) : true) ||
                        (x.cdate != null ? x.cdate.ToLower().Contains(Search) : true)
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

        public static ExpenseModel getExpensebyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.Expenses.Where(x => x.ExpenseID == id).Select(x => new ExpenseModel
                    {
                        Description = x.Description,
                        EmpName = x.EmpName,
                        BankName = x.BankName,
                        IsBank = x.IsBank,
                        IFSCCode = x.IFSCCode, 
                        Amount = x.Amount,
                        CreatedBy = x.CreatedBy
                }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}