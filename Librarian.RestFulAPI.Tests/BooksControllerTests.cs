using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.Tests.Tools;
using Librarian.RestFulAPI.V1.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests
{
    public class BooksControllerTests : BaseController
    {
        public BooksControllerTests(AppTestFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {

        }

        [Fact]
        public async Task BooksController_GetById_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book model = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();

            ContentResult<Book> result = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{model.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Equal(model.Id, result.Result.Id);
        }

        [Fact]
        public async Task BooksController_GetList_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            ContentResult<IEnumerable<Book>> result = await HttpHelper.Get<IEnumerable<Book>>(this.client, $"/api/v1/Books/list");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Equal(DataProvider.Books.Select(b => b.Title), result.Result.Select(b => b.Title));
        }

        [Fact]
        public async Task BooksController_Create_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelf = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();
            CreateBookViewModel viewModel = new CreateBookViewModel()
            {
                Title = "Escadron spectre",
                Categories = new List<EBookCategory>() { EBookCategory.ScienceFiction },
                ReleaseDate = DateTime.UtcNow.Date,
                NumberOfCopies = 10,
                ShelfId = shelf.Id
            };
            ContentResult<string> result1 = await HttpHelper.Post<CreateBookViewModel, string>(this.client, $"/api/v1/Books/create", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.NotNull(result1.Result);

            // get book from database
            ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{result1.Result}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.Title, result2.Result.Title);
            Assert.Equal(viewModel.Categories, result2.Result.Categories);
            Assert.Equal(viewModel.NumberOfCopies, result2.Result.NumberOfCopies);
            Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task BooksController_Update_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelf = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();
            Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();
            UpdateBookViewModel viewModel = new UpdateBookViewModel()
            {
                Id = book.Id,
                Title = $"{book.Title} - 2",
                Categories = book.Categories,
                ReleaseDate = book.RealeaseDate,
                ShelfId = book.ShelfId
            };
            ContentResult<string> result1 = await HttpHelper.Put<UpdateBookViewModel, string>(this.client, $"/api/v1/Books/update/{book.Id}", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.NotNull(result1.Result);
            Assert.Equal(book.Id, result1.Result);

            // get book from database
            ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{book.Id}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.Title, result2.Result.Title);
            Assert.Equal(viewModel.Categories, result2.Result.Categories);
            Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task BooksController_Delete_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();

            ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Books/delete/{book.Id}");

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Result);

            // try to get book from database
            ContentResult<Book> result2 = await HttpHelper.Get<Book>(this.client, $"/api/v1/Books/getById/{book.Id}");

            Assert.False(result2.Success);
            Assert.Equal("Book not found", result2.Message);
            Assert.Null(result2.Result);
        }

        [Fact]
        public async Task BooksController_Search_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("title", "mer"),
                new KeyValuePair<string, string>("categories", "11"),
                new KeyValuePair<string, string>("categories", "31"),
                new KeyValuePair<string, string>("authorIds", "5e79ca27bc7f674b08a17608"),
                new KeyValuePair<string, string>("authorIds", "5e79ca27bc7f674b08a1760d"),
            };
            ContentResult<IEnumerable<FindBooksByFilters>> result = await HttpHelper.Get<IEnumerable<FindBooksByFilters>>(this.client, $"/api/v1/Books/search", parameters);

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("Vingt Mille Lieues sous les mers", item.Title);
                    Assert.Equal((float)0.6, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("Voyage au centre de la Terre", item.Title);
                    Assert.Equal((float)0.4, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("Le Tour du monde en quatre-vingts jours", item.Title);
                    Assert.Equal((float)0.2, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("L'Île mystérieuse", item.Title);
                    Assert.Equal((float)0.4, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("De la Terre à la Lune", item.Title);
                    Assert.Equal((float)0.4, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("Cinq Semaines en ballon", item.Title);
                    Assert.Equal((float)0.2, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("Le Vieil Homme et la Mer", item.Title);
                    Assert.Equal((float)0.2, item.Pertinence);
                }
            );
        }

        [Fact]
        public async Task BooksController_GetAuthors_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();

            ContentResult<IEnumerable<Author>> result = await HttpHelper.Get<IEnumerable<Author>>(this.client, $"/api/v1/Books/authors/{book.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("Jules", item.FirstName);
                    Assert.Equal("Verne", item.LastName);
                }
            );
        }

        [Fact]
        public async Task BooksController_AddAuthor_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();
            Author author = DataProvider.Authors.Where(a => a.LastName == "Rowling").Single();

            AddAuthorsToBookViewModel viewModel = new AddAuthorsToBookViewModel()
            {
                BookId = book.Id,
                AuthorId = author.Id
            };
            ContentResult<string> result1 = await HttpHelper.Post<AddAuthorsToBookViewModel, string>(this.client, $"/api/v1/Books/authors/add", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.NotNull(result1.Result);

            ContentResult<IEnumerable<Author>> result2 = await HttpHelper.Get<IEnumerable<Author>>(this.client, $"/api/v1/Books/authors/{book.Id}");
            Author addedAuthor = result2.Result.Where(a => a.Id == author.Id).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.NotNull(addedAuthor);
            Assert.Equal(author.FirstName, addedAuthor.FirstName);
            Assert.Equal(author.LastName, addedAuthor.LastName);
        }

        [Fact]
        public async Task BooksController_DeleteAuthor_Ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book book = DataProvider.Books.Where(b => b.Title == "Vingt Mille Lieues sous les mers").Single();
            Author author = DataProvider.Authors.Where(a => a.LastName == "Verne").Single();

            DeleteAuthorsOfBookViewModel viewModel = new DeleteAuthorsOfBookViewModel()
            {
                BookId = book.Id,
                AuthorId = author.Id
            };
            ContentResult<string> result1 = await HttpHelper.Post<DeleteAuthorsOfBookViewModel, string>(this.client, $"/api/v1/Books/authors/delete", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Result);

            ContentResult<IEnumerable<Author>> result2 = await HttpHelper.Get<IEnumerable<Author>>(this.client, $"/api/v1/Books/authors/{book.Id}");
            Author deletedAuthor = result2.Result.Where(a => a.Id == author.Id).SingleOrDefault();

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(deletedAuthor);
        }
    }
}
