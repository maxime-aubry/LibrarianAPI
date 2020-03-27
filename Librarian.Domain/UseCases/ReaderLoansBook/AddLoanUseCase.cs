using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class AddLoanUseCase : IAddLoanUseCase
    {
        public AddLoanUseCase(
            IAuthorRepository authorRepository,
            IAuthorWritesBookRepository authorWritesBookRepository,
            IBookRepository bookRepository,
            IReaderLoansBookRepository readerLoansBookRepository,
            IReaderRatesBookRepository readerRatesBookRepository,
            IReaderRepository readerRepository,
            IShelfRepository shelfRepository
        )
        {
            this.authorRepository = authorRepository;
            this.authorWritesBookRepository = authorWritesBookRepository;
            this.bookRepository = bookRepository;
            this.readerLoansBookRepository = readerLoansBookRepository;
            this.readerRatesBookRepository = readerRatesBookRepository;
            this.readerRepository = readerRepository;
            this.shelfRepository = shelfRepository;
        }

        private readonly IAuthorRepository authorRepository;
        private readonly IAuthorWritesBookRepository authorWritesBookRepository;
        private readonly IBookRepository bookRepository;
        private readonly IReaderLoansBookRepository readerLoansBookRepository;
        private readonly IReaderRatesBookRepository readerRatesBookRepository;
        private readonly IReaderRepository readerRepository;
        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(AddLoanRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.readerLoansBookRepository.Get();

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
                GateawayResponse<string> loanId = await this.readerLoansBookRepository.Add(loan);

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
