using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FASTService;
using System.Data.Entity;

namespace Repository.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T:class
    {
        protected FASTDBModel _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(FASTDBModel context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public FASTDBModel Context
        {
            set
            {
                _context = value;
                _dbSet = _context.Set<T>();
            }
        }

        public GenericRepository()
        {
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public T Insert(T data)
        {
            return _dbSet.Add(data);
        }

        public void Update(T data)
        {
            try
            {
                T result = _dbSet.Attach(data);
                _context.Entry(data).State = EntityState.Modified;
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException dex)
            {
                Console.WriteLine(dex.ToString());
            }
        }

        public T Delete(object id)
        {
            T toDelete = _dbSet.Find(id);
            if ( toDelete !=null)
            {
                return _dbSet.Remove(toDelete);
            }

            return null;
        }


        public IQueryable<T> GetAllQueryable()
        {
            return _dbSet;
        }


        public T Delete(T data)
        {
            return _dbSet.Remove(data);
        }
    }
}
