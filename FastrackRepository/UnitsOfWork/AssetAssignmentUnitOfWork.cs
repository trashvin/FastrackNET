using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using Common;

namespace Repository.UnitsOfWork
{
    public class AssetAssignmentUnitOfWork<T,S> : AUnitOfWork<T> 
        where T: AssetAssignment
        where S: FixAsset
    {
        public GenericRepository<T> Assignments {get; set;}
        public GenericRepository<S> Assets { get; set; }
    
        public AssetAssignmentUnitOfWork()
        {
            Assignments = new GenericRepository<T>();
            Assets = new GenericRepository<S>();
            Assignments.Context = _context;
            Assets.Context = _context;    

        }





    }
}
