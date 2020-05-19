using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Authors.DeleteAuthor
{
    public class DeleteAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteAuthorRequest(string id)
        {
            this.AuthorId = id;
        }

        public string AuthorId { get; set; }
    }
}
