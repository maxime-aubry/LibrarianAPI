using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
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
            try
            {
                GateawayResponse<Librarian.Core.Domain.Entities.ReaderLoansBook> loan = await this.readerLoansBookRepository.Get(message.LoanId);

                if (!loan.Success)
                    throw new UseCaseException("Loan not found", loan.Errors);

                if (loan.Data.EndDateOfLoaning != null)
                    throw new UseCaseException("Loan is already closed", null);

                loan.Data.EndDateOfLoaning = DateTime.UtcNow.Date;
                loan.Data.IsLost = true;

                GateawayResponse<string> loanId = await this.readerLoansBookRepository.Update(message.LoanId, loan.Data);

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
