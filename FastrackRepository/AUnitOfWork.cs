using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FASTService;
using Repository.Repositories;
using Common;

namespace Repository
{
    public abstract class AUnitOfWork<T> where T:class 
    {
        protected FASTDBModel _context;
        protected GenericRepository<Common.AuditTrail> _audits =
            new GenericRepository<Common.AuditTrail>();
        
        public AUnitOfWork()
        {
            _context = new FASTDBModel();
            _audits.Context = _context;
            
        }

        //public abstract void SetRepositoryContext();

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException dex)
            {
                Console.Write(dex.ToString());
                return 0;
            }

        }

    

        public virtual void LogFailure(string action, string remarks, int employeeID = 0, string assetTag = "", string assignID = "")
        {
            Common.AuditTrail newLog = new Common.AuditTrail()
            {
                EmployeeID = employeeID,
                Action = action,
                AdditionalInformation = "FAILED." + remarks,
                AssetTag = assetTag,
                AssignmentID = assignID,
                Date = DateTime.Now
            };
            _audits.Insert(newLog);
            Save();

        }

        public virtual void LogSuccess(string action, string remarks, int employeeID = 0, string assetTag = "", string assignID = "")
        {
            Common.AuditTrail newLog = new Common.AuditTrail()
            {
                EmployeeID = employeeID,
                Action = action,
                AdditionalInformation = "SUCCESSFUL." + remarks,
                AssetTag = assetTag,
                AssignmentID = assignID,
                Date = DateTime.Now
            };
            _audits.Insert(newLog);
            Save();
        }

        public virtual void Log(string action, string remarks, int employeeID = 0, string assetTag = "", string assignID = "")
        {
            Common.AuditTrail newLog = new Common.AuditTrail()
            {
                EmployeeID = employeeID,
                Action = action,
                AdditionalInformation = remarks,
                AssetTag = assetTag,
                AssignmentID = assignID,
                Date = DateTime.Now
            };
            _audits.Insert(newLog);
            Save();
        }

    }
}
