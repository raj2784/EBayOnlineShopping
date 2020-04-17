    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EbayOnlineShopping.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required (ErrorMessage ="Category Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and maximum 100 Character",MinimumLength = 3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}