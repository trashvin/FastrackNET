using Common;
using FASTProcess;
using Provider.Cryptography;
using FASTWeb.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;


namespace FASTWeb.Controllers
{
    public class AuthenticateController : Controller
    {
        //
        // GET: /Authenticate/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //TODO : Added just to create a login view. Change this if needed
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginCredentialModel loginCredentials)
        {
            UserProcess userProcessor = new UserProcess();

            if (ModelState.IsValid)
            {
                int result = userProcessor.LoginUser(loginCredentials.Username,MD5HashProvider.CreateMD5Hash(loginCredentials.Password));
                
                if (result == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    // set the forms auth cookie
                    FormsAuthentication.SetAuthCookie(loginCredentials.Username.ToString(), false);

                    // reset request.isauthenticated
                    var authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        if (authTicket != null && !authTicket.Expired)
                        {
                            var roles = authTicket.UserData.Split(',');
                            System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                        }
                    }

                    return RedirectToAction("MyAssets", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Employee Employee ID or Password");
                }
            }
            return View();
        }

        //TODO : Added just to create a login view. Change this if needed
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegistrationModel registrationModel)
        {
            UserProcess userProcessor = new UserProcess();

            int result = userProcessor.RegisterUser(registrationModel.EmployeeID);

            if ( result == FASTConstant.RETURN_VAL_SUCCESS )
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "An email confirmation will be sent to you. This will contain a random generated initial password. You can change the password inside the user area";
            }
            else
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "Registration failed. Please try again or contact the AppAdmin.";
            }

            TempData[FASTConstant.TMPDATA_SOURCE] = "User Registration";
            TempData[FASTConstant.TMPDATA_CONTROLLER] = "Home";
            TempData[FASTConstant.TMPDATA_ACTION] = "Index";

            return View("~/Views/Shared/Result.cshtml");
        }

        //TODO : Added just to create a login view. Change this if needed
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            UserProcess userProcessor = new UserProcess();

            int result = userProcessor.ChangePassword(changePasswordModel.Username, 
                                    MD5HashProvider.CreateMD5Hash(changePasswordModel.NewPassword), MD5HashProvider.CreateMD5Hash(changePasswordModel.OldPassword));

            if ( result == FASTConstant.RETURN_VAL_SUCCESS )
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "Thank you. Change password was successful.";
            }
            else
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "Change password failed. Please try again or contact the AppAdmin.";
            }

            TempData[FASTConstant.TMPDATA_SOURCE] = "Change  Password";
            TempData[FASTConstant.TMPDATA_CONTROLLER] = "Home";
            TempData[FASTConstant.TMPDATA_ACTION] = "Index";

            return View("~/Views/Shared/Result.cshtml");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authenticate");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(RegistrationModel resetModel)
        {
            UserProcess userProcessor = new UserProcess();

            int result = userProcessor.ResetPassword(resetModel.EmployeeID);

            if (result == FASTConstant.RETURN_VAL_SUCCESS)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "An email confirmation will be sent to you. This will contain a random generated initial password. You can change the password inside the user area";
            }
            else
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "Reset password failed. Please try again or contact the AppAdmin";
            }
            TempData[FASTConstant.TMPDATA_SOURCE] = "Reset Password";
            TempData[FASTConstant.TMPDATA_CONTROLLER] = "Home";
            TempData[FASTConstant.TMPDATA_ACTION] = "Index";

            return View("~/Views/Shared/Result.cshtml");
        }

	}
}