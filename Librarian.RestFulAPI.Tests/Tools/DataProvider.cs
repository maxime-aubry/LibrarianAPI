using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.V1.ViewModels.Authors;
using Librarian.RestFulAPI.V1.ViewModels.Books;
using Librarian.RestFulAPI.V1.ViewModels.Readers;
using Librarian.RestFulAPI.V1.ViewModels.Shelves;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.Tests.Tools
{
    public static class DataProvider
    {
        public static IEnumerable<Librarian.Core.Domain.Entities.Reader> Readers { get; set; }
        public static IEnumerable<Librarian.Core.Domain.Entities.Author> Authors { get; set; }
        public static IEnumerable<Librarian.Core.Domain.Entities.Book> Books { get; set; }
        public static IEnumerable<Librarian.Core.Domain.Entities.Shelf> Shelves { get; set; }

        public static void GenerateData()
        {
            DataProvider.Readers = new List<Librarian.Core.Domain.Entities.Reader>()
            {
                new Librarian.Core.Domain.Entities.Reader("Maxime", "AUBRY", new DateTime(1986, 12, 22), false),
                new Librarian.Core.Domain.Entities.Reader("David", "Zippari", new DateTime(1978, 10, 7), false),
                new Librarian.Core.Domain.Entities.Reader("Simon", "Louail", new DateTime(1995, 8, 5), false),
                new Librarian.Core.Domain.Entities.Reader("Wilfrid", "Lepape", new DateTime(1991, 5, 10), false),
                new Librarian.Core.Domain.Entities.Reader("Mathieu", "Decroocq", new DateTime(1989, 7, 21), false)
            };
            DataProvider.Authors = new List<Librarian.Core.Domain.Entities.Author>()
            {
                new Librarian.Core.Domain.Entities.Author("Jules", "Verne"),
                new Librarian.Core.Domain.Entities.Author("William", "Shakespeare"),
                new Librarian.Core.Domain.Entities.Author("Charles", "Baudelaire"),
                new Librarian.Core.Domain.Entities.Author("Charles", "Dickens"),
                new Librarian.Core.Domain.Entities.Author("J.R.R.", "Tolkien"),
                new Librarian.Core.Domain.Entities.Author("J.K.", "Rowling"),
                new Librarian.Core.Domain.Entities.Author("Stephen", "King"),
                new Librarian.Core.Domain.Entities.Author("Victor", "Hugo"),
                new Librarian.Core.Domain.Entities.Author("Ernest", "Hemingway"),
                new Librarian.Core.Domain.Entities.Author("Antoine", "de Saint-Exupéry"),
                new Librarian.Core.Domain.Entities.Author("R.L.", "Stine")
            };
            DataProvider.Books = new List<Librarian.Core.Domain.Entities.Book>()
            {
                // Jules Verne
                new Librarian.Core.Domain.Entities.Book("Vingt Mille Lieues sous les mers", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1870, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Voyage au centre de la Terre", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1864, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Tour du monde en quatre-vingts jours", new List<EBookCategory>() { EBookCategory.ActionAndAdventure }, new DateTime(1873, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("L'Île mystérieuse", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1874, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("De la Terre à la Lune", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1865, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Cinq Semaines en ballon", new List<EBookCategory>() { EBookCategory.ActionAndAdventure }, new DateTime(1863, 1, 1), 10, string.Empty),
                // William Shakespeare
                new Librarian.Core.Domain.Entities.Book("Roméo et Juliette", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1595, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Hamlet", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1603, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Macbeth", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1623, 1, 1), 10, string.Empty),
                // Charles Baudelaire
                new Librarian.Core.Domain.Entities.Book("Les Fleurs du mal", new List<EBookCategory>() { EBookCategory.Poetry }, new DateTime(1857, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Petits Poèmes en prose", new List<EBookCategory>() { EBookCategory.Poetry }, new DateTime(1869, 1, 1), 10, string.Empty),
                // Charkes Dickens
                new Librarian.Core.Domain.Entities.Book("Oliver Twist", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1839, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Les Grandes Espérances", new List<EBookCategory>() { EBookCategory.BiographyAndAutobiography, EBookCategory.ComicAndGraphicNovel }, new DateTime(1861, 1, 1), 10, string.Empty),
                // J.R.R. Tolkien
                new Librarian.Core.Domain.Entities.Book("Bilbo le Hobbit", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1937, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Silmarillion", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1977, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Seigneur des anneaux - La Communauté de l'Anneau", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1954, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Seigneur des anneaux - Les Deux Tours", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1954, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Seigneur des anneaux - Le Retour du Roi", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1955, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Les Enfants de Húrin", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2007, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Contes et légendes inachevés, tome 1", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1980, 1, 1), 10, string.Empty),
                // J.K. Rowling
                new Librarian.Core.Domain.Entities.Book("Harry Potter à l'École des Sorciers", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1997, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et la Chambre des Secrets", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1998, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et le Prisonnier d'Azkaban", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1999, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et la Coupe de Feu", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2000, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et l'Ordre du Phénix", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2003, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et le Prince de Sang-Mêlé", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2005, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Harry Potter et les Reliques de la Mort", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2007, 1, 1), 10, string.Empty),
                // Stephen King
                new Librarian.Core.Domain.Entities.Book("Ça", new List<EBookCategory>() { EBookCategory.Horror, EBookCategory.SuspenseAndThriller }, new DateTime(1956, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Shining, l'enfant lumière", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1977, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Misery", new List<EBookCategory>() { EBookCategory.Horror, EBookCategory.SuspenseAndThriller }, new DateTime(1987, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("La Ligne verte", new List<EBookCategory>() { EBookCategory.Fantasy, EBookCategory.MagicalRealism }, new DateTime(1996, 1, 1), 10, string.Empty),
                // Victor Hugo
                new Librarian.Core.Domain.Entities.Book("Les Misérables", new List<EBookCategory>() { EBookCategory.Classic }, new DateTime(1862, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Notre-Dame de Paris", new List<EBookCategory>() { EBookCategory.Classic }, new DateTime(1831, 1, 1), 10, string.Empty),
                // Ernest Hemingway
                new Librarian.Core.Domain.Entities.Book("Le Vieil Homme et la Mer", new List<EBookCategory>() { EBookCategory.ShortAndStory }, new DateTime(1852, 1, 1), 10, string.Empty),
                // Antoine de Saint-Exupéry
                new Librarian.Core.Domain.Entities.Book("Le Petit Prince", new List<EBookCategory>() { EBookCategory.ShortAndStory }, new DateTime(1943, 1, 1), 10, string.Empty),
                // R.L. Stine
                new Librarian.Core.Domain.Entities.Book("La Nuit des pantins", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Sang de monstre", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Dangereuses Photos", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Masque hanté", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("La Maison des morts", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Le Loup-garou des marécages", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Librarian.Core.Domain.Entities.Book("Sous-sol interdit", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty)
            };
            DataProvider.Shelves = new List<Librarian.Core.Domain.Entities.Shelf>()
            {
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.BiographyAndAutobiography),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.BiographyAndAutobiography),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Essay),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Essay),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Memoir),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Memoir),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.NarrativeNonfiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.NarrativeNonfiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Periodicals),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Periodicals),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.ReferenceBooks),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.ReferenceBooks),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.SelfhelpBook),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.SelfhelpBook),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Speech),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Speech),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Textbook),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Textbook),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Poetry),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.FirstFloor, EBookCategory.Poetry),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ActionAndAdventure),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ActionAndAdventure),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Anthology),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Anthology),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Classic),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Classic),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ComicAndGraphicNovel),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ComicAndGraphicNovel),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.CrimeAndDetective),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.CrimeAndDetective),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Drama),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Drama),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Fable),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Fable),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.FairyTale),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.FairyTale),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.FanFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.FanFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Fantasy),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Fantasy),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.HistoricalFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.HistoricalFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Horror),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Horror),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Humor),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Humor),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Legend),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Legend),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.MagicalRealism),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.MagicalRealism),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Mystery),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Mystery),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Mythology),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Mythology),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.RealisticFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.RealisticFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Romance),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Romance),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Satire),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.Satire),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ScienceFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ScienceFiction),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ShortAndStory),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.ShortAndStory),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.SuspenseAndThriller),
                new Librarian.Core.Domain.Entities.Shelf(string.Empty, 600, EFloor.SecondFloor, EBookCategory.SuspenseAndThriller)
            };
        }

        public static async Task CleanDatabase(HttpClient client)
        {
            await client.PostAsync("/api/v1/Admin/cleanDatabase", null);
        }

        public static async Task PopulateDatabase(HttpClient client)
        {
            DataProvider.GenerateData();

            await client.PostAsync("/api/v1/Admin/cleanDatabase", null);

            foreach (Librarian.Core.Domain.Entities.Reader reader in DataProvider.Readers)
            {
                CreateReaderViewModel viewModel = new CreateReaderViewModel()
                {
                    FirstName = reader.FirstName,
                    LastName = reader.LastName,
                    Birthday = reader.Birthday
                };
                string json = JsonConvert.SerializeObject(viewModel);
                StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
                string response = await client.PostAsync("/api/v1/Readers/create", formContent).Result.Content.ReadAsStringAsync();
                ContentResult<string> result = JsonConvert.DeserializeObject<ContentResult<string>>(response);

                if (!result.Success)
                    throw new Exception();

                reader.Id = result.Result;
            }

            foreach (Librarian.Core.Domain.Entities.Shelf shelf in DataProvider.Shelves)
            {
                CreateShelfViewModel viewModel = new CreateShelfViewModel()
                {
                    MaxQtyOfBooks = shelf.MaxQtyOfBooks,
                    Floor = shelf.Floor,
                    BookCategory = shelf.BookCategory
                };
                string json = JsonConvert.SerializeObject(viewModel);
                StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
                string response = await client.PostAsync("/api/v1/Shelves/create", formContent).Result.Content.ReadAsStringAsync();
                ContentResult<string> result = JsonConvert.DeserializeObject<ContentResult<string>>(response);

                if (!result.Success)
                    throw new Exception();

                shelf.Id = result.Result;
            }

            foreach (Librarian.Core.Domain.Entities.Author author in DataProvider.Authors)
            {
                CreateAuthorViewModel viewModel = new CreateAuthorViewModel()
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
                string json = JsonConvert.SerializeObject(viewModel);
                StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
                string response = await client.PostAsync("/api/v1/Authors/create", formContent).Result.Content.ReadAsStringAsync();
                ContentResult<string> result = JsonConvert.DeserializeObject<ContentResult<string>>(response);

                if (!result.Success)
                    throw new Exception();

                author.Id = result.Result;
            }

            foreach (Librarian.Core.Domain.Entities.Book book in DataProvider.Books)
            {
                Librarian.Core.Domain.Entities.Shelf shelf = (from s in Shelves
                                                              join b in Books on s.Id equals b.ShelfId into bookItems
                                                              where s.BookCategory == book.Categories.First()
                                                              && (bookItems.Sum(bi => bi.NumberOfCopies) + book.NumberOfCopies) <= s.MaxQtyOfBooks
                                                                select s).FirstOrDefault();
                book.ShelfId = shelf.Id;

                CreateBookViewModel viewModel = new CreateBookViewModel()
                {
                    Title = book.Title,
                    Categories = book.Categories,
                    ReleaseDate = book.RealeaseDate,
                    NumberOfCopies = book.NumberOfCopies,
                    ShelfId = book.ShelfId
                };
                string json = JsonConvert.SerializeObject(viewModel);
                StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
                string response = await client.PostAsync("/api/v1/Books/create", formContent).Result.Content.ReadAsStringAsync();
                ContentResult<string> result = JsonConvert.DeserializeObject<ContentResult<string>>(response);

                if (!result.Success)
                    throw new Exception();

                book.Id = result.Result;
            }

            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "Vingt Mille Lieues sous les mers");
            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "Voyage au centre de la Terre");
            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "Le Tour du monde en quatre-vingts jours");
            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "L'Île mystérieuse");
            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "De la Terre à la Lune");
            await DataProvider.AuthorWritesBook(client, "Jules", "Verne", "Cinq Semaines en ballon");
            await DataProvider.AuthorWritesBook(client, "William", "Shakespeare", "Roméo et Juliette");
            await DataProvider.AuthorWritesBook(client, "William", "Shakespeare", "Hamlet");
            await DataProvider.AuthorWritesBook(client, "William", "Shakespeare", "Macbeth");
            await DataProvider.AuthorWritesBook(client, "Charles", "Baudelaire", "Les Fleurs du mal");
            await DataProvider.AuthorWritesBook(client, "Charles", "Baudelaire", "Petits Poèmes en prose");
            await DataProvider.AuthorWritesBook(client, "Charles", "Dickens", "Oliver Twist");
            await DataProvider.AuthorWritesBook(client, "Charles", "Dickens", "Les Grandes Espérances");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Bilbo le Hobbit");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Silmarillion");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Les Enfants de Húrin");
            await DataProvider.AuthorWritesBook(client, "J.R.R.", "Tolkien", "Contes et légendes inachevés, tome 1");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter à l'École des Sorciers");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et la Chambre des Secrets");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et le Prisonnier d'Azkaban");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et la Coupe de Feu");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et l'Ordre du Phénix");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et le Prince de Sang-Mêlé");
            await DataProvider.AuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et les Reliques de la Mort");
            await DataProvider.AuthorWritesBook(client, "Stephen", "King", "Ça");
            await DataProvider.AuthorWritesBook(client, "Stephen", "King", "Shining, l'enfant lumière");
            await DataProvider.AuthorWritesBook(client, "Stephen", "King", "Misery");
            await DataProvider.AuthorWritesBook(client, "Stephen", "King", "La Ligne verte");
            await DataProvider.AuthorWritesBook(client, "Victor", "Hugo", "Les Misérables");
            await DataProvider.AuthorWritesBook(client, "Victor", "Hugo", "Notre-Dame de Paris");
            await DataProvider.AuthorWritesBook(client, "Ernest", "Hemingway", "Le Vieil Homme et la Mer");
            await DataProvider.AuthorWritesBook(client, "Antoine", "de Saint-Exupéry", "Le Petit Prince");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "La Nuit des pantins");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "Sang de monstre");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "Dangereuses Photos");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "Le Masque hanté");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "La Maison des morts");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "Le Loup-garou des marécages");
            await DataProvider.AuthorWritesBook(client, "R.L.", "Stine", "Sous-sol interdit");

            await DataProvider.ReaderLoansBook(client, "Maxime", "AUBRY", "Vingt Mille Lieues sous les mers");
            await DataProvider.ReaderLoansBook(client, "Maxime", "AUBRY", "Le Silmarillion");
            await DataProvider.ReaderLoansBook(client, "Maxime", "AUBRY", "Misery");
            await DataProvider.ReaderLoansBook(client, "Maxime", "AUBRY", "Le Petit Prince");
            await DataProvider.ReaderLoansBook(client, "Maxime", "AUBRY", "La Nuit des pantins");
            await DataProvider.ReaderLoansBook(client, "David", "Zippari", "Roméo et Juliette");
            await DataProvider.ReaderLoansBook(client, "David", "Zippari", "Hamlet");
            await DataProvider.ReaderLoansBook(client, "David", "Zippari", "Macbeth");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "La Nuit des pantins");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "Sang de monstre");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "Dangereuses Photos");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "Le Masque hanté");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "Vingt Mille Lieues sous les mers");
            await DataProvider.ReaderLoansBook(client, "Simon", "Louail", "Misery");
            await DataProvider.ReaderLoansBook(client, "Wilfrid", "Lepape", "Ça");
            await DataProvider.ReaderLoansBook(client, "Wilfrid", "Lepape", "Shining, l'enfant lumière");
            await DataProvider.ReaderLoansBook(client, "Wilfrid", "Lepape", "Misery");
            await DataProvider.ReaderLoansBook(client, "Wilfrid", "Lepape", "La Ligne verte");
            await DataProvider.ReaderLoansBook(client, "Wilfrid", "Lepape", "Le Silmarillion");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Bilbo le Hobbit");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Le Silmarillion");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Les Enfants de Húrin");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Contes et légendes inachevés, tome 1");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Dangereuses Photos");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Misery");
            await DataProvider.ReaderLoansBook(client, "Mathieu", "Decroocq", "Le Petit Prince");

            await DataProvider.ReaderRatesBook(client, "Maxime", "AUBRY", "Vingt Mille Lieues sous les mers");
            await DataProvider.ReaderRatesBook(client, "Maxime", "AUBRY", "Le Silmarillion");
            await DataProvider.ReaderRatesBook(client, "Maxime", "AUBRY", "Misery");
            await DataProvider.ReaderRatesBook(client, "Maxime", "AUBRY", "Le Petit Prince");
            await DataProvider.ReaderRatesBook(client, "Maxime", "AUBRY", "La Nuit des pantins");
            await DataProvider.ReaderRatesBook(client, "David", "Zippari", "Roméo et Juliette");
            await DataProvider.ReaderRatesBook(client, "David", "Zippari", "Hamlet");
            await DataProvider.ReaderRatesBook(client, "David", "Zippari", "Macbeth");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "La Nuit des pantins");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "Sang de monstre");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "Dangereuses Photos");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "Le Masque hanté");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "Vingt Mille Lieues sous les mers");
            await DataProvider.ReaderRatesBook(client, "Simon", "Louail", "Misery");
            await DataProvider.ReaderRatesBook(client, "Wilfrid", "Lepape", "Ça");
            await DataProvider.ReaderRatesBook(client, "Wilfrid", "Lepape", "Shining, l'enfant lumière");
            await DataProvider.ReaderRatesBook(client, "Wilfrid", "Lepape", "Misery");
            await DataProvider.ReaderRatesBook(client, "Wilfrid", "Lepape", "La Ligne verte");
            await DataProvider.ReaderRatesBook(client, "Wilfrid", "Lepape", "Le Silmarillion");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Bilbo le Hobbit");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Le Silmarillion");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Les Enfants de Húrin");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Contes et légendes inachevés, tome 1");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Dangereuses Photos");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Misery");
            await DataProvider.ReaderRatesBook(client, "Mathieu", "Decroocq", "Le Petit Prince");
        }

        private static async Task AuthorWritesBook(HttpClient client, string firstname, string lastname, string title)
        {
            Librarian.Core.Domain.Entities.Author author = DataProvider.GetAuthorByName(firstname, lastname);
            Librarian.Core.Domain.Entities.Book book = DataProvider.GetBookByName(title);

            if (author == null | book == null)
                throw new Exception();

            AddAuthorsToBookViewModel viewModel = new AddAuthorsToBookViewModel()
            {
                BookId = book.Id,
                AuthorIds = new List<string>() { author.Id }
            };
            string json = JsonConvert.SerializeObject(viewModel);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response = await client.PostAsync("/api/v1/Books/authors/add", formContent).Result.Content.ReadAsStringAsync();
        }

        private static async Task ReaderLoansBook(HttpClient client, string firstname, string lastname, string title)
        {
            Librarian.Core.Domain.Entities.Reader reader = DataProvider.GetReaderByName(firstname, lastname);
            Librarian.Core.Domain.Entities.Book book = DataProvider.GetBookByName(title);

            if (reader == null | book == null)
                throw new Exception();

            AddLoanViewModel viewModel = new AddLoanViewModel()
            {
                ReaderId = reader.Id,
                BookId = book.Id
            };
            string json = JsonConvert.SerializeObject(viewModel);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response = await client.PostAsync("/api/v1/Readers/loan/add", formContent).Result.Content.ReadAsStringAsync();
        }

        private static async Task ReaderRatesBook(HttpClient client, string firstname, string lastname, string title)
        {
            Librarian.Core.Domain.Entities.Reader reader = DataProvider.GetReaderByName(firstname, lastname);
            Librarian.Core.Domain.Entities.Book book = DataProvider.GetBookByName(title);

            if (reader == null | book == null)
                throw new Exception();

            Random rand = new Random();
            float rate = (float)(rand.NextDouble() * (5 - 3) + 3);

            CreateRateBookViewModel viewModel = new CreateRateBookViewModel()
            {
                ReaderId = reader.Id,
                BookId = book.Id,
                Rate = rate,
                Commment = "J'ai adoré !"
            };
            string json = JsonConvert.SerializeObject(viewModel);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response = await client.PostAsync("/api/v1/Books/rating/add", formContent).Result.Content.ReadAsStringAsync();
        }

        private static Librarian.Core.Domain.Entities.Reader GetReaderByName(string firstname, string lastname)
        {
            Librarian.Core.Domain.Entities.Reader reader = (from r in DataProvider.Readers
                                                            where r.FirstName.ToLower() == firstname.ToLower()
                                                            && r.LastName.ToLower() == lastname.ToLower()
                                                            select r).SingleOrDefault();
            return reader;
        }

        private static Librarian.Core.Domain.Entities.Author GetAuthorByName(string firstname, string lastname)
        {
            Librarian.Core.Domain.Entities.Author author = (from a in DataProvider.Authors
                                                            where a.FirstName.ToLower() == firstname.ToLower()
                                                            && a.LastName.ToLower() == lastname.ToLower()
                                                            select a).SingleOrDefault();
            return author;
        }

        private static Librarian.Core.Domain.Entities.Book GetBookByName(string title)
        {
            Librarian.Core.Domain.Entities.Book book = (from b in DataProvider.Books
                                                        where b.Title.ToLower() == title.ToLower()
                                                        select b).SingleOrDefault();
            return book;
        }
    }
}
