using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook.GetAuthorsByBookId
{
    public class GetAuthorsByBookIdUseCase : UseCase, IGetAuthorsByBookIdUseCase
    {
        public GetAuthorsByBookIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetAuthorsByBookIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Author>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.repositories.AuthorWritesBooks.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                GateawayResponse<IEnumerable<Author>> authors = await this.repositories.Author.Get();

                if (!authors.Success)
                    throw new UseCaseException("Authors not found", authors.Errors);

                IEnumerable<Author> authorsOfBook = (from awb in properties.Data
                                                        join a in authors.Data on awb.AuthorId equals a.Id
                                                        where awb.BookId == message.BookId
                                                        select a);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Author>>(authorsOfBook, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Author>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
