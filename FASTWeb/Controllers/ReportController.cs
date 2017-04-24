
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;

using FASTProcess;
using Common;
using Provider.ExtensionMethod;

namespace FASTWeb.Controllers
{
    
    public class ReportController : Controller
    {
        [Authorize]
        public void ExportEmployeeSearchResult()
        {

            List<vwEmployeeList> list = (List<vwEmployeeList>)TempData["Employees"];
            TempData["Employees"] = list;

            FASTWeb.Models.ModularReport reportData = GenerateEmployeesReportData(list);

            GenerateExcel(reportData,"ListOfEmployees.xls"); 

        }

        [Authorize]
        public ActionResult PrintEmployeeSearchResult()
        {
            List<vwEmployeeList> list = (List<vwEmployeeList>)TempData["Employees"];

            TempData["Employees"] = list;

            return View("GenericReport", GenerateEmployeesReportData(list));
        }

        //[HttpPost]
        //public void EmployeeReport(vwEmployeeList emp)
        //{
        //    DepartmentProcess department = new DepartmentProcess();
        //    PositionProcess position = new PositionProcess();
        //    ViewBag.Departments = department.GetDepartments().ToList();
        //    ViewBag.Positions = position.GetPositions().ToList();

        //    ReportsProcess reportsProcess = new ReportsProcess();
        //    int employeeStatus = (int)EmployeeStatus.Active;

        //    List<vwEmployeeList> sourcefile = new List<vwEmployeeList>();

        //    if (emp.DepartmentID != 0 && emp.PositionID != 0)
        //    {
        //        sourcefile=(reportsProcess.GetEmployeeListByDepartmentIDPositionID(employeeStatus, emp.DepartmentID, emp.PositionID));
        //    }
        //    else if (emp.DepartmentID > 0 && emp.PositionID == 0)
        //    {
        //        sourcefile = (reportsProcess.GetEmployeeListByDepartmentID(employeeStatus, emp.DepartmentID));
        //    }
        //    else if (emp.PositionID > 0 && emp.DepartmentID == 0)
        //    {
        //        sourcefile = (reportsProcess.GetEmployeeListByPositionID(employeeStatus, emp.PositionID));
        //    }
        //    else
        //    {
        //        sourcefile = (reportsProcess.GetEmployeeList(employeeStatus));
        //    }

        //    WebGrid grid = new WebGrid(source: sourcefile, canPage: false, canSort: false);
        //    string gridHtml = grid.GetHtml(
        //                             tableStyle: "table table-striped table-hover",
        //        //mode: WebGridPagerModes.All,
        //        //firstText: "First",
        //        //previousText: "Prev",
        //        //nextText: "Next",
        //        //lastText: "Last",
        //        //htmlAttributes: new
        //        //{
        //        //    id = "grid"
        //        //},
        //                        columns: grid.Columns(
        //                         grid.Column("EmployeeID", "Employee ID"),
        //                         grid.Column("FullName", "Full Name"),
        //                         grid.Column("FirstName", "First Name"),
        //                         grid.Column("MiddleName", "Middle Name"),
        //                         grid.Column("LastName", "Last Name"),
        //                         grid.Column("ManagerID", "Manager ID"),
        //                         grid.Column("Gender", "Gender"),
        //                         grid.Column("EmailAddress", "Email"),
        //                         grid.Column("PhoneNumber", "Phone #"),
        //                         grid.Column("DepartmentName", "Department"),
        //                         grid.Column("GroupName", "Group"),
        //                         grid.Column("Description", "Position"),
        //                         grid.Column("Status", "Status")
        //                         )
        //                    ).ToString();

        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename=ListOfEmployees.xls");
        //    Response.ContentType = "application/excel";
        //    Response.Write(gridHtml);
        //    Response.End();

        //}

        //[HttpPost]
        //public void GetPDF(vwEmployeeList employee)
        //{
        //    ReportsProcess reportsProcess = new ReportsProcess();
        //    int employeeStatus = (int)EmployeeStatus.Active;

        //    var all = reportsProcess.GetEmployeeList(employeeStatus);

