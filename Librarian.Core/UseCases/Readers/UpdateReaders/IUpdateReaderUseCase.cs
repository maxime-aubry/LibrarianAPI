using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Readers.UpdateReader
{
    public interface IUpdateReaderUseCase : IUseCaseRequestHandler<UpdateReaderRequest, UseCaseResponseMessage<string>>
    {
    }
}
