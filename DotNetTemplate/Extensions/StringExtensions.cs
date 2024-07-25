using System.Net.Mail;

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
}