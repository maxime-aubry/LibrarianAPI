using HexagonalArchitecture.Core.Presenters;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.Shelves.CreateShelf;
using Librarian.Core.UseCases.Shelves.DeleteShelf;
using Librarian.Core.UseCases.Shelves.GetAvailableShelves;
using Librarian.Core.UseCases.Shelves.GetShelfById;
using Librarian.Core.UseCases.Shelves.GetShelves;
using Librarian.RestFulAPI.V1.ViewModels.Shelves;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        public ShelvesController()
        {
        }

        #region Main CRUD Methods
        [HttpGet("getById/{shelfId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<Shelf> presenter,
            string shelfId
        )
        {
            if (string.IsNullOrEmpty(shelfId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ShelvesController_forgotten_reader_id_on_request);

            await useCasesProvider.Shelves.GetShelfById.Handle(new GetShelfByIdRequest(shelfId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Shelf>> presenter
        )
        {
            await useCasesProvider.Shelves.GetShelves.Handle(new GetShelvesRequest(), presenter);
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
            [FromBody] CreateShelfViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Shelves.CreateShelf.Handle(new CreateShelfRequest(viewmodel.MaxQtyOfBooks, viewmodel.Floor, viewmodel.BookCategory), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("delete/{shelfId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string shelfId
        )
        {
            if (string.IsNullOrEmpty(shelfId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ShelvesController_forgotten_reader_id_on_request);

            await useCasesProvider.Shelves.DeleteShelf.Handle(new DeleteShelfRequest(shelfId), presenter);
            return presenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("available/{category}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableShelves(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Shelf>> presenter,
            EBookCategory category,
            int numberOfCopies
        )
        {
            await useCasesProvider.Shelves.GetAvailableShelves.Handle(new GetAvailableShelvesRequest(category), presenter);
            return presenter.ContentResult;
        }
        #endregion
    }
}