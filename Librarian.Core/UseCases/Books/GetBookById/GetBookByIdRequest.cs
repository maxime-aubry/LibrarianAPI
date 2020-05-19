using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Books.GetBookById
{
    public class GetBookByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Book>>
    {
        public GetBookByIdRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
