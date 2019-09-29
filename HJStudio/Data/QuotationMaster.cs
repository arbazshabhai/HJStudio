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
    
    public partial class QuotationMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuotationMaster()
        {
            this.ProductQuotations = new HashSet<ProductQuotation>();
            this.QuotationDays = new HashSet<QuotationDay>();
        }
    
        public int QuotationID { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public Nullable<System.DateTime> EventStartDate { get; set; }
        public Nullable<int> EventDays { get; set; }
        public string Remark { get; set; }
        public Nullable<double> QuotationAmount { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> FinalAmount { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Refrence { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductQuotation> ProductQuotations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotationDay> QuotationDays { get; set; }
    }
}
