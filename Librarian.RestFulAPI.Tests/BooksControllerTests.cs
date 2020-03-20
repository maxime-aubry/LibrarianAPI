using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.Tests.Tools;
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

            Librarian.Core.Domain.Entities.Book model = DataProvider.Books.First();
            string response = await this.client.GetAsync($"/api/v1/Books/getById/{model.Id}").Result.Content.ReadAsStringAsync();
            ContentResult<Librarian.Core.Domain.Entities.Book> result = JsonConvert.DeserializeObject<ContentResult<Librarian.Core.Domain.Entities.Book>>(response);
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
            ContentResult<IEnumerable<Librarian.Core.Domain.Entities.Book>> result = JsonConvert.DeserializeObject<ContentResult<IEnumerable<Librarian.Core.Domain.Entities.Book>>>(response);
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
                Assert.Equal("L'Île mystérieuse", item.Title);
            },
            item =>
            {
                Assert.Equal("De la Terre à la Lune", item.Title);
            },
            item =>
            {
                Assert.Equal("Cinq Semaines en ballon", item.Title);
            },
            item =>
            {
                Assert.Equal("Roméo et Juliette", item.Title);
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
                Assert.Equal("Petits Poèmes en prose", item.Title);
            },
            item =>
            {
                Assert.Equal("Oliver Twist", item.Title);
            },
            item =>
            {
                Assert.Equal("Les Grandes Espérances", item.Title);
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
                Assert.Equal("Le Seigneur des anneaux - La Communauté de l'Anneau", item.Title);
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
                Assert.Equal("Les Enfants de Húrin", item.Title);
            },
            item =>
            {
                Assert.Equal("Contes et légendes inachevés, tome 1", item.Title);
            },
            item =>
            {
                Assert.Equal("Harry Potter à l'École des Sorciers", item.Title);
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
                Assert.Equal("Harry Potter et l'Ordre du Phénix", item.Title);
            },
            item =>
            {
                Assert.Equal("Harry Potter et le Prince de Sang-Mêlé", item.Title);
            },
            item =>
            {
                Assert.Equal("Harry Potter et les Reliques de la Mort", item.Title);
            },
            item =>
            {
                Assert.Equal("Ça", item.Title);
            },
            item =>
            {
                Assert.Equal("Shining, l'enfant lumière", item.Title);
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
                Assert.Equal("Les Misérables", item.Title);
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
                Assert.Equal("Le Masque hanté", item.Title);
            },
            item =>
            {
                Assert.Equal("La Maison des morts", item.Title);
            },
            item =>
            {
                Assert.Equal("Le Loup-garou des marécages", item.Title);
            },
            item =>
            {
                Assert.Equal("Sous-sol interdit", item.Title);
            });
        }

        //[Fact]
        //public async Task Created_book_is_equals_to_model()
        //{
        //    await DataProvider.CleanDatabase(this.client);

        //    //Librarian.Core.Domain.Entities.Shelf shelves = DataProvider.Shelves.First();

        //    Librarian.RestFulAPI.V1.ViewModels.Books.CreateBookViewModel viewModel = new Librarian.RestFulAPI.V1.ViewModels.Books.CreateBookViewModel()
        //    {
        //        Title = "Escadron spectre",
        //        Categories = new List<EBookCategory>() { EBookCategory.ScienceFiction },
        //        ReleaseDate = DateTime.Now,
        //        NumberOfCopies = 10,
        //        ShelfId = shelves.Id
        //    };
        //    string json = JsonConvert.SerializeObject(viewModel);
        //    StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    string response = await this.client.PostAsync($"/api/v1/Books/create", formContent).Result.Content.ReadAsStringAsync();
        //    ContentResult<string> result = JsonConvert.DeserializeObject<ContentResult<string>>(response);
        //    Assert.True(result.Success);
        //    Assert.Null(result.Message);
        //    Assert.Null(result.Errors);
        //    Assert.NotNull(result.Result);
        //}
    }
}
