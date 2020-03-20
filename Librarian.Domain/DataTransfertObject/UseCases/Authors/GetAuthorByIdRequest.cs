namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class GetAuthorByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Author>>
    {
        public GetAuthorByIdRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
