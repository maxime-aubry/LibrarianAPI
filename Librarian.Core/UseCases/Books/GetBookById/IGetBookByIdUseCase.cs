using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Books.GetBookById
{
    public interface IGetBookByIdUseCase : IUseCaseRequestHandler<GetBookByIdRequest, UseCaseResponseMessage<Book>>
    {
    }
}
