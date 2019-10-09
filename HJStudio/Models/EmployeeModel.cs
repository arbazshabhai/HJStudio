using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        // public string EmplyeeUniqueID__UniqueId__UniqueId__UniqueID_______ { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "Use letters only")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile No Required")]
        [StringLength(10, ErrorMessage = "Mobile No must be 10 number", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile No must be numeric")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email-Id Required")]
        [EmailAddress (ErrorMessage = "Email-Id is Invalid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Required")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Select User Type")]
        public Nullable<int> UserType { get; set; }
        [Required(ErrorMessage = "Address1 Required")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Address2 Required")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "City Required")]
        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "Use letters only")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Required")]
        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "Use letters only")]
        public string State { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage = "Daily Wages Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Value must be numeric")]
        public Nullable<double> DailyWages { get; set; }
        [Required(ErrorMessage = "Half Day Wages Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Value must be numeric")]
        public Nullable<double> HalfDayWages { get; set; }
        [Required(ErrorMessage = "Monthly Wages Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Value must be numeric")]
        public Nullable<double> MonthlyWages { get; set; }
        public Nullable<int> Status { get; set; }
        public string cdate { get; set; }
        public string mdate { get; set; }
        public string ActionType { get; set; }
        public Nullable<int> WagesType { get; set; }
    }
}