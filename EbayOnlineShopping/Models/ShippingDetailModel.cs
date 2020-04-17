using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EbayOnlineShopping.Models
{
    public class ShippingDetailModel
    {
        public int ShippingDetailId { get; set; }
        [Required]
        public Nullable<int> UserId { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required!")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "ZipCode is required!")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "OrderId is required!")]
        public Nullable<int> OrderId { get; set; }

        public Nullable<decimal> AmountPaid { get; set; }

        [Required(ErrorMessage = "PaymentType is required!")]
        public string PaymentType { get; set; }
    }
}