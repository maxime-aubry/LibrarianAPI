using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public class GetBooksByAuthorIdUseCase : UseCase, IGetBooksByAuthorIdUseCase
    {
        public GetBooksByAuthorIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetBooksByAuthorIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Book>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.repositories.AuthorWritesBooks.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                GateawayResponse<IEnumerable<Book>> books = await this.repositories.Books.Get();

                if (!books.Success)
                    throw new UseCaseException("Books not found", books.Errors);

                IEnumerable<Book> booksOfAuthor = (from awb in properties.Data
                                                    join b in books.Data on awb.BookId equals b.Id
                                                    where awb.AuthorId == message.AuthorId
                                                    select b);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(booksOfAuthor, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
