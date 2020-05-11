using Librarian.Core.DataTransfertObject.UseCases.Users;

namespace Librarian.Core.UseCases
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
