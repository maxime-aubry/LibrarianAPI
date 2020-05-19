using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors.GetAuthors
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
