using Librarian.Core.Domain.Entities;
using Librarian.RestFulAPI.Tests.Tools;
using Librarian.RestFulAPI.V1.ViewModels.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests
{
    public class ReadersControllerTests : BaseController
    {
        public ReadersControllerTests(AppTestFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {

        }

        [Fact]
        public async Task Targeted_reader_is_got_from_database()
        {
            await DataProvider.PopulateDatabase(this.client);

            Reader model = DataProvider.Readers.Where(b => b.LastName == "AUBRY").Single();

            ContentResult<Reader> result = await HttpHelper.Get<Reader>(this.client, $"/api/v1/Readers/getById/{model.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Equal(model.Id, result.Result.Id);
        }

        [Fact]
        public async Task Readers_are_all_in_static_list()
        {
            await DataProvider.PopulateDatabase(this.client);

            ContentResult<IEnumerable<Reader>> result = await HttpHelper.Get<IEnumerable<Reader>>(this.client, $"/api/v1/Readers/list");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("Maxime", item.FirstName);
                    Assert.Equal("AUBRY", item.LastName);
                },
                item =>
                {
                    Assert.Equal("David", item.FirstName);
                    Assert.Equal("Zippari", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Simon", item.FirstName);
                    Assert.Equal("Louail", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Wilfrid", item.FirstName);
                    Assert.Equal("Lepape", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Mathieu", item.FirstName);
                    Assert.Equal("Decroocq", item.LastName);
                }
            );
        }

        [Fact]
        public async Task Reader_is_created()
        {
            await DataProvider.PopulateDatabase(this.client);

            CreateReaderViewModel viewModel = new CreateReaderViewModel()
            {
                FirstName = "Ninon",
                LastName = "BRIONNE",
                Birthday = new DateTime(1989, 11, 2)
            };
            ContentResult<string> result1 = await HttpHelper.Post<CreateReaderViewModel, string>(this.client, $"/api/v1/Readers/create", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);

            // get reader from database
            ContentResult<Reader> result2 = await HttpHelper.Get<Reader>(this.client, $"/api/v1/Readers/getById/{result1.Result}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.FirstName, result2.Result.FirstName);
            Assert.Equal(viewModel.LastName, result2.Result.LastName);
        }

        [Fact]
        public async Task Reader_is_updated()
        {
            await DataProvider.PopulateDatabase(this.client);

            Reader reader = DataProvider.Readers.Where(r => r.LastName == "AUBRY").Single();

            UpdateReaderViewModel viewModel = new UpdateReaderViewModel()
            {
                Id = reader.Id,
                FirstName = "Max Puissant",
                LastName = reader.LastName,
                Birthday = reader.Birthday
            };
            ContentResult<string> result1 = await HttpHelper.Put<UpdateReaderViewModel, string>(this.client, $"/api/v1/Readers/update/{reader.Id}", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);
            Assert.Equal(viewModel.Id, result1.Result);

            // get Reader from database
            ContentResult<Reader> result2 = await HttpHelper.Get<Reader>(this.client, $"/api/v1/Readers/getById/{reader.Id}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.FirstName, result2.Result.FirstName);
            Assert.Equal(viewModel.LastName, result2.Result.LastName);
        }

        [Fact]
        public async Task Reader_is_deleted()
        {
            await DataProvider.PopulateDatabase(this.client);

            Reader reader = DataProvider.Readers.Where(b => b.LastName == "AUBRY").Single();

            ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Readers/delete/{reader.Id}");

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.Null(result1.Result);

            // try to get book from database
            ContentResult<Reader> result2 = await HttpHelper.Get<Reader>(this.client, $"/api/v1/Readers/getById/{reader.Id}");

            Assert.False(result2.Success);
            Assert.Equal("Reader not found", result2.Message);
            Assert.Null(result2.Errors);
            Assert.Null(result2.Result);
        }

        [Fact]
        public async Task Loans_of_reader_are_in_static_list()
        {
            await DataProvider.PopulateDatabase(this.client);

            Reader reader = DataProvider.Readers.Where(b => b.LastName == "AUBRY").Single();

            ContentResult<IEnumerable<ReaderLoansBook>> result = await HttpHelper.Get<IEnumerable<ReaderLoansBook>>(this.client, $"/api/v1/Readers/loans/{reader.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Loan_is_added()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book book = DataProvider.Books.Where(b => b.Title == "Harry Potter à l'École des Sorciers").Single();
            Reader reader = DataProvider.Readers.Where(a => a.LastName == "AUBRY").Single();

            AddLoanViewModel viewModel = new AddLoanViewModel()
            {
                BookId = book.Id,
                ReaderId = reader.Id
            };
            ContentResult<string> result1 = await HttpHelper.Post<AddLoanViewModel, string>(this.client, $"/api/v1/Readers/loans/add", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);

            ContentResult<IEnumerable<ReaderLoansBook>> result2 = await HttpHelper.Get<IEnumerable<ReaderLoansBook>>(this.client, $"/api/v1/Readers/loans/{reader.Id}");
            ReaderLoansBook addedLoan = result2.Result.Where(l => l.Id == result1.Result).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(addedLoan);
            Assert.Equal(DateTime.UtcNow.Date, addedLoan.DateOfLoaning);
            Assert.Null(addedLoan.EndDateOfLoaning);
            Assert.Null(addedLoan.IsLost);
        }

        [Fact]
        public async Task Loan_is_closed()
        {
            await DataProvider.PopulateDatabase(this.client);

            ReaderLoansBook loan = DataProvider.ReaderLoansBook.First();

            CloseLoanViewModel viewModel = new CloseLoanViewModel()
            {
                LoanId = loan.Id
            };
            ContentResult<string> result1 = await HttpHelper.Put<CloseLoanViewModel, string>(this.client, $"/api/v1/Readers/loans/close", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);
            Assert.Equal(viewModel.LoanId, result1.Result);

            ContentResult<IEnumerable<ReaderLoansBook>> result2 = await HttpHelper.Get<IEnumerable<ReaderLoansBook>>(this.client, $"/api/v1/Readers/loans/{loan.ReaderId}");
            ReaderLoansBook closedLoan = result2.Result.Where(l => l.Id == loan.Id).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(closedLoan);
            Assert.Equal(DateTime.UtcNow.Date, closedLoan.EndDateOfLoaning);
            Assert.False(closedLoan.IsLost);
        }

        [Fact]
        public async Task Loan_is_closed_and_lost()
        {
            await DataProvider.PopulateDatabase(this.client);

            ReaderLoansBook loan = DataProvider.ReaderLoansBook.First();

            CloseLoanViewModel viewModel = new CloseLoanViewModel()
            {
                LoanId = loan.Id
            };
            ContentResult<string> result1 = await HttpHelper.Put<CloseLoanViewModel, string>(this.client, $"/api/v1/Readers/loans/lost", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);
            Assert.Equal(viewModel.LoanId, result1.Result);

            ContentResult<IEnumerable<ReaderLoansBook>> result2 = await HttpHelper.Get<IEnumerable<ReaderLoansBook>>(this.client, $"/api/v1/Readers/loans/{loan.ReaderId}");
            ReaderLoansBook lostLoan = result2.Result.Where(l => l.Id == loan.Id).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(lostLoan);
            Assert.Equal(DateTime.UtcNow.Date, lostLoan.EndDateOfLoaning);
            Assert.True(lostLoan.IsLost);
        }

        [Fact]
        public async Task Loan_is_deleted()
        {
            await DataProvider.PopulateDatabase(this.client);

            ReaderLoansBook loan = DataProvider.ReaderLoansBook.First();

            ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Readers/loans/delete/{loan.Id}");

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.Null(result1.Result);

            ContentResult<IEnumerable<ReaderLoansBook>> result2 = await HttpHelper.Get<IEnumerable<ReaderLoansBook>>(this.client, $"/api/v1/Readers/loans/{loan.ReaderId}");
            ReaderLoansBook deletedLoan = result2.Result.Where(l => l.Id == loan.Id).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.Null(deletedLoan);
        }
    }
}
