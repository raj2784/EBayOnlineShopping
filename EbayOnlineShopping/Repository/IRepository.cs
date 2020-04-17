using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbayOnlineShopping.Repository
{
    public interface IRepository<tblEntity> where tblEntity : class

    {
        IEnumerable<tblEntity> GetProduct();
        IEnumerable<tblEntity> GetAllRecords();
        IQueryable<tblEntity> GetAllRecordsIQueryable();
        int GetAllRecordsCount();
        void Add(tblEntity entity);
        void Update(tblEntity entity);
        void UpdateByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict);
        tblEntity GetFirstOrDefault(int recordId);
        void Remove(tblEntity entity);
        void RemoveByWhereClause(Expression<Func<tblEntity, bool>> wherePredict);
        void RemoveByWhereCluase(Expression<Func<tblEntity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict);
        tblEntity GetFirstOrDefaultByParameter(Expression<Func<tblEntity, bool>> wherePredict);
        IEnumerable<tblEntity> GetListParameter(Expression<Func<tblEntity, bool>> wherePredict);
        IEnumerable<tblEntity> GetResultBySqlprocedure(string query, params object[] parameters);
        IEnumerable<tblEntity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<tblEntity, bool>>
            wherePredict, Expression<Func<tblEntity, int>> orderByPredict);

    }
}
