using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorsUseCase : IUseCaseRequestHandler<GetAuthorsRequest, UseCaseResponseMessage<IEnumerable<Author>>>
    {
    }
}
