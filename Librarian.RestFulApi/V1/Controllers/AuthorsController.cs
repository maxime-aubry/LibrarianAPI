using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.UseCases;
using Librarian.RestFulAPI.Tools.Presenters;
using Librarian.RestFulAPI.V1.ViewModels.Authors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region Main CRUD Presenters
        private readonly IJsonPresenter<Librarian.Core.Domain.Entities.Author> getAuthorByIdPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Author>> getAuthorsPresenter;
        private readonly IJsonPresenter<string> createAuthorPresenter;
        private readonly IJsonPresenter<string> updateAuthorPresenter;
        private readonly IJsonPresenter<string> deleteAuthorPresenter;
        #endregion

        #region Secondaries CRUD Presenters
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>> getAuthorsByFiltersPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Book>> getBooksPresenter;
        #endregion

        private readonly IUseCasesProvider useCasesProvider;

        public AuthorsController(
            IJsonPresenter<Librarian.Core.Domain.Entities.Author> getAuthorByIdPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Author>> getAuthorsPresenter,
            IJsonPresenter<string> createAuthorPresenter,
            IJsonPresenter<string> updateAuthorPresenter,
            IJsonPresenter<string> deleteAuthorPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>> getAuthorsByFiltersPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Book>> getBooksPresenter,
            IUseCasesProvider useCasesProvider)
        {
            this.getAuthorByIdPresenter = getAuthorByIdPresenter;
            this.getAuthorsPresenter = getAuthorsPresenter;
            this.getAuthorsByFiltersPresenter = getAuthorsByFiltersPresenter;
            this.createAuthorPresenter = createAuthorPresenter;
            this.updateAuthorPresenter = updateAuthorPresenter;
            this.deleteAuthorPresenter = deleteAuthorPresenter;
            this.getBooksPresenter = getBooksPresenter;
            this.useCasesProvider = useCasesProvider;
        }

        #region Main CRUD Methods
        [HttpGet("getById/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string authorId)
        {
            if (string.IsNullOrEmpty(authorId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.AuthorsController_forgotten_author_id_on_request);

            await this.useCasesProvider.Authors.GetById.Handle(new GetAuthorByIdRequest(authorId), this.getAuthorByIdPresenter);
            return this.getAuthorByIdPresenter.ContentResult;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            await this.useCasesProvider.Authors.GetList.Handle(new GetAuthorsRequest(), this.getAuthorsPresenter);
            return this.getAuthorsPresenter.ContentResult;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateAuthorViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Authors.Create.Handle(new CreateAuthorRequest(viewmodel.FirstName, viewmodel.LastName), this.createAuthorPresenter);
            return this.createAuthorPresenter.ContentResult;
        }

        [HttpPut("update/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string authorId, [FromBody] UpdateAuthorViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Authors.Update.Handle(new UpdateAuthorRequest(authorId, viewmodel.FirstName, viewmodel.LastName), this.updateAuthorPresenter);
            return this.updateAuthorPresenter.ContentResult;
        }

        [HttpDelete("delete/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string authorId)
        {
            if (string.IsNullOrEmpty(authorId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.AuthorsController_forgotten_author_id_on_request);

            await this.useCasesProvider.Authors.Delete.Handle(new Core.DataTransfertObject.UseCases.Authors.DeleteAuthorRequest(authorId), this.deleteAuthorPresenter);
            return this.deleteAuthorPresenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("search")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByFilters(
            [FromQuery(Name = "firstName")] string firstName,
            [FromQuery(Name = "lastName")] string lastName
        )
        {
            await this.useCasesProvider.Authors.GetAuthorsByFilters.Handle(new GetAuthorsByFiltersRequest(firstName, lastName), this.getAuthorsByFiltersPresenter);
            return this.getAuthorsPresenter.ContentResult;
        }

        [HttpGet("books/list/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooks(string authorId)
        {
            await this.useCasesProvider.BooksOfAuthors.GetBooks.Handle(new GetBooksByAuthorIdRequest(authorId), this.getBooksPresenter);
            return this.getAuthorsPresenter.ContentResult;
        }
        #endregion
    }
}