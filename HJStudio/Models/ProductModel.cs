using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name Required")]
        // [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string ProductName { get; set; }
        //[Required(ErrorMessage = "Product Description Required")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Product Amount Required")]
        public Nullable<double> Amount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string cdate { get; set; }
        public string mdate { get; set; }
    }
}