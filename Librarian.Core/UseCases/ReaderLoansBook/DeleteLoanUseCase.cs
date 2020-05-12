using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class DeleteLoanUseCase : UseCase, IDeleteLoanUseCase
    {
        public DeleteLoanUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteLoanRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<string> deletedLoan = await this.repositories.ReaderLoansBook.Delete(message.LoanId);

                if (!deletedLoan.Success)
                    throw new UseCaseException("Loan not deleted", deletedLoan.Errors);

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
