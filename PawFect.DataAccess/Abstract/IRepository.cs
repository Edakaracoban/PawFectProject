using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Abstract
{
    public interface IRepository<T> where T : class //T entitye karşılık gelmekte
    {
        T GetById(int id);
        T GetOne(Expression<Func<T, bool>> filter = null); //filter gelmesse default parametresi null olacak//sql sorgusu
        List<T> GetAll(Expression<Func<T, bool>> filter = null); //sorgu almassa null olucak
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
    //data ile ilgili manipülasyonları dataaccess yapar.
}
