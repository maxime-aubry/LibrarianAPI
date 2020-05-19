using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors.CreateAuthor
{
    public class CreateAuthorUseCase : UseCase, ICreateAuthorUseCase
    {
        public CreateAuthorUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CreateAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                Author author = new Author(message.FirstName, message.LastName);
                GateawayResponse<string> authorId = await this.repositories.Author.Add(author);

                if (!authorId.Success)
                    throw new UseCaseException("Author not saved", authorId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(authorId.Data, true, null, null));
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
