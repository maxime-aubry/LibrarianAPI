using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.Core.UseCases;
using Librarian.RestFulAPI.Tools.Presenters;
using Librarian.RestFulAPI.V1.ViewModels.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController()
        {
        }

        #region Main CRUD Methods
        [HttpGet("getById/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<Book> presenter,
            string bookId
        )
        {
            if (string.IsNullOrEmpty(bookId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.BooksController_forgotten_book_id_on_request);

            await useCasesProvider.Books.GetById.Handle(new GetBookByIdRequest(bookId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Book>> presenter
        )
        {
            await useCasesProvider.Books.GetList.Handle(new GetBooksRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] CreateBookViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Books.Create.Handle(new CreateBookRequest(viewmodel.Title, viewmodel.Categories, viewmodel.ReleaseDate, viewmodel.NumberOfCopies, viewmodel.ShelfId), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("update/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] UpdateBookViewModel viewmodel,
            string bookId
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Books.Update.Handle(new UpdateBookRequest(bookId, viewmodel.Title, viewmodel.Categories, viewmodel.ReleaseDate, viewmodel.ShelfId), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("delete/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string bookId
        )
        {
            if (string.IsNullOrEmpty(bookId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.BooksController_forgotten_book_id_on_request);

            await useCasesProvider.Books.Delete.Handle(new DeleteBookRequest(bookId), presenter);
            return presenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("search")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByFilters(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<FindBooksByFilters>> presenter,
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "categories")] List<EBookCategory> categories,
            [FromQuery(Name = "authorIds")] List<string> authorIds
        )
        {
            await useCasesProvider.Books.GetBooksByFilters.Handle(new GetBooksByFiltersRequest(title, categories, authorIds), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("authors/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthors(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Author>> presenter,
            string bookId
        )
        {
            await useCasesProvider.BooksOfAuthors.GetAuthors.Handle(new GetAuthorsByBookIdRequest(bookId), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("authors/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthor(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] AddAuthorsToBookViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.BooksOfAuthors.AddAuthor.Handle(new AddAuthorRequest(viewmodel.BookId, viewmodel.AuthorId), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("authors/delete")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] DeleteAuthorsOfBookViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.BooksOfAuthors.DeleteAuthor.Handle(new DeleteAuthorRequest(viewmodel.BookId, viewmodel.AuthorId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("rates/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRates(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<ReaderRatesBook>> presenter,
            string bookId
        )
        {
            if (string.IsNullOrEmpty(bookId))
                return BadRequest(ModelState);

            await useCasesProvider.ReadersRates.GetRates.Handle(new GetRatesRequest(bookId), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("rates/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRate(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] CreateRateBookViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.ReadersRates.AddRate.Handle(new AddRateRequest(viewmodel.ReaderId, viewmodel.BookId, viewmodel.Rate, viewmodel.Commment), presenter);
            return presenter.ContentResult;
        }
        #endregion
    }
}