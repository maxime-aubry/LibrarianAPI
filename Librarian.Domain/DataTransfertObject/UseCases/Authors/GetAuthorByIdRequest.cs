using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class GetAuthorByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Author>>
    {
        public GetAuthorByIdRequest(string authorId)
        {
            this.AuthorId = authorId;
        }

        public string AuthorId { get; set; }
    }
}
