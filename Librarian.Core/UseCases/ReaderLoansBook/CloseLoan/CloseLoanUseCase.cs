using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook.CloseLoan
{
    public class CloseLoanUseCase : UseCase, ICloseLoanUseCase
    {
        public CloseLoanUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CloseLoanRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Librarian.Core.Domain.Entities.ReaderLoansBook> loan = await this.repositories.ReaderLoansBook.Get(message.LoanId);

                if (!loan.Success)
                    throw new UseCaseException("Loan not found", loan.Errors);

                if (loan.Data.EndDateOfLoaning != null)
                    throw new UseCaseException("Loan is already closed", null);

                loan.Data.EndDateOfLoaning = DateTime.UtcNow.Date;
                loan.Data.IsLost = false;

                GateawayResponse<string> loanId = await this.repositories.ReaderLoansBook.Update(message.LoanId, loan.Data);

                if (!loanId.Success)
                    throw new UseCaseException("Loan not saved", null);

                outputPort.Handle(new UseCaseResponseMessage<string>(loanId.Data, true));
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
