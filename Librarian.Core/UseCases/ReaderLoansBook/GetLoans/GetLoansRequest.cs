using HexagonalArchitecture.Core.DataTransfertObject;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.ReaderLoansBook.GetLoans
{
    public class GetLoansRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>>
    {
        public GetLoansRequest(string readerId)
        {
            this.ReaderId = readerId;
        }

        public string ReaderId { get; set; }
    }
}
