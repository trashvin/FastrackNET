using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Repositories;
using Common;

namespace Repository.UnitsOfWork
{
    public class UserProcessingUnitOfWork<T,W> : AUnitOfWork<T> 
        where T: Employee 
        where W: Registration 
    {
        public GenericRepository<T> Employees { get; set; }
        public GenericRepository<W> Registrations { get; set; }
    
        public UserProcessingUnitOfWork()
        {
            Employees = new GenericRepository<T>();
            Registrations = new GenericRepository<W>();
            Employees.Context = _context;
            Registrations.Context = _context;    

        }

      

       
    }

}
