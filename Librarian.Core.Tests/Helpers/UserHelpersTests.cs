using Librarian.Core.Helpers;
using Xunit;

namespace Librarian.Core.Tests.Helpers
{
    public class UserHelpersTests
    {
        [Fact]
        public void GetLogin_method_provide_generated_logins()
        {
            Assert.Equal("jp.rouve", UserHelpers.GetLogin("Jean Paul", "Rouve"));
            Assert.Equal("jp.rouve", UserHelpers.GetLogin("Jean-Paul", "Rouve"));
            Assert.Equal("m.aubry", UserHelpers.GetLogin("Maxime", "AUBRY"));
        }

        [Fact]
        public void GetLogin_method_does_not_provide_generated_logins()
        {
            Assert.NotEqual("jp.rouve", UserHelpers.GetLogin("JeanPaul", "Rouve"));
        }
    }
}
