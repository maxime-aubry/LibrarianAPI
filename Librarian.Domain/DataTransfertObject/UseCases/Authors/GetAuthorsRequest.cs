using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class GetAuthorsRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>>
    {
        public GetAuthorsRequest()
        {
        }
    }
}
