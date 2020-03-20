using System;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public class DeleteLoanRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteLoanRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
