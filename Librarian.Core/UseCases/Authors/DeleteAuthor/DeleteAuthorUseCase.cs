using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors.DeleteAuthor
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

                IEnumerable<string> awbIds = (from awb in properties.Data
                                              where awb.AuthorId == message.AuthorId
                                              select awb.Id);

                if (awbIds.Any())
                {
                    GateawayResponse<string> deletedProperties = await this.repositories.AuthorWritesBooks.DeleteMany(awbIds);

                    if (!deletedProperties.Success)
                        throw new UseCaseException("Authors not unlinked from books", deletedProperties.Errors);
                }

                GateawayResponse<string> deletedAuthor = await this.repositories.Author.Delete(message.AuthorId);

                if (!deletedAuthor.Success)
                    throw new UseCaseException("Author not deleted", deletedAuthor.Errors);

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
