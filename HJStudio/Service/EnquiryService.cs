using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Data;
using HJStudio.Models;

namespace HJStudio.Service
{
    public class EnquiryService
    {
        public static bool AddEnquiry(EnquiryModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    ClientMaster clt = new ClientMaster();
                    var temp = context.ClientMasters.Find(model.client.ClientID);
                    clt = temp == null ? new ClientMaster() : temp;

                    clt.Name = model.client.Name;
                    clt.MobileNo = model.client.MobileNo;
                    clt.EmailId = model.client.EmailId;
                    clt.Address1 = model.client.Address1;
                    clt.Address2 = model.client.Address2;
                    clt.City = model.client.City;
                    clt.State = model.client.State;
                    clt.Refrence = model.client.Refrence;



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
                    if (temp == null)
                        model.client.ClientID = clt.ClientID;


                    FunctionDetail flt = new FunctionDetail();
                    var temp1 = context.FunctionDetails.Find(model.Id);
                    flt = temp1 == null ? new FunctionDetail() : temp1;

                    flt.FunctionName = model.FunctionName;
                    flt.FunctionDescription = model.FunctionDescription;
                    flt.FunctionDate = model.FunctionDate;
                    flt.City = model.City;
                    flt.State = model.State;
                    flt.Address = model.Address;
                    flt.ClientId= model.client.ClientID;

                    if(temp1 == null)
                    {
                        flt.CreatedDate = DateTime.Now;
                        flt.CreatedBy = "Admin";
                        context.FunctionDetails.Add(flt);
                    }
                    else
                    {
                        flt.ModifiedDate = DateTime.Now;
                        flt.ModifiedBy = "Admin";
                    }

                    EnquiryFollowUp EF = new EnquiryFollowUp();
                    EF.NextFolowupDate = model.FunctionDate;
                    EF.EnquiryId = flt.Id;
                    EF.CreatedDate = DateTime.Now;
                    EF.CreatedBy = "Admin";
                    EF.EnquiryStatus = 2;
                    context.SaveChanges();

                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static List<EnquiryModel> LoadEnquiryDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = (from ED in context.FunctionDetails
                               join CI in context.ClientMasters on ED.ClientId equals CI.ClientID
                               select new { ED, CI }).ToList().Select(x => new EnquiryModel
                               {
                                   Id = x.ED.Id,
                                   FunctionName = x.ED.FunctionName,
                                   FunctionDescription = x.ED.FunctionDescription,
                                   FunctionDate = x.ED.FunctionDate,
                                   client = new ClientModel()
                                   {
                                       Name = x.CI.Name,
                                       MobileNo = x.CI.MobileNo,
                                       EmailId = x.CI.EmailId,
                                       Refrence = x.CI.Refrence,
                                       Address1 = x.CI.Refrence,
                                       Address2 = x.CI.Refrence,
                                       City = x.CI.City,
                                       State = x.CI.State

                                   }
                               }).ToList();
                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.FunctionName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.client.Name).ToList();
                                    break;
                                case 3:
                                    list = list.OrderByDescending(x => x.client.MobileNo).ToList();
                                    break;
                                case 4:
                                    list = list.OrderByDescending(x => x.CreatedBy).ToList();
                                    break;
                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.FunctionName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.client.Name).ToList();
                                    break;
                                case 3:
                                    list = list.OrderBy(x => x.client.MobileNo).ToList();
                                    break;
                                case 4:
                                    list = list.OrderBy(x => x.CreatedBy).ToList();
                                    break;
                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Id).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.FunctionName != null ? x.FunctionName.Contains(Search) : true) ||
                        (x.City != null ? x.City.ToLower().Contains(Search) : true) ||
                        (x.client.Name != null ? x.client.Name.ToLower().Contains(Search) : true) ||
                        (x.FunctionDate != null ? x.FunctionDate.ToString().Contains(Search) : true) ||
                        (x.client.MobileNo != null ? x.client.MobileNo.ToLower().Contains(Search) : true) ||
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

        public static EnquiryModel getEnquirybyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return (from ED in context.FunctionDetails
                     join CI in context.ClientMasters on ED.ClientId equals CI.ClientID
                     select new { ED, CI }).ToList().Select(x => new EnquiryModel
                     {
                          Id = x.ED.Id,
                          FunctionName = x.ED.FunctionName,
                          FunctionDescription = x.ED.FunctionDescription,
                          FunctionDate = x.ED.FunctionDate,
                          client = new ClientModel()
                          {
                              Name = x.CI.Name,
                              MobileNo = x.CI.MobileNo,
                              EmailId = x.CI.EmailId,
                              Refrence = x.CI.Refrence,
                              Address1 = x.CI.Refrence,
                              Address2 = x.CI.Refrence,
                              City = x.CI.City,
                              State = x.CI.State

                          }

                     }).FirstOrDefault();
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int EnquiryFollowUpModel(EnquiryFollowUpModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    EnquiryFollowUp ef = new EnquiryFollowUp();
                    //var temp1 = context.EnquiryFollowUps.Find(model.InquiryId);
                   // ef = temp1 == null ? new EnquiryFollowUp() : temp1;

                    ef.EnquiryId = model.InquiryId;
                    ef.EnquiryStatus = model.InquiryStatus;
                    ef.Remarks = model.Remarks;
                    ef.NextFolowupDate = model.NextFolowupDate;
                    context.EnquiryFollowUps.Add(ef);
                    context.SaveChanges();


                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static List<EnquiryFollowUpModel> GetInquiryFollowupByInquiryId(int Id)
        {
            try
            {
                using (HJStudioEntities db = new HJStudioEntities())
                {
                    return db.EnquiryFollowUps.Where(x => x.EnquiryId == Id).OrderByDescending(x => x.EnquiryFollowupId).ToList().Select(x => new EnquiryFollowUpModel
                    {
                        InquiryFollowupId = x.EnquiryFollowupId,
                        InquiryId = x.EnquiryId,
                        Remarks = x.Remarks,
                        NextFolowupDate = x.NextFolowupDate,
                        InquiryStatusString = Enum.GetName(typeof(Enquirytype), x.EnquiryStatus),
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}