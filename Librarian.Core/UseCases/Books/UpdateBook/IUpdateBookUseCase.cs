using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.UpdateBook
{
    public interface IUpdateBookUseCase : IUseCaseRequestHandler<UpdateBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
