using System.Linq;
using System.Text.RegularExpressions;

namespace Librarian.Core.Helpers
{
    public static class UserHelpers
    {
        public static string GetLogin(string firstName, string lastName)
        {
            string cleanFirstName = string.Join("", Regex.Replace(firstName, @"[^\p{L}]", ",").Split(",").Select(f => f[0].ToString()));
            string cleanLastName = Regex.Replace(lastName, @"[^\p{L}]", "");
            string login = $"{cleanFirstName}.{cleanLastName}".ToLower();
            return login;
        }
    }
}
