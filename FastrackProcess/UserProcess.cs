using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Repositories;
using Repository.UnitsOfWork;
using Common;
using FASTProcess;
using Provider.Cryptography;
using System.Text.RegularExpressions;
using Provider.Email;
using log4net;
using log4net.Config;


namespace FASTProcess
{
    public class UserProcess
    {
        UserProcessingUnitOfWork<Employee,Registration> _unitOfWork =
            new UserProcessingUnitOfWork<Employee,Registration>();

        ConfigurationProcess configProcess = new ConfigurationProcess();

        private static readonly ILog tracer = LogManager.GetLogger(typeof(UserProcess));

        private FastEmailConfiguration _emailConfig = new FastEmailConfiguration();

        public UserProcess()
        {
            XmlConfigurator.Configure();
            _emailConfig = configProcess.GetEmailConfiguration();
        }

        public int RegisterUser(int userID)
        {
            tracer.Info("Registering : " + userID.ToString());

            Registration regData;
            Employee empData = _unitOfWork.Employees.GetByID(userID);
            

            string password = string.Empty;
            string clearPwd = string.Empty;

            if ( empData != null )
            {
                int recordFound = _unitOfWork.Registrations.GetAllQueryable().Where(m => m.EmployeeID == userID).ToList().Count;

                if (recordFound == 0 )
                {
                    password = System.Web.Security.Membership.GeneratePassword(8, 0);
                    clearPwd = password;
                    password = MD5HashProvider.CreateMD5Hash(Regex.Replace(password, @"[^a-zA-Z0-9]", m => "$"));

                    regData = new Registration() { EmployeeID = userID, Password = password, Status = 1, DateStamp = DateTime.Now };
                    _unitOfWork.Registrations.Insert(regData);

                    if (_unitOfWork.Save() > 0)
                    {
                        tracer.Info("Registration Success. Sending Email Notification.");

                        //send email notification to user uisng the emp email

                        FastEmail email = new FastEmail();
                        email.Receipients = new List<string>() { empData.EmailAddress };
                        email.Subject = FASTConstant.EMAIL_SIMPLE_SUBJECT.Replace("[XXX]", "User Registration");
                        email.HTMLBody = Helper.EmailHelper.GenerateHTMLBody(Helper.EmailHelper.EmailType.REGISTRATION);

                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_RECEIPIENT_NAME, empData.FirstName + " " + empData.LastName);
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_USERNAME, empData.EmployeeID.ToString());
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_PASSWORD, clearPwd);

                        SMTPEmail emailSender = new SMTPEmail(_emailConfig, email);
                        emailSender.SendEmail();

                        _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_USER_REG, "", employeeID: userID);
                        return FASTConstant.RETURN_VAL_SUCCESS;
                        
                    }
                    else
                    {
                        tracer.Warn("Registration Failed.");
                        return FASTConstant.RETURN_VAL_FAILED;
                    }

                    
                }
                else
                {
                    tracer.Warn("Registration Failed. User already registered.");
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_REG, "Duplicate ID", employeeID: userID);
                    return FASTConstant.RETURN_VAL_DUPLICATE;
                }
            }
            else
            {
                tracer.Warn("Registration Failed. User not found in DB.");

                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_REG, "Not Found", employeeID: userID);  
                return FASTConstant.RETURN_VAL_NOT_FOUND;
            }
            
        }

        public int LoginUser(int userID, string hashedPassword)
        {
            tracer.Info("Logging In : " + userID.ToString());

            Employee empData = _unitOfWork.Employees.GetByID(userID);

            if ( empData != null )
            {
                int recordFound = _unitOfWork.Registrations.GetAllQueryable()
                                  .Where(m => m.EmployeeID == userID && m.Password == hashedPassword)
                                  .ToList().Count;

                if ( recordFound > 0)
                {
                    tracer.Warn("Login Success.");
                    _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_USER_LOGIN, "", employeeID: userID);
                    return FASTConstant.RETURN_VAL_SUCCESS;
                }
                else
                {
                    tracer.Info("Login Failed.");
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_LOGIN, "Wrong Password", employeeID: userID);
                    return FASTConstant.RETURN_VAL_FAILED;
                }

            }
            else
            {
                tracer.Warn("Login Failed.");
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_LOGIN, "Not Found", employeeID: userID);
                return FASTConstant.RETURN_VAL_NOT_FOUND;
            }

        }

        public int ChangePassword(int userID, string newHashedPassword, string oldHashedPassword)
        {
            tracer.Info("Change Password  : " + userID.ToString());

            Employee empData = _unitOfWork.Employees.GetByID(userID);
            Registration regData = _unitOfWork.Registrations.GetAllQueryable().Where(m => m.EmployeeID == userID).First();
            
            if (empData != null)
            {
                if (regData != null)
                {
                    if (regData.Password == oldHashedPassword)
                    {
                        regData.Password = newHashedPassword;
                        _unitOfWork.Registrations.Update(regData);

                        if (_unitOfWork.Save() > 0)
                        {
                            tracer.Info("Change Password Success. Sending Email Notification.");
                            //Notify through email
                            FastEmail email = new FastEmail();
                            email.Receipients = new List<string>() { empData.EmailAddress };
                            email.Subject = FASTConstant.EMAIL_SIMPLE_SUBJECT.Replace("[XXX]", "Change Password");
                            email.HTMLBody = Helper.EmailHelper.GenerateHTMLBody(Helper.EmailHelper.EmailType.CHANGE_PASSWORD);

                            email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_RECEIPIENT_NAME, empData.FirstName + " " + empData.LastName);
                            
                            SMTPEmail emailSender = new SMTPEmail(_emailConfig, email);
                            emailSender.SendEmail();

                            _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_USER_CHANGE_PWD, "", employeeID: userID);
                            return FASTConstant.RETURN_VAL_SUCCESS;
                        }
                        else
                        {
                            tracer.Warn("Change Password Failed.");
                            _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_CHANGE_PWD, "", employeeID: userID);
                            return FASTConstant.RETURN_VAL_FAILED;
                        }
                    }
                    else
                    {
                        tracer.Warn("Change Password Failed. Old Password Invalid.");
                        _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_CHANGE_PWD, "Old password not match.", employeeID: userID);
                        return FASTConstant.RETURN_VAL_FAILED;
                    }
                }
                else
                {
                    tracer.Warn("Change Password Failed. User Not Found.");
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_CHANGE_PWD, "Not Found", employeeID: userID);
                    return FASTConstant.RETURN_VAL_NOT_FOUND;
                }
            }
            else
            {
                tracer.Warn("Change Password Failed. User Not Found.");
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_CHANGE_PWD, "Not Found", employeeID: userID);
                return FASTConstant.RETURN_VAL_NOT_FOUND;
            }
        }

        public int ResetPassword(int userID)
        {
            tracer.Info("Reset Password : " + userID.ToString());

            Employee empData = _unitOfWork.Employees.GetByID(userID);
            Registration regData = _unitOfWork.Registrations.GetAllQueryable().Where(m => m.EmployeeID == userID).FirstOrDefault();
            string password;
            string clearPassword = string.Empty;

            if ( empData != null )
            {
                if (regData != null)
                {

                    password = System.Web.Security.Membership.GeneratePassword(8, 0);
                    clearPassword = Regex.Replace(password, @"[^a-zA-Z0-9]", m => "$");
                    password = MD5HashProvider.CreateMD5Hash(clearPassword);

                    regData.Password = password;

                    _unitOfWork.Registrations.Update(regData);

                    if ( _unitOfWork.Save() > 0)
                    {
                        tracer.Info("Reset Password Success. Sending Email Notification.");
                        //notify through email
                        FastEmail email = new FastEmail();
                        email.Receipients = new List<string>() { empData.EmailAddress };
                        email.Subject = FASTConstant.EMAIL_SIMPLE_SUBJECT.Replace("[XXX]", "Reset Password");
                        email.HTMLBody = Helper.EmailHelper.GenerateHTMLBody(Helper.EmailHelper.EmailType.RESET_PASSWORD);

                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_RECEIPIENT_NAME, empData.FirstName + " " + empData.LastName);
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_USERNAME, empData.EmployeeID.ToString());
                        email.HTMLBody = email.HTMLBody.Replace(FASTConstant.EMAIL_PASSWORD, clearPassword);


                        SMTPEmail emailSender = new SMTPEmail(_emailConfig, email);
                        emailSender.SendEmail();


                        _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_USER_RESET, "", employeeID: userID);
                        return FASTConstant.RETURN_VAL_SUCCESS;
                    }
                    else
                    {
                        tracer.Warn("Reset Password Failed.");
                        _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_RESET, "", employeeID: userID);
                        return FASTConstant.RETURN_VAL_FAILED;
                    }


                    
                }
                else
                {
                    tracer.Warn("Reset Password Failed. User Not Found.");
                    _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_RESET, "Not Found", employeeID: userID);
                    return FASTConstant.RETURN_VAL_NOT_FOUND;
                }
            }
            else
            {
                tracer.Warn("Reset Password Failed. User Not Found.");
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_USER_RESET, "Not Found", employeeID: userID);
                return FASTConstant.RETURN_VAL_NOT_FOUND;
            }


            
        }

      

    }
}
