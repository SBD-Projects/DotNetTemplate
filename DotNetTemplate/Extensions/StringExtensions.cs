using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetTemplate.Extensions;

public static class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var mail = new MailAddress(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static bool IsValidPassword(this string password)
    {
        try
        {
            if (password == null) return false;
            if (password.Length < 8 || password.Length > 26) return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");
            bool hasSpecialChar = Regex.IsMatch(password, @"[\W_]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
        catch (Exception e)
        {
            return false;
        } 
    }
    
    public static string HashPassword(this string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}