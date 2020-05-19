using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors.GetAuthorById
{
    public class GetAuthorByIdUseCase : UseCase, IGetAuthorByIdUseCase
    {
        public GetAuthorByIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetAuthorByIdRequest message, IOutputPort<UseCaseResponseMessage<Author>> outputPort)
        {
            try
            {
                GateawayResponse<Author> author = await this.repositories.Author.Get(message.AuthorId);

                if (!author.Success)
                    throw new UseCaseException("Author not found", author.Errors);

                outputPort.Handle(new UseCaseResponseMessage<Author>(author.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<Author>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
