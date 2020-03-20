namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBookByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>>
    {
        public GetBookByIdRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
