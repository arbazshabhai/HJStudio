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
    
    public partial class EnquiryFollowUp
    {
        public int EnquiryFollowupId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> NextFolowupDate { get; set; }
        public Nullable<int> EnquiryStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
