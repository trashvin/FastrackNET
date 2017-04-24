using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> 
    {
        
        IList<T> GetAll();
        IQueryable<T> GetAllQueryable();
        T GetByID(object id);
       
        
        T Insert(T data);
        void Update(T data);
        T Delete(object id);
        T Delete(T data);
        
    }
}
