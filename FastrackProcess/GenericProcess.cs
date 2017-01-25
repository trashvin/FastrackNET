using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Repositories;
using Repository.UnitsOfWork;
using Common;
using FASTProcess;
using Provider.Cryptography;

namespace FASTProcess
{
    public class GenericProcess<T> where T:class
    {
        GenericUnitOfWork<T> _unitOfWork =
            new GenericUnitOfWork<T>();

        public int UserID { get; set; }

        public virtual int Add(T data)
        {
            _unitOfWork.Repository.Insert(data);
            if (_unitOfWork.Save() > 0)
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_ADD + typeof(T).Name, "", employeeID: UserID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_ADD + typeof(T).Name, "", employeeID: UserID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

        public virtual int Update(T data)
        {
            _unitOfWork.Repository.Update(data);
            try
            {
                if (_unitOfWork.Save() > 0)
                {
                    _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_EDIT + typeof(T).Name, "", employeeID: UserID);
                    return FASTConstant.RETURN_VAL_SUCCESS;
                }
                else
                {
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_EDIT + typeof(T).Name, "", employeeID: UserID);
                    return FASTConstant.RETURN_VAL_FAILED;
                }
            }
            catch (Exception ex) 
            {
                Console.Write(ex.ToString());
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

        public IQueryable<T> GetAll()
        {
            return _unitOfWork.Repository.GetAllQueryable();
        }

        public T GetByID(object id)
        {
            return _unitOfWork.Repository.GetByID(id);
        }

        public virtual int Delete(object id)
        {
            T deleted =_unitOfWork.Repository.Delete(id);

            if ( deleted != null )
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_DELETE + typeof(T).Name, "", employeeID: UserID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_DELETE + typeof(T).Name, "", employeeID: UserID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }




    }
}
