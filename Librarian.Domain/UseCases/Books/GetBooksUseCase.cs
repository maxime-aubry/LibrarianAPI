using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBooksUseCase : IGetBooksUseCase
    {
        public GetBooksUseCase(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        private readonly IBookRepository bookRepository;

        public async Task<bool> Handle(GetBooksRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Book>>> outputPort)
        {
            try
            {
                IEnumerable<Book> books = await this.bookRepository.Get();

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(books, true));
                return true;
            }
            catch (Exception e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(null, false, e.Message));
            }

            return false;
        }
    }
}
