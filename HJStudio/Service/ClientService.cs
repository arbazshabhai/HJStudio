using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Data;
using HJStudio.Models;

namespace HJStudio.Service
{
    public class ClientService
    {
        public static bool AddClient(ClientModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    ClientMaster clt = new ClientMaster();
                    var temp = context.ClientMasters.Find(model.ClientID);
                    clt = temp == null ? new ClientMaster() : temp;

                    clt.Name = model.Name;
                    clt.MobileNo = model.MobileNo;
                    clt.EmailId = model.EmailId;
                    clt.Address1 = model.Address1;
                    clt.Address2 = model.Address2;
                    clt.City = model.City;
                    clt.State = model.State;
                    clt.Refrence = model.Refrence;
                    
                    

                    if (temp == null)
                    {
                        clt.CreatedBy = "Admin";
                        clt.CreatedDate = DateTime.Now;
                        context.ClientMasters.Add(clt);
                    }
                    else
                    {
                        clt.ModifiedBy = "Admin";
                        clt.ModifiedDate = DateTime.Now;

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

        public static List<ClientModel> LoadClientDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = context.ClientMasters.ToList().Select(x => new ClientModel
                    {
                        ClientID = x.ClientID,
                        Name = x.Name,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        EmailId = x.EmailId,
                        MobileNo = x.MobileNo,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        cdate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                        mdate = x.ModifiedDate != null ? x.ModifiedDate.Value.ToString("dd-MM-yyyy") : "",
                        Refrence = x.Refrence
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
                                    list = list.OrderBy(x => x.CreatedBy).ToList();
                                    break;
                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ClientID).ToList();
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

        public static ClientModel getClientbyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.ClientMasters.Where(x => x.ClientID == id).Select(x => new ClientModel
                    {
                        ClientID = x.ClientID,
                        Name = x.Name,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        EmailId = x.EmailId,
                        MobileNo = x.MobileNo,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        Refrence = x.Refrence
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static bool DeleteClient(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    ClientMaster TblUser = new ClientMaster();
                    var temp = context.ClientMasters.Find(id);
                    TblUser = temp == null ? new ClientMaster() : temp;


                    if (temp == null)
                        return false;
                    context.ClientMasters.Remove(TblUser);
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