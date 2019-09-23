using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HJStudio.Models
{
    public class QuotationModel
    {
        public QuotationModel()
        {
            QuotationDate = DateTime.Now;
            EventStartDate = DateTime.Now;
            City = "Anand";
            State = "Gujarat";
        }
        public int? QuotationID { get; set; }
        public DateTime? QuotationDate { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime? EventStartDate { get; set; }

        [Required(ErrorMessage = "Event Days is required")]
        public int? EventDays { get; set; }
        public string Remark { get; set; }

        [Required(ErrorMessage = "Quotation Amount is required")]
        public double? QuotationAmount { get; set; }
        public double? Discount { get; set; }

        [Required(ErrorMessage = "Final Amount is required")]
        public double? FinalAmount { get; set; }
        public int? ClientID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        public string MobileNo { get; set; }
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Address2 is required")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public string Refrence { get; set; }
        public int? Status { get; set; }

        public List<QuotationDayModel> QuotationDayList { get; set; }
        public List<ProductQuotationModel> ProductQuotationList { get; set; }
    }

    public class QuotationDayModel
    {
        public int? QuotationDaysID { get; set; }
        public int? QuotationID { get; set; }
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Remarks { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ClientMobileNo { get; set; }
        public bool IsDelete { get; set; }
        public string Daytype { get; set; }
        public bool SameasAbove { get; set; }

        public List<EmployeeQuotationDayModel> EmployeeQuotationDayList { get; set; }
    }

    public class EmployeeQuotationDayModel
    {
        public int EmployeeQuotationDaysID { get; set; }
        public int? QuotationDaysID { get; set; }
        public int? EmployeeID { get; set; }
        public double? Wages { get; set; }
        public string EmployeeRole { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
    }

    public class ProductQuotationModel
    {
        public int ProductQuotationID { get; set; }
        public int? QuotationID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public double? TotalAmount { get; set; }
        public string Remark { get; set; }
    }
}