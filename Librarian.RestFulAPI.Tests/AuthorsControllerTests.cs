using Librarian.Core.Domain.Entities;
using Librarian.RestFulAPI.Tests.Tools;
using Librarian.RestFulAPI.V1.ViewModels.Authors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests
{
    public class AuthorsControllerTests : BaseController
    {
        public AuthorsControllerTests(AppTestFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {

        }

        [Fact]
        public async Task Targeted_author_is_got_from_database()
        {
            await DataProvider.PopulateDatabase(this.client);

            Author model = DataProvider.Authors.Where(b => b.FirstName == "Charles" && b.LastName == "Dickens").Single();

            ContentResult<Author> result = await HttpHelper.Get<Author>(this.client, $"/api/v1/Authors/getById/{model.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Equal(model.Id, result.Result.Id);
        }

        [Fact]
        public async Task Books_are_all_in_static_list()
        {
            await DataProvider.PopulateDatabase(this.client);

            ContentResult<IEnumerable<Author>> result = await HttpHelper.Get<IEnumerable<Author>>(this.client, $"/api/v1/Authors/list");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("Jules", item.FirstName);
                    Assert.Equal("Verne", item.LastName);
                },
                item =>
                {
                    Assert.Equal("William", item.FirstName);
                    Assert.Equal("Shakespeare", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Charles", item.FirstName);
                    Assert.Equal("Baudelaire", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Charles", item.FirstName);
                    Assert.Equal("Dickens", item.LastName);
                },
                item =>
                {
                    Assert.Equal("J.R.R.", item.FirstName);
                    Assert.Equal("Tolkien", item.LastName);
                },
                item =>
                {
                    Assert.Equal("J.K.", item.FirstName);
                    Assert.Equal("Rowling", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Stephen", item.FirstName);
                    Assert.Equal("King", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Victor", item.FirstName);
                    Assert.Equal("Hugo", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Ernest", item.FirstName);
                    Assert.Equal("Hemingway", item.LastName);
                },
                item =>
                {
                    Assert.Equal("Antoine", item.FirstName);
                    Assert.Equal("de Saint-Exupéry", item.LastName);
                },
                item =>
                {
                    Assert.Equal("R.L.", item.FirstName);
                    Assert.Equal("Stine", item.LastName);
                }
            );
        }

        [Fact]
        public async Task Author_is_created()
        {
            await DataProvider.PopulateDatabase(this.client);

            CreateAuthorViewModel viewModel = new CreateAuthorViewModel()
            {
                FirstName = "Isaac",
                LastName = "Asimov"
            };
            ContentResult<string> result1 = await HttpHelper.Post<CreateAuthorViewModel, string>(this.client, $"/api/v1/Authors/create", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);

            // get author from database
            ContentResult<Author> result2 = await HttpHelper.Get<Author>(this.client, $"/api/v1/Authors/getById/{result1.Result}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.FirstName, result2.Result.FirstName);
            Assert.Equal(viewModel.LastName, result2.Result.LastName);
        }

        [Fact]
        public async Task Author_is_updated()
        {
            await DataProvider.PopulateDatabase(this.client);

            Author author = DataProvider.Authors.Where(a => a.LastName == "Verne").Single();

            UpdateAuthorViewModel viewModel = new UpdateAuthorViewModel()
            {
                Id = author.Id,
                FirstName = "Jul",
                LastName = author.LastName
            };
            ContentResult<string> result1 = await HttpHelper.Put<UpdateAuthorViewModel, string>(this.client, $"/api/v1/Authors/update/{author.Id}", viewModel);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);
            Assert.Equal(author.Id, viewModel.Id);

            // get author from database
            ContentResult<Author> result2 = await HttpHelper.Get<Author>(this.client, $"/api/v1/Authors/getById/{author.Id}");

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.FirstName, result2.Result.FirstName);
            Assert.Equal(viewModel.LastName, result2.Result.LastName);
        }

        [Fact]
        public async Task Author_is_deleted()
        {
            await DataProvider.PopulateDatabase(this.client);

            Author author = DataProvider.Authors.Where(b => b.LastName == "Verne").Single();

            ContentResult<string> result1 = await HttpHelper.Delete<string>(this.client, $"/api/v1/Authors/delete/{author.Id}");

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.Null(result1.Result);
            Assert.Equal(author.Id, result1.Result);

            // try to get book from database
            ContentResult<Author> result2 = await HttpHelper.Get<Author>(this.client, $"/api/v1/Authors/getById/{author.Id}");

            Assert.False(result2.Success);
            Assert.Equal("Author not found", result2.Message);
            Assert.Null(result2.Errors);
            Assert.Null(result2.Result);
        }

        [Fact]
        public async Task Search_authors_give_good_results()
        {
            await DataProvider.PopulateDatabase(this.client);

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("firstName", "J."),
                new KeyValuePair<string, string>("lastName", null)
            };
            ContentResult<IEnumerable<FindAuthorsByFilters>> result = await HttpHelper.Get<IEnumerable<FindAuthorsByFilters>>(this.client, $"/api/v1/Authors/search", parameters);

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("J.R.R.", item.FirstName);
                    Assert.Equal("Tolkien", item.LastName);
                },
                item =>
                {
                    Assert.Equal("J.K.", item.FirstName);
                    Assert.Equal("Rowling", item.LastName);
                }
            );
        }

        [Fact]
        public async Task Those_are_books_of_this_author()
        {
            await DataProvider.PopulateDatabase(this.client);

            Author author = DataProvider.Authors.Where(b => b.LastName == "Verne").Single();

            ContentResult<IEnumerable<Book>> result = await HttpHelper.Get<IEnumerable<Book>>(this.client, $"/api/v1/Authors/books/list/{author.Id}");

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Collection(result.Result,
                item =>
                {
                    Assert.Equal("Vingt Mille Lieues sous les mers", item.Title);
                },
                item =>
                {
                    Assert.Equal("Voyage au centre de la Terre", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Tour du monde en quatre-vingts jours", item.Title);
                },
                item =>
                {
                    Assert.Equal("L'Île mystérieuse", item.Title);
                },
                item =>
                {
                    Assert.Equal("De la Terre à la Lune", item.Title);
                },
                item =>
                {
                    Assert.Equal("Cinq Semaines en ballon", item.Title);
                }
            );
        }
    }
}
