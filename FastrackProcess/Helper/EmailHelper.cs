using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;
using Common;
using log4net;
using log4net.Config;

namespace FASTProcess.Helper
{
    public static class EmailHelper
    {

        private static readonly ILog tracer = LogManager.GetLogger(typeof(EmailHelper));

        public enum EmailType{
            REGISTRATION,
            RESET_PASSWORD,
            CHANGE_PASSWORD,
            BULK_UPLOAD

        };

      

        public static string GenerateHTMLBody(EmailType type)
        {
            ConfigurationProcess configProcess = new ConfigurationProcess();
            XmlConfigurator.Configure();
            try
            {
                string tempData = string.Empty;

                string header = configProcess.GetConfigurationByID(Common.FASTConstant.CONFIG_EMAILHEADER).Value;
                string footer = configProcess.GetConfigurationByID(Common.FASTConstant.CONFIG_EMAILFOOTER).Value;
                string adminEmail = configProcess.GetConfigurationByID(Common.FASTConstant.CONFIG_ADMINEMAIL).Value;
                string adminTel = configProcess.GetConfigurationByID(Common.FASTConstant.CONFIG_ADMINTEL).Value;

                switch (type)
                {
                    case EmailType.REGISTRATION:
                        tempData = GetTemplate("Registration.html");
                        break;
                    case EmailType.CHANGE_PASSWORD:
                        tempData = GetTemplate("ChangePassword.html");
                        break;
                    case EmailType.RESET_PASSWORD:
                        tempData = GetTemplate("ResetPassword.html");
                        break;
                    case EmailType.BULK_UPLOAD:
                        tempData = GetTemplate("BulkUploadResult.html");
                        break;
                    default:
                        return String.Empty;
                }

                if (!String.IsNullOrEmpty(tempData))
                {
                    tempData = tempData.Replace(Common.FASTConstant.EMAIL_HEADER, header)
                        .Replace(Common.FASTConstant.EMAIL_FOOTER, footer)
                        .Replace(Common.FASTConstant.EMAIL_APPADMIN_EMAIL, adminEmail)
                        .Replace(Common.FASTConstant.EMAIL_APPADMIN_TELNO, adminTel);
                }
                else
                {
                    tempData = string.Empty;
                }

                return tempData;
            }
            catch(Exception ex)
            {
                tracer.Error(ex.ToString());
                return String.Empty;
            }

        }

        private static string GetTemplate(string fileName)
        {
            string completeFile = HttpContext.Current.Server.MapPath("\\App_Data\\EmailTemplates\\") + fileName;

            using (FileStream fs = new FileStream(completeFile, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }

        }

    }
}
