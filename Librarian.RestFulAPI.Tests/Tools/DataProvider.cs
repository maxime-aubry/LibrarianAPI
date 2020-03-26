using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using Librarian.RestFulAPI.V1.ViewModels.Authors;
using Librarian.RestFulAPI.V1.ViewModels.Books;
using Librarian.RestFulAPI.V1.ViewModels.Readers;
using Librarian.RestFulAPI.V1.ViewModels.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.Tests.Tools
{
    public static class DataProvider
    {
        public static IEnumerable<Reader> Readers { get; set; }
        public static IEnumerable<Author> Authors { get; set; }
        public static IEnumerable<Book> Books { get; set; }
        public static IEnumerable<Shelf> Shelves { get; set; }
        public static IDictionary<string, AuthorWritesBook> AuthorWritesBook { get; set; }
        public static List<ReaderLoansBook> ReaderLoansBook { get; set; }
        public static List<ReaderRatesBook> ReaderRatesBook { get; set; }

        public static void GenerateLocalData()
        {
            DataProvider.Readers = new List<Reader>()
            {
                new Reader("Maxime", "AUBRY", new DateTime(1986, 12, 22), false),
                new Reader("David", "Zippari", new DateTime(1978, 10, 7), false),
                new Reader("Simon", "Louail", new DateTime(1995, 8, 5), false),
                new Reader("Wilfrid", "Lepape", new DateTime(1991, 5, 10), false),
                new Reader("Mathieu", "Decroocq", new DateTime(1989, 7, 21), false)
            };
            DataProvider.Authors = new List<Author>()
            {
                new Author("Jules", "Verne"),
                new Author("William", "Shakespeare"),
                new Author("Charles", "Baudelaire"),
                new Author("Charles", "Dickens"),
                new Author("J.R.R.", "Tolkien"),
                new Author("J.K.", "Rowling"),
                new Author("Stephen", "King"),
                new Author("Victor", "Hugo"),
                new Author("Ernest", "Hemingway"),
                new Author("Antoine", "de Saint-Exupéry"),
                new Author("R.L.", "Stine")
            };
            DataProvider.Books = new List<Book>()
            {
                // Jules Verne
                new Book("Vingt Mille Lieues sous les mers", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1870, 1, 1, 0, 0, 0, DateTimeKind.Utc), 10, string.Empty),
                new Book("Voyage au centre de la Terre", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1864, 1, 1), 10, string.Empty),
                new Book("Le Tour du monde en quatre-vingts jours", new List<EBookCategory>() { EBookCategory.ActionAndAdventure }, new DateTime(1873, 1, 1), 10, string.Empty),
                new Book("L'Île mystérieuse", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1874, 1, 1), 10, string.Empty),
                new Book("De la Terre à la Lune", new List<EBookCategory>() { EBookCategory.ActionAndAdventure, EBookCategory.ScienceFiction }, new DateTime(1865, 1, 1), 10, string.Empty),
                new Book("Cinq Semaines en ballon", new List<EBookCategory>() { EBookCategory.ActionAndAdventure }, new DateTime(1863, 1, 1), 10, string.Empty),
                // William Shakespeare
                new Book("Roméo et Juliette", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1595, 1, 1), 10, string.Empty),
                new Book("Hamlet", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1603, 1, 1), 10, string.Empty),
                new Book("Macbeth", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1623, 1, 1), 10, string.Empty),
                // Charles Baudelaire
                new Book("Les Fleurs du mal", new List<EBookCategory>() { EBookCategory.Poetry }, new DateTime(1857, 1, 1), 10, string.Empty),
                new Book("Petits Poèmes en prose", new List<EBookCategory>() { EBookCategory.Poetry }, new DateTime(1869, 1, 1), 10, string.Empty),
                // Charkes Dickens
                new Book("Oliver Twist", new List<EBookCategory>() { EBookCategory.Drama }, new DateTime(1839, 1, 1), 10, string.Empty),
                new Book("Les Grandes Espérances", new List<EBookCategory>() { EBookCategory.BiographyAndAutobiography, EBookCategory.ComicAndGraphicNovel }, new DateTime(1861, 1, 1), 10, string.Empty),
                // J.R.R. Tolkien
                new Book("Bilbo le Hobbit", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1937, 1, 1), 10, string.Empty),
                new Book("Le Silmarillion", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1977, 1, 1), 10, string.Empty),
                new Book("Le Seigneur des anneaux - La Communauté de l'Anneau", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1954, 1, 1), 10, string.Empty),
                new Book("Le Seigneur des anneaux - Les Deux Tours", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1954, 1, 1), 10, string.Empty),
                new Book("Le Seigneur des anneaux - Le Retour du Roi", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1955, 1, 1), 10, string.Empty),
                new Book("Les Enfants de Húrin", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2007, 1, 1), 10, string.Empty),
                new Book("Contes et légendes inachevés, tome 1", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1980, 1, 1), 10, string.Empty),
                // J.K. Rowling
                new Book("Harry Potter à l'École des Sorciers", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1997, 1, 1), 10, string.Empty),
                new Book("Harry Potter et la Chambre des Secrets", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1998, 1, 1), 10, string.Empty),
                new Book("Harry Potter et le Prisonnier d'Azkaban", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(1999, 1, 1), 10, string.Empty),
                new Book("Harry Potter et la Coupe de Feu", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2000, 1, 1), 10, string.Empty),
                new Book("Harry Potter et l'Ordre du Phénix", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2003, 1, 1), 10, string.Empty),
                new Book("Harry Potter et le Prince de Sang-Mêlé", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2005, 1, 1), 10, string.Empty),
                new Book("Harry Potter et les Reliques de la Mort", new List<EBookCategory>() { EBookCategory.Fantasy }, new DateTime(2007, 1, 1), 10, string.Empty),
                // Stephen King
                new Book("Ça", new List<EBookCategory>() { EBookCategory.Horror, EBookCategory.SuspenseAndThriller }, new DateTime(1956, 1, 1), 10, string.Empty),
                new Book("Shining, l'enfant lumière", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1977, 1, 1), 10, string.Empty),
                new Book("Misery", new List<EBookCategory>() { EBookCategory.Horror, EBookCategory.SuspenseAndThriller }, new DateTime(1987, 1, 1), 10, string.Empty),
                new Book("La Ligne verte", new List<EBookCategory>() { EBookCategory.Fantasy, EBookCategory.MagicalRealism }, new DateTime(1996, 1, 1), 10, string.Empty),
                // Victor Hugo
                new Book("Les Misérables", new List<EBookCategory>() { EBookCategory.Classic }, new DateTime(1862, 1, 1), 10, string.Empty),
                new Book("Notre-Dame de Paris", new List<EBookCategory>() { EBookCategory.Classic }, new DateTime(1831, 1, 1), 10, string.Empty),
                // Ernest Hemingway
                new Book("Le Vieil Homme et la Mer", new List<EBookCategory>() { EBookCategory.ShortAndStory }, new DateTime(1852, 1, 1), 10, string.Empty),
                // Antoine de Saint-Exupéry
                new Book("Le Petit Prince", new List<EBookCategory>() { EBookCategory.ShortAndStory }, new DateTime(1943, 1, 1), 10, string.Empty),
                // R.L. Stine
                new Book("La Nuit des pantins", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Book("Sang de monstre", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty),
                new Book("Dangereuses Photos", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty),
                new Book("Le Masque hanté", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Book("La Maison des morts", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Book("Le Loup-garou des marécages", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1993, 1, 1), 10, string.Empty),
                new Book("Sous-sol interdit", new List<EBookCategory>() { EBookCategory.Horror }, new DateTime(1992, 1, 1), 10, string.Empty)
            };
            DataProvider.Shelves = new List<Shelf>()
            {
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.BiographyAndAutobiography),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.BiographyAndAutobiography),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Essay),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Essay),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Memoir),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Memoir),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.NarrativeNonfiction),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.NarrativeNonfiction),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Periodicals),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Periodicals),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.ReferenceBooks),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.ReferenceBooks),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.SelfhelpBook),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.SelfhelpBook),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Speech),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Speech),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Textbook),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Textbook),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Poetry),
                new Shelf(string.Empty, 600, 600, EFloor.FirstFloor, EBookCategory.Poetry),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ActionAndAdventure),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ActionAndAdventure),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Anthology),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Anthology),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Classic),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Classic),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ComicAndGraphicNovel),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ComicAndGraphicNovel),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.CrimeAndDetective),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.CrimeAndDetective),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Drama),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Drama),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Fable),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Fable),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.FairyTale),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.FairyTale),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.FanFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.FanFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Fantasy),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Fantasy),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.HistoricalFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.HistoricalFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Horror),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Horror),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Humor),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Humor),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Legend),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Legend),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.MagicalRealism),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.MagicalRealism),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Mystery),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Mystery),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Mythology),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Mythology),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.RealisticFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.RealisticFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Romance),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Romance),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Satire),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.Satire),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ScienceFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ScienceFiction),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ShortAndStory),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.ShortAndStory),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.SuspenseAndThriller),
                new Shelf(string.Empty, 600, 600, EFloor.SecondFloor, EBookCategory.SuspenseAndThriller)
            };
            DataProvider.AuthorWritesBook = new Dictionary<string, AuthorWritesBook>();
            DataProvider.ReaderLoansBook = new List<ReaderLoansBook>();
            DataProvider.ReaderRatesBook = new List<ReaderRatesBook>();
        }

        public static async Task CleanDatabase(HttpClient client)
        {
            await client.PostAsync("/api/v1/Admin/cleanDatabase", null);
        }

        public static async Task PopulateDatabase(HttpClient client)
        {
            DataProvider.GenerateLocalData();

            await client.PostAsync("/api/v1/Admin/cleanDatabase", null);

            foreach (Reader reader in DataProvider.Readers)
            {
                CreateReaderViewModel viewModel = new CreateReaderViewModel()
                {
                    FirstName = reader.FirstName,
                    LastName = reader.LastName,
                    Birthday = reader.Birthday
                };
                ContentResult<string> result = await HttpHelper.Post<CreateReaderViewModel, string>(client, "/api/v1/Readers/create", viewModel);

                if (!result.Success)
                    throw new Exception();

                reader.Id = result.Result;
            }

            foreach (Shelf shelf in DataProvider.Shelves)
            {
                CreateShelfViewModel viewModel = new CreateShelfViewModel()
                {
                    MaxQtyOfBooks = shelf.MaxQtyOfBooks,
                    Floor = shelf.Floor,
                    BookCategory = shelf.BookCategory
                };
                ContentResult<string> result1 = await HttpHelper.Post<CreateShelfViewModel, string>(client, "/api/v1/Shelves/create", viewModel);

                if (!result1.Success)
                    throw new Exception();

                shelf.Id = result1.Result;

                ContentResult<Shelf> result2 = await HttpHelper.Get<Shelf>(client, $"/api/v1/Shelves/getById/{shelf.Id}");

                if (!result1.Success)
                    throw new Exception();

                shelf.Name = result2.Result.Name;
            }

            foreach (Author author in DataProvider.Authors)
            {
                CreateAuthorViewModel viewModel = new CreateAuthorViewModel()
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
                ContentResult<string> result = await HttpHelper.Post<CreateAuthorViewModel, string>(client, "/api/v1/Authors/create", viewModel);

                if (!result.Success)
                    throw new Exception();

                author.Id = result.Result;
            }

            foreach (Book book in DataProvider.Books)
            {
                Shelf shelf = (from s in Shelves
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
                ContentResult<string> result = await HttpHelper.Post<CreateBookViewModel, string>(client, "/api/v1/Books/create", viewModel);

                if (!result.Success)
                    throw new Exception();

                book.Id = result.Result;
            }

            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "Vingt Mille Lieues sous les mers");
            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "Voyage au centre de la Terre");
            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "Le Tour du monde en quatre-vingts jours");
            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "L'Île mystérieuse");
            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "De la Terre à la Lune");
            await DataProvider.AddAuthorWritesBook(client, "Jules", "Verne", "Cinq Semaines en ballon");
            await DataProvider.AddAuthorWritesBook(client, "William", "Shakespeare", "Roméo et Juliette");
            await DataProvider.AddAuthorWritesBook(client, "William", "Shakespeare", "Hamlet");
            await DataProvider.AddAuthorWritesBook(client, "William", "Shakespeare", "Macbeth");
            await DataProvider.AddAuthorWritesBook(client, "Charles", "Baudelaire", "Les Fleurs du mal");
            await DataProvider.AddAuthorWritesBook(client, "Charles", "Baudelaire", "Petits Poèmes en prose");
            await DataProvider.AddAuthorWritesBook(client, "Charles", "Dickens", "Oliver Twist");
            await DataProvider.AddAuthorWritesBook(client, "Charles", "Dickens", "Les Grandes Espérances");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Bilbo le Hobbit");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Silmarillion");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Les Enfants de Húrin");
            await DataProvider.AddAuthorWritesBook(client, "J.R.R.", "Tolkien", "Contes et légendes inachevés, tome 1");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter à l'École des Sorciers");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et la Chambre des Secrets");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et le Prisonnier d'Azkaban");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et la Coupe de Feu");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et l'Ordre du Phénix");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et le Prince de Sang-Mêlé");
            await DataProvider.AddAuthorWritesBook(client, "J.K.", "Rowling", "Harry Potter et les Reliques de la Mort");
            await DataProvider.AddAuthorWritesBook(client, "Stephen", "King", "Ça");
            await DataProvider.AddAuthorWritesBook(client, "Stephen", "King", "Shining, l'enfant lumière");
            await DataProvider.AddAuthorWritesBook(client, "Stephen", "King", "Misery");
            await DataProvider.AddAuthorWritesBook(client, "Stephen", "King", "La Ligne verte");
            await DataProvider.AddAuthorWritesBook(client, "Victor", "Hugo", "Les Misérables");
            await DataProvider.AddAuthorWritesBook(client, "Victor", "Hugo", "Notre-Dame de Paris");
            await DataProvider.AddAuthorWritesBook(client, "Ernest", "Hemingway", "Le Vieil Homme et la Mer");
            await DataProvider.AddAuthorWritesBook(client, "Antoine", "de Saint-Exupéry", "Le Petit Prince");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "La Nuit des pantins");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "Sang de monstre");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "Dangereuses Photos");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "Le Masque hanté");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "La Maison des morts");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "Le Loup-garou des marécages");
            await DataProvider.AddAuthorWritesBook(client, "R.L.", "Stine", "Sous-sol interdit");

            await DataProvider.AddReaderLoansBook(client, "Maxime", "AUBRY", "Vingt Mille Lieues sous les mers");
            await DataProvider.AddReaderLoansBook(client, "Maxime", "AUBRY", "Le Silmarillion");
            await DataProvider.AddReaderLoansBook(client, "Maxime", "AUBRY", "Misery");
            await DataProvider.AddReaderLoansBook(client, "Maxime", "AUBRY", "Le Petit Prince");
            await DataProvider.AddReaderLoansBook(client, "Maxime", "AUBRY", "La Nuit des pantins");
            await DataProvider.AddReaderLoansBook(client, "David", "Zippari", "Roméo et Juliette");
            await DataProvider.AddReaderLoansBook(client, "David", "Zippari", "Hamlet");
            await DataProvider.AddReaderLoansBook(client, "David", "Zippari", "Macbeth");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "La Nuit des pantins");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "Sang de monstre");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "Dangereuses Photos");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "Le Masque hanté");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "Vingt Mille Lieues sous les mers");
            await DataProvider.AddReaderLoansBook(client, "Simon", "Louail", "Misery");
            await DataProvider.AddReaderLoansBook(client, "Wilfrid", "Lepape", "Ça");
            await DataProvider.AddReaderLoansBook(client, "Wilfrid", "Lepape", "Shining, l'enfant lumière");
            await DataProvider.AddReaderLoansBook(client, "Wilfrid", "Lepape", "Misery");
            await DataProvider.AddReaderLoansBook(client, "Wilfrid", "Lepape", "La Ligne verte");
            await DataProvider.AddReaderLoansBook(client, "Wilfrid", "Lepape", "Le Silmarillion");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Bilbo le Hobbit");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Le Silmarillion");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Les Enfants de Húrin");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Contes et légendes inachevés, tome 1");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Dangereuses Photos");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Misery");
            await DataProvider.AddReaderLoansBook(client, "Mathieu", "Decroocq", "Le Petit Prince");

            await DataProvider.AddReaderRatesBook(client, "Maxime", "AUBRY", "Vingt Mille Lieues sous les mers");
            await DataProvider.AddReaderRatesBook(client, "Maxime", "AUBRY", "Le Silmarillion");
            await DataProvider.AddReaderRatesBook(client, "Maxime", "AUBRY", "Misery");
            await DataProvider.AddReaderRatesBook(client, "Maxime", "AUBRY", "Le Petit Prince");
            await DataProvider.AddReaderRatesBook(client, "Maxime", "AUBRY", "La Nuit des pantins");
            await DataProvider.AddReaderRatesBook(client, "David", "Zippari", "Roméo et Juliette");
            await DataProvider.AddReaderRatesBook(client, "David", "Zippari", "Hamlet");
            await DataProvider.AddReaderRatesBook(client, "David", "Zippari", "Macbeth");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "La Nuit des pantins");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "Sang de monstre");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "Dangereuses Photos");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "Le Masque hanté");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "Vingt Mille Lieues sous les mers");
            await DataProvider.AddReaderRatesBook(client, "Simon", "Louail", "Misery");
            await DataProvider.AddReaderRatesBook(client, "Wilfrid", "Lepape", "Ça");
            await DataProvider.AddReaderRatesBook(client, "Wilfrid", "Lepape", "Shining, l'enfant lumière");
            await DataProvider.AddReaderRatesBook(client, "Wilfrid", "Lepape", "Misery");
            await DataProvider.AddReaderRatesBook(client, "Wilfrid", "Lepape", "La Ligne verte");
            await DataProvider.AddReaderRatesBook(client, "Wilfrid", "Lepape", "Le Silmarillion");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Bilbo le Hobbit");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Le Silmarillion");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - La Communauté de l'Anneau");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Les Deux Tours");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Le Seigneur des anneaux - Le Retour du Roi");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Les Enfants de Húrin");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Contes et légendes inachevés, tome 1");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Dangereuses Photos");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Misery");
            await DataProvider.AddReaderRatesBook(client, "Mathieu", "Decroocq", "Le Petit Prince");
        }

        private static async Task AddAuthorWritesBook(HttpClient client, string firstname, string lastname, string title)
        {
            Author author = DataProvider.GetAuthorByName(firstname, lastname);
            Book book = DataProvider.GetBookByName(title);

            if (author == null | book == null)
                throw new Exception();

            AddAuthorsToBookViewModel viewModel = new AddAuthorsToBookViewModel()
            {
                BookId = book.Id,
                AuthorId = author.Id
            };
            ContentResult<string> result = await HttpHelper.Post<AddAuthorsToBookViewModel, string>(client, "/api/v1/Books/authors/add", viewModel);

            DataProvider.AuthorWritesBook.Add(result.Result, new AuthorWritesBook(author.Id, book.Id));
        }

        private static async Task AddReaderLoansBook(HttpClient client, string firstname, string lastname, string title)
        {
            Reader reader = DataProvider.GetReaderByName(firstname, lastname);
            Book book = DataProvider.GetBookByName(title);

            if (reader == null | book == null)
                throw new Exception();

            AddLoanViewModel viewModel = new AddLoanViewModel()
            {
                ReaderId = reader.Id,
                BookId = book.Id
            };
            ContentResult<string> result1 = await HttpHelper.Post<AddLoanViewModel, string>(client, "/api/v1/Readers/loans/add", viewModel);

            // get loans
            ContentResult<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>> result2 = await HttpHelper.Get<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>(client, $"/api/v1/Readers/loans/{reader.Id}");

            DataProvider.ReaderLoansBook.Add(result2.Result.Where(r => r.Id == result1.Result).Single());
        }

        private static async Task AddReaderRatesBook(HttpClient client, string firstname, string lastname, string title)
        {
            Reader reader = DataProvider.GetReaderByName(firstname, lastname);
            Book book = DataProvider.GetBookByName(title);

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
            ContentResult<string> result1 = await HttpHelper.Post<CreateRateBookViewModel, string>(client, "/api/v1/Books/rates/add", viewModel);

            // get rates
            ContentResult<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>> result2 = await HttpHelper.Get<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>(client, $"/api/v1/Books/rates/{book.Id}");

            DataProvider.ReaderRatesBook.Add(result2.Result.Where(r => r.Id == result1.Result).Single());
        }

        private static Reader GetReaderByName(string firstname, string lastname)
        {
            Reader reader = (from r in DataProvider.Readers
                            where r.FirstName.ToLower() == firstname.ToLower()
                            && r.LastName.ToLower() == lastname.ToLower()
                            select r).SingleOrDefault();
            return reader;
        }

        private static Author GetAuthorByName(string firstname, string lastname)
        {
            Author author = (from a in DataProvider.Authors
                            where a.FirstName.ToLower() == firstname.ToLower()
                            && a.LastName.ToLower() == lastname.ToLower()
                            select a).SingleOrDefault();
            return author;
        }

        private static Book GetBookByName(string title)
        {
            Book book = (from b in DataProvider.Books
                        where b.Title.ToLower() == title.ToLower()
                        select b).SingleOrDefault();
            return book;
        }
    }
}
