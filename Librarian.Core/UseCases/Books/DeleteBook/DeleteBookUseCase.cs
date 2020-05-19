using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books.DeleteBook
{
    public class DeleteBookUseCase : UseCase, IDeleteBookUseCase
    {
        public DeleteBookUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.repositories.AuthorWritesBooks.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                IEnumerable<string> awbIds = (from awb in properties.Data
                                                where awb.BookId == message.BookId
                                                select awb.Id);

                if (awbIds.Any())
                {
                    GateawayResponse<string> deletedProperties = await this.repositories.AuthorWritesBooks.DeleteMany(awbIds);

                    if (!deletedProperties.Success)
                        throw new UseCaseException("Books not unlinked from authors", deletedProperties.Errors);
                }

                GateawayResponse<string> deletedBook = await this.repositories.Author.Delete(message.BookId);

                if (!deletedBook.Success)
                    throw new UseCaseException("Book not deleted", deletedBook.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
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
