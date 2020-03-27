using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.Tests.Tools;
using Librarian.RestFulAPI.V1.ViewModels.Shelves;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests
{
    public class ShelvesContollerTests : BaseController
    {
        public ShelvesContollerTests(AppTestFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {

        }

        [Fact]
        public async Task Targeted_shelf_is_got_from_database()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelf = DataProvider.Shelves.First();

            ContentResult<Shelf> result = await HttpHelper.Get<Shelf>(this.client, $"/api/v1/Shelves/getById/{shelf.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Equal(shelf.Id, result.Result.Id);
        }

        [Fact]
        public async Task Books_are_all_in_static_list()
        {
            await DataProvider.PopulateDatabase(this.client);

            ContentResult<IEnumerable<Shelf>> result = await HttpHelper.Get<IEnumerable<Shelf>>(this.client, $"/api/v1/Shelves/list");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Equal(DataProvider.Shelves.Select(s => s.Name), result.Result.Select(s => s.Name));
        }

        [Fact]
        public async Task Shelf_is_created()
        {
            await DataProvider.PopulateDatabase(this.client);

            CreateShelfViewModel viewModel = new CreateShelfViewModel()
            {
                Floor = EFloor.FirstFloor,
                BookCategory = EBookCategory.FanFiction,
                MaxQtyOfBooks = 400
            };
            ContentResult<string> result1 = await HttpHelper.Post<CreateShelfViewModel, string>(this.client, $"/api/v1/Shelves/create", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.NotNull(result1.Result);

            // get book from database
            ContentResult<Shelf> result2 = await HttpHelper.Get<Shelf>(this.client, $"/api/v1/Shelves/getById/{result1.Result}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.Floor, result2.Result.Floor);
            Assert.Equal(viewModel.BookCategory, result2.Result.BookCategory);
            Assert.Equal(viewModel.MaxQtyOfBooks, result2.Result.MaxQtyOfBooks);
        }

        [Fact]
        public async Task Shelf_is_deleted()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelf = DataProvider.Shelves.Where(b => b.Name == "F1-BC1-NB2").Single();

            ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Shelves/delete/{shelf.Id}");

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Result);

            // try to get shelf from database
            ContentResult<Shelf> result2 = await HttpHelper.Get<Shelf>(this.client, $"/api/v1/Shelves/getById/{shelf.Id}");

            Assert.False(result2.Success);
            Assert.Equal("Shelf not found", result2.Message);
            Assert.Null(result2.Result);
        }

        [Fact]
        public async Task Available_shelves_are_ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            ContentResult<IEnumerable<Shelf>> result = await HttpHelper.Get<IEnumerable<Shelf>>(this.client, $"/api/v1/Shelves/available/{(int)EBookCategory.ScienceFiction}");

            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).Select(s => s.Name), result.Result.Select(s => s.Name));
        }
    }
}
