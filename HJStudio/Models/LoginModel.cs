using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LogedUserDetails
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
    }
}