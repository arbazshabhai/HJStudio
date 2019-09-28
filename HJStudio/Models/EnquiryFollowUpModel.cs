using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class EnquiryFollowUpModel
    {
        public int? InquiryFollowupId { get; set; }
        public int? InquiryId { get; set; }

        [Required(ErrorMessage = "Remarks is required")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime? NextFolowupDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public int? InquiryStatus { get; set; }
        public string InquiryStatusString { get; set; }

        public List<EnquiryFollowUpModel> EnquiryFollowUpList { get; set; }
    }
}