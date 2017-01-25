using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.UnitsOfWork;

namespace FASTProcess
{
    public class AccessRightsProcess : GenericProcess<AccessRight>
    {
        GenericUnitOfWork<AccessRight> _unitOfWork =
           new GenericUnitOfWork<AccessRight>();

        public IQueryable<AccessRight> GetAccessRightsByEmloyeeID(int employeeID)
        {
            return GetAll().Where(i => i.EmployeeID == employeeID).AsQueryable();
        }

        public bool HasAccess(int employeeID, int accessLevel )
        {
            if ( GetAccessRightsByEmloyeeID(employeeID).Where(i => i.AccessLevel == accessLevel).Count() > 0)
            {
                return true;
            }

            return false;
        }

       
        public int AddNewRight(AccessRight right)
        {
            int existingRight = GetAccessRightsByEmloyeeID(right.EmployeeID)
                .Where(i => ((i.AccessLevel == right.AccessLevel) && (i.DepartmentID == right.DepartmentID))).Count();

            if ( existingRight == 0)
            {
                _unitOfWork.Repository.Insert(right);
                if (_unitOfWork.Save() > 0)
                {
                    _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_ADD + "Access Right", 
                        "Add access level " + right.AccessLevel.ToString() + " to department " + right.DepartmentID.ToString() + " for " 
                        + right.EmployeeID.ToString(), employeeID: UserID);
                    return FASTConstant.RETURN_VAL_SUCCESS;
                }
                else
                {
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_ADD + "Access Right", "", employeeID: UserID);
                    return FASTConstant.RETURN_VAL_FAILED;
                }

            }


            return FASTConstant.RETURN_VAL_FAILED;
            
        }

    }
}
