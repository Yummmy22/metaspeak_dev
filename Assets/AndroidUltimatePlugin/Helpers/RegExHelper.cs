using System.Text.RegularExpressions;

namespace Gigadrillgames.AUP.Helpers
{
    public class RegExHelper
    {
        public const string MatchEmailPattern =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
    }
}