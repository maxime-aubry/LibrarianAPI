using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.Domain.Enums;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class BookRepository : Repository<Librarian.Infrastructure.Entities.Book, Librarian.Core.Domain.Entities.Book>, IBookRepository
    {
        public BookRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public new async Task<GateawayResponse<string>> Add(Librarian.Core.Domain.Entities.Book model)
        {
            Librarian.Infrastructure.Entities.Shelf shelf = (from s in this.dbContext.Shelves.AsQueryable().AsEnumerable()
                                                             where s.Id == model.ShelfId
                                                             select s).FirstOrDefault();

            if (shelf == null)
                return new GateawayResponse<string>(null, false, new[] { new Error($"add_book_failure", "No shelf found.") });

            int qtyOfBooksInShelf = (from b in this.dbContext.Books.AsQueryable().AsEnumerable()
                                    where b.ShelfId == model.ShelfId
                                    select b).Count();

            if (qtyOfBooksInShelf == shelf.MaxQtyOfBooks)
                return new GateawayResponse<string>(null, false, new[] { new Error($"add_book_failure", "Shelf is full.") });

            if ((qtyOfBooksInShelf + model.NumberOfCopies) > shelf.MaxQtyOfBooks)
                return new GateawayResponse<string>(null, false, new[] { new Error($"add_book_failure", "Missing space in shelf.") });

            return await base.Add(model);
        }

        public async Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>> GetByFilters(string title, IEnumerable<int> categories, IEnumerable<string> authorIds)
        {
            int numberOfPoints = (1 + categories.Count() + authorIds.Count());

            void SetPertinence(Librarian.Core.Domain.Entities.FindBooksByFilters result)
            {
                int numberOfPointsForItem = 0;

                // check title
                if (result.Title.ToLower().Contains(title.ToLower()))
                    numberOfPointsForItem++;

                // check categories
                foreach (int category in categories)
                    if (result.Categories.Select(c => (int)c).Contains(category))
                        numberOfPointsForItem++;

                // check authors
                foreach (string authorId in authorIds)
                    if (result.Authors.Select(a => a.Id).Contains(authorId))
                        numberOfPointsForItem++;

                result.Pertinence = ((float)numberOfPointsForItem / numberOfPoints);
            }

            IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters> results = await Task.Run(() => {
                // get all items
                IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters> items = (from b in this.dbContext.Books.AsQueryable().AsEnumerable()
                                                                                        join awb in this.dbContext.AuthorWritesBook.AsQueryable().AsEnumerable() on b.Id equals awb.BookId
                                                                                        join a in this.dbContext.Authors.AsQueryable().AsEnumerable() on awb.AuthorId equals a.Id into authors
                                                                                        join rlb in this.dbContext.ReaderLoansBook.AsQueryable().AsEnumerable() on b.Id equals rlb.BookId into loans
                                                                                        join rrb in this.dbContext.ReaderRatesBook.AsQueryable().AsEnumerable() on b.Id equals rrb.BookId into rates
                                                                                        select new Librarian.Core.Domain.Entities.FindBooksByFilters
                                                                                        (
                                                                                            b.Id,
                                                                                            b.Title,
                                                                                            b.Categories.Select(c => (EBookCategory)Enum.ToObject(typeof(EBookCategory), c)),
                                                                                            b.RealeaseDate,
                                                                                            authors.Select(author => new Librarian.Core.Domain.Entities.AuthorOfBook(author.Id, author.FirstName, author.LastName)),
                                                                                            0,
                                                                                            loans?.Count() ?? 0,
                                                                                            rates?.Select(r => r.Rate).Average() ?? 0
                                                                                        ));

                foreach (Librarian.Core.Domain.Entities.FindBooksByFilters item in items)
                    SetPertinence(item);
                items = items.Where(b => b.Pertinence > 0);
                return items;
            });

            if (results == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>(null, false, new[] { new Error($"get_books_by_filers_failure", "No books found.") });

            return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>(results, true);
        }

        public async Task<GateawayResponse<string>> AddCopies(string id, int numberOfCopies)
        {
            GateawayResponse<Librarian.Core.Domain.Entities.Book> book = await this.Get(id);

            if (book == null)
                return new GateawayResponse<string>(null, false, new[] { new Error($"set_number_of_copies_failure", "No book found.") });

            book.Data.NumberOfCopies += numberOfCopies;

            return await this.Update(id, book.Data);
        }

        public async Task<GateawayResponse<string>> ReduceCopies(string id, int numberOfCopies)
        {
            GateawayResponse<Librarian.Core.Domain.Entities.Book> book = await this.Get(id);

            if (book == null)
                return new GateawayResponse<string>(null, false, new[] { new Error($"set_number_of_copies_failure", "No book found.") });

            book.Data.NumberOfCopies -= numberOfCopies;

            return await this.Update(id, book.Data);
        }
    }
}
