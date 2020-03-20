using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class AuthorWritesBookRepository : Repository<Librarian.Infrastructure.Entities.AuthorWritesBook, Librarian.Core.Domain.Entities.AuthorWritesBook>, IAuthorWritesBookRepository
    {
        public AuthorWritesBookRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public async Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>>> GetBooks(string authorId)
        {
            IEnumerable<Librarian.Infrastructure.Entities.Book> books = await Task<IEnumerable<Librarian.Infrastructure.Entities.Book>>.Run(() =>
            {
                return (from  awb in this.dbContext.AuthorWritesBook.AsQueryable().AsEnumerable()
                        join b in this.dbContext.Books.AsQueryable().AsEnumerable() on awb.BookId equals b.Id
                        where awb.AuthorId == authorId
                        select b);
            });

            if (books == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>>(null, false, new[] { new Error($"get_books_failure", "Not items found.") });

            IEnumerable<Librarian.Core.Domain.Entities.Book> results = this.mapper.Map<IEnumerable<Librarian.Core.Domain.Entities.Book>>(books);

            if (results == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>>(null, false, new[] { new Error($"get_books_failure", "Not items found.") });

            return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>>(results, true);
        }

        public async Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>>> GetAuthors(string bookId)
        {
            IEnumerable<Librarian.Infrastructure.Entities.Author> authors = await Task<IEnumerable<Librarian.Infrastructure.Entities.Author>>.Run(() =>
            {
                return (from awb in this.dbContext.AuthorWritesBook.AsQueryable().AsEnumerable()
                        join a in this.dbContext.Authors.AsQueryable().AsEnumerable() on awb.AuthorId equals a.Id
                        where awb.BookId == bookId
                        select a);
            });

            if (authors == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>>(null, false, new[] { new Error($"get_authors_failure", "No items found.") });

            IEnumerable<Librarian.Core.Domain.Entities.Author> results = this.mapper.Map<IEnumerable<Librarian.Core.Domain.Entities.Author>>(authors);

            if (results == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>>(null, false, new[] { new Error($"get_authors_failure", "No items found.") });

            return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>>(results, true);
        }

        public async Task<GateawayResponse<IEnumerable<string>>> AddAuthors(string bookId, List<string> authorIds)
        {
            // check if book exists
            bool bookExists = this.dbContext.Books.FindSync(item => item.Id == bookId).Any();

            if (!bookExists)
                return new GateawayResponse<IEnumerable<string>>(null, false, new[] { new Error($"add_authors_failure", "No books found.") });

            // check if authors exists
            foreach (string authorId in authorIds)
            {
                bool authorExists = this.dbContext.Authors.FindSync(item => item.Id == authorId).Any();

                if (!authorExists)
                    return new GateawayResponse<IEnumerable<string>>(null, false, new[] { new Error($"add_authors_failure", "No authors found.") });
            }

            // get authors and delete pre-existing
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>> authors = this.GetAuthors(bookId).Result;

            if (!authors.Success)
                return new GateawayResponse<IEnumerable<string>>(null, false, new[] { new Error($"add_authors_failure", "No authors found.") });

            authorIds.RemoveAll((string authorId) => authors.Data.Select(a => a.Id).Contains(authorId));

            // add authors of book
            IEnumerable<Librarian.Infrastructure.Entities.AuthorWritesBook> entities = authorIds.Select(authorId => new Librarian.Infrastructure.Entities.AuthorWritesBook(null, authorId, bookId));
            await this.dbContext.AuthorWritesBook.InsertManyAsync(entities);

            return new GateawayResponse<IEnumerable<string>>(entities.Select(e => e.Id), true);
        }

        public async Task<GateawayResponse<string>> DeleteAuthors(string bookId, List<string> authorIds)
        {
            // check if book exists
            bool bookExists = this.dbContext.Books.FindSync(item => item.Id == bookId).Any();

            if (!bookExists)
                return new GateawayResponse<string>(null, false, new[] { new Error($"delete_authors_failure", "No books found.") });

            // check if authors exists
            foreach (string authorId in authorIds)
            {
                bool authorExists = this.dbContext.Authors.FindSync(item => item.Id == authorId).Any();

                if (!authorExists)
                    return new GateawayResponse<string>(null, false, new[] { new Error($"delete_authors_failure", "No books found.") });
            }

            // get authors and delete non-pre-existing
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>> authors = this.GetAuthors(bookId).Result;

            if (!authors.Success)
                return new GateawayResponse<string>(null, false, new[] { new Error($"delete_authors_failure", "No authors found.") });

            authorIds.RemoveAll((string authorId) => !authors.Data.Select(a => a.Id).Contains(authorId));

            // delete authors of book
            DeleteResult result = await this.dbContext.AuthorWritesBook.DeleteManyAsync(item => item.BookId == bookId && authorIds.Contains(item.AuthorId));

            if (result.DeletedCount > 0)
                return new GateawayResponse<string>(null, false, new[] { new Error($"delete_authors_failure", "No deleted authors.") });

            return new GateawayResponse<string>(null, true);
        }
    }
}
