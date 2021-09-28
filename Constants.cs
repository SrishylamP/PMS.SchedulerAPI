using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.SchedulerAPI
{
    public static class Constants
    {

        public const string DefaultPasswordSentSuccessfully = "A default password has been sent on this registered mail id.";
        public const string EmailAndEmpIdExists = "Email Id and  Employee Id are  already Exists";
        public const string DuplicateEmpIdExists = "Duplicate Employee ID exists. Please contact the helpdesk or call on 123456 for more information.";
        public const string EmailAlreadyRegistered = "Email is already registered";
        public const string Dummy = "";
        public const string PasswordSentSuccessfully = "Password sent successfully";
        public const string LoginUserLocked = "Your account has been locked. Please contact the hospital administrator or call helpdesk on 123456 for more information.";
        public const string InvalidLoginCredentials ="Invalid Credentials";
        public const string LOGINSUCCESSFULL = "Welcome to CT General Hospital:";
        public const string OldPasswordIsInvalid = "Old password is invalid";
        public const string NewPasswordOldPasswordSame = "Your old password and new password are same.";
        public const string PasswordChangedSuccessfully = "Password Changed successfully";
        public const string FromEmail = "sendtestmail15@gmail.com";
        public const string BlockedSubject = "Account Blocked";
        public const string BlockedMessage = "Your account has been blocked. Please contact the hospital administrator or call helpdesk on 123456 for more information.";
        public const string RegisterSubject = "Default password link";
        public const string RegisterMessage = "Your registered email and default password is : ";
        public const string ActivateUserMessage = "Your account has been activated. your one time password is : ";
        public const string ActivateUserSubject = "Activate your account";
        public const string AppointmentSubject = "Appointment Scheduled ";
        public const string AppointmentBookedSuccessfully = "Your appointment booked successfully ";
        public const string AppointmentRescheduledSuccessfully = "Your appointment rescheduled successfully ";
        public const string AppointmentCancelledSuccessfully = "Your appointment is successfully canceled ";
        public const string AppointmentCancelledSubject = "Appointment Cancelled ";
        public const string EditAppointmentSubject = "Appointment Rescheduled";



        public const string ModelIsEmpty = "Model is empty";
        public const string UpdatedSuccessfully = "Updated successfully";

        //Audit operations
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";

    }
}
