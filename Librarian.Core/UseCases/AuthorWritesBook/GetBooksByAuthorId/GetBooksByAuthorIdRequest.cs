using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.AuthorWritesBook.GetBooksByAuthorId
{
    public class GetBooksByAuthorIdRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Book>>>
    {
        public GetBooksByAuthorIdRequest(string authorId)
        {
            this.AuthorId = authorId;
        }

        public string AuthorId { get; set; }
    }
}
