using HexagonalArchitecture.Core.DataTransfertObject;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.ReaderRatesBook.GetRates
{
    public class GetRatesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>>
    {
        public GetRatesRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
