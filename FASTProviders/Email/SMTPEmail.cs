using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using log4net;
using log4net.Config;
using Provider.ExtensionMethod;
using Provider.Cryptography;

namespace Provider.Email
{
    public class SMTPEmail
    {
        private string _mailFrom;
        private string _password;
        private string _mailServer;
        private int _mailPort;
        private bool _enableSSL;

        FastEmail _emailData = new FastEmail();

        private static readonly ILog tracer = LogManager.GetLogger(typeof(SMTPEmail));

        public SMTPEmail()
        {
            XmlConfigurator.Configure();
        }

        public SMTPEmail(FastEmailConfiguration configuration, FastEmail data)
        {
            XmlConfigurator.Configure();
            tracer.Info("Sending Email to : " + data.Receipients.ListToString());

            _mailServer = configuration.MailServer;
            _mailPort = configuration.MailPort;
            _password = BasicCrypto.DecryptString(configuration.Password);
            _enableSSL = configuration.EnableSSL;
            _mailFrom = configuration.SenderAddress;

            _emailData = data;
        }

        public void SendEmail()
        {
            tracer.Info("Sending Email.");

            try
            {
                System.Threading.Thread emailThread = new System.Threading.Thread(SMTPSend);
                emailThread.Start();
            }
            catch(Exception ex)
            {
               tracer.Error(ex.ToString());
            }

        }

        public void SendSynchronousEmail()
        {
            tracer.Info("Sending Email.");
            try
            {
                System.Threading.Thread emailThread = new System.Threading.Thread(SMTPSend);
                SMTPSend();
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());
            }

        }


        private void SMTPSend()
        {
            var message = new MailMessage();

            if (_emailData.Receipients != null)
            {
                foreach (string receiver in _emailData.Receipients)
                {
                    message.To.Add(receiver);
                }
            }

            if (_emailData.CCList != null)
            {
                foreach (string cc in _emailData.CCList)
                {
                    message.CC.Add(cc);
                }
            }

            if (_emailData.Attachments != null)
            {

                foreach (string attach in _emailData.Attachments)
                {
                    Attachment newAttachment = new Attachment(attach);
                    message.Attachments.Add(newAttachment);
                }
            }

            message.From = new MailAddress(_mailFrom);

            message.Subject = _emailData.Subject;
            message.Body = _emailData.HTMLBody;

            message.IsBodyHtml = true;
            
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.SendCompleted += smtp_SendCompleted;
                    
                    var credentials = new NetworkCredential
                    {
                        UserName = _mailFrom,
                        Password = _password
                    };

                    smtp.Timeout = 60000; //Set timeout to 1 minutes
                    smtp.Credentials = credentials;
                    smtp.Host = _mailServer;
                    smtp.Port = _mailPort;
                    smtp.EnableSsl = _enableSSL;

                    smtp.Send(message);

                    tracer.Info("Email was sent successfully.");
                }
            }
            catch (Exception ex)
            {
                tracer.Error(ex.ToString());
            }
        }

        private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            tracer.Info("Email was sent successfully");
        }
    }
}
