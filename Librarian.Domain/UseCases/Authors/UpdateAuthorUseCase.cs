using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class UpdateAuthorUseCase : UseCase, IUpdateAuthorUseCase
    {
        public UpdateAuthorUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(UpdateAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                Author author = new Author(message.AuthorId, message.FirstName, message.LastName);
                GateawayResponse<string> authorId = await this.repositories.Author.Update(message.AuthorId, author);

                if (!authorId.Success)
                    throw new UseCaseException("Author not saved", authorId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(authorId.Data, true));
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
