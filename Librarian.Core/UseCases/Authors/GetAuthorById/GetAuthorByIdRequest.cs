using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Authors.GetAuthorById
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
