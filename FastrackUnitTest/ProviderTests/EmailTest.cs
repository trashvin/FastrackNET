using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Provider.Email;
using log4net;
using log4net.Config;


namespace FastrackUnitTest.ProviderTests
{
    [TestClass]
    public class EmailTest
    {
        SMTPEmail _mailer = new SMTPEmail();


        [TestMethod]
        public void TestEmailSend()
        {
            FastEmail email = new FastEmail();
            FastEmailConfiguration config = new FastEmailConfiguration(sender: "fams@fpi.sea.css.fujitsu.com", server: "10.164.34.27", password: "Fujitsu.959", port: 25, enableSSL: false);

            email.Receipients = new System.Collections.Generic.List<string>() { "m3lles@gmail.com" };
            email.CCList = new System.Collections.Generic.List<string>() { "m3lles@yahoo.com", "m3lles@live.com" };
            email.Subject = "Test Email";
            email.HTMLBody = "<div style=\"padding-top: 5px; padding-left: 5px; padding-right: 5px; padding-bottom: 5px; width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #666666; background-color: #e00000; \"><img width=\"200\" height=\"50\" src=\"https://dl.dropboxusercontent.com/u/97422769/Images/fastrack_brand.png\" alt=\"fastrack brand logo\" /></div>";

            try
            {
                _mailer = new SMTPEmail(config, email);
                _mailer.SendSynchronousEmail();
                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }
}
