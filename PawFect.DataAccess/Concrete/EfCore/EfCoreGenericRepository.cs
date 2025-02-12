using Microsoft.EntityFrameworkCore;
using PawFect.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore //Dataacces katmanı veri tabanına bağlanıp CRUD(create-read-update-delete) işlemlerini yapar
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext, new()
    {
        private readonly TContext _context;

        public EfCoreGenericRepository(TContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();// changetracker değişiklikleri takip eder ve değişiklikleri veritabanına yansıtır
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("An error occurred while creating the entity.", ex);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new RepositoryException("An error occurred while deleting the entity.");
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null) //polymorphism
        {
            try
            {
                return filter == null ? _context.Set<T>().ToList() : _context.Set<T>().Where(filter).ToList();
            }
            catch (Exception)
            {
                throw new RepositoryException("An error occurred while getting the entities.");
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw new RepositoryException("An error occurred while getting the entity by id.");
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                return _context.Set<T>().Where(filter).FirstOrDefault();//verdiğimiz şarta uygun verilerin arasından ilkini getirir
            }
            catch (Exception)
            {
                throw new RepositoryException("An error occurred while getting the entity by filter.");
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new RepositoryException("An error occurred while updating the entity.");
            }
        }
    }


     

    // Özel durumlar için özel bir istisna sınıfı
    public class RepositoryException : Exception
    {
        // Parametresiz constructor
        public RepositoryException()
        {
        }

        // Hata mesajı ile constructor
        public RepositoryException(string message)
            : base(message)
        {
        }

        // Hata mesajı ve iç istisna (inner exception) ile constructor
        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
