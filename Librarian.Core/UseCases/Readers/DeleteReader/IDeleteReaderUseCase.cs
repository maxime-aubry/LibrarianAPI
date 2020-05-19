using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Readers.DeleteReader
{
    public interface IDeleteReaderUseCase : IUseCaseRequestHandler<DeleteReaderRequest, UseCaseResponseMessage<string>>
    {
    }
}
