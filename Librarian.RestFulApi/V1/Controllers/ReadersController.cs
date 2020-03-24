using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.UseCases;
using Librarian.RestFulAPI.Tools.Presenters;
using Librarian.RestFulAPI.V1.ViewModels.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        #region Main CRUD Presenters
        private readonly IJsonPresenter<Librarian.Core.Domain.Entities.Reader> getReaderByIdPresenter;
        private readonly IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Reader>> getReadersPresenter;
        private readonly IJsonPresenter<string> createReaderPresenter;
        private readonly IJsonPresenter<string> updateReaderPresenter;
        private readonly IJsonPresenter<string> deleteReaderPresenter;
        #endregion

        #region Secondaries CRUD Presenters
        private readonly IJsonPresenter<string> addLoanPresenter;
        private readonly IJsonPresenter<string> closeLoanPresenter;
        private readonly IJsonPresenter<string> closeLoanAdndDeclareAsLostPresenter;
        private readonly IJsonPresenter<string> deleteLoanPresenter;
        #endregion

        private readonly IUseCasesProvider useCasesProvider;

        public ReadersController(
            IJsonPresenter<Librarian.Core.Domain.Entities.Reader> getReaderByIdPresenter,
            IJsonPresenter<IEnumerable<Librarian.Core.Domain.Entities.Reader>> getReadersPresenter,
            IJsonPresenter<string> createReaderPresenter,
            IJsonPresenter<string> updateReaderPresenter,
            IJsonPresenter<string> deleteReaderPresenter,
            IJsonPresenter<string> addLoanPresenter,
            IJsonPresenter<string> closeLoanPresenter,
            IJsonPresenter<string> closeLoanAdndDeclareAsLostPresenter,
            IJsonPresenter<string> deleteLoanPresenter,
            IUseCasesProvider useCasesProvider)
        {
            this.getReaderByIdPresenter = getReaderByIdPresenter;
            this.getReadersPresenter = getReadersPresenter;
            this.createReaderPresenter = createReaderPresenter;
            this.updateReaderPresenter = updateReaderPresenter;
            this.deleteReaderPresenter = deleteReaderPresenter;
            this.addLoanPresenter = addLoanPresenter;
            this.closeLoanPresenter = closeLoanPresenter;
            this.closeLoanAdndDeclareAsLostPresenter = closeLoanAdndDeclareAsLostPresenter;
            this.deleteLoanPresenter = deleteLoanPresenter;
            this.useCasesProvider = useCasesProvider;
        }

        #region Main CRUD Methods
        [HttpGet("getById/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string readerId)
        {
            if (string.IsNullOrEmpty(readerId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_reader_id_on_request);

            await this.useCasesProvider.Readers.GetById.Handle(new GetReaderByIdRequest(readerId), this.getReaderByIdPresenter);
            return this.getReaderByIdPresenter.ContentResult;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            await this.useCasesProvider.Readers.GetList.Handle(new GetReadersRequest(), this.getReadersPresenter);
            return this.getReadersPresenter.ContentResult;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateReaderViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Readers.Create.Handle(new CreateReaderRequest(viewmodel.FirstName, viewmodel.LastName, viewmodel.Birthday), this.createReaderPresenter);
            return this.createReaderPresenter.ContentResult;
        }

        [HttpPut("update/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string readerId, [FromBody] UpdateReaderViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.Readers.Update.Handle(new UpdateReaderRequest(readerId, viewmodel.FirstName, viewmodel.LastName, viewmodel.Birthday, false), this.updateReaderPresenter);
            return this.updateReaderPresenter.ContentResult;
        }

        [HttpDelete("delete/{readerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string readerId)
        {
            if (string.IsNullOrEmpty(readerId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_reader_id_on_request);

            await this.useCasesProvider.Readers.Delete.Handle(new DeleteReaderRequest(readerId), this.deleteReaderPresenter);
            return this.deleteReaderPresenter.ContentResult;
        }
        #endregion

        #region Secondaries CRUD Methods
        [HttpPost("loan/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLoan([FromBody] AddLoanViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.ReadersLoans.AddLoan.Handle(new AddLoanRequest(viewmodel.ReaderId, viewmodel.BookId), this.addLoanPresenter);
            return this.addLoanPresenter.ContentResult;
        }

        [HttpPost("loan/close")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseLoan([FromBody] CloseLoanViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.ReadersLoans.CloseLoan.Handle(new CloseLoanRequest(viewmodel.LoanId), this.closeLoanPresenter);
            return this.closeLoanPresenter.ContentResult;
        }

        [HttpPost("loan/lost")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseLoanAndDeclareAsLost([FromBody] CloseLoanViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.useCasesProvider.ReadersLoans.CloseLoanAndDeclareAsLost.Handle(new CloseLoanAndDeclareAsLostRequest(viewmodel.LoanId), this.closeLoanAdndDeclareAsLostPresenter);
            return this.closeLoanAdndDeclareAsLostPresenter.ContentResult;
        }

        [HttpDelete("loan/delete/{loanId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLoan(string loanId)
        {
            if (string.IsNullOrEmpty(loanId))
                return BadRequest(Librarian.RestFulAPI.Properties.Resources.ReadersController_forgotten_loan_id_on_request);

            await this.useCasesProvider.ReadersLoans.DeleteLoan.Handle(new DeleteLoanRequest(loanId), this.deleteLoanPresenter);
            return this.deleteLoanPresenter.ContentResult;
        }
        #endregion
    }
}