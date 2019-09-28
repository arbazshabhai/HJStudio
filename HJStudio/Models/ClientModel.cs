using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class ClientModel
    {
        public int? ClientID { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile No Required")]
        [StringLength(10, ErrorMessage = "Mobile No must be 10 number", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile No must be numeric")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email-Id Required")]
        [EmailAddress(ErrorMessage = "Email-Id is Invalid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Address1 Required")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Address2 Required")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "City Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string State { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string Refrence { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string cdate { get; set; }
        public string mdate { get; set; }
    }
}