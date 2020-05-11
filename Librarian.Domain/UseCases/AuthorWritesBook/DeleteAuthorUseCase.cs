using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public class DeleteAuthorUseCase : UseCase, IDeleteAuthorUseCase
    {
        public DeleteAuthorUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.repositories.AuthorWritesBooks.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                // delete author from this book
                Librarian.Core.Domain.Entities.AuthorWritesBook property = (from awb in properties.Data
                                                                            where awb.BookId == message.BookId
                                                                            && awb.AuthorId == message.AuthorId
                                                                            select awb).SingleOrDefault();
                if (property == null)
                    throw new UseCaseException("Author is not linked to this book", null);

                GateawayResponse<string> deletedProperty = await this.repositories.AuthorWritesBooks.Delete(property.Id);

                if (!deletedProperty.Success)
                    throw new UseCaseException("Book not unlinked to this book", deletedProperty.Errors);

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
