using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbayOnlineShopping.DAL;

namespace EbayOnlineShopping.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private EbayOnlineShoppingEntities DBEntity = new EbayOnlineShoppingEntities();
        public IRepository<tblEntityType> GetRepositoryInstance<tblEntityType>() where tblEntityType : class
        {
            return new GenericRepository<tblEntityType>(DBEntity);

        }
        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();

                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}