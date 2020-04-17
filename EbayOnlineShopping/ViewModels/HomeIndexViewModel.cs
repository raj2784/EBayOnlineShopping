using EbayOnlineShopping.DAL;
using EbayOnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using PagedList;
using PagedList.Mvc;
using System.Web;

namespace EbayOnlineShopping.ViewModel
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenricUnitOfWork();
        EbayOnlineShoppingEntities context = new EbayOnlineShoppingEntities();

        public IPagedList<tblProduct> ListOfProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search,int pageSize, int? page)
        {

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                 new SqlParameter ("@search",search??(object)DBNull.Value)
            };
            IPagedList<tblProduct> data = context.Database.SqlQuery<tblProduct>("GetBySearch @search", sqlParameters)
                                                                             .ToList().ToPagedList(page ?? 1, pageSize);

            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }
}