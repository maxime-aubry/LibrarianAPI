﻿using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class CloseLoanAndDeclareAsLostUseCase : UseCase, ICloseLoanAndDeclareAsLostUseCase
    {
        public CloseLoanAndDeclareAsLostUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CloseLoanAndDeclareAsLostRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Librarian.Core.Domain.Entities.ReaderLoansBook> loan = await this.repositories.ReaderLoansBook.Get(message.LoanId);

                if (!loan.Success)
                    throw new UseCaseException("Loan not found", loan.Errors);

                if (loan.Data.EndDateOfLoaning != null)
                    throw new UseCaseException("Loan is already closed", null);

                loan.Data.EndDateOfLoaning = DateTime.UtcNow.Date;
                loan.Data.IsLost = true;

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
