using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class GetBooksByAuthorIdRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>>
    {
        public GetBooksByAuthorIdRequest(string authorId)
        {
            this.AuthorId = authorId;
        }

        public string AuthorId { get; set; }
    }
}
