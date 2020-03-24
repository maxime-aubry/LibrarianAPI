using Librarian.Core.DataTransfertObject;
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
            if (!string.IsNullOrEmpty(message.ReaderId) &&
                !string.IsNullOrEmpty(message.BookId))
            {
                try
                {
                    IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook> loans = (from rlb in await this.readerLoansBookRepository.Get()
                                                                                         where rlb.ReaderId == message.ReaderId
                                                                                         && rlb.BookId == message.BookId
                                                                                         && rlb.EndDateOfLoaning == null
                                                                                         select rlb);

                    if (loans.Any())
                        throw new Exception("Reader has already loaned this book");

                    Librarian.Core.Domain.Entities.ReaderLoansBook loan = new Librarian.Core.Domain.Entities.ReaderLoansBook(message.ReaderId, message.BookId, DateTime.UtcNow.Date);
                    string loanId = await this.readerLoansBookRepository.Add(loan);

                    if (string.IsNullOrEmpty(loanId))
                        throw new Exception("Loan not saved");

                    outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
                    return true;
                }
                catch (Exception e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message));
                }
            }

            return false;
        }
    }
}
