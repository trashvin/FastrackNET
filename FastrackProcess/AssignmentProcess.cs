using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.UnitsOfWork;
using Repository.Repositories;
using Repository;
using Common;
using log4net;
using log4net.Config;

namespace FASTProcess
{
    public class AssignmentProcess : GenericProcess<AssetAssignment>
    {
        GenericUnitOfWork<vwAssetAssignment> _assignments =
            new GenericUnitOfWork<vwAssetAssignment>();
        GenericUnitOfWork<vwAssetAssignmentsForCustodian> _custodianAssignments =
            new GenericUnitOfWork<vwAssetAssignmentsForCustodian>();
        GenericUnitOfWork<vwAssetAssignmentsForManager> _managerAssignments =
            new GenericUnitOfWork<vwAssetAssignmentsForManager>();
        GenericUnitOfWork<vwAssetAssignmentsForMI> _misAssignments =
            new GenericUnitOfWork<vwAssetAssignmentsForMI>();
        GenericProcess<vwFixAssetList> _fixAssetView =
            new GenericProcess<vwFixAssetList>();

        AssetAssignmentUnitOfWork<AssetAssignment, FixAsset> _assetAssignment =
            new AssetAssignmentUnitOfWork<AssetAssignment, FixAsset>();

        private static readonly ILog tracer = LogManager.GetLogger(typeof(AssignmentProcess));

      
        public List<vwAssetAssignment> GetCurrentAssignmentsByEmployeeID(int employeeID)
        {
            tracer.Info("GetCurrentAssignmentsByEmployeeID :" + employeeID.ToString());

            List<vwAssetAssignment> assignments = new List<vwAssetAssignment>();

            try
            {
                assignments = _assignments.Repository.GetAllQueryable()
                                 .Where(i => i.EmployeeID == employeeID)
                                 .Where(j => (j.AssignmentStatusID == FASTConstant.ASSIGNMENT_STATUS_ACCEPTED
                                     && j.AssetStatusID == FASTConstant.ASSET_STATUS_ASSIGNED)).ToList();

                return assignments;
            }
            catch(Exception ex)
            {
                tracer.Error(ex.ToString());

                return assignments;
            }
        }

        public List<vwAssetAssignment> GetAssignmentsForAcceptanceByEmployeeID(int employeeID)
        {
            tracer.Info("GetCurrentAssignmentsByEmployeeID :" + employeeID.ToString());

            List<vwAssetAssignment> assignments = new List<vwAssetAssignment>();

            try
            {
                assignments = _assignments.Repository.GetAllQueryable()
                                 .Where(i => i.EmployeeID == employeeID)
                                 .Where(j => (j.AssignmentStatusID == FASTConstant.ASSIGNMENT_STATUS_WT_ACCEPTANCE
                                     && j.AssetStatusID == FASTConstant.ASSET_STATUS_ASSIGNED)).ToList();

                return assignments;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return assignments;
            }
        }

        public List<vwAssetAssignment> GetAllAssignmentsByEmployeeID(int employeeID)
        {
            tracer.Info("GetAllAssignmentsByEmployeeID :" + employeeID.ToString());

            List<vwAssetAssignment> assignments = new List<vwAssetAssignment>();

            try
            {
                assignments = _assignments.Repository.GetAllQueryable()
                                 .Where(i => i.EmployeeID == employeeID).ToList();

                return assignments;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return assignments;
            }
        }

        public List<vwAssetAssignmentsForManager> GetAssignmentsForManagerApprovalByDepartmentID(int departmentID, int employeeID)
        {
            tracer.Info("GetAssignmentsForManagerApprovalByDepartmentID :" + departmentID.ToString());

            List<vwAssetAssignmentsForManager> approvals = new List<vwAssetAssignmentsForManager>();

            try
            {
                approvals = _managerAssignments.Repository.GetAllQueryable()
                                 .Where(i => (i.DepartmentID == departmentID && i.EmployeeID == employeeID))
                                 .ToList();

                return approvals;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return approvals;
            }
        }

        public List<vwAssetAssignmentsForCustodian> GetAssignmentsForAdminAcceptanceByDepartmentID(int departmentID , int employeeID)
        {
            tracer.Info("GetAssignmentsForAdminApprovalByDepartmentID : " + departmentID.ToString());

            List<vwAssetAssignmentsForCustodian> acceptances = new List<vwAssetAssignmentsForCustodian>();

            try
            {
                acceptances = _custodianAssignments.Repository.GetAllQueryable()
                    .Where(i => (i.DepartmentID == departmentID && i.EmployeeID == employeeID)).ToList();

                return acceptances;
            }
            catch(Exception ex)
            {
                tracer.Error(ex.ToString());

                return acceptances;
            }

        }

        public IQueryable<vwAssetAssignmentsForMI> GetAssignmentsForMISByMISEmployeeID(int employeeID)
        {
            tracer.Info("GetAssignmentsForMISByMISEmployeeID");

            IQueryable<vwAssetAssignmentsForMI> assignments;

            try
            {
                assignments = _misAssignments.Repository.GetAllQueryable().Where(i => i.MISEmployeeID == employeeID);

                return assignments;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return null;
            }

        }

        public List<vwFixAssetList> GetAssignmentsForReleases()
        {
            tracer.Info("GetAssignmentsForReleases");

            List<vwFixAssetList> releases = new List<vwFixAssetList>();

            try
            {
                releases = _fixAssetView.GetAll().Where(i => i.AssetStatusID == FASTConstant.ASSET_STATUS_FORRELEASE).ToList();

                return releases;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return releases;
            }

        }

        public List<vwFixAssetList> GetAssignmentsForRepairs()
        {
            tracer.Info("GetAssignmentsForRepairs");

            List<vwFixAssetList> repairs = new List<vwFixAssetList>();

            try
            {
                repairs = _fixAssetView.GetAll().Where(i => i.AssetStatusID == FASTConstant.ASSET_STATUS_FORREPAIR).ToList();

                return repairs;
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());

                return repairs;
            }

        }
        
        


    }
}
