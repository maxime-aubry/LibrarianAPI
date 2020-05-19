using Librarian.Core.UseCases.Users.CreateUser;
using Librarian.Core.UseCases.Users.DeleteUser;
using Librarian.Core.UseCases.Users.GetUserById;
using Librarian.Core.UseCases.Users.GetUsers;
using Librarian.Core.UseCases.Users.UpdateUser;

namespace Librarian.Core.UseCases.Users
{
    public interface IUsersUseCasesProvider
    {
        IGetUserByIdUseCase GetById { get; set; }
        IGetUsersUseCase GetList { get; set; }
        ICreateUserUseCase Create { get; set; }
        IUpdateUserUseCase Update { get; set; }
        IDeleteUserUseCase Delete { get; set; }
    }
}
