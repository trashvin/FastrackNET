using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FASTWeb.Models;
using System.Text;
using System.IO;
using System.Data;

using FASTProcess;
using Provider.ExtensionMethod;
using Provider.Email;
using Provider.Document;
using Common;

namespace FASTWeb.Controllers
{
    public class AssignmentController : Controller
    {
        //
        private enum ACTION{
            ACCEPTREJECT = 0,
            APPROVEDENY =1,
            TRANSFERRELEASE = 2
        };

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ManageAssignment(int assignID)
        {
            Models.ManageAssignmentViewModel model = new Models.ManageAssignmentViewModel();
            model.FillData(assignID);

            if (model.AssignmentStatusID == 3)
            {
                ViewBag.Action = 2;
            }
            else if ( model.AssignmentStatusID == 1)
            {
                ViewBag.Action = 1;
            }
            else
            {
                ViewBag.Action = 0;
            }

      
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ManageAssignment(Models.ManageAssignmentViewModel model)
        {

            return RedirectToAction("Unavailable", "Home");
        }

        [Authorize]
        public ActionResult UploadAssignments()
        {
            return View();
        }

        [Authorize]
        public FileResult GetTemplate()
        {
            //TODO: Make the file name configurable so that it can be versioned
            string path = HttpContext.Server.MapPath("\\App_Data\\Templates\\AssetAssignmentTemplate.xlsx");
            byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
            string fileName = "AssetAssignmentTemplate.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        [Authorize]
        public ActionResult BulkUpload()
        {
            return View();
        }
        
        
        [Authorize]
        public ActionResult UploadFile()
        {
            ExcelHelper excelHelp = new ExcelHelper();
            List<AssetAssignmentUploadModel> assignments = new List<AssetAssignmentUploadModel>();
            BulkUpload currentUpload = new Common.BulkUpload();
            BulkUploadProcess bulkProcess = new BulkUploadProcess();
            AssetProcess assetProcess = new AssetProcess();
            EmployeeProcess employeeProcess = new EmployeeProcess();
            Employee sender = new Employee();
            ConfigurationProcess configProcess = new ConfigurationProcess();
            FastEmailConfiguration emailConfig = configProcess.GetEmailConfiguration();
            AssignmentProcess assignProcess = new AssignmentProcess();

            StringBuilder uploadLog = new StringBuilder();

            string filename = string.Empty;
            string completeFileName = string.Empty;

            //This is for the logging
            bulkProcess.UserID = User.Identity.Name.ToInteger();
            configProcess.UserID = User.Identity.Name.ToInteger();
            employeeProcess.UserID = User.Identity.Name.ToInteger();
            assetProcess.UserID = User.Identity.Name.ToInteger();
            assignProcess.UserID = User.Identity.Name.ToInteger();

            try
            {
                #region Get Request Files
                foreach (string upload in Request.Files)
                {

                    if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;
                    string path = HttpContext.Server.MapPath("\\App_Data\\BulkUploads");
                    filename = Path.GetFileName(Request.Files[upload].FileName);

                    //check the filename and ensure its an xlsx file
                    if (String.Compare(filename.Substring(filename.Length - 4), "xlsx", true) != 0)
                    {
                        throw new Exception("Invalid file extension.");
                    }

                    //add the current time as unique indicator
                    filename = DateTime.Now.ToFileTime().ToString() + "_" + filename;

                    // If Upload folder is not yet existing, this code will create that directory.
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    completeFileName = Path.Combine(path, filename);
                    Request.Files[upload].SaveAs(completeFileName);
                }
                #endregion

                BulkUpload newFile = new BulkUpload() 
                { 
                    EmployeeID = User.Identity.Name.ToInteger(), 
                    FilePath = filename, 
                    TotalRecords = 0, 
                    TotalInserts = 0, 
                    RequestDate = DateTime.Now ,
                    Type = FASTConstant.BULKIPLOAD_TYPE_ASSIGNMENT
                };

                if (bulkProcess.Add(newFile) == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    //get the current upload
                    currentUpload = bulkProcess.GetCurrentUpload(newFile.FilePath, newFile.EmployeeID);
                }

                if (currentUpload != null)
                {
                    #region Process the excel file
                    //Success! Lets process the file.
                    System.Data.DataTable assignmentTable = new DataTable();

                    assignmentTable  = excelHelp.GetExcelDataTable(completeFileName, "SELECT * FROM [AssetAssignment$]");
                   
                    if (assignmentTable == null)
                    {
                        throw new Exception("The upload file contains null data.");
                    }

                    assignments = assignmentTable.ToList<Models.AssetAssignmentUploadModel>();
                    sender = employeeProcess.GetEmployeeByID(currentUpload.EmployeeID);

                    if (assignments.Count > 0)
                    {
                        int totalInserts = 0;
                        currentUpload.TotalRecords = assignments.Count;

                        bulkProcess.UpdateProcessingStep(currentUpload, FASTConstant.BULKUPLOAD_STATUS_INPROGRESS);

                        foreach (AssetAssignmentUploadModel assign in assignments)
                        {
                            //Get the Fix Asset to be added
                            Common.vwFixAssetList newAssignedAsset = assetProcess.GetFixAssetByAssetTag(assign.AssetTag);
                         
                            AssetAssignment tempAssignment = new AssetAssignment()
                            {
                                EmployeeID = assign.EmployeeID,
                                AssignmentStatusID = FASTConstant.ASSIGNMENT_STATUS_WT_ACCEPTANCE,
                                DateAssigned = DateTime.Now,
                                FixAssetID = newAssignedAsset.FixAssetID
                            };

                            if ( String.Compare(newAssignedAsset.SerialNumber, assign.SerialNumber,true) == 0)
                            {
                                //totalInserts++;
                                //Try Assigning the asset to the Employee


                                //Add delay to allow sending of email
                                System.Threading.Thread.Sleep(2000);
                                uploadLog.AppendLine(String.Format("<p>{0} : {1} assigned to {2}.</p>",
                                                        FASTConstant.SUCCESSFUL, assign.AssetTag, assign.EmployeeID.ToString()));
                            }
                            else
                            {
                                uploadLog.AppendLine(String.Format("<p>{0} : {1} not assigned to {2}. Serial Number Mismatch.</p>", 
                                                        FASTConstant.FAILURE, assign.AssetTag,assign.EmployeeID.ToString()));
                            }
                        }

                        currentUpload.TotalInserts = totalInserts;
                        bulkProcess.UpdateProcessingStep(currentUpload, FASTConstant.BULKUPLOAD_STATUS_DONE);


                        //Send email to the requestor
                        FastEmail email = new FastEmail();
                        email.Receipients = new List<string>() { sender.EmailAddress };
                        email.Subject = FASTConstant.EMAIL_SIMPLE_SUBJECT.Replace("[XXX]", "Fix Asset Bulk Upload Result");
                        email.HTMLBody = FASTProcess.Helper.EmailHelper.GenerateHTMLBody(FASTProcess.Helper.EmailHelper.EmailType.BULK_UPLOAD);

                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_RECEIPIENT_NAME, sender.FirstName + " " + sender.LastName);
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_BULKUPLOAD_INFO, bulkProcess.GenerateUploadinformationHTML(currentUpload));
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_BULKUPLOAD_LOG, uploadLog.ToString());
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_BULKUPLOAD_SUMMARY, bulkProcess.GenerateSummaryinformationHTML(currentUpload));

                        SMTPEmail emailSender = new SMTPEmail(emailConfig, email);
                        emailSender.SendEmail();

                    }
                    else
                    {
                        bulkProcess.UpdateProcessingStep(currentUpload, FASTConstant.BULKUPLOAD_STATUS_DONE);
                    }
                    #endregion
                }

                TempData["Result"] = "SUCCESSFUL";
                TempData["Source"] = "File Upload";
                TempData["ExtraMessage"] = "An email will be sent to you containing the results of the upload process.";
                TempData["Action"] = "Index";
                TempData["Controller"] = "FixAsset";

                return View("~/Views/Shared/Result.cshtml");

            }
            catch (Exception ex)
            {
                TempData["Result"] = "FAILURE";
                TempData["Source"] = "Fix Asset Bulk Upload";
                TempData["ExtraMessage"] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }
        }
        
        
        [Authorize]
        public ActionResult BulkUploadHistory()
        {
            BulkUploadProcess bulkProcess = new BulkUploadProcess();
            bulkProcess.UserID = User.Identity.Name.ToInteger();

            List<BulkUpload> uploads = bulkProcess.GetAll().Where(i => i.EmployeeID == bulkProcess.UserID).ToList();

            return View(uploads);
        }
        

	}
}