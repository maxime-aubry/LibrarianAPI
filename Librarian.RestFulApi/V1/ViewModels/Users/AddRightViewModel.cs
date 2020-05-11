using Librarian.Core.Domain.Enums;

namespace Librarian.RestFulAPI.V1.ViewModels.Users
{
    public class AddRightViewModel
    {
        public string UserId { get; set; }
        public EUserRight UserRight { get; set; }
    }
}
