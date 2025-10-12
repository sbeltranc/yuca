using System.Text.RegularExpressions;

namespace Shared.Services
{
    public static class PasswordValidator
    {
        public static (bool, string) Validate(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return (false, "Password must be at least 8 characters long.");
            }

            // Check for at least one number
            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                return (false, "Password must contain at least one number.");
            }

            // Check for at least one special character
            if (!Regex.IsMatch(password, @"[!@#$%^&*()]"))
            {
                return (false, "Password must contain at least one special character.");
            }

            return (true, "Password is valid.");
        }
    }
}
