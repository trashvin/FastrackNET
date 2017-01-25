using Common;
using FASTProcess;
using Provider.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System;
using Provider.ExtensionMethod;

namespace FASTWeb.Controllers
{
    public class ConfigurationController : Controller
    {
        //
        // GET: /Configuration/
        [Authorize]
        public ActionResult Index()
        {
            GenericProcess<Configuration> configProcess =
                new GenericProcess<Configuration>();

            List<Configuration> configs = configProcess.GetAll().ToList();

            foreach (Configuration config in configs)
            {
                bool encrypted = false;
                Boolean.TryParse(config.IsEncrypted.ToString(), out encrypted);

                if (encrypted)
                {
                    config.Value = BasicCrypto.DecryptString(config.Value);
                }
            }


            return View(configs);
        }

      
        [Authorize]
        public ActionResult EditConfigurations(int configID)
        {
            GenericProcess<Configuration> configProcess =
                new GenericProcess<Configuration>();

            configProcess.UserID = User.Identity.Name.ToInteger();

            FASTWeb.Models.ConfigurationViewModel model = new Models.ConfigurationViewModel();

            Configuration toEdit = configProcess.GetByID(configID);

            bool encrypted = false;
            Boolean.TryParse(toEdit.IsEncrypted.ToString(), out encrypted);

            model.ConfigID = toEdit.ConfigID;

            model.ConfigName = toEdit.Name;
            if (encrypted)
            {
                model.Value = BasicCrypto.DecryptString(toEdit.Value);
            }
            else
            {
                model.Value = toEdit.Value;
            }

            model.IsEncrypted = toEdit.IsEncrypted.HasValue ? (bool)toEdit.IsEncrypted : false;
            model.CanExpire = toEdit.CanExpire.HasValue ? (bool)toEdit.CanExpire : false;
            model.ExpiryDays = toEdit.ExpiryDays.HasValue ? (int)toEdit.ExpiryDays : 0;
            model.ModuleID = toEdit.ModuleID.HasValue ? (int)toEdit.ModuleID : 0;

            model.LastModified = toEdit.LastModified;
            model.OriginalValue = toEdit.Value;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditConfigurations(FASTWeb.Models.ConfigurationViewModel model)
        {
            Configuration config = new Configuration();

            try
            {

                bool encrypted = false;
                Boolean.TryParse(model.IsEncrypted.ToString(), out encrypted);

                config.ConfigID = model.ConfigID;
                config.Name = model.ConfigName;

                if (encrypted)
                {
                    config.Value = BasicCrypto.EncryptString(model.Value);
                }
                else
                {
                    config.Value = model.Value;
                }

                config.ModuleID = model.ModuleID;
                config.IsEncrypted = model.IsEncrypted;
                config.CanExpire = model.CanExpire;
                config.ExpiryDays = model.ExpiryDays;

                if ( String.Compare(config.Value,model.OriginalValue) != 0)
                {
                    config.LastModified = DateTime.Now;
                }
                else
                {
                    config.LastModified = model.LastModified;
                }


                GenericProcess<Configuration> configProcess =
                    new GenericProcess<Configuration>();

                configProcess.UserID = User.Identity.Name.ToInteger();

                int result = configProcess.Update(config);

                if (result == FASTConstant.RETURN_VAL_SUCCESS)
                {
                    TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.SUCCESSFUL;
                    TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Configuration";
                    TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = "The configuration record has been modified.";
                    TempData[FASTConstant.TMPDATA_ACTION] = "Index";
                    TempData[FASTConstant.TMPDATA_CONTROLLER] = "Configuration";

                    return View("~/Views/Shared/Result.cshtml");
                }
                else
                {
                    throw new Exception("There was an error while editing configuration.");
                }
            }
            catch (Exception ex)
            {
                TempData[FASTConstant.TMPDATA_RESULT] = FASTConstant.FAILURE;
                TempData[FASTConstant.TMPDATA_SOURCE] = "Edit Configuration";
                TempData[FASTConstant.TMPDATA_EXTRAMESSAGE] = ex.Message;
                return View("~/Views/Shared/Result.cshtml");
            }

        }

	}
}