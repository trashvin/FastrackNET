using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;

namespace Repository.UnitsOfWork
{
    public class GenericUnitOfWork<T> : AUnitOfWork<T> where T:class
    {
        public GenericRepository<T> Repository
        {
            get;
            set;
        }

        public GenericUnitOfWork()
        {
            Repository = new GenericRepository<T>();
            Repository.Context = _context;
        }

      

    }
}
