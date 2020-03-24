namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBookByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>>
    {
        public GetBookByIdRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
