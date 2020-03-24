using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class GetAuthorsByFiltersUseCase : IGetAuthorsByFiltersUseCase
    {
        public GetAuthorsByFiltersUseCase(
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

        public async Task<bool> Handle(GetAuthorsByFiltersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.FirstName) &&
                !string.IsNullOrEmpty(message.LastName))
            {
                int numberOfPoints = (2);

                void SetPertinence(FindAuthorsByFilters result)
                {
                    int numberOfPointsForItem = 0;

                    // check firstname
                    if (result.FirstName.ToLower().Contains(message.FirstName.ToLower()))
                        numberOfPointsForItem++;

                    // check lastname
                    if (result.LastName.ToLower().Contains(message.LastName.ToLower()))
                        numberOfPointsForItem++;

                    result.Pertinence = ((float)numberOfPointsForItem / numberOfPoints);
                }

                try
                {

                    IEnumerable<FindAuthorsByFilters> authors = (from a in await this.authorRepository.Get()
                                                               join awb in await this.authorWritesBookRepository.Get() on a.Id equals awb.AuthorId
                                                               join b in await this.bookRepository.Get() on awb.BookId equals b.Id
                                                               join l in await this.readerLoansBookRepository.Get() on b.Id equals l.BookId into loans
                                                               select new FindAuthorsByFilters
                                                               (
                                                                   a.Id,
                                                                   a.FirstName,
                                                                   a.LastName,
                                                                   0,
                                                                   loans?.Count() ?? 0
                                                               ));
                    foreach (FindAuthorsByFilters author in authors)
                        SetPertinence(author);
                    authors = authors.Where(b => b.Pertinence > 0);

                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(authors, true));
                    return true;
                }
                catch (Exception e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(null, false, e.Message));
                }
            }

            return false;
        }
    }
}