        //    WebGrid grid = new WebGrid(source: all, canPage: false, canSort: false);
        //    string gridHtml = grid.GetHtml(
        //                             tableStyle: "table table-striped table-hover",
        //                        //mode: WebGridPagerModes.All,
        //                        //firstText: "First",
        //                        //previousText: "Prev",
        //                        //nextText: "Next",
        //                        //lastText: "Last",
        //                        //htmlAttributes: new
        //                        //{
        //                        //    id = "grid"
        //                        //},
        //                        columns: grid.Columns(
        //                         grid.Column("EmployeeID", "Employee ID"),
        //                         grid.Column("FullName", "Full Name"),
        //                         grid.Column("Gender", "Gender"),
        //                         grid.Column("EmailAddress", "Email"),
        //                         grid.Column("PhoneNumber", "Phone #"),
        //                         grid.Column("DepartmentName", "Department"),
        //                         grid.Column("GroupName", "Group"),
        //                         grid.Column("Description", "Position")
        //                         )
        //                    ).ToString();
        //    //string exportData = String.Format("<html><head>{0}</head><body>{1}</body></html>","<style>table { border-spacing:5px; border:thick; }</style>", gridHtml);
        //    //var bytes = Encoding.UTF8.GetBytes(exportData);
        //    //using (var input = new MemoryStream(bytes))
        //    //{
        //    //    var output = new MemoryStream();
        //    //    var document = new Document(PageSize.A4, 5, 5, 5, 5);
        //    //    var writer = PdfWriter.GetInstance(document, output);
        //    //    writer.CloseStream = false;
        //    //    document.Open();

        //    //    var xmlWorker = XMLWorkerHelper.GetInstance();
        //    //    xmlWorker.ParseXHtml(writer, document, input, Encoding.UTF8);
        //    //    document.Close();
        //    //    output.Position = 0;

        //    //    return new FileStreamResult(output, "application/pdf");

        //    //}

        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename=Employees.xls");
        //    Response.ContentType = "application/excel";
        //    Response.Write(gridHtml);
        //    Response.End();

        //    //byte[] bytesArray = null;
        //    //using (var ms = new MemoryStream())
        //    //{
        //    //    using (var document = new Document())
        //    //    {
        //    //        using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
        //    //        {
        //    //            document.Open();
        //    //            using (var strReader = new StringReader(exportData))
        //    //            {
        //    //                //Set factories
        //    //                HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
        //    //                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
        //    //                //Set css
        //    //                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
        //    //                cssResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/bootstrap.min.css"), true);
        //    //                //Export
        //    //                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
        //    //                var worker = new XMLWorker(pipeline, true);
        //    //                var xmlParse = new XMLParser(true, worker);
        //    //                xmlParse.Parse(strReader);
        //    //                xmlParse.Flush();
        //    //            }
        //    //            document.Close();
        //    //        }
        //    //    }
        //    //    bytesArray = ms.ToArray();
        //    //    return new FileStreamResult(ms, "application/pdf");
        //    //}
            
        //}

        [Authorize]
        public ActionResult MyAssignedAssetsReport()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();
            EmployeeProcess employeeProcess = new EmployeeProcess();

            int employeeID = User.Identity.Name.ToInteger();

            ViewBag.Employee = employeeProcess.GetEmployeeProfileByID(employeeID);
            ViewBag.Assets = assignProcess.GetCurrentAssignmentsByEmployeeID(employeeID);

            return View();
        }

        [Authorize]
        public ActionResult MyAssetHistoryReport()
        {
            AssignmentProcess assignProcess = new AssignmentProcess();
            EmployeeProcess employeeProcess = new EmployeeProcess();

            int employeeID = User.Identity.Name.ToInteger();

            ViewBag.Employee = employeeProcess.GetEmployeeProfileByID(employeeID);
            ViewBag.Assets = assignProcess.GetAllAssignmentsByEmployeeID(employeeID);

            return View();
        }

        [AllowAnonymous]
        public ActionResult GeneratePDFReport(string reportName)
        {
            return new Rotativa.ActionAsPdf(reportName);
        }

        [Authorize]
        public ActionResult PrintAssetSearchResult()
        {

            List<vwFixAssetList> list = (List<vwFixAssetList>)TempData["Assets"];

            TempData["Assets"] = list;
    
            return View("GenericReport", GenerateAssetReportData(list));
        }

