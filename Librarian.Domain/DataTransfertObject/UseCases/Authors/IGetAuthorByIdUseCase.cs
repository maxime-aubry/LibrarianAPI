namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorByIdUseCase : IUseCaseRequestHandler<GetAuthorByIdRequest, UseCaseResponseMessage<Librarian.Core.Domain.Entities.Author>>
    {
    }
}
