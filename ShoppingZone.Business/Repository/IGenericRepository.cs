using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingZone.Business.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetOne(int id);

        void NewOne(T entity);

        void Put(T entity);

        void Delete(int id);
    }
}
