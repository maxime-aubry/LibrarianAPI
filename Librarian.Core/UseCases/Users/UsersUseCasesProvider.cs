using Librarian.Core.UseCases.Users.CreateUser;
using Librarian.Core.UseCases.Users.DeleteUser;
using Librarian.Core.UseCases.Users.GetUserById;
using Librarian.Core.UseCases.Users.GetUsers;
using Librarian.Core.UseCases.Users.UpdateUser;

namespace Librarian.Core.UseCases.Users
{
    public class UsersUseCasesProvider : IUsersUseCasesProvider
    {
        public UsersUseCasesProvider(
            IGetUserByIdUseCase getById,
            IGetUsersUseCase getList,
            ICreateUserUseCase create,
            IUpdateUserUseCase update,
            IDeleteUserUseCase delete
        )
        {
            this.GetById = getById;
            this.GetList = getList;
            this.Create = create;
            this.Update = update;
            this.Delete = delete;
        }

        public IGetUserByIdUseCase GetById { get; set; }
        public IGetUsersUseCase GetList { get; set; }
        public ICreateUserUseCase Create { get; set; }
        public IUpdateUserUseCase Update { get; set; }
        public IDeleteUserUseCase Delete { get; set; }
    }
}
