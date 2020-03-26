using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class CloseLoanAndDeclareAsLostUseCase : ICloseLoanAndDeclareAsLostUseCase
    {
        public CloseLoanAndDeclareAsLostUseCase(
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

        public async Task<bool> Handle(CloseLoanAndDeclareAsLostRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.LoanId))
            {
                try
                {
                    Librarian.Core.Domain.Entities.ReaderLoansBook loan = await this.readerLoansBookRepository.Get(message.LoanId);

                    if (loan == null)
                        throw new Exception("Loan not found");

                    if (loan.EndDateOfLoaning != null)
                        throw new Exception("Loan is already closed");

                    loan.EndDateOfLoaning = DateTime.UtcNow.Date;
                    loan.IsLost = true;

                    string loanId = await this.readerLoansBookRepository.Update(message.LoanId, loan);

                    if (string.IsNullOrEmpty(loanId))
                        throw new Exception("Loan not saved");

                    outputPort.Handle(new UseCaseResponseMessage<string>(loanId, true));
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
