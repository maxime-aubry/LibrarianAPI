using HexagonalArchitecture.Core.Presenters;
using Librarian.Core.Domain.Entities;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.UserHasRight.AddRight;
using Librarian.Core.UseCases.UserHasRight.DeleteRight;
using Librarian.Core.UseCases.Users.CreateUser;
using Librarian.Core.UseCases.Users.DeleteUser;
using Librarian.Core.UseCases.Users.GetUserById;
using Librarian.Core.UseCases.Users.GetUsers;
using Librarian.Core.UseCases.Users.UpdateUser;
using Librarian.RestFulAPI.V1.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            await useCasesProvider.Users.Create.Handle(new CreateUserRequest(viewmodel.FirstName, viewmodel.LastName), presenter);
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
            [FromBody] UpdateUserViewModel viewmodel,
            string userId
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Users.Update.Handle(new UpdateUserRequest(userId, viewmodel.FirstName, viewmodel.LastName), presenter);
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
        [HttpPost("rights/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRight(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] AddRightViewModel viewmodel
        )
        {
            await useCasesProvider.UserRights.AddRight.Handle(new AddRightRequest(viewmodel.UserId, viewmodel.UserRight), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("rights/delete")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRight(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] DeleteRightViewModel viewmodel
        )
        {
            await useCasesProvider.UserRights.DeleteRight.Handle(new DeleteRightRequest(viewmodel.UserId, viewmodel.UserRight), presenter);
            return presenter.ContentResult;
        }
        #endregion
    }
}