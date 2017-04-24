using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FASTWeb.Models
{
    public class ModularReport
    {
        public string ReportTitle { get; set; }
        public string ReportDescription {get;set;}
        public string ReportGenerator { get; set; }
        public string ReportDate { get; set; }
        public string FooterRemarks { get; set; }

        public List<ReportColumn> Columns = new List<ReportColumn>();
        public List<ReportData> Data = new List<ReportData>();

    }

    public class ReportColumn
    {
        public string ColumnName { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Format { get; set; }
        public string Style { get; set; }
        public string Width { get; set; }
    }

    public class ReportData
    {
        public List<ReportColumn> DataList = new List<ReportColumn>();
    }
}