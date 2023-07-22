using ShoppingZone.Data.Entity;
using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Business.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        DataContext db;
        DbSet<T> dbSet;

        public GenericRepository()
        {
            db = new DataContext();
            this.dbSet = db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetOne(int id)
        {
            return dbSet.Find(id);
        }

        public void NewOne(T entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public void Put(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                db.SaveChanges();
            }
        }
    }
}
