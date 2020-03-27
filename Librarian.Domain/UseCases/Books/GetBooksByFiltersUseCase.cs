using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBooksByFiltersUseCase : IGetBooksByFiltersUseCase
    {
        public GetBooksByFiltersUseCase(
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

        public async Task<bool> Handle(GetBooksByFiltersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>> outputPort)
        {
            int numberOfPoints = (1 + message.Categories.Count() + message.AuthorIds.Count());

            void SetPertinence(FindBooksByFilters result)
            {
                int numberOfPointsForItem = 0;

                // check title
                if (result.Title.ToLower().Contains(message.Title.ToLower()))
                    numberOfPointsForItem++;

                // check categories
                foreach (int category in message.Categories)
                    if (result.Categories.Select(c => (int)c).Contains(category))
                        numberOfPointsForItem++;

                // check authors
                foreach (string authorId in message.AuthorIds)
                    if (result.Authors.Select(a => a.Id).Contains(authorId))
                        numberOfPointsForItem++;

                result.Pertinence = ((float)numberOfPointsForItem / numberOfPoints);
            }

            try
            {
                GateawayResponse<IEnumerable<Book>> books = await this.bookRepository.Get();

                if (!books.Success)
                    throw new UseCaseException("Books not found", books.Errors);

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                GateawayResponse<IEnumerable<Author>> authors = await this.authorRepository.Get();

                if (!authors.Success)
                    throw new UseCaseException("Authors not found", authors.Errors);

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.readerLoansBookRepository.Get();

                if (!loans.Success)
                    throw new UseCaseException("Loans not found", loans.Errors);

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>> rates = await this.readerRatesBookRepository.Get();

                if (!rates.Success)
                    throw new UseCaseException("Rates not found", rates.Errors);

                IEnumerable<FindBooksByFilters> filteredBooks = (from b in books.Data
                                                                 join awb in properties.Data on b.Id equals awb.BookId
                                                                 join a in authors.Data on awb.AuthorId equals a.Id into authorsGroup
                                                                 join rlb in loans.Data on b.Id equals rlb.BookId into loansGroup
                                                                 join rrb in rates.Data on b.Id equals rrb.BookId into ratesGroup
                                                                 select new FindBooksByFilters
                                                                 (
                                                                     b.Id,
                                                                     b.Title,
                                                                     b.Categories.Select(c => (EBookCategory)Enum.ToObject(typeof(EBookCategory), c)).ToList(),
                                                                     b.RealeaseDate,
                                                                     authorsGroup.Select(author => new AuthorOfBook(author.Id, author.FirstName, author.LastName)).ToList(),
                                                                     0,
                                                                     loansGroup.Any() ? loansGroup.Count() : 0,
                                                                     ratesGroup.Any() ? ratesGroup.Select(r => r.Rate).Average() : 0
                                                                 )).ToList();

                foreach (FindBooksByFilters book in filteredBooks)
                    SetPertinence(book);
                filteredBooks = filteredBooks.Where(b => b.Pertinence > 0).ToList();

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>(filteredBooks, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
