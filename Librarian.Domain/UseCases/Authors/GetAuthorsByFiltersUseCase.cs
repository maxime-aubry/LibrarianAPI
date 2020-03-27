using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.Domain.Entities;
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
                GateawayResponse<IEnumerable<Author>> authors = await this.authorRepository.Get();

                if (!authors.Success)
                    throw new UseCaseException("Authors not found", authors.Errors);

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> loans = await this.readerLoansBookRepository.Get();

                if (!loans.Success)
                    throw new UseCaseException("Loans not found", loans.Errors);

                IEnumerable<FindAuthorsByFilters> filteredAuthors = (from a in authors.Data
                                                                     select new FindAuthorsByFilters
                                                                    (
                                                                        a.Id, a.FirstName,
                                                                        a.LastName,
                                                                        0,
                                                                        (from awb in properties.Data
                                                                         join l in loans.Data on awb.BookId equals l.BookId into loansGroup
                                                                         where awb.AuthorId == a.Id
                                                                         select loansGroup.Count()).Sum()
                                                                    )).ToList();
                foreach (FindAuthorsByFilters author in filteredAuthors)
                    SetPertinence(author);
                filteredAuthors = filteredAuthors.Where(b => b.Pertinence > 0).ToList();

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(filteredAuthors, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
