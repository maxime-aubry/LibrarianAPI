using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Enums;
using Librarian.Core.UseCases;
using Librarian.RestFulAPI.Tools.Presenters;
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
        #region Main CRUD Presenters
        private readonly IJsonPresenter<string> createShelfPresenter;
        private readonly IJsonPresenter<string> updateShelfPresenter;
        private readonly IJsonPresenter<string> deleteShelfPresenter;
        private readonly IJsonPresenter<Librarian.Core.Domain.Entities.Shelf> getShelfByIdPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Shelf>> getShelvesPresenter;
        #endregion

        #region Main CRUD Presenters
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Shelf>> getAvailableShelves;
        #endregion

        private readonly IUseCasesProvider useCasesProvider;

        public ShelvesController(
            IJsonPresenter<string> createShelfPresenter,
            IJsonPresenter<string> updateShelfPresenter,
            IJsonPresenter<string> deleteShelfPresenter,
            IJsonPresenter<Librarian.Core.Domain.Entities.Shelf> getShelfByIdPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Shelf>> getShelvesPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Shelf>> getAvailableShelves,
            IUseCasesProvider useCasesProvider)
        {
            this.createShelfPresenter = createShelfPresenter;
            this.updateShelfPresenter = updateShelfPresenter;
            this.deleteShelfPresenter = deleteShelfPresenter;
            this.getShelfByIdPresenter = getShelfByIdPresenter;
            this.getShelvesPresenter = getShelvesPresenter;
            this.getAvailableShelves = getAvailableShelves;
            this.useCasesProvider = useCasesProvider;
        }

        #region Main CRUD Methods
        [HttpGet("getById/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string shelfId)
        {
            if (string.IsNullOrEmpty(shelfId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ShelvesController_forgotten_reader_id_on_request);

            await this.useCasesProvider.Shelves.GetShelfById.Handle(new GetShelfByIdRequest(shelfId), this.getShelfByIdPresenter);
            return this.getShelfByIdPresenter.ContentResult;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            await this.useCasesProvider.Shelves.GetShelves.Handle(new GetShelvesRequest(), this.getShelvesPresenter);
            return this.getShelvesPresenter.ContentResult;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateShelfViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Shelves.CreateShelf.Handle(new CreateShelfRequest(viewmodel.MaxQtyOfBooks, viewmodel.Floor, viewmodel.BookCategory), this.createShelfPresenter);
            return this.createShelfPresenter.ContentResult;
        }

        [HttpPut("update/{shelfId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string shelfId, [FromBody] UpdateShelfViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Shelves.UpdateShelf.Handle(new UpdateShelfRequest(shelfId, viewmodel.MaxQtyOfBooks, viewmodel.Floor, viewmodel.BookCategory), this.updateShelfPresenter);
            return this.updateShelfPresenter.ContentResult;
        }

        [HttpDelete("delete/{shelfId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string shelfId)
        {
            if (string.IsNullOrEmpty(shelfId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ShelvesController_forgotten_reader_id_on_request);

            await this.useCasesProvider.Shelves.DeleteShelf.Handle(new DeleteShelfRequest(shelfId), this.deleteShelfPresenter);
            return this.deleteShelfPresenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("availableShelves/{category}/{numberOfCopies}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableShelves(EBookCategory category, int numberOfCopies)
        {
            await this.useCasesProvider.Shelves.GetAvailableShelves.Handle(new GetAvailableShelvesRequest(category, numberOfCopies), this.getAvailableShelves);
            return this.getAvailableShelves.ContentResult;
        }
        #endregion
    }
}