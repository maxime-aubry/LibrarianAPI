using Librarian.Core.DataTransfertObject;
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
                IEnumerable<FindBooksByFilters> books = (from b in await this.bookRepository.Get()
                                                    join awb in await this.authorWritesBookRepository.Get() on b.Id equals awb.BookId
                                                    join a in await this.authorRepository.Get() on awb.AuthorId equals a.Id into authors
                                                    join rlb in await this.readerLoansBookRepository.Get() on b.Id equals rlb.BookId into loans
                                                    join rrb in await this.readerRatesBookRepository.Get() on b.Id equals rrb.BookId into rates
                                                    select new FindBooksByFilters
                                                    (
                                                        b.Id,
                                                        b.Title,
                                                        b.Categories.Select(c => (EBookCategory)Enum.ToObject(typeof(EBookCategory), c)).ToList(),
                                                        b.RealeaseDate,
                                                        authors.Select(author => new AuthorOfBook(author.Id, author.FirstName, author.LastName)).ToList(),
                                                        0,
                                                        loans.Any() ? loans.Count() : 0,
                                                        rates.Any() ? rates.Select(r => r.Rate).Average() : 0
                                                    )).ToList();

                foreach (FindBooksByFilters book in books)
                    SetPertinence(book);
                books = books.Where(b => b.Pertinence > 0).ToList();

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>(books, true));
                return true;
            }
            catch (Exception e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>(null, false, e.Message));
            }

            return false;
        }
    }
}
