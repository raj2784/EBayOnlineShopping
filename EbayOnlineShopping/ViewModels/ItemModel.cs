using EbayOnlineShopping.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbayOnlineShopping.ViewModels
{
    public class ItemModel
    {
        public tblProduct Product { get; set; }

        public int Quntity { get; set; }
    }
}