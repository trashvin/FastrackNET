using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Provider.Email;
using FASTProcess;
using Common;
using Provider.ExtensionMethod;

namespace FASTWeb.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ConfigurationProcess process = new ConfigurationProcess();
            FastEmailConfiguration configuration = process.GetEmailConfiguration();

            //Lets store the config in a session
            Session[FASTConstant.SESSION_EMAIL_CONFIG] = configuration;
            
            if (Request.IsAuthenticated)
            {
                //If authenticated, should go to the Dashboard. For now, lets assume that the Dashboard is the report page
                return RedirectToAction("MyAssets", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Authenticate");
            }
            
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Unavailable()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DownloadPage()
        {
            return View();
        }

        [Authorize]
        public ActionResult AppAdmin()
        {
            return View();
        }

        [Authorize]
        public ActionResult Custodian()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            GenericProcess<vwFixAssetList> fixAssetProcess = new GenericProcess<vwFixAssetList>();

            List<vwAssetAssignmentsForCustodian> acceptances = new List<vwAssetAssignmentsForCustodian>();
            List<vwFixAssetList> releases = new List<vwFixAssetList>();
            List<vwFixAssetList> repairs = new List<vwFixAssetList>();

            List<AccessRight> accessRights = new List<AccessRight>();

            accessRights = accessProcess.GetAccessRightsByEmloyeeID(User.Identity.Name.ToInteger()).ToList();


            foreach (AccessRight right in accessRights)
            {
                acceptances.AddRange(assignProcess.GetAssignmentsForAdminAcceptanceByDepartmentID(right.DepartmentID, User.Identity.Name.ToInteger()));
            }


            repairs = assignProcess.GetAssignmentsForRepairs();
            releases = assignProcess.GetAssignmentsForReleases();

            ViewBag.Acceptances = acceptances;
            ViewBag.Repairs = repairs;
            ViewBag.Releases = releases;

            return View();

        }

        [Authorize]
        public ActionResult MyAssets()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();
            EmployeeProcess employeeProcess = new EmployeeProcess();

            ViewBag.Assignments = assignProcess.GetCurrentAssignmentsByEmployeeID(User.Identity.Name.ToInteger());
            ViewBag.Acceptances = assignProcess.GetAssignmentsForAcceptanceByEmployeeID(User.Identity.Name.ToInteger());
            ViewBag.History = assignProcess.GetAllAssignmentsByEmployeeID(User.Identity.Name.ToInteger());

            return View(employeeProcess.GetEmployeeProfileByID(User.Identity.Name.ToInteger()));
        }

        [Authorize]
        public ActionResult MIS()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();

            assignProcess.UserID = User.Identity.Name.ToInteger();

            List<Common.vwAssetAssignmentsForMI> assignments = new List<vwAssetAssignmentsForMI>();
            List<Common.vwAssetAssignmentsForMI> acceptances = new List<vwAssetAssignmentsForMI>();

            IQueryable<vwAssetAssignmentsForMI> allAssignments = assignProcess.GetAssignmentsForMISByMISEmployeeID(assignProcess.UserID);

            assignments = allAssignments.Where(i => i.AssignmentStatusID == FASTConstant.ASSIGNMENT_STATUS_ACCEPTED).ToList();
            acceptances = allAssignments.Where(i => i.AssignmentStatusID == FASTConstant.ASSIGNMENT_STATUS_WT_ACCEPTANCE).ToList();

            ViewBag.Acceptances = acceptances;
            ViewBag.Assignments = assignments;

            return View();
        }

        [Authorize]
        public ActionResult MyDepartment()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            GenericProcess<vwDepartmentList> deptProcess = new GenericProcess<vwDepartmentList>();

            List<vwDepartmentList> departments = new List<vwDepartmentList>();
            List<AccessRight> accessRights = accessProcess.GetAccessRightsByEmloyeeID(User.Identity.Name.ToInteger()).ToList();

            accessRights = accessProcess.GetAccessRightsByEmloyeeID(User.Identity.Name.ToInteger())
                .Where(i => i.AccessLevel == FASTConstant.ACCESS_LVL_MANAGER).ToList();

            foreach(AccessRight right in accessRights)
            {
                departments.Add(deptProcess.GetAll().Where(i => i.DepartmentID == right.DepartmentID).FirstOrDefault());
            }


            //Default department is the first department the manager is allowed to manage.
            int defaultDepartment = departments.FirstOrDefault().DepartmentID;

            Session[FASTConstant.SESSION_MYDEPARTMENT] = defaultDepartment;

            vwDepartmentList theDepartment = deptProcess.GetAll().Where(i => i.DepartmentID == defaultDepartment).First();

            List<vwAssetAssignmentsForManager> approvals =
                assignProcess.GetAssignmentsForManagerApprovalByDepartmentID(defaultDepartment, User.Identity.Name.ToInteger());

            ViewBag.Approvals = approvals;
            ViewBag.MyDepartments = departments;

            TempData["Department"] = theDepartment;

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult MyDepartment(int departmentID)
        {
            Session[FASTConstant.SESSION_MYDEPARTMENT] = departmentID;

            AssignmentProcess assignProcess = new AssignmentProcess();
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            GenericProcess<vwDepartmentList> deptProcess = new GenericProcess<vwDepartmentList>();

            List<vwDepartmentList> departments = new List<vwDepartmentList>();
            List<AccessRight> accessRights = accessProcess.GetAccessRightsByEmloyeeID(User.Identity.Name.ToInteger()).ToList();

            accessRights = accessProcess.GetAccessRightsByEmloyeeID(User.Identity.Name.ToInteger())
                .Where(i => i.AccessLevel == FASTConstant.ACCESS_LVL_MANAGER).ToList();

            foreach (AccessRight right in accessRights)
            {
                departments.Add(deptProcess.GetAll().Where(i => i.DepartmentID == right.DepartmentID).FirstOrDefault());
            }


            vwDepartmentList theDepartment = deptProcess.GetAll().Where(i => i.DepartmentID == departmentID).First();

            List<vwAssetAssignmentsForManager> approvals =
                assignProcess.GetAssignmentsForManagerApprovalByDepartmentID(departmentID, User.Identity.Name.ToInteger());

            ViewBag.Approvals = approvals;
            ViewBag.MyDepartments = departments;

            TempData["Department"] = theDepartment;

            return View();


        }


      
    }
}