        private FASTWeb.Models.ModularReport GenerateAssetReportData(List<vwFixAssetList> assets)
        {
            List<vwFixAssetList> list = assets;


            FASTWeb.Models.ModularReport reportdata = new Models.ModularReport();

            reportdata.ReportTitle = "Fix Asset Report";

            List<FASTWeb.Models.ReportColumn> header = new List<Models.ReportColumn>();
            FASTWeb.Models.ReportColumn col1 = new Models.ReportColumn() { Content = "Asset Tag", Style = "center", Width = "30" };
            FASTWeb.Models.ReportColumn col2 = new Models.ReportColumn() { Content = "Serial Number", Style = "center", Width = "30" };
            FASTWeb.Models.ReportColumn col3 = new Models.ReportColumn() { Content = "Brand", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col4 = new Models.ReportColumn() { Content = "Model", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col6 = new Models.ReportColumn() { Content = "Type", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col7 = new Models.ReportColumn() { Content = "Acquisition Date", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col8 = new Models.ReportColumn() { Content = "Status", Style = "center", Width = "100" };

            header.Add(col1);
            header.Add(col2);
            header.Add(col3);
            header.Add(col4);
            header.Add(col6);
            header.Add(col7);
            header.Add(col8);



            FASTWeb.Models.ReportColumn f1 = new Models.ReportColumn() { Style = "center", Format = "" };
            FASTWeb.Models.ReportColumn f2 = new Models.ReportColumn() { Style = "left", Format = "" };

            if (list != null)
            {
                foreach (vwFixAssetList asset in list)
                {
                    FASTWeb.Models.ReportData newData = new Models.ReportData();
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.AssetTag, Width = col1.Width, Format = "", Style = f1.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.SerialNumber, Width = col2.Width, Format = "", Style = f1.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.Brand, Width = col3.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.Model, Width = col4.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.TypeDescription, Width = col6.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.AcquisitionDate.ToString(), Width = col7.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = asset.StatusDescription, Width = col8.Width, Format = "", Style = f2.Style });

                    reportdata.Data.Add(newData);
                }
            }


            reportdata.Columns = header;

            return reportdata;
        }

        [Authorize]
        public void ExportAssetSearchResult()
        {
            List<vwFixAssetList> list = (List<vwFixAssetList>)TempData["Assets"];
            TempData["Assets"] = list;

            FASTWeb.Models.ModularReport reportData = GenerateAssetReportData(list);

            GenerateExcel(reportData,"ListOfAssets.xls");            

        }

        private void GenerateExcel(FASTWeb.Models.ModularReport reportData, string fileName)
        {
            StringBuilder gridHtml = new StringBuilder();
            gridHtml.Append("<Table>");
            gridHtml.Append("<thead>");

            foreach (FASTWeb.Models.ReportColumn column in reportData.Columns)
            {
                gridHtml.Append(String.Format("<th>{0}</th>", column.Content));
            }
            gridHtml.Append("</thead>");
            gridHtml.Append("<tbody>");

            foreach (FASTWeb.Models.ReportData data in reportData.Data)
            {
                gridHtml.Append("<tr>");
                foreach (FASTWeb.Models.ReportColumn column in data.DataList)
                {
                    gridHtml.Append(String.Format("<td>{0}</td>", column.Content));
                }
                gridHtml.Append("</tr>");
            }

            gridHtml.Append("</tbody>");
            gridHtml.Append("</Table>");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/xls";
            Response.Write(gridHtml.ToString());
            Response.End();
        }

        private FASTWeb.Models.ModularReport GenerateEmployeesReportData(List<vwEmployeeList> employees)
        {
            List<vwEmployeeList> list = employees;

            FASTWeb.Models.ModularReport reportdata = new Models.ModularReport();

            reportdata.ReportTitle = "Employee List Report";

            List<FASTWeb.Models.ReportColumn> header = new List<Models.ReportColumn>();
            FASTWeb.Models.ReportColumn col1 = new Models.ReportColumn() { Content = "Employee ID", Style = "center", Width = "30" };
            FASTWeb.Models.ReportColumn col2 = new Models.ReportColumn() { Content = "Full Name", Style = "center", Width = "30" };
            FASTWeb.Models.ReportColumn col3 = new Models.ReportColumn() { Content = "Gender", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col4 = new Models.ReportColumn() { Content = "Email", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col6 = new Models.ReportColumn() { Content = "Phone Number", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col7 = new Models.ReportColumn() { Content = "Position", Style = "center", Width = "100" };
            FASTWeb.Models.ReportColumn col8 = new Models.ReportColumn() { Content = "Status", Style = "center", Width = "100" };

            header.Add(col1);
            header.Add(col2);
            header.Add(col3);
            header.Add(col4);
            header.Add(col6);
            header.Add(col7);
            header.Add(col8);



            FASTWeb.Models.ReportColumn f1 = new Models.ReportColumn() { Style = "center", Format = "" };
            FASTWeb.Models.ReportColumn f2 = new Models.ReportColumn() { Style = "left", Format = "" };

            if (list != null)
            {
                foreach (vwEmployeeList employee in list)
                {

                    FASTWeb.Models.ReportData newData = new Models.ReportData();
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.EmployeeID.ToString(), Width = col1.Width, Format = "", Style = f1.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.FullName, Width = col2.Width, Format = "", Style = f1.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.Gender, Width = col3.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.EmailAddress, Width = col4.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.PhoneNumber, Width = col6.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.Description, Width = col7.Width, Format = "", Style = f2.Style });
                    newData.DataList.Add(new Models.ReportColumn() { Content = employee.Status.ToString(), Width = col8.Width, Format = "", Style = f2.Style });

                    reportdata.Data.Add(newData);
                }
            }


            reportdata.Columns = header;

            return reportdata;
        }
    }
     
} 