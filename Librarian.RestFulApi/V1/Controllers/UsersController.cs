using Librarian.Core.DataTransfertObject.UseCases.Users;
using Librarian.Core.Domain.Entities;
using Librarian.Core.UseCases;
using Librarian.RestFulAPI.Tools.Presenters;
using Librarian.RestFulAPI.V1.ViewModels.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }

        #region Main CRUD Methods
        [HttpGet("getById/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<User> presenter,
            string userId
        )
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.UsersController_forgotten_user_id_on_request);

            await useCasesProvider.Users.GetById.Handle(new GetUserByIdRequest(userId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<User>> presenter
        )
        {
            await useCasesProvider.Users.GetList.Handle(new GetUsersRequest(), presenter);
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
            [FromBody] CreateUserViewModel viewmodel
        )
        {
            await useCasesProvider.Users.Create.Handle(new CreateUserRequest(viewmodel.FirstName, viewmodel.LastName, viewmodel.Login), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("update/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] UpdateAuthorViewModel viewmodel,
            string userId
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Users.Update.Handle(new UpdateUserRequest(userId, viewmodel.FirstName, viewmodel.LastName, viewmodel.Login), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("delete/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string userId
        )
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.UsersController_forgotten_user_id_on_request);

            await useCasesProvider.Users.Delete.Handle(new DeleteUserRequest(userId), presenter);
            return presenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods

        #endregion
    }
}