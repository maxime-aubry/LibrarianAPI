using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook.AddLoan
{
    public class AddLoanUseCase : UseCase, IAddLoanUseCase
    {
        public AddLoanUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(AddLoanRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.repositories.ReaderLoansBook.Get();

                if (!loans.Success)
                    throw new UseCaseException("Loans not found", loans.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook> readerLoans = (from rlb in loans.Data
                                where rlb.ReaderId == message.ReaderId
                                && rlb.BookId == message.BookId
                                && rlb.EndDateOfLoaning == null
                                select rlb);

                if (readerLoans.Any())
                    throw new UseCaseException("Reader has already loaned this book", null);

                Librarian.Core.Domain.Entities.ReaderLoansBook loan = new Librarian.Core.Domain.Entities.ReaderLoansBook(message.ReaderId, message.BookId, DateTime.UtcNow.Date);
                GateawayResponse<string> loanId = await this.repositories.ReaderLoansBook.Add(loan);

                if (!loanId.Success)
                    throw new UseCaseException("Loan not saved", loanId.Errors);

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
