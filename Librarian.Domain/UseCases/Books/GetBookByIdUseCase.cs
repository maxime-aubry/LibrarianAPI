using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBookByIdUseCase : UseCase, IGetBookByIdUseCase
    {
        public GetBookByIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetBookByIdRequest message, IOutputPort<UseCaseResponseMessage<Book>> outputPort)
        {
            try
            {
                GateawayResponse<Book> book = await this.repositories.Books.Get(message.BookId);

                if (!book.Success)
                    throw new UseCaseException("Book not found", book.Errors);

                outputPort.Handle(new UseCaseResponseMessage<Book>(book.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<Book>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
