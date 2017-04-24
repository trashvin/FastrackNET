using Common;
using FASTProcess;
using Provider.Document;
using Provider.ExtensionMethod;
using FASTWeb.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System;
using log4net;
using log4net.Config;
using Provider.Email;

namespace FASTWeb.Controllers
{
    public class FixAssetController : Controller
    {

        private static readonly ILog tracer = LogManager.GetLogger(typeof(FixAssetController));
        //
        // GET: /FixAsset/
        [Authorize]
        public ActionResult Index()
        {

            GenericProcess<vwFixAssetList> assetProcess = new GenericProcess<vwFixAssetList>();
            GenericProcess<AssetStatu> assetStatusProcess = new GenericProcess<AssetStatu>();

            List<vwFixAssetList> assetList = assetProcess.GetAll().ToList();
            ViewBag.AssetStatus = assetStatusProcess.GetAll().ToList();

            TempData["Assets"] = assetList;

            return View(assetList);
        }

        [Authorize]
        public ActionResult NewAsset()
        {
            GenericProcess<Location> locationProcess = new GenericProcess<Location>();
            GenericProcess<Issuer> issuerProcess = new GenericProcess<Issuer>();
            GenericProcess<AssetType> typeProcess = new GenericProcess<AssetType>();
            GenericProcess<AssetStatu> statusProcess = new GenericProcess<AssetStatu>();
            GenericProcess<AssetClass> classProcess = new GenericProcess<AssetClass>();


            ViewBag.Location = locationProcess.GetAll().ToList();
            ViewBag.Issuer = issuerProcess.GetAll().ToList();
            ViewBag.AssetType = typeProcess.GetAll().ToList();
            ViewBag.AssetStatus = statusProcess.GetAll().ToList();
            ViewBag.AssetClass = classProcess.GetAll().ToList();
            return View(new FixAssetViewModel());

            
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveNewAsset(FixAssetViewModel assetModel)
        {
            FixAsset newAsset = new FixAsset();

            newAsset.AssetTag = assetModel.AssetTag;
            newAsset.FixAssetID = assetModel.FixAssetID;
            newAsset.SerialNumber = assetModel.SerialNumber;
            newAsset.Model = assetModel.Model;
            newAsset.Brand = assetModel.Brand;
            newAsset.AssetClassID = assetModel.AssetClassID;
            newAsset.AssetStatusID = assetModel.AssetStatusID;
            newAsset.AssetTypeID = assetModel.AssetTypeID;
            newAsset.IssuerID = assetModel.IssuerID;
            newAsset.LocationID = assetModel.LocationID;
            newAsset.Remarks = assetModel.Remarks;
            
            if (assetModel.AcquisitionDate != null)
            {
                newAsset.AcquisitionDate = assetModel.AcquisitionDate;
            }
            if (assetModel.ExpiryDate != null)
            {
                newAsset.ExpiryDate = assetModel.ExpiryDate;
            }

            AssetProcess assetProcess = new AssetProcess();

            try
            {
                if (assetProcess.Add(newAsset) == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                    TempData[FASTConstant.TMPDATA_SOURCE] = "Add New Fix Asset";
                    TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "The new assset has been added to the database.";
                    TempData[FASTConstant.TMPDATA_ACTION] = "Index";
                    TempData[FASTConstant.TMPDATA_CONTROLLER] = "FixAsset";

                    return View("~/Views/Shared/Result.cshtml");
                }
                else
                {
                    throw new Exception("There was an error while adding the new employee.");
                }
            }
            catch(Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Add New Fix Asset";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }
           
        }
        

        [Authorize]
        public ActionResult EditAsset(string tag)
        {
            AssetProcess assetProcess = new AssetProcess();
            GenericProcess<Location> locationProcess = new GenericProcess<Location>();
            GenericProcess<Issuer> issuerProcess = new GenericProcess<Issuer>();
            GenericProcess<AssetType> typeProcess = new GenericProcess<AssetType>();
            GenericProcess<AssetStatu> statusProcess = new GenericProcess<AssetStatu>();
            GenericProcess<AssetClass> classProcess = new GenericProcess<AssetClass>();


            string assetTag = tag;

            vwFixAssetList assetToEdit = assetProcess.GetFixAssetByAssetTag(assetTag);

            FixAssetViewModel assetView = new FixAssetViewModel();

            assetView.AssetTag = assetToEdit.AssetTag;
            assetView.FixAssetID = assetToEdit.FixAssetID;
            assetView.SerialNumber = assetToEdit.SerialNumber;
            assetView.Model = assetToEdit.Model;
            assetView.Brand = assetToEdit.Brand;
            assetView.AssetClassID = assetToEdit.AssetClassID;
            assetView.AssetStatusID = assetToEdit.AssetStatusID;
            assetView.AssetTypeID = assetToEdit.AssetTypeID;
            assetView.IssuerID = assetToEdit.IssuerID;
            assetView.LocationID = assetToEdit.LocationID;
            assetView.Remarks = assetToEdit.Remarks;

            if( assetToEdit.AcquisitionDate != null)
            {
                assetView.AcquisitionDate = assetToEdit.AcquisitionDate;
            }
          

            if ( assetToEdit.ExpiryDate != null )
            {
                assetView.ExpiryDate = assetToEdit.ExpiryDate;
            }

            ViewBag.Location = locationProcess.GetAll().ToList();
            ViewBag.Issuer = issuerProcess.GetAll().ToList();
            ViewBag.AssetType = typeProcess.GetAll().ToList();
            ViewBag.AssetStatus = statusProcess.GetAll().ToList();
            ViewBag.AssetClass = classProcess.GetAll().ToList();

            return View(assetView);

        }
        
        [Authorize]
        [HttpPost]
        public ActionResult EditAsset(FixAssetViewModel assetModel)
        {
            FixAsset assetToEdit = new FixAsset();

            assetToEdit.AssetTag = assetModel.AssetTag;
            assetToEdit.FixAssetID = assetModel.FixAssetID;
            assetToEdit.SerialNumber = assetModel.SerialNumber;
            assetToEdit.Model = assetModel.Model;
            assetToEdit.Brand = assetModel.Brand;
            assetToEdit.AssetClassID = assetModel.AssetClassID;
            assetToEdit.AssetStatusID = assetModel.AssetStatusID;
            assetToEdit.AssetTypeID = assetModel.AssetTypeID;
            assetToEdit.IssuerID = assetModel.IssuerID;
            assetToEdit.LocationID = assetModel.LocationID;
            assetToEdit.Remarks = assetModel.Remarks;

            if ( assetModel.AcquisitionDate != null )
            {
                assetToEdit.AcquisitionDate = assetModel.AcquisitionDate;
            }

            if ( assetModel.ExpiryDate != null )
            {
                assetToEdit.ExpiryDate = assetModel.ExpiryDate;
            }


            AssetProcess assetProcess = new AssetProcess();

            try
            {

                if (assetProcess.Update(assetToEdit) == FASTConstant.RETURN_VAL_SUCCESS)
                {

                    TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                    TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Fix Asset";
                    TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "The asset has been successfully modified.";
                    TempData[FASTConstant.TMPDATA_ACTION] = "Index";
                    TempData[FASTConstant.TMPDATA_CONTROLLER] = "FixAsset";

                    return View("~/Views/Shared/Result.cshtml");
                }
                else
                {
                    throw new Exception("There was an error while adding the new employee.");
                }

            }
            catch(Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Fix Asset";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(string assetTag ="", string serialNumber ="", int statusID =-1)
        {
            //FixAssetManagementProcess fixAssetManagement = new FixAssetManagementProcess();
            //BaseProcess baseProc = new BaseProcess();
            //List<FixAssetModel> assets = fixAssetManagement.Search(assetTag, serialNumber, statusID);

            //ViewBag.AssetStatus = baseProc.GetAllAssetStatus();

            AssetProcess assetProcess = new AssetProcess();

            List<vwFixAssetList> resultList = assetProcess.SearchAsset(anAssetTag: assetTag, aSerialNumber: serialNumber, aStatusID: statusID).ToList();
            GenericProcess<AssetStatu> statusProcess = new GenericProcess<AssetStatu>();

            ViewBag.AssetStatus = statusProcess.GetAll().ToList();

            TempData["Assets"] = resultList;

            return View("Index", resultList);

            
        }

        [Authorize]
        [HttpPost]
        public ActionResult TrackAsset(string assetTag)
        {
            return View();
        }

        [Authorize]
        
        public ActionResult PrintResult()
        {
            List<vwFixAssetList> list = (List<vwFixAssetList>) TempData["Assets"];
            return View();
        }

        [Authorize]
        public ActionResult UploadAssets()
        {
            return View();
        }

        [Authorize]
        public FileResult GetTemplate()
        {
            string path = HttpContext.Server.MapPath("\\App_Data\\Templates\\FixAssetTemplate.xlsx");
            byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
            string fileName = "FixAssetTemplate.xlsx";
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
            List<FixAssetExcelUploadModel> assets = new List<FixAssetExcelUploadModel>();
            BulkUpload currentUpload = new Common.BulkUpload();
            BulkUploadProcess bulkProcess = new BulkUploadProcess();
            AssetProcess assetProcess = new AssetProcess();
            EmployeeProcess employeeProcess = new EmployeeProcess();
            Employee sender = new Employee();
            ConfigurationProcess configProcess = new ConfigurationProcess();

            FastEmailConfiguration emailConfig = configProcess.GetEmailConfiguration();

            StringBuilder uploadLog = new StringBuilder();
            
            string filename = string.Empty;
            string completeFileName = string.Empty;

            //This is for the logging
            bulkProcess.UserID = User.Identity.Name.ToInteger();
            configProcess.UserID = User.Identity.Name.ToInteger();
            employeeProcess.UserID = User.Identity.Name.ToInteger();
            assetProcess.UserID = User.Identity.Name.ToInteger();
            

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
                    completeFileName = Path.Combine(path,filename);
                    Request.Files[upload].SaveAs(completeFileName);
                }
                #endregion

                BulkUpload newFile = new BulkUpload() 
                { 
                    EmployeeID = User.Identity.Name.ToInteger(), 
                    FilePath = filename, 
                    TotalRecords = 0, 
                    TotalInserts = 0 , 
                    RequestDate = DateTime.Now,
                    Type = FASTConstant.BULKIPLOAD_TYPE_ASSET
                };

                if ( bulkProcess.Add(newFile) == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    //get the current upload
                    currentUpload = bulkProcess.GetCurrentUpload(newFile.FilePath, newFile.EmployeeID);
                }

                if ( currentUpload != null )
                {
                    #region Process the excel file
                    //Success! Lets process the file.
                    System.Data.DataTable fixAssetTable = new DataTable();

                    fixAssetTable = excelHelp.GetExcelDataTable(completeFileName, "SELECT * FROM [FixAsset$]");
                    fixAssetTable = excelHelp.ConvertToFixAssetTable(fixAssetTable);

                    if ( fixAssetTable == null )
                    {
                        throw new Exception("The upload file contains null data.");
                    }

                    assets = fixAssetTable.ToList<Models.FixAssetExcelUploadModel>();
                    sender = employeeProcess.GetEmployeeByID(currentUpload.EmployeeID);

                    if (assets.Count > 0)
                    {
                        int totalInserts = 0;
                        currentUpload.TotalRecords = assets.Count;

                        bulkProcess.UpdateProcessingStep(currentUpload, FASTConstant.BULKUPLOAD_STATUS_INPROGRESS);

                        foreach( FixAssetExcelUploadModel asset in assets)
                        {
                            FixAsset tempAsset = new FixAsset()
                            {
                                AssetTag = asset.AssetTag,
                                SerialNumber = asset.SerialNumber,
                                Model = asset.Model,
                                Brand = asset.Brand,
                                AssetClassID = asset.AssetClassID,
                                AssetTypeID = asset.AssetTypeID,
                                AssetStatusID = asset.AssetStatusID,
                                AcquisitionDate = asset.AcquisitionDate
                                
                            };


                            if ( assetProcess.Add(tempAsset) == FASTConstant.RETURN_VAL_SUCCESS )
                            {
                                totalInserts++;
                                uploadLog.AppendLine(String.Format("<p>{0} : {1} inserted.</p>", FASTConstant.SUCCESSFUL, asset.AssetTag));
                            }
                            else
                            {
                                uploadLog.AppendLine(String.Format("<p>{0} : {1} not inserted.</p>", FASTConstant.FAILURE, asset.AssetTag));
                            }
                        }

                        currentUpload.TotalInserts = totalInserts;
                        bulkProcess.UpdateProcessingStep(currentUpload, FASTConstant.BULKUPLOAD_STATUS_DONE);


                        //Send email to the requestor
                        FastEmail email = new FastEmail();
                        email.Receipients = new List<string>() { sender.EmailAddress };
                        email.Subject = FASTConstant.EMAIL_SIMPLE_SUBJECT.Replace("[XXX]", "Fix Asset Bulk Upload Result");
                        email.HTMLBody =FASTProcess.Helper.EmailHelper.GenerateHTMLBody(FASTProcess.Helper.EmailHelper.EmailType.BULK_UPLOAD);

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

            List<BulkUpload> uploads = bulkProcess.GetAll()
                                            .Where(i => i.EmployeeID == bulkProcess.UserID 
                                                    && i.Type == Common.FASTConstant.BULKIPLOAD_TYPE_ASSET).ToList();

            return View(uploads);
        }
	}
}