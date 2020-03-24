using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public interface IGetShelfByIdUseCase : IUseCaseRequestHandler<GetShelfByIdRequest, UseCaseResponseMessage<Shelf>>
    {
    }
}
