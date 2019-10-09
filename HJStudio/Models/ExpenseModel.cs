using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class ExpenseModel
    {
        public int ExpenseID { get; set; }
        public string EmpName { get; set; }
        public string Description { get; set; }
        public Nullable<double> Amount { get; set; }
        public bool IsBank { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string IFSCCode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string cdate { get; set; }
    }
}