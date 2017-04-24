using System;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class ConfigurationViewModel : Common.Configuration
    {
        [Display(Name = "Configuration ID")]
        public int ConfigID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Name")]
        public string ConfigName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Is Encrypted")]
        public bool IsEncrypted { get; set; }

        [Display(Name = "Can Expire")]
        public bool CanExpire { get; set; }

        [Display(Name = "Expiry Days")]
        public int ExpiryDays { get; set; }

        [Display(Name = "Module ID")]
        public int ModuleID { get; set; }

        public string OriginalValue { get; set; }

        public DateTime LastModified { get; set; }


    }
}