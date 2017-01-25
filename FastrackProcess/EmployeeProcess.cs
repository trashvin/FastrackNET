using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.UnitsOfWork;
using Repository.Repositories;
using Common;

namespace FASTProcess
{
    public class EmployeeProcess : GenericProcess<Employee>
    {
        GenericUnitOfWork<Employee> _unitOfWork =
            new GenericUnitOfWork<Employee>();

        GenericUnitOfWork<vwEmployeeList> _employeeList =
            new GenericUnitOfWork<vwEmployeeList>();


        public IQueryable<vwEmployeeList> GetEmployeeProfiles()
        {
            return _employeeList.Repository.GetAllQueryable();
        }

        public vwEmployeeList GetEmployeeProfileByID(int userID)
        {
            try
            {
                return _employeeList.Repository.GetAllQueryable()
                    .Where(m => m.EmployeeID == userID).First();
            }
            catch
            {
                return new vwEmployeeList();
            }
        }

        public List<vwEmployeeList> GetEmployeesByDepartmentID(int departmentID)
        {
            try
            {
                return _employeeList.Repository.GetAllQueryable()
                    .Where(m => m.DepartmentID == departmentID).ToList();
            }
            catch
            {
                return new List<vwEmployeeList>();
            }
        }

        public Employee GetEmployeeByID(int userID)
        {
            try
            {
                return _unitOfWork.Repository.GetByID(userID);
            }
            catch
            {
                return new Employee();
            }

        }

        public int Add(Employee employee)
        {
            _unitOfWork.Repository.Insert(employee);
            if (_unitOfWork.Save() > 0 )
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_ADD + " Employee", "", employeeID: employee.EmployeeID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_ADD + " Employee", "", employeeID: employee.EmployeeID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

        public int Update(Employee employee)
        {
            _unitOfWork.Repository.Update(employee);
            if (_unitOfWork.Save() > 0)
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_EDIT + " Employee", "", employeeID: employee.EmployeeID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_EDIT + " Employee", "", employeeID: employee.EmployeeID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

    }
}
