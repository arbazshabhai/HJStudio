using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Models;

namespace HJStudio.Models
{
    public class EnquiryModel
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<System.DateTime> FunctionDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public ClientModel client {get; set;}
    }
}