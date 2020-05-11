using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
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
