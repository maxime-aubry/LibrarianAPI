namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public interface IGetShelfByIdUseCase : IUseCaseRequestHandler<GetShelfByIdRequest, UseCaseResponseMessage<Librarian.Core.Domain.Entities.Shelf>>
    {
    }
}
