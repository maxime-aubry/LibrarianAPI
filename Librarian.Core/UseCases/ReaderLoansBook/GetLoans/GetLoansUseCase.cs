using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook.GetLoans
{
    public class GetLoansUseCase : UseCase, IGetLoansUseCase
    {
        public GetLoansUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetLoansRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.repositories.ReaderLoansBook.Get();

                if (!loans.Success)
                    throw new UseCaseException("Loans not found", loans.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook> readerLoans = (from rlb in loans.Data
                                                                                            where rlb.ReaderId == message.ReaderId
                                                                                            select rlb);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>(readerLoans, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
