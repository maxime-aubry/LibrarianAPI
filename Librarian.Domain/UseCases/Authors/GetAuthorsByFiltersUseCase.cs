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
            int numberOfPoints = (2);

            void SetPertinence(FindAuthorsByFilters result)
            {
                int numberOfPointsForItem = 0;

                // check firstname
                if (!string.IsNullOrEmpty(message.FirstName) && result.FirstName.ToLower().Contains(message.FirstName.ToLower()))
                    numberOfPointsForItem++;

                // check lastname
                if (!string.IsNullOrEmpty(message.LastName) && result.LastName.ToLower().Contains(message.LastName.ToLower()))
                    numberOfPointsForItem++;

                result.Pertinence = ((float)numberOfPointsForItem / numberOfPoints);
            }

            try
            {
                IEnumerable<FindAuthorsByFilters> authors = (from a in await this.authorRepository.Get()
                                                             select new FindAuthorsByFilters
                                                            (
                                                                a.Id,
                                                                a.FirstName,
                                                                a.LastName,
                                                                0,
                                                                (from awb in this.authorWritesBookRepository.Get().Result
                                                                 join l in this.readerLoansBookRepository.Get().Result on awb.BookId equals l.BookId into loans
                                                                 where awb.AuthorId == a.Id
                                                                 select loans.Count()).Sum()
                                                            )).ToList();
                foreach (FindAuthorsByFilters author in authors)
                    SetPertinence(author);
                authors = authors.Where(b => b.Pertinence > 0).ToList();

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(authors, true));
                return true;
            }
            catch (Exception e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(null, false, e.Message));
            }

            return false;
        }
    }
}
