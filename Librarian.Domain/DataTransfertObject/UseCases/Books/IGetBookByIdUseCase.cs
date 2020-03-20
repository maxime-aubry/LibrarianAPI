namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBookByIdUseCase : IUseCaseRequestHandler<GetBookByIdRequest, UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>>
    {
    }
}
