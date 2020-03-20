using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
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
        #region Main CRUD Presenters
        private readonly IJsonPresenter<Librarian.Core.Domain.Entities.Book> getBookByIdPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Book>> getBooksPresenter;
        private readonly IJsonPresenter<string> createBookPresenter;
        private readonly IJsonPresenter<string> updateBookPresenter;
        private readonly IJsonPresenter<string> deleteBookPresenter;
        #endregion

        #region Secondaries CRUD Presenters
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>> getBooksByFiltersPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Author>> getAuthorsPresenter;
        private readonly IJsonPresenter<IEnumerable<string>> addAuthorsPresenter;
        private readonly IJsonPresenter<string> deleteAuthorsPresenter;
        private readonly IJsonPresenter<string> ratesBookPresenter;
        #endregion

        private readonly IUseCasesProvider useCasesProvider;

        public BooksController(
            IJsonPresenter<Librarian.Core.Domain.Entities.Book> getBookByIdPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Book>> getBooksPresenter,
            IJsonPresenter<string> createBookPresenter,
            IJsonPresenter<string> updateBookPresenter,
            IJsonPresenter<string> deleteBookPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>> getBooksByFiltersPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Author>> getAuthorsPresenter,
            IJsonPresenter<IEnumerable<string>> addAuthorsPresenter,
            IJsonPresenter<string> deleteAuthorsPresenter,
            IJsonPresenter<string> ratesBookPresenter,
            IUseCasesProvider useCasesProvider)
        {
            this.getBookByIdPresenter = getBookByIdPresenter;
            this.getBooksPresenter = getBooksPresenter;
            this.createBookPresenter = createBookPresenter;
            this.updateBookPresenter = updateBookPresenter;
            this.deleteBookPresenter = deleteBookPresenter;
            this.getBooksByFiltersPresenter = getBooksByFiltersPresenter;
            this.getAuthorsPresenter = getAuthorsPresenter;
            this.addAuthorsPresenter = addAuthorsPresenter;
            this.deleteAuthorsPresenter = deleteAuthorsPresenter;
            this.ratesBookPresenter = ratesBookPresenter;
            this.useCasesProvider = useCasesProvider;
        }

        #region Main CRUD Methods
        [HttpGet("getById/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.BooksController_forgotten_book_id_on_request);

            await this.useCasesProvider.Books.GetById.Handle(new GetBookByIdRequest(bookId), this.getBookByIdPresenter);
            return this.getBookByIdPresenter.ContentResult;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            await this.useCasesProvider.Books.GetList.Handle(new GetBooksRequest(), this.getBooksPresenter);
            return this.getBooksPresenter.ContentResult;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateBookViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Books.Create.Handle(new CreateBookRequest(viewmodel.Title, viewmodel.Categories, viewmodel.ReleaseDate, viewmodel.NumberOfCopies, viewmodel.ShelfId), this.createBookPresenter);
            return this.createBookPresenter.ContentResult;
        }

        [HttpPut("update/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string bookId, [FromBody] CreateBookViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Books.Update.Handle(new UpdateBookRequest(bookId, viewmodel.Title, viewmodel.Categories, viewmodel.ReleaseDate, viewmodel.NumberOfCopies, viewmodel.ShelfId), this.updateBookPresenter);
            return this.updateBookPresenter.ContentResult;
        }

        [HttpDelete("delete/{bookId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.BooksController_forgotten_book_id_on_request);

            await this.useCasesProvider.Books.Delete.Handle(new DeleteBookRequest(bookId), this.deleteBookPresenter);
            return this.deleteBookPresenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("search")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByFilters(
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "categories")] List<EBookCategory> categories,
            [FromQuery(Name = "authorIds")] List<string> authorIds
        )
        {
            await this.useCasesProvider.Books.GetBooksByFilters.Handle(new GetBooksByFiltersRequest(title, categories, authorIds), this.getBooksByFiltersPresenter);
            return this.getBooksByFiltersPresenter.ContentResult;
        }

        [HttpGet("authors")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthors(string bookId)
        {
            await this.useCasesProvider.BooksOfAuthors.GetAuthors.Handle(new GetAuthorsByBookIdRequest(bookId), this.getAuthorsPresenter);
            return this.getAuthorsPresenter.ContentResult;
        }

        [HttpPost("authors/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthors([FromBody] AddAuthorsToBookViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.BooksOfAuthors.AddAuthors.Handle(new AddAuthorsRequest(viewmodel.BookId, viewmodel.AuthorIds), this.addAuthorsPresenter);
            return this.addAuthorsPresenter.ContentResult;
        }

        [HttpPost("authors/delete")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthors([FromBody] DeleteAuthorsOfBookViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.BooksOfAuthors.DeleteAuthors.Handle(new DeleteAuthorsRequest(viewmodel.BookId, viewmodel.AuthorIds), this.deleteAuthorsPresenter);
            return this.deleteAuthorsPresenter.ContentResult;
        }

        [HttpPost("rating/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RateBook([FromBody] CreateRateBookViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.ReadersRates.AddRate.Handle(new AddRateRequest(viewmodel.ReaderId, viewmodel.BookId, viewmodel.Rate, viewmodel.Commment), this.ratesBookPresenter);
            return this.ratesBookPresenter.ContentResult;
        }
        #endregion
    }
}