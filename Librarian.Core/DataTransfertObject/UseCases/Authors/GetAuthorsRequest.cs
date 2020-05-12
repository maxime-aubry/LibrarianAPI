using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class GetAuthorsRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Author>>>
    {
        public GetAuthorsRequest()
        {
        }
    }
}
