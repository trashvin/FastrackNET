using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Repository.Repositories;
using Repository.UnitsOfWork;
using Common;


namespace FASTProcess
{
    public class BulkUploadProcess : GenericProcess<BulkUpload>
    {
        GenericUnitOfWork<BulkUpload> _unitOfWork =
            new GenericUnitOfWork<BulkUpload>();

        GenericProcess<BulkUpload> _bulkProcess = new GenericProcess<BulkUpload>();

        ConfigurationProcess _configProcess = new ConfigurationProcess();

        private static readonly ILog _tracer = LogManager.GetLogger(typeof(UserProcess));

        private FastEmailConfiguration _emailConfig = new FastEmailConfiguration();

        public BulkUpload GetCurrentUpload(string fileName , int employeeID)
        {
            return _bulkProcess.GetAll().Where(i => ((String.Compare(i.FilePath, fileName) == 0) && (i.EmployeeID == employeeID))).FirstOrDefault();
        }

        public void UpdateProcessingStep(BulkUpload currentUpload, int status)
        {
            _bulkProcess.UserID = this.UserID;
            currentUpload.Status = status;
            if ( _bulkProcess.Update(currentUpload) == FASTConstant.RETURN_VAL_SUCCESS)
            {
                _tracer.Info(String.Format("Updating bulk process state to {0} successful.",status));
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_BULK + "Fix Asset", "Status Update : " + status.ToString() , employeeID: UserID);
            }
            else
            {
                _tracer.Info(String.Format("Updating bulk process state to {0} failed.", status));
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_BULK + "Fix Asset", "Status Update : " + status.ToString(), employeeID: UserID);
            }
        }


        public string GenerateUploadinformationHTML(BulkUpload upload)
        {
            StringBuilder temp = new StringBuilder();

            temp.AppendLine(String.Format("<p> Sender Employee ID  : {0}</p>", upload.EmployeeID.ToString()));
            temp.AppendLine(String.Format("<p> Request Date        : {0}</p>", DateTime.Now.ToLongDateString()));

            return temp.ToString();
        }

        public string GenerateSummaryinformationHTML(BulkUpload upload)
        {
            StringBuilder temp = new StringBuilder();

            int totalFailed = (int) upload.TotalRecords - (int) upload.TotalInserts;

            temp.AppendLine(String.Format("<p> Total Records  : {0}</p>", upload.TotalRecords.ToString()));
            temp.AppendLine(String.Format("<p> Total Inserts  : {0}</p>", upload.TotalInserts.ToString()));
            temp.AppendLine(String.Format("<p> Total Failure  : {0}</p>", totalFailed.ToString()));


            return temp.ToString();
        }

        

    }
}
