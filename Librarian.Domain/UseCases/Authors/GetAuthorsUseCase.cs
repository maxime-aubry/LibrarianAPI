using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class GetAuthorsUseCase : UseCase, IGetAuthorsUseCase
    {
        public GetAuthorsUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetAuthorsRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Author>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Author>> authors = await this.repositories.Author.Get();

                if (!authors.Success)
                    throw new UseCaseException("Authors not found", authors.Errors);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Author>>(authors.Data, true));
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
