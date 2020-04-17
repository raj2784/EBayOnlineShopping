using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EbayOnlineShopping.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Required!")]
        [StringLength(100, ErrorMessage = "Minimum 3 and maximum 100 Character!", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 100)]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage = "Descriptaion Name is Required!")]
        public string Description { get; set; }
        // public HttpPostedFileBase PhotoFile { get; set; }

        [Required]
        public string ProductImage { get; set; }
        public Nullable<bool> IsFetured { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity!")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(int), "1", "10000", ErrorMessage = "Invalid Price!")]
        public Nullable<decimal> Price { get; set; }
        public SelectList Categories { get; set; }
    }
}