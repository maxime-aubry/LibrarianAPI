using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetAvailableShelvesUseCase : IGetAvailableShelvesUseCase
    {
        public GetAvailableShelvesUseCase(
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


        public async Task<bool> Handle(GetAvailableShelvesRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Shelf>>> outputPort)
        {
            try
            {
                IEnumerable<Shelf> shelves = (from s in await this.shelfRepository.Get()
                                              where s.BookCategory == message.Category
                                              && s.QtyOfRemainingPlaces > 0
                                              //&& (s.QtyOfRemainingPlaces + message.NumberOfCopies) <= s.MaxQtyOfBooks
                                              select s);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(shelves, true));
                return true;
            }
            catch (Exception e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(null, false, e.Message));
            }

            return false;
        }
    }
}
