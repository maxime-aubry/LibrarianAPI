using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
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
