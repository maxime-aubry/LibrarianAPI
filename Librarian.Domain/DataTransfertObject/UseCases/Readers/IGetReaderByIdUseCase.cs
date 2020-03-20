namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public interface IGetReaderByIdUseCase : IUseCaseRequestHandler<GetReaderByIdRequest, UseCaseResponseMessage<Librarian.Core.Domain.Entities.Reader>>
    {
    }
}
