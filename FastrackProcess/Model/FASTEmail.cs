using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASTProcess.Model
{
    internal class FASTEmail
    {
        public string Header { get; set; }
        public string ReceipientName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Footer { get; set; }
        public string BulkUploadInfo { get; set; }
        public string BulkUploadLog { get; set; }
        public string BulkUploadSummary { get; set; }
    }
}
