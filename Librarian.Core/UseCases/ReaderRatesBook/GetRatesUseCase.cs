using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderRatesBook
{
    public class GetRatesUseCase : UseCase, IGetRatesUseCase
    {
        public GetRatesUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetRatesRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>> rates = await this.repositories.ReaderRatesBook.Get();

                if (!rates.Success)
                    throw new UseCaseException("Rates not found", rates.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook> ratesOfBook = (from rrb in rates.Data
                                                                                           where rrb.BookId == message.BookId
                                                                                           select rrb);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>(ratesOfBook, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
