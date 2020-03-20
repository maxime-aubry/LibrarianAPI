using System;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public class CloseLoanRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CloseLoanRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
