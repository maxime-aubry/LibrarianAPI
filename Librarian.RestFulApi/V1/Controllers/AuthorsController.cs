using HexagonalArchitecture.Core.Presenters;
using Librarian.Core.Domain.Entities;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.Authors.CreateAuthor;
using Librarian.Core.UseCases.Authors.DeleteAuthor;
using Librarian.Core.UseCases.Authors.GetAuthorById;
using Librarian.Core.UseCases.Authors.GetAuthors;
using Librarian.Core.UseCases.Authors.GetAuthorsByFilters;
using Librarian.Core.UseCases.Authors.UpdateAuthor;
using Librarian.Core.UseCases.AuthorWritesBook.GetBooksByAuthorId;
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
        public AuthorsController()
        {
        }

        #region Main CRUD Methods
        [HttpGet("getById/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<Author> presenter,
            string authorId
        )
        {
            if (string.IsNullOrEmpty(authorId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.AuthorsController_forgotten_author_id_on_request);

            await useCasesProvider.Authors.GetById.Handle(new GetAuthorByIdRequest(authorId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Author>> presenter
        )
        {
            await useCasesProvider.Authors.GetList.Handle(new GetAuthorsRequest(), presenter);
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
            [FromBody] CreateAuthorViewModel viewmodel
        )
        {
            await useCasesProvider.Authors.Create.Handle(new CreateAuthorRequest(viewmodel.FirstName, viewmodel.LastName), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("update/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] UpdateAuthorViewModel viewmodel,
            string authorId
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Authors.Update.Handle(new UpdateAuthorRequest(authorId, viewmodel.FirstName, viewmodel.LastName), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("delete/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string authorId
        )
        {
            if (string.IsNullOrEmpty(authorId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.AuthorsController_forgotten_author_id_on_request);

            await useCasesProvider.Authors.Delete.Handle(new DeleteAuthorRequest(authorId), presenter);
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
            [FromServices] IJsonPresenter<IEnumerable<FindAuthorsByFilters>> presenter,
            [FromQuery(Name = "firstName")] string firstName,
            [FromQuery(Name = "lastName")] string lastName
        )
        {
            await useCasesProvider.Authors.GetAuthorsByFilters.Handle(new GetAuthorsByFiltersRequest(firstName, lastName), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("books/list/{authorId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooks(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Book>> presenter,
            string authorId
        )
        {
            await useCasesProvider.BooksOfAuthors.GetBooks.Handle(new GetBooksByAuthorIdRequest(authorId), presenter);
            return presenter.ContentResult;
        }
        #endregion
    }
}