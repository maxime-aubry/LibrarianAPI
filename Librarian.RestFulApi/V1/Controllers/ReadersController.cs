using HexagonalArchitecture.Core.Presenters;
using Librarian.Core.Domain.Entities;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.ReaderLoansBook.AddLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoanAndDeclareAsLost;
using Librarian.Core.UseCases.ReaderLoansBook.DeleteLoan;
using Librarian.Core.UseCases.ReaderLoansBook.GetLoans;
using Librarian.Core.UseCases.Readers.CreateReader;
using Librarian.Core.UseCases.Readers.DeleteReader;
using Librarian.Core.UseCases.Readers.GetReaderById;
using Librarian.Core.UseCases.Readers.GetReaders;
using Librarian.Core.UseCases.Readers.UpdateReader;
using Librarian.RestFulAPI.V1.ViewModels.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        public ReadersController()
        {
        }

        #region Main CRUD Methods
        [HttpGet("getById/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<Reader> presenter,
            string readerId
        )
        {
            if (string.IsNullOrEmpty(readerId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_reader_id_on_request);

            await useCasesProvider.Readers.GetById.Handle(new GetReaderByIdRequest(readerId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<Reader>> presenter
        )
        {
            await useCasesProvider.Readers.GetList.Handle(new GetReadersRequest(), presenter);
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
            [FromBody] CreateReaderViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Readers.Create.Handle(new CreateReaderRequest(viewmodel.FirstName, viewmodel.LastName, viewmodel.Birthday), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("update/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] UpdateReaderViewModel viewmodel,
            string readerId
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.Readers.Update.Handle(new UpdateReaderRequest(readerId, viewmodel.FirstName, viewmodel.LastName, viewmodel.Birthday, false), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("delete/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string readerId
        )
        {
            if (string.IsNullOrEmpty(readerId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_reader_id_on_request);

            await useCasesProvider.Readers.Delete.Handle(new DeleteReaderRequest(readerId), presenter);
            return presenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpGet("loans/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoans(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<IEnumerable<ReaderLoansBook>> presenter,
            string readerId
        )
        {
            if (string.IsNullOrEmpty(readerId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_reader_id_on_request);

            await useCasesProvider.ReadersLoans.GetLoans.Handle(new GetLoansRequest(readerId), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("loans/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLoan(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] AddLoanViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.ReadersLoans.AddLoan.Handle(new AddLoanRequest(viewmodel.ReaderId, viewmodel.BookId), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("loans/close")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseLoan(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] CloseLoanViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.ReadersLoans.CloseLoan.Handle(new CloseLoanRequest(viewmodel.LoanId), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("loans/lost")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseLoanAndDeclareAsLost(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            [FromBody] CloseLoanViewModel viewmodel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await useCasesProvider.ReadersLoans.CloseLoanAndDeclareAsLost.Handle(new CloseLoanAndDeclareAsLostRequest(viewmodel.LoanId), presenter);
            return presenter.ContentResult;
        }

        [HttpDelete("loans/delete/{loanId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLoan(
            [FromServices] IUseCasesProvider useCasesProvider,
            [FromServices] IJsonPresenter<string> presenter,
            string loanId
        )
        {
            if (string.IsNullOrEmpty(loanId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_loan_id_on_request);

            await useCasesProvider.ReadersLoans.DeleteLoan.Handle(new DeleteLoanRequest(loanId), presenter);
            return presenter.ContentResult;
        }
        #endregion
    }
}