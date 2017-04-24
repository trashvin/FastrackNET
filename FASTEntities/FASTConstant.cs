using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FASTConstant
    {
        public const string AUDIT_ACTION_USER_REG = "User Registration";
        public const string AUDIT_ACTION_USER_LOGIN = "Login";
        public const string AUDIT_ACTION_USER_RESET = "Reset Password";
        public const string AUDIT_ACTION_USER_CHANGE_PWD = "Change Password";
        public const string AUDIT_ACTION_ADD = "Add : ";
        public const string AUDIT_ACTION_EDIT = "Update : ";
        public const string AUDIT_ACTION_DELETE = "Delete : ";
        public const string AUDIT_ACTION_BULK = "Bulk Upload : ";
      
        public const int ASSET_CLASS_NON_IT = 1;
        public const int ASSET_CLASS_IT = 2;
        public const int ASSET_CLASS_OTHERS = 3;

        public const int ASSIGNMENT_STATUS_WT_APPROVAL = 1;
        public const int ASSIGNMENT_STATUS_WT_ACCEPTANCE = 2;
        public const int ASSIGNMENT_STATUS_ACCEPTED = 3;
        public const int ASSIGNMENT_STATUS_FOR_TRANSFER = 4;
        public const int ASSIGNMENT_STATUS_REJECTED = 5;
        public const int ASSIGNMENT_STATUS_RELEASED = 6;
        public const int ASSIGNMENT_STATUS_DENIED = 7;

        public const int ASSET_STATUS_NEW = 1;
        public const int ASSET_STATUS_FORASSIGNMENT = 2;
        public const int ASSET_STATUS_WITH_MIS = 3;
        public const int ASSET_STATUS_ASSIGNED = 4;
        public const int ASSET_STATUS_DECOMMISSIONED = 5;
        public const int ASSET_STATUS_FORTRANSFER = 6;
        public const int ASSET_STATUS_FORRELEASE = 7;
        public const int ASSET_STATUS_FORREPAIR = 8;
        public const int ASSET_STATUS_RELEASED = 9;

        public const int RELEASE_REASON_FOR_REASSIGNMENT = 1;
        public const int RELEASE_REASON_FOR_REPAIR = 2;
        public const int RELEASE_REASON_FOR_DISPOSAL = 3;
        public const int RELEASE_REASON_FOR_CORRECTION = 4;
        public const int RELEASE_REASON_OTHERS = 5;

        public const string ASSET_STATUS = "FA";
        public const string ASSIGN_STATUS = "AA";

        public const string EXISTS = "EXISTS";
        public const string MISSING_ID = "MISSING ID";
        public const string MISSING_CD = "MISSING CODE";
        public const string NOT_AVAILABLE = "NOT AVAILABLE";

        public const int RETURN_VAL_SUCCESS = 0;
        public const int RETURN_VAL_NOT_FOUND = -1;
        public const int RETURN_VAL_FAILED = -2;
        public const int RETURN_VAL_DUPLICATE = -3;
        public const int REURN_VAL_NOT_AVAILABLE = -4;

        public static int ACCESS_LVL_ADMIN = 1;
        public static int ACCESS_LVL_MANAGER = 2;
        public static int ACCESS_LVL_MIS = 3;
        public static int ACCESS_LVL_APPADMIN = 4;


        public static string SECRET = "12A!45cd"; //must be 8 characters

        public const string EMAIL_RECEIPIENT_NAME = "{{RECEIPIENT_NAME}}";
        public const string EMAIL_HEADER = "{{HEADER}}";
        public const string EMAIL_FOOTER = "{{FOOTER}}";
        public const string EMAIL_USERNAME = "{{USERNAME}}";
        public const string EMAIL_PASSWORD = "{{PASSWORD}}";
        public const string EMAIL_BULKUPLOAD_LOG = "{{BULKUPLOAD_LOG}}";
        public const string EMAIL_BULKUPLOAD_INFO = "{{BULKUPLOAD_INFO}}";
        public const string EMAIL_BULKUPLOAD_SUMMARY = "{{BULKUPLOAD_SUMMARY}}";
        public const string EMAIL_APPADMIN_EMAIL = "{{APPADMIN_EMAIL}}";
        public const string EMAIL_APPADMIN_TELNO = "{{APPADMIN_TELNO}}";

        public const int CONFIG_ACTIVEEMAIL = 1;
        public const int CONFIG_MAILSERVER = 3;
        public const int CONFIG_MAILPORT = 4;
        public const int CONFIG_MAILUSER = 5;
        public const int CONFIG_MAILPASSWORD = 6;
        public const int CONFIG_MAILSSL = 7;
        public const int CONFIG_ALTMAILSERVER = 8;
        public const int CONFIG_ALTMAILPORT = 9;
        public const int CONFIG_ALTMAILUSER = 10;
        public const int CONFIG_ALTMAILPASSWORD = 11;
        public const int CONFIG_ALTMAILSSL = 12;
        public const int CONFIG_ADMINEMAIL = 15;
        public const int CONFIG_ADMINTEL = 16;
        public const int CONFIG_EMAILHEADER = 17;
        public const int CONFIG_EMAILFOOTER = 18;

        public const string EMAIL_COMPLEX_SUBJECT = "FASTrack : [XXX] - [YYY]";
        public const string EMAIL_SIMPLE_SUBJECT = "FASTrack : [XXX]";


        public const int BULKUPLOAD_STATUS_DONE = 1;
        public const int BULKUPLOAD_STATUS_NEW = 0;
        public const int BULKUPLOAD_STATUS_INPROGRESS = -1;
        public const int BULKUPLOAD_STATUS_NOTFOUND = -2;
        public const int BULKUPLOAD_STATUS_FAILED = -3;

        public const int BULKIPLOAD_TYPE_ASSET = 1;
        public const int BULKIPLOAD_TYPE_ASSIGNMENT= 2;

        public const string SUCCESSFUL = "SUCCESSFUL";
        public const string FAILURE = "FAILURE";

        /***************  FASTWEB Constants Only **********************/

        public const string SESSION_EMAIL_CONFIG = "EMAIL_CONFIGS";
        public const string SESSION_MYDEPARTMENT = "MY_DEPARTMENT";
        public const string TMPDATA_RESULT = "Result";
        public const string TMPDATA_SOURCE = "Source";
        public const string TMPDATA_EXTRAMESSAGE = "ExtraMessage";
        public const string TMPDATA_ACTION = "Action";
        public const string TMPDATA_CONTROLLER = "Controller";

        /************************************************************/


    }
}
