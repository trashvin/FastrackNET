using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using FASTProcess;
using Provider.Cryptography;
using Provider.ExtensionMethod;

namespace FASTWeb.Controllers
{
    public class AccessRightController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            GenericProcess<vwAccessRight> rightProcess =
                new GenericProcess<vwAccessRight>();
            GenericProcess<Department> deptProcess = new GenericProcess<Department>();
            GenericProcess<AccessLevel> accessProcess = new GenericProcess<AccessLevel>();


            List<vwAccessRight> rights = new List<vwAccessRight>();

            ViewBag.AccessModes = accessProcess.GetAll().ToList();
            ViewBag.Departments = deptProcess.GetAll().ToList();


            rights = rightProcess.GetAll().ToList();

            return View(rights);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string firstName = "", string lastName = "", int accessMode = -1, int department = -1)
        {
            GenericProcess<vwAccessRight> rightProcess =
                new GenericProcess<vwAccessRight>();
            GenericProcess<Department> deptProcess = new GenericProcess<Department>();
            GenericProcess<AccessLevel> accessProcess = new GenericProcess<AccessLevel>();


            IQueryable<vwAccessRight> rights;

            ViewBag.AccessModes = accessProcess.GetAll().ToList();
            ViewBag.Departments = deptProcess.GetAll().ToList();

            if (department != -1)
            {
                rights = rightProcess.GetAll().Where(i => i.DepartmentID == department);
            }
            else
            {
                rights = rightProcess.GetAll();
            }

            if (accessMode != -1)
            {
                rights = rights.Where(i => i.AccessLevel == accessMode);
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                rights = rights.Where(i => (i.FullName.Contains(lastName)));
            }

            if (!String.IsNullOrEmpty(firstName))
            {
                rights = rights.Where(i => (i.FullName.Contains(firstName)));
            }


            return View(rights.ToList());
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [Authorize]
        public ActionResult NewAccessRight()
        {
            //EmployeeProcess empProcess = new EmployeeProcess();
            vwEmployeeList employee = new vwEmployeeList();
            
            GenericProcess<AccessLevel> levels = new GenericProcess<AccessLevel>();
            GenericProcess<vwDepartmentList> departments = new GenericProcess<vwDepartmentList>();
            ViewBag.AccessLevel = levels.GetAll().ToList();
            ViewBag.Department = departments.GetAll().ToList();
            ViewBag.EmployeeID = 0;
           
            ViewBag.Rights = new List<vwAccessRight>();

            return View(employee);
        }

        [Authorize]
        
        public ActionResult SearchEmployeeRights(int employeeID)
        {
            EmployeeProcess empProcess = new EmployeeProcess();
            vwEmployeeList employee = empProcess.GetEmployeeProfileByID(employeeID);

            GenericProcess<vwAccessRight> accessProcess = new GenericProcess<vwAccessRight>();
            List<vwAccessRight> rights = accessProcess.GetAll().Where(i => i.EmployeeID == employeeID).ToList();

            GenericProcess<AccessLevel> levels = new GenericProcess<AccessLevel>();
            GenericProcess<vwDepartmentList> departments = new GenericProcess<vwDepartmentList>();
            ViewBag.EmployeeID = employeeID;
            ViewBag.AccessLevel = levels.GetAll().ToList();
            ViewBag.Department = departments.GetAll().ToList();
           


            ViewBag.Rights = rights;

            return View("NewAccessRight", employee);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddAccessRight(int employeeID = 0, int accessID = 0, int departmentID = 0)
        {
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            accessProcess.UserID = User.Identity.Name.ToInteger();

            AccessRight newRight = new AccessRight() { AccessLevel =(short) accessID, DepartmentID = departmentID, EmployeeID = employeeID };

            try
            {
                if (employeeID == 0 || accessID == 0  ||  (departmentID == 0 && accessID != FASTConstant.ACCESS_LVL_APPADMIN))
                {
                    throw new Exception("Invalid entry. Please check your input.");
                }
                else
                {

                    if (accessProcess.AddNewRight(newRight) == FASTConstant.RETURN_VAL_SUCCESS)
                    {
                        return RedirectToAction("SearchEmployeeRights",  new { employeeID = employeeID });
                    }
                    else
                    {
                        throw new Exception("Failed to add access right");
                    }
                }
            }
            catch(Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Add Access Right";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                TempData[FASTConstant.TMPDATA_CONTROLLER] = "AccessRight";
                TempData[FASTConstant.TMPDATA_ACTION] = "NewAccessRight";
                return View("~/Views/Shared/Result.cshtml");
            }

        }

        [Authorize]
       
        public ActionResult DeleteRight(int accessID, int empID)
        {
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            accessProcess.UserID = User.Identity.Name.ToInteger();

            try
            {
                if (empID == 0 || accessID == 0 )
                {
                    throw new Exception("Invalid entry. Please check your input.");
                }
                else
                {

                    if (accessProcess.Delete(accessID) == FASTConstant.RETURN_VAL_SUCCESS)
                    {
                        return RedirectToAction("SearchEmployeeRights", new { employeeID = empID });
                    }
                    else
                    {
                        throw new Exception("Failed to delete access right");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Delete Access Right.";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                TempData[FASTConstant.TMPDATA_CONTROLLER] = "AccessRight";
                TempData[FASTConstant.TMPDATA_ACTION] = "NewAccessRight";
                return View("~/Views/Shared/Result.cshtml");
            }

        }
        [Authorize]
        public ActionResult DeleteRightFromList(int accessID, int empID)
        {
            AccessRightsProcess accessProcess = new AccessRightsProcess();
            accessProcess.UserID = User.Identity.Name.ToInteger();

            try
            {
                if (empID == 0 || accessID == 0)
                {
                    throw new Exception("Invalid entry. Please check your input.");
                }
                else
                {

                    if (accessProcess.Delete(accessID) == FASTConstant.RETURN_VAL_SUCCESS)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception("Failed to delete access right");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Delete Access Right";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                TempData[FASTConstant.TMPDATA_CONTROLLER] = "AccessRight";
                TempData[FASTConstant.TMPDATA_ACTION] = "Index";
                return View("~/Views/Shared/Result.cshtml");
            }

        }
	}
}