using Librarian.Core.UseCases.Authors.CreateAuthor;
using Librarian.Core.UseCases.Authors.DeleteAuthor;
using Librarian.Core.UseCases.Authors.GetAuthorById;
using Librarian.Core.UseCases.Authors.GetAuthors;
using Librarian.Core.UseCases.Authors.GetAuthorsByFilters;
using Librarian.Core.UseCases.Authors.UpdateAuthor;

namespace Librarian.Core.UseCases.Authors
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
