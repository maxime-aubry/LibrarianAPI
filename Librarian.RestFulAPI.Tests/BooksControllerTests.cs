using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.Tests.Tools;
using Librarian.RestFulAPI.V1.ViewModels.Books;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        public async Task Get_by_id_result_is_equals_of_statis_object()
        {
            await DataProvider.PopulateDatabase(this.client);

            Book model = DataProvider.Books.First();
            string response = await this.client.GetAsync($"/api/v1/Books/getById/{model.Id}").Result.Content.ReadAsStringAsync();
            ContentResult<Book> result = JsonConvert.DeserializeObject<ContentResult<Book>>(response);

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Equal(model.Id, result.Result.Id);
        }

        [Fact]
        public async Task Get_list_results_are_equivalent_of_static_list()
        {
            await DataProvider.PopulateDatabase(this.client);

            string response = await this.client.GetAsync($"/api/v1/Books/list").Result.Content.ReadAsStringAsync();
            ContentResult<IEnumerable<Book>> result = JsonConvert.DeserializeObject<ContentResult<IEnumerable<Book>>>(response);

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
            Assert.Collection(DataProvider.Books,
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
                    Assert.Equal("L'�le myst�rieuse", item.Title);
                },
                item =>
                {
                    Assert.Equal("De la Terre � la Lune", item.Title);
                },
                item =>
                {
                    Assert.Equal("Cinq Semaines en ballon", item.Title);
                },
                item =>
                {
                    Assert.Equal("Rom�o et Juliette", item.Title);
                },
                item =>
                {
                    Assert.Equal("Hamlet", item.Title);
                },
                item =>
                {
                    Assert.Equal("Macbeth", item.Title);
                },
                item =>
                {
                    Assert.Equal("Les Fleurs du mal", item.Title);
                },
                item =>
                {
                    Assert.Equal("Petits Po�mes en prose", item.Title);
                },
                item =>
                {
                    Assert.Equal("Oliver Twist", item.Title);
                },
                item =>
                {
                    Assert.Equal("Les Grandes Esp�rances", item.Title);
                },
                item =>
                {
                    Assert.Equal("Bilbo le Hobbit", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Silmarillion", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Seigneur des anneaux - La Communaut� de l'Anneau", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Seigneur des anneaux - Les Deux Tours", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Seigneur des anneaux - Le Retour du Roi", item.Title);
                },
                item =>
                {
                    Assert.Equal("Les Enfants de H�rin", item.Title);
                },
                item =>
                {
                    Assert.Equal("Contes et l�gendes inachev�s, tome 1", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter � l'�cole des Sorciers", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et la Chambre des Secrets", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et le Prisonnier d'Azkaban", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et la Coupe de Feu", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et l'Ordre du Ph�nix", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et le Prince de Sang-M�l�", item.Title);
                },
                item =>
                {
                    Assert.Equal("Harry Potter et les Reliques de la Mort", item.Title);
                },
                item =>
                {
                    Assert.Equal("�a", item.Title);
                },
                item =>
                {
                    Assert.Equal("Shining, l'enfant lumi�re", item.Title);
                },
                item =>
                {
                    Assert.Equal("Misery", item.Title);
                },
                item =>
                {
                    Assert.Equal("La Ligne verte", item.Title);
                },
                item =>
                {
                    Assert.Equal("Les Mis�rables", item.Title);
                },
                item =>
                {
                    Assert.Equal("Notre-Dame de Paris", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Vieil Homme et la Mer", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Petit Prince", item.Title);
                },
                item =>
                {
                    Assert.Equal("La Nuit des pantins", item.Title);
                },
                item =>
                {
                    Assert.Equal("Sang de monstre", item.Title);
                },
                item =>
                {
                    Assert.Equal("Dangereuses Photos", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Masque hant�", item.Title);
                },
                item =>
                {
                    Assert.Equal("La Maison des morts", item.Title);
                },
                item =>
                {
                    Assert.Equal("Le Loup-garou des mar�cages", item.Title);
                },
                item =>
                {
                    Assert.Equal("Sous-sol interdit", item.Title);
                }
            );
        }

        [Fact]
        public async Task Create_book_should_return_book_id()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelves = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();

            CreateBookViewModel viewModel = new CreateBookViewModel()
            {
                Title = "Escadron spectre",
                Categories = new List<EBookCategory>() { EBookCategory.ScienceFiction },
                ReleaseDate = DateTime.UtcNow.Date,
                NumberOfCopies = 10,
                ShelfId = shelves.Id
            };
            string json = JsonConvert.SerializeObject(viewModel);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response1 = await this.client.PostAsync($"/api/v1/Books/create", formContent).Result.Content.ReadAsStringAsync();
            ContentResult<string> result1 = JsonConvert.DeserializeObject<ContentResult<string>>(response1);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);

            // get book from database
            string response2 = await this.client.GetAsync($"/api/v1/Books/getById/{result1.Result}").Result.Content.ReadAsStringAsync();
            ContentResult<Book> result2 = JsonConvert.DeserializeObject<ContentResult<Book>>(response2);

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.Title, result2.Result.Title);
            Assert.Equal(viewModel.Categories, result2.Result.Categories);
            Assert.Equal(viewModel.NumberOfCopies, result2.Result.NumberOfCopies);
            Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task Updated_book_is_equal_to_model()
        {
            await DataProvider.PopulateDatabase(this.client);

            Shelf shelves = DataProvider.Shelves.Where(s => s.BookCategory == EBookCategory.ScienceFiction).First();
            UpdateBookViewModel viewModel = DataProvider.Books.Select(b => new UpdateBookViewModel()
            {
                Id = b.Id,
                Title = $"{b.Title} - 2",
                Categories = b.Categories,
                ReleaseDate = b.RealeaseDate,
                ShelfId = b.ShelfId
            }).First();
            string json = JsonConvert.SerializeObject(viewModel);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response1 = await this.client.PutAsync($"/api/v1/Books/update/{viewModel.Id}", formContent).Result.Content.ReadAsStringAsync();
            ContentResult<string> result1 = JsonConvert.DeserializeObject<ContentResult<string>>(response1);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.NotNull(result1.Result);

            // get book from database
            string response2 = await this.client.GetAsync($"/api/v1/Books/getById/{result1.Result}").Result.Content.ReadAsStringAsync();
            ContentResult<Book> result2 = JsonConvert.DeserializeObject<ContentResult<Book>>(response2);

            Assert.True(result2.Success);
            Assert.Null(result2.Message);
            Assert.Null(result2.Errors);
            Assert.NotNull(result2.Result);
            Assert.Equal(viewModel.Title, result2.Result.Title);
            Assert.Equal(viewModel.Categories, result2.Result.Categories);
            Assert.Equal(viewModel.ReleaseDate.ToString("o"), result2.Result.RealeaseDate.ToString("o"));
            Assert.Equal(viewModel.ShelfId, result2.Result.ShelfId);
        }

        [Fact]
        public async Task Deleted_book_cannot_be_got()
        {
            await DataProvider.PopulateDatabase(this.client);

            string bookId = DataProvider.Books.First().Id;

            string response1 = await this.client.DeleteAsync($"/api/v1/Books/delete/{bookId}").Result.Content.ReadAsStringAsync();
            ContentResult<string> result1 = JsonConvert.DeserializeObject<ContentResult<string>>(response1);

            Assert.True(result1.Success);
            Assert.Null(result1.Message);
            Assert.Null(result1.Errors);
            Assert.Null(result1.Result);

            // try to get book from database
            string response2 = await this.client.GetAsync($"/api/v1/Books/getById/{bookId}").Result.Content.ReadAsStringAsync();
            ContentResult<Book> result2 = JsonConvert.DeserializeObject<ContentResult<Book>>(response2);

            Assert.False(result2.Success);
            Assert.Equal("Book not found", result2.Message);
            Assert.Null(result2.Errors);
            Assert.Null(result2.Result);
        }

        [Fact]
        public async Task Get_all_by_filters_results_are_ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            string query = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("title", "mer"),
                new KeyValuePair<string, string>("categories", "11"),
                new KeyValuePair<string, string>("categories", "31"),
                new KeyValuePair<string, string>("authorIds", "5e79ca27bc7f674b08a17608"),
                new KeyValuePair<string, string>("authorIds", "5e79ca27bc7f674b08a1760d"),
            }).ReadAsStringAsync().Result;
            string response = await this.client.GetAsync($"/api/v1/Books/search?{query}").Result.Content.ReadAsStringAsync();
            ContentResult<IEnumerable<FindBooksByFilters>> result = JsonConvert.DeserializeObject<ContentResult<IEnumerable<FindBooksByFilters>>>(response);

            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Null(result.Errors);
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
                    Assert.Equal("L'�le myst�rieuse", item.Title);
                    Assert.Equal((float)0.4, item.Pertinence);
                },
                item =>
                {
                    Assert.Equal("De la Terre � la Lune", item.Title);
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
        public async Task Get_authors_results_are_ok()
        {
            await DataProvider.PopulateDatabase(this.client);

            //string response = await this.client.GetAsync($"/api/v1/Books/authors").Result.Content.ReadAsStringAsync();
            //ContentResult<IEnumerable<Author>> result = JsonConvert.DeserializeObject<ContentResult<IEnumerable<Author>>>(response);

            //Assert.True(result.Success);
            //Assert.Null(result.Message);
            //Assert.Null(result.Errors);
        }
    }
}
