using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class AuthorRepository : Repository<Librarian.Infrastructure.Entities.Author, Librarian.Core.Domain.Entities.Author>, IAuthorRepository
    {
        public AuthorRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public async Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>> GetByFilters(string firstName, string lastName)
        {
            int numberOfPoints = (2);

            void SetPertinence(Librarian.Core.Domain.Entities.FindAuthorsByFilters result)
            {
                int numberOfPointsForItem = 0;

                // check firstname
                if (result.FirstName.ToLower().Contains(firstName.ToLower()))
                    numberOfPointsForItem++;

                // check lastname
                if (result.LastName.ToLower().Contains(lastName.ToLower()))
                    numberOfPointsForItem++;

                result.Pertinence = ((float)numberOfPointsForItem / numberOfPoints);
            }

            IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters> results = await Task.Run(() => {
                // get all items
                IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters> items = (from a in this.dbContext.Authors.AsQueryable().AsEnumerable()
                                                                                          join awb in this.dbContext.AuthorWritesBook.AsQueryable().AsEnumerable() on a.Id equals awb.AuthorId
                                                                                          join b in this.dbContext.Books.AsQueryable().AsEnumerable() on awb.BookId equals b.Id
                                                                                          join l in this.dbContext.ReaderLoansBook.AsQueryable().AsEnumerable() on b.Id equals l.BookId into loans
                                                                                            select new Librarian.Core.Domain.Entities.FindAuthorsByFilters
                                                                                            (
                                                                                                a.Id,
                                                                                                a.FirstName,
                                                                                                a.LastName,
                                                                                                0,
                                                                                                loans?.Count() ?? 0
                                                                                            ));

                foreach (Librarian.Core.Domain.Entities.FindAuthorsByFilters item in items)
                    SetPertinence(item);
                items = items.Where(b => b.Pertinence > 0);
                return items;
            });

            if (results == null)
                return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>(null, false, new[] { new Error($"get_authors_by_filers_failure", "No authors found.") });

            return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>(results, true);
        }
    }
}
