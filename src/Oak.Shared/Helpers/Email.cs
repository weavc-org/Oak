using System;

namespace Oak.Shared.Helpers
{
    /// <summary>
    /// Validate an email address
    /// </summary>
    public static class IsEmail
    {
        public static bool Validate(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}