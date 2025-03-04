using System.Text.RegularExpressions;

namespace Sixram.Common.Helpers
{
    public static class ExtensionHelper
    {
        public static bool IsValidEmail(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            return regex.IsMatch(s);
        }
    }
}
