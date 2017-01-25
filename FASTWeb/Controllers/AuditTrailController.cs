using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using FASTProcess;

namespace FASTWeb.Controllers
{
    public class AuditTrailController : Controller
    {
        //
        // GET: /AuditTrail/
        [Authorize]
        public ActionResult Index()
        {
            GenericProcess<AuditTrail> auditProcess = new GenericProcess<AuditTrail>();
            List<AuditTrail> trails = new List<AuditTrail>();

            trails = auditProcess.GetAll().OrderByDescending(i => i.Date).ToList();

            return View(trails);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Search(string startDate, string employeeID, string assetTag, string assignmentID)
        {
            TempData["StartDate"] = startDate;
            TempData["EmployeeID"] = employeeID;
            TempData["AssetTag"] = assetTag;
            TempData["AssignmentID"] = assignmentID;

            GenericProcess<AuditTrail> auditProcess = new GenericProcess<AuditTrail>();
            IQueryable<AuditTrail> results;

            if ( !string.IsNullOrEmpty(startDate))
            {
                DateTime date = DateTime.Now;
                DateTime.TryParse(startDate,out date);

                results = auditProcess.GetAll().Where(i => i.Date >= date);
            }
            else
            {
                results = auditProcess.GetAll();
            }


            if ( !string.IsNullOrEmpty(employeeID))
            {
                results = results.Where(i => i.EmployeeID.ToString().Contains(employeeID) == true);
            }

            if ( !string.IsNullOrEmpty(assignmentID))
            {
                results = results.Where(i => i.AssignmentID.Contains(assignmentID) == true);
            }

            if ( !string.IsNullOrEmpty(assetTag))
            {
                results = results.Where(i => i.AssetTag.Contains(assetTag) == true);
            }

            results = results.OrderByDescending(i => i.Date);


            if (Request["Search"] != null)
            {
                
                return View("Index", results.ToList());
            }
            else
            {
                return View("AuditReport", results.ToList());
            }


           
        }
    }

       
}