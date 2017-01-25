using FASTWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using FASTProcess;
using Common;
using Provider.ExtensionMethod;


namespace FASTWeb.Controllers
{
    public class EmployeeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
      
            EmployeeProcess employeeProcess = new EmployeeProcess();
            IQueryable<vwEmployeeList> list  = employeeProcess.GetEmployeeProfiles();

            int departmentID = 0;

            if (Session["Department"] != null )
            {
                if ( Int32.TryParse(Session["Department"].ToString(), out departmentID))
                {
                    list = employeeProcess.GetEmployeeProfiles().Where(i => i.DepartmentID == departmentID);    
                }
            }

            TempData["Employees"] = list.ToList();

            return View(list.ToList());

        }

        [Authorize]
        [HttpPost]
        public ActionResult Index( string firstName="", string lastName="" )
        {
            GenericProcess<vwEmployeeList> employeeProcess = new GenericProcess<vwEmployeeList>();

            IQueryable<vwEmployeeList> list;

            
            if (!String.IsNullOrEmpty(lastName))
            {
                list = employeeProcess.GetAll().Where(i => i.LastName.Contains(lastName) == true);
            }
            else
            {
                list = employeeProcess.GetAll();
            }

            if (!String.IsNullOrEmpty(firstName))
            {
                list = list.Where(i => i.FirstName.Contains(firstName) == true);
            }
            
            TempData["Employees"] = list.ToList();

            return View("Index", list.ToList());


        }

        [Authorize]
        public ActionResult ShowDepartmentEmployee(int departmentID)
        {
            EmployeeProcess employeeProcess = new EmployeeProcess();
            GenericProcess<Common.vwDepartmentList> deptProcess =
                new GenericProcess<Common.vwDepartmentList>();

            List<vwEmployeeList> listOfEmployees = new List<vwEmployeeList>();
            vwDepartmentList department = new vwDepartmentList();

            try
            {
                listOfEmployees = employeeProcess.GetEmployeesByDepartmentID(departmentID);

                department = deptProcess.GetAll().Where(i => i.DepartmentID == departmentID).First();

            }
            catch { }

            ViewBag.Department = department.GroupName;

            return View("Index",listOfEmployees);

        }

        [Authorize]
        public ActionResult NewEmployee()
        {
            GenericProcess<Department> deptProcess = new GenericProcess<Department>();
            GenericProcess<Position> positionProcess = new GenericProcess<Position>();

            ViewBag.Departments = deptProcess.GetAll().ToList();
            ViewBag.Positions = positionProcess.GetAll().ToList();
            return View(new EmployeeViewModel());

        }

        [Authorize]
        [HttpPost]
        public ActionResult NewEmployee(EmployeeViewModel model)
        {
            Employee newEmployee = new Employee();

            try
            {
                //Transfer the models info to our main Employee Model
                newEmployee.EmployeeID = model.EmployeeID;
                newEmployee.FirstName = model.FirstName;
                newEmployee.LastName = model.LastName;
                newEmployee.MiddleName = model.MiddleName;
                newEmployee.PhoneNumber = model.PhoneNumber;
                newEmployee.EmailAddress = model.EmailAddress;
                newEmployee.Gender = model.Gender;
                newEmployee.PositionID = model.PositionID;
                newEmployee.DepartmentID = model.DepartmentID;

                if (model.Status_booleanVal)
                {
                    newEmployee.Status = 1;
                }
                else
                {
                    newEmployee.Status = 0;
                }

                if (model.CompanyName != null)
                {
                    newEmployee.IsCompany = 1;
                    newEmployee.CompanyName = model.CompanyName;
                }

                EmployeeProcess employeeProcess = new EmployeeProcess();

                int result = employeeProcess.Add(newEmployee);

                if (result ==  FASTConstant.RETURN_VAL_SUCCESS)
                {

                    TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                    TempData[FASTConstant.TMPDATA_SOURCE] = "Add New Employee";
                    TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "The new employee has been added to the database.";
                    TempData[FASTConstant.TMPDATA_ACTION] = "Index";
                    TempData[FASTConstant.TMPDATA_CONTROLLER] = "Employee";

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
                TempData[FASTConstant.TMPDATA_SOURCE] = "Add New Employee";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }
        }

        [Authorize]
        public ActionResult EditEmployee(int mod)
        {

            GenericProcess<Department> deptProcess = new GenericProcess<Department>();
            GenericProcess<Position> positionProcess = new GenericProcess<Position>();
            EmployeeProcess employeeProcess = new EmployeeProcess();

            ViewBag.Departments = deptProcess.GetAll().ToList();
            ViewBag.Positions = positionProcess.GetAll().ToList();

            int employeeID = mod;

            FASTWeb.Models.EmployeeViewModel employeeEditView =
                new EmployeeViewModel();

            Employee employeeToEdit = employeeProcess.GetEmployeeByID(employeeID);

            //move data to editview
            employeeEditView.EmployeeID = employeeToEdit.EmployeeID;
            employeeEditView.FirstName = employeeToEdit.FirstName;
            employeeEditView.MiddleName = employeeToEdit.MiddleName;
            employeeEditView.LastName = employeeToEdit.LastName;
            employeeEditView.EmailAddress = employeeToEdit.EmailAddress;
            employeeEditView.PhoneNumber = employeeToEdit.PhoneNumber;
            employeeEditView.Gender = employeeToEdit.Gender;
            employeeEditView.PositionID = employeeToEdit.PositionID;
            employeeEditView.DepartmentID = employeeToEdit.DepartmentID;
            employeeEditView.IsCompany = employeeToEdit.IsCompany;
            employeeEditView.CompanyName = employeeToEdit.CompanyName;
            employeeEditView.Status = employeeToEdit.Status;

            if (employeeToEdit.Status == 1)
            {
                employeeEditView.Status_booleanVal = true;
            }
            else
            {
                employeeEditView.Status_booleanVal = false;
            }
            
            return View(employeeEditView);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel model)
        {
            Employee modEmployee = new Employee();

            try
            {
                //Transfer the models info to our main Employee Model
                modEmployee.EmployeeID = model.EmployeeID;
                modEmployee.FirstName = model.FirstName;
                modEmployee.LastName = model.LastName;
                modEmployee.MiddleName = model.MiddleName;
                modEmployee.PhoneNumber = model.PhoneNumber;
                modEmployee.EmailAddress = model.EmailAddress;
                modEmployee.Gender = model.Gender;
                modEmployee.PositionID = model.PositionID;
                modEmployee.DepartmentID = model.DepartmentID;

                if (model.Status_booleanVal)
                {
                    modEmployee.Status = 1;
                }
                else
                {
                    modEmployee.Status = 0;
                }

                if (model.CompanyName != null)
                {
                    modEmployee.IsCompany = 1;
                    modEmployee.CompanyName = model.CompanyName;
                }

                EmployeeProcess employeeProcess = new EmployeeProcess();

                int result = employeeProcess.Update(modEmployee);

                if (result == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                    TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Employee";
                    TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "The employee record has been modified.";
                    TempData[FASTConstant.TMPDATA_ACTION] = "MyAssets";
                    TempData[FASTConstant.TMPDATA_CONTROLLER] = "Employee";

                    return View("~/Views/Shared/Result.cshtml");
                }
                else
                {
                    throw new Exception("There was an error while editing employee.");
                }
            }
            catch (Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Employee";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }
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
    }
}