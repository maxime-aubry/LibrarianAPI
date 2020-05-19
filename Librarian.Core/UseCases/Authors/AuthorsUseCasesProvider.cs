using Librarian.Core.UseCases.Authors.CreateAuthor;
using Librarian.Core.UseCases.Authors.DeleteAuthor;
using Librarian.Core.UseCases.Authors.GetAuthorById;
using Librarian.Core.UseCases.Authors.GetAuthors;
using Librarian.Core.UseCases.Authors.GetAuthorsByFilters;
using Librarian.Core.UseCases.Authors.UpdateAuthor;

namespace Librarian.Core.UseCases.Authors
{
    public class AuthorsUseCasesProvider : IAuthorsUseCasesProvider
    {
        public AuthorsUseCasesProvider(
            IGetAuthorByIdUseCase getById,
            IGetAuthorsUseCase getList,
            ICreateAuthorUseCase create,
            IUpdateAuthorUseCase update,
            IDeleteAuthorUseCase delete,
            IGetAuthorsByFiltersUseCase getAuthorsByFilters
        )
        {
            this.GetById = getById;
            this.GetList = getList;
            this.Create = create;
            this.Update = update;
            this.Delete = delete;
            this.GetAuthorsByFilters = getAuthorsByFilters;
        }

        public IGetAuthorByIdUseCase GetById { get; set; }
        public IGetAuthorsUseCase GetList { get; set; }
        public ICreateAuthorUseCase Create { get; set; }
        public IUpdateAuthorUseCase Update { get; set; }
        public IDeleteAuthorUseCase Delete { get; set; }
        public IGetAuthorsByFiltersUseCase GetAuthorsByFilters { get; set; }
    }
}
