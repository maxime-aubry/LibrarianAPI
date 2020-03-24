using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class UpdateReaderUseCase : IUpdateReaderUseCase
    {
        public UpdateReaderUseCase(
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

        public async Task<bool> Handle(UpdateReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.ReaderId) &&
                !string.IsNullOrEmpty(message.FirstName) &&
                !string.IsNullOrEmpty(message.LastName) &&
                message.Birthday != null &&
                message.Birthday != DateTime.MinValue)
            {
                try
                {
                    Reader reader = new Reader(message.ReaderId, message.FirstName, message.LastName, message.Birthday, message.IsForbidden);
                    string readerId = await this.readerRepository.Update(message.ReaderId, reader);

                    if (string.IsNullOrEmpty(readerId))
                        throw new Exception("Reader not found");

                    outputPort.Handle(new UseCaseResponseMessage<string>(readerId, true));
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
