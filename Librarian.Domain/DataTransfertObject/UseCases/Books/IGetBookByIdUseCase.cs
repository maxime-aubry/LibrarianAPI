using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBookByIdUseCase : IUseCaseRequestHandler<GetBookByIdRequest, UseCaseResponseMessage<Book>>
    {
    }
}
