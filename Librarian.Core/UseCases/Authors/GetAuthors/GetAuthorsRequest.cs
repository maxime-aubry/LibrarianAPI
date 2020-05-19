using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Authors.GetAuthors
{
    public class GetAuthorsRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Author>>>
    {
        public GetAuthorsRequest()
        {
        }
    }
}
