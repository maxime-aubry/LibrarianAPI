using Librarian.Core.DataTransfertObject.UseCases.Users;

namespace Librarian.Core.UseCases
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
