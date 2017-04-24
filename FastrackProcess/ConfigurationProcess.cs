using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provider.Email;
using Common;
using log4net;
using log4net.Config;
using Provider.ExtensionMethod;

namespace FASTProcess
{
    public class ConfigurationProcess : GenericProcess<Configuration>
    {
        private static readonly ILog tracer = LogManager.GetLogger(typeof(UserProcess));

        public ConfigurationProcess()
        {
            XmlConfigurator.Configure();
        }

        public FastEmailConfiguration GetEmailConfiguration()
        {
            FastEmailConfiguration config = new FastEmailConfiguration();

            List<Configuration> configs = new List<Configuration>();
            GenericProcess<Configuration> genericProcess = new GenericProcess<Configuration>();

            int activeEmail = genericProcess.GetByID(FASTConstant.CONFIG_ACTIVEEMAIL).Value.ToInteger();

            if ( activeEmail == 1 )
            {
                config.MailPort = genericProcess.GetByID(FASTConstant.CONFIG_MAILPORT).Value.ToInteger();
                config.MailServer = genericProcess.GetByID(FASTConstant.CONFIG_MAILSERVER).Value;
                config.Password = genericProcess.GetByID(FASTConstant.CONFIG_MAILPASSWORD).Value;
                config.SenderAddress = genericProcess.GetByID(FASTConstant.CONFIG_MAILUSER).Value;
                config.EnableSSL = genericProcess.GetByID(FASTConstant.CONFIG_MAILSSL).Value.ToBoolean();
            }
            else
            {
                config.MailPort = genericProcess.GetByID(FASTConstant.CONFIG_ALTMAILPORT).Value.ToInteger();
                config.MailServer = genericProcess.GetByID(FASTConstant.CONFIG_ALTMAILSERVER).Value;
                config.Password = genericProcess.GetByID(FASTConstant.CONFIG_ALTMAILPASSWORD).Value;
                config.SenderAddress = genericProcess.GetByID(FASTConstant.CONFIG_ALTMAILUSER).Value;
                config.EnableSSL = genericProcess.GetByID(FASTConstant.CONFIG_ALTMAILSSL).Value.ToBoolean();
            }


            return config;

        }

        public Configuration GetConfigurationByID(int id)
        {
            GenericProcess < Configuration > genericProcess = new GenericProcess<Configuration>();
            return genericProcess.GetByID(id);
        }
    }
}
