using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Readers.GetReaderById
{
    public interface IGetReaderByIdUseCase : IUseCaseRequestHandler<GetReaderByIdRequest, UseCaseResponseMessage<Reader>>
    {
    }
}
