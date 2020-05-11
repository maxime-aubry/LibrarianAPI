using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBooksByFiltersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>>
    {
        public GetBooksByFiltersRequest(string title, IEnumerable<EBookCategory> categories, IEnumerable<string> authorIds)
        {
            this.Title = title;
            this.Categories = categories;
            this.AuthorIds = authorIds;
        }

        public string Title { get; set; }
        public IEnumerable<EBookCategory> Categories { get; set; }
        public IEnumerable<string> AuthorIds { get; set; }
    }
}
