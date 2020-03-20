using Librarian.Core.DataTransfertObject.UseCases.Authors;

namespace Librarian.Core.UseCases
{
    public interface IAuthorsUseCasesProvider
    {
        IGetAuthorByIdUseCase GetById { get; set; }
        IGetAuthorsUseCase GetList { get; set; }
        ICreateAuthorUseCase Create { get; set; }
        IUpdateAuthorUseCase Update { get; set; }
        IDeleteAuthorUseCase Delete { get; set; }
        IGetAuthorsByFiltersUseCase GetAuthorsByFilters { get; set; }
    }
}
