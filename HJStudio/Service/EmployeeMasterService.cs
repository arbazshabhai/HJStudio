using HJStudio.Data;
using HJStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HJStudio.Service
{
    public class EmployeeMasterService
    {
        public static bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    EmployeeMaster emp = new EmployeeMaster();
                    var temp = context.EmployeeMasters.Find(model.EmployeeID);
                    emp = temp == null ? new EmployeeMaster() : temp;

                    emp.Name = model.Name;
                    emp.Address1 = model.Address1;
                    emp.Address2 = model.Address2;
                    emp.EmailId = model.EmailId;
                    emp.MobileNo = model.MobileNo;
                    emp.City = model.City;
                    emp.State = model.State;
                    emp.Password = model.Password;
                    emp.UserType = model.UserType;
                    emp.DailyWages = model.DailyWages;
                    emp.HalfDayWages = model.HalfDayWages;
                    emp.MonthlyWages = model.MonthlyWages;
                    
                    if(temp == null)
                    {
                        emp.CreatedBy = "Admin";
                        emp.CreatedDate = DateTime.Now;
                        emp.Status = 1;
                        context.EmployeeMasters.Add(emp);
                    }
                    else
                    {
                        emp.ModifiedBy = "Admin";
                        emp.ModifiedDate = DateTime.Now;

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

        public static List<EmployeeModel> LoadEmpDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = context.EmployeeMasters.ToList().Select(x => new EmployeeModel
                    {
                        EmployeeID = x.EmployeeID,
                        Name = x.Name,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        EmailId = x.EmailId,
                        MobileNo = x.MobileNo,
                        UserType = x.UserType,
                        Password = x.Password,
                        DailyWages =x.DailyWages,
                        HalfDayWages = x.HalfDayWages,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        MonthlyWages = x.MonthlyWages,
                        cdate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                        mdate = x.ModifiedDate!= null ? x.ModifiedDate.Value.ToString("dd-MM-yyyy") : "",
                        Status = x.Status
                    }).ToList();

                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.Name).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.EmailId).ToList();
                                    break;
                                case 3:
                                    list = list.OrderByDescending(x => x.MobileNo).ToList();
                                    break;
                                case 4:
                                    list = list.OrderByDescending(x => x.UserType).ToList();
                                    break;
                                case 5:
                                    list = list.OrderByDescending(x => x.CreatedBy).ToList();
                                    break;
                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.Name).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.EmailId).ToList();
                                    break;
                                case 3:
                                    list = list.OrderBy(x => x.MobileNo).ToList();
                                    break;
                                case 4:
                                    list = list.OrderBy(x => x.UserType).ToList();
                                    break;
                                case 5:
                                    list = list.OrderBy(x => x.CreatedBy).ToList();
                                    break;
                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.EmployeeID).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.Name != null ? x.Name.Contains(Search) : true) ||
                        (x.City != null ? x.City.ToLower().Contains(Search) : true) ||
                        (x.State != null ? x.State.ToLower().Contains(Search) : true) ||
                        (x.EmailId != null ? x.EmailId.ToLower().Contains(Search) : true) ||
                        (x.MobileNo != null ? x.MobileNo.ToLower().Contains(Search) : true) ||
                        (x.CreatedBy != null ? x.CreatedBy.ToLower().Contains(Search) : true) 
                        
                        //(x.UserType != null ? x.UserType == Search : true)
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

        public static EmployeeModel getEventbyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.EmployeeMasters.Where(x => x.EmployeeID == id).Select(x => new EmployeeModel
                    {
                        EmployeeID = x.EmployeeID,
                        Name = x.Name,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        EmailId = x.EmailId,
                        MobileNo = x.MobileNo,
                        UserType = x.UserType,
                        Password = x.Password,
                        DailyWages = x.DailyWages,
                        HalfDayWages = x.HalfDayWages,
                        MonthlyWages = x.MonthlyWages,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        //cdate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                        //mdate = x.ModifiedDate != null ? x.ModifiedDate.Value.ToString("dd-MM-yyyy") : "",
                        Status = x.Status

                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool DeActivateEmployee(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    EmployeeMaster TblUser = new EmployeeMaster();
                    var temp = context.EmployeeMasters.Find(id);
                    TblUser = temp == null ? new EmployeeMaster() : temp;
                    TblUser.Status = 2;

                    if (temp == null)
                        return false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool ActivatetEmployee(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    EmployeeMaster TblUser = new EmployeeMaster();
                    var temp = context.EmployeeMasters.Find(id);
                    TblUser = temp == null ? new EmployeeMaster() : temp;
                    TblUser.Status = 1;

                    if (temp == null)
                        return false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteEmployee(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    EmployeeMaster TblUser = new EmployeeMaster();
                    var temp = context.EmployeeMasters.Find(id);
                    TblUser = temp == null ? new EmployeeMaster() : temp;


                    if (temp == null)
                        return false;
                    context.EmployeeMasters.Remove(TblUser);
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