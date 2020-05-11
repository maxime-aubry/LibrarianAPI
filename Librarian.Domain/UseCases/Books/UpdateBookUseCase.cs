using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class UpdateBookUseCase : UseCase, IUpdateBookUseCase
    {
        public UpdateBookUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(UpdateBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Book> existingBook = await this.repositories.Books.Get(message.BookId);

                if (!existingBook.Success)
                    throw new UseCaseException("Book not found", existingBook.Errors);

                Book book = new Book(message.BookId, message.Title, message.CategoryIds, message.ReleaseDate, existingBook.Data.NumberOfCopies, message.ShelfId);
                GateawayResponse<string> bookId = await this.repositories.Books.Update(message.BookId, book);

                if (!bookId.Success)
                    throw new UseCaseException("Book not saved", bookId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(bookId.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
