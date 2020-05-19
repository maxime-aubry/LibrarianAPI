using HexagonalArchitecture.Core.DataTransfertObject;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.ReaderLoansBook.GetLoans
{
    public interface IGetLoansUseCase : IUseCaseRequestHandler<GetLoansRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>>
    {
    }
}
