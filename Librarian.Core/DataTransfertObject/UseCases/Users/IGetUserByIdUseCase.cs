using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public interface IGetUserByIdUseCase : IUseCaseRequestHandler<GetUserByIdRequest, UseCaseResponseMessage<User>>
    {
    }
}
