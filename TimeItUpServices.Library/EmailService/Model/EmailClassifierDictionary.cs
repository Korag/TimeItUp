using System.Collections.Generic;

namespace TimeItUpServices.Library.EmailService.Model
{
    public static class EmailClassifierDictionary
    {
        public enum EmailClassifierType
        {
            ResetAccountPassword
        }

        public static readonly Dictionary<string, string> EmailTopic = new Dictionary<string, string>
         {
           {EmailClassifierType.ResetAccountPassword.ToString(), "Data Analysis System - Resetting the system password."},
        };

        public static readonly Dictionary<string, string> HeaderOfEmailContent = new Dictionary<string, string>
         {
           {EmailClassifierType.ResetAccountPassword.ToString(), "Reset your user account password."},
        };

        public static readonly Dictionary<string, string> PrimaryContent = new Dictionary<string, string>
         {
           {EmailClassifierType.ResetAccountPassword.ToString(), "We have just registered a request to reset the password of the user account associated with this email address. If you wish to reset your password please click on the button below."},
        };

        public static readonly Dictionary<string, string> SecondaryContent = new Dictionary<string, string>
         {
           {EmailClassifierType.ResetAccountPassword.ToString(), "Did you not give instructions to reset your account password? It may be that someone is in possession of your email address and has attempted to do so. Ignore this message or contact the service administrator."},
        };

        public static readonly Dictionary<string, string> URLActionText = new Dictionary<string, string>
         {
           {EmailClassifierType.ResetAccountPassword.ToString(), "Reset your password"},
        };
    }
}