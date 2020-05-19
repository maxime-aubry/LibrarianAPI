using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers.GetReaders
{
    public class GetReadersUseCase : UseCase, IGetReadersUseCase
    {
        public GetReadersUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetReadersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Reader>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Reader>> readers = await this.repositories.Reader.Get();

                if (!readers.Success)
                    throw new UseCaseException("Readers not found", readers.Errors);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Reader>>(readers.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Reader>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
