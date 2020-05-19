using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Readers.CreateReader
{
    public interface ICreateReaderUseCase : IUseCaseRequestHandler<CreateReaderRequest, UseCaseResponseMessage<string>>
    {
    }
}
