using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Shelves.GetShelfById
{
    public interface IGetShelfByIdUseCase : IUseCaseRequestHandler<GetShelfByIdRequest, UseCaseResponseMessage<Shelf>>
    {
    }
}
