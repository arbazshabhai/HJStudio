using HJStudio.Data;
using HJStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HJStudio.Service
{
    public class QuotationService
    {
        public static bool AddQuotation(QuotationModel model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {

                    ClientMaster clt = new ClientMaster();
                    var Clienttemp = context.ClientMasters.Find(model.ClientID);
                    clt = Clienttemp == null ? new ClientMaster() : Clienttemp;

                    clt.Name = model.Name;
                    clt.MobileNo = model.MobileNo;
                    clt.EmailId = model.EmailId;
                    clt.Address1 = model.Address1;
                    clt.Address2 = model.Address2;
                    clt.City = model.City;
                    clt.State = model.State;
                    clt.Refrence = model.Refrence;



                    if (Clienttemp == null)
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
                    int ClientID = clt.ClientID;


                    QuotationMaster _Quotation = new QuotationMaster();
                    var temp = context.QuotationMasters.Find(model.QuotationID);
                    _Quotation = temp == null ? new QuotationMaster() : temp;

                    if (temp != null)
                    {

                    }

                    _Quotation.QuotationDate = model.QuotationDate;
                    _Quotation.EventStartDate = model.EventStartDate;
                    _Quotation.EventDays = model.EventDays;
                    _Quotation.Remark = model.Remark;
                    _Quotation.QuotationAmount = model.QuotationAmount;
                    _Quotation.Discount = model.Discount;
                    _Quotation.FinalAmount = model.FinalAmount;
                    _Quotation.ClientID = ClientID;
                    _Quotation.Name = model.Name;
                    _Quotation.MobileNo = model.MobileNo;
                    _Quotation.EmailId = model.EmailId;
                    _Quotation.Address1 = model.Address1;
                    _Quotation.Address2 = model.Address2;
                    _Quotation.City = model.City;
                    _Quotation.State = model.State;
                    _Quotation.Refrence = model.Refrence;
                    _Quotation.Status = model.Status;
                    List<QuotationDay> _QuotationDay = new List<Data.QuotationDay>();
                    if (model.QuotationDayList != null)
                        foreach (var d in model.QuotationDayList)
                        {
                            List<EmployeeQuotationDay> _EmployeeQuotationDay = new List<EmployeeQuotationDay>();
                            if (d.EmployeeQuotationDayList != null)
                                foreach (var e in d.EmployeeQuotationDayList)
                                {
                                    if (e.IsDelete == false && e.EmployeeID > 0)
                                        _EmployeeQuotationDay.Add(new EmployeeQuotationDay
                                        {
                                            EmployeeID = e.EmployeeID,
                                            Wages = e.Wages,
                                            EmployeeRole = e.EmployeeRole,
                                        });
                                }
                            _QuotationDay.Add(new QuotationDay
                            {
                                EventName = d.EventName,
                                EventDate = d.EventDate,
                                StartTime = d.StartTime,
                                EndTime = d.EndTime,
                                Remarks = d.Remarks,
                                Address1 = d.Address1,
                                Address2 = d.Address2,
                                City = d.City,
                                State = d.State,
                                ClientMobileNo = d.ClientMobileNo,
                                EmployeeQuotationDays = _EmployeeQuotationDay
                            });
                        }

                    _Quotation.QuotationDays = _QuotationDay;

                    List<ProductQuotation> _ProductQuotation = new List<ProductQuotation>();
                    if (model.ProductQuotationList != null)
                        foreach (var p in model.ProductQuotationList)
                        {
                            if (p.ProductID > 0)
                                _ProductQuotation.Add(new ProductQuotation
                                {
                                    ProductID = p.ProductID,
                                    Quantity = p.Quantity,
                                    Amount = p.Amount,
                                    TotalAmount = p.TotalAmount,
                                    Remark = p.Remark,
                                });
                        }
                    _Quotation.ProductQuotations = _ProductQuotation;

                    if (temp == null)
                    {
                        _Quotation.CreatedBy = CommanModel.LoginUserDetails.EmployeeID;
                        _Quotation.CreatedDate = DateTime.Now;
                        context.QuotationMasters.Add(_Quotation);
                    }
                    else
                    {
                        _Quotation.ModifiedBy = CommanModel.LoginUserDetails.EmployeeID;
                        _Quotation.ModifiedDate = DateTime.Now;

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

        public static List<QuotationModel> LoadQuotationDetail(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;
                    var list = context.QuotationMasters.ToList().Select(x => new QuotationModel
                    {
                        QuotationDate = x.QuotationDate,
                        QuotationDateStr = x.QuotationDate != null ? x.QuotationDate.Value.ToString("dd-MM-yyyy") : "",
                        EventStartDate = x.EventStartDate,
                        EventDays = x.EventDays,
                        Remark = x.Remark,
                        QuotationAmount = x.QuotationAmount,
                        Discount = x.Discount,
                        FinalAmount = x.FinalAmount,
                        ClientID = x.ClientID,
                        Name = x.Name,
                        MobileNo = x.MobileNo,
                        EmailId = x.EmailId,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        Refrence = x.Refrence,
                        Status = x.Status,
                    }).ToList();

                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.QuotationDate).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.Name).ToList();
                                    break;
                                case 3:
                                    list = list.OrderByDescending(x => x.MobileNo).ToList();
                                    break;
                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.QuotationDate).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.Name).ToList();
                                    break;
                                case 3:
                                    list = list.OrderBy(x => x.MobileNo).ToList();
                                    break;

                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.QuotationID).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.QuotationDateStr != null ? x.QuotationDateStr.ToLower().Contains(Search) : true) ||
                        (x.Name != null ? x.Name.ToLower().Contains(Search) : true) ||
                        (x.MobileNo != null ? x.MobileNo.Equals(Search) : true)
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

        public static QuotationModel getQuotationbyId(int id)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    var data= context.QuotationMasters.Where(x => x.QuotationID == id).Select(x => new QuotationModel
                    {
                        QuotationID = x.QuotationID,
                        QuotationDate = x.QuotationDate,
                        EventStartDate = x.EventStartDate,
                        EventDays = x.EventDays,
                        Remark = x.Remark,
                        QuotationAmount = x.QuotationAmount,
                        Discount = x.Discount,
                        FinalAmount = x.FinalAmount,
                        ClientID = x.ClientID,
                        Name = x.Name,
                        MobileNo = x.MobileNo,
                        EmailId = x.EmailId,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        Refrence = x.Refrence,
                        Status = x.Status,
                    }).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}