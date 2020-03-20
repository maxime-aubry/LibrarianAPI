using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        public UpdateBookUseCase(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        private readonly IBookRepository bookRepository;

        public async Task<bool> Handle(UpdateBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id) &&
                !string.IsNullOrEmpty(message.Title) &&
                message.CategoryIds != null &&
                message.CategoryIds.Any() &&
                !string.IsNullOrEmpty(message.ShelfId))
            {
                Librarian.Core.Domain.Entities.Book book = new Librarian.Core.Domain.Entities.Book(message.Id, message.Title, message.CategoryIds, message.ReleaseDate, message.NumberOfCopies, message.ShelfId);
                GateawayResponse<string> response = await this.bookRepository.Update(message.Id, book);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
