using System;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public class CloseLoanAndDeclareAsLostRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CloseLoanAndDeclareAsLostRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
