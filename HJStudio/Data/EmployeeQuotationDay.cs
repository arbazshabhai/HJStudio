//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HJStudio.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeQuotationDay
    {
        public int EmployeeQuotationDaysID { get; set; }
        public Nullable<int> QuotationDaysID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<double> Wages { get; set; }
        public string EmployeeRole { get; set; }
    
        public virtual QuotationDay QuotationDay { get; set; }
    }
}
