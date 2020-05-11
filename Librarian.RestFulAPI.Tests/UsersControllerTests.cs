using Librarian.Core.Domain.Entities;
using Librarian.RestFulAPI.Tests.Tools;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests
{
    public class UsersControllerTests : BaseController
    {
        public UsersControllerTests(AppTestFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {

        }

        [Fact]
        public async Task UsersController_GetById_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            User model = DataProvider.Users.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();

            //ContentResult<Book> result = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{model.Id}");

            //Assert.True(result.Success);
            //Assert.Null(result.Message);
            //Assert.Equal(model.Id, result.Result.Id);
        }

        [Fact]
        public async Task UsersController_GetList_Ok()
        {
            //await DataProvider.PopulateDatabase(this.client);

            //ContentResult<IEnumerable<Book>> result = await HttpHelper.Get<IEnumerable<Book>>(this.client, $"/api/v1/Books/list");

            //Assert.True(result.Success);
            //Assert.Null(result.Message);
            //Assert.Equal(DataProvider.Books.Select(b => b.Title), result.Result.Select(b => b.Title));
        }

        [Fact]
        public async Task UsersController_Create_Ok()
        {
            //await DataProvider.PopulateDatabase(this.client);

            //Shelf shelf = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();
            //CreateBookViewModel viewModel = new CreateBookViewModel()
            //{
            //    Title = "Escadron spectre",
            //    Categories = new List<EBookCategory>() { EBookCategory.ScienceFiction },
            //    ReleaseDate = DateTime.UtcNow.Date,
            //    NumberOfCopies = 10,
            //    ShelfId = shelf.Id
            //};
            //ContentResult<string> result1 = await HttpHelper.Post<CreateBookViewModel, string>(this.client, $"/api/v1/Books/create", viewModel);

            //Assert.True(result1.Success);
            //Assert.Null(result1.Message);
            //Assert.NotNull(result1.Result);

            //// get book from database
            //ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{result1.Result}");

            //Assert.True(result2.Success);
            //Assert.Null(result2.Message);
            //Assert.NotNull(result2.Result);
            //Assert.Equal(viewModel.Title, result2.Result.Title);
            //Assert.Equal(viewModel.Categories, result2.Result.Categories);
            //Assert.Equal(viewModel.NumberOfCopies, result2.Result.NumberOfCopies);
            //Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            //Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task UsersController_Update_Ok()
        {
            //await DataProvider.PopulateDatabase(this.client);

            //Shelf shelf = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();
            //Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();
            //UpdateBookViewModel viewModel = new UpdateBookViewModel()
            //{
            //    Id = book.Id,
            //    Title = $"{book.Title} - 2",
            //    Categories = book.Categories,
            //    ReleaseDate = book.RealeaseDate,
            //    ShelfId = book.ShelfId
            //};
            //ContentResult<string> result1 = await HttpHelper.Put<UpdateBookViewModel, string>(this.client, $"/api/v1/Books/update/{book.Id}", viewModel);

            //Assert.True(result1.Success);
            //Assert.Null(result1.Message);
            //Assert.NotNull(result1.Result);
            //Assert.Equal(book.Id, result1.Result);

            //// get book from database
            //ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{book.Id}");

            //Assert.True(result2.Success);
            //Assert.Null(result2.Message);
            //Assert.NotNull(result2.Result);
            //Assert.Equal(viewModel.Title, result2.Result.Title);
            //Assert.Equal(viewModel.Categories, result2.Result.Categories);
            //Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            //Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task UsersController_Delete_Ok()
        {
            //await DataProvider.PopulateDatabase(this.client);

            //Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();

            //ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Books/delete/{book.Id}");

            //Assert.True(result1.Success);
            //Assert.Null(result1.Message);
            //Assert.Null(result1.Result);

            //// try to get book from database
            //ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{book.Id}");

            //Assert.False(result2.Success);
            //Assert.Equal("Book not found", result2.Message);
            //Assert.Null(result2.Result);
        }
    }
}
