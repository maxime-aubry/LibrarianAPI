using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers.DeleteReader
{
    public class DeleteReaderUseCase : UseCase, IDeleteReaderUseCase
    {
        public DeleteReaderUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.repositories.ReaderLoansBook.Get();

                if (!loans.Success)
                    throw new UseCaseException("Loans not found", loans.Errors);

                IEnumerable<string> rlbIds = (from rlb in loans.Data
                                                where rlb.ReaderId == message.ReaderId
                                                select rlb.Id);

                if (rlbIds.Any())
                {
                    GateawayResponse<string> deletedLoans = await this.repositories.ReaderLoansBook.DeleteMany(rlbIds);

                    if (!deletedLoans.Success)
                        throw new UseCaseException("Loans not deleted", deletedLoans.Errors);
                }

                GateawayResponse<string> deletedReader = await this.repositories.Reader.Delete(message.ReaderId);

                outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
