using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public interface IGetReaderByIdUseCase : IUseCaseRequestHandler<GetReaderByIdRequest, UseCaseResponseMessage<Reader>>
    {
    }
}
