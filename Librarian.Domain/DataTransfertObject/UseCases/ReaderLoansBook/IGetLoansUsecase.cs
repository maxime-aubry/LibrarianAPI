using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public interface IGetLoansUseCase : IUseCaseRequestHandler<GetLoansRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>>
    {
    }
}
