namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class DeleteAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteAuthorRequest(string bookId, string authorId)
        {
            this.BookId = bookId;
            this.AuthorId = authorId;
        }

        public string BookId { get; set; }
        public string AuthorId { get; set; }
    }
}
