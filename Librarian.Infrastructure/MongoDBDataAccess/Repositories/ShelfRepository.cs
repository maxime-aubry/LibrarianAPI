using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ShelfRepository : Repository<Librarian.Infrastructure.Entities.Shelf, Librarian.Core.Domain.Entities.Shelf>, IShelfRepository
    {
        public ShelfRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        //public async Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>> GetAvailableShelves(EBookCategory category, int numberOfCopies)
        //{
        //    IEnumerable<Librarian.Infrastructure.Entities.Shelf> entities = await Task.Run(() =>
        //    {
        //        IEnumerable<Librarian.Infrastructure.Entities.Shelf> items = (from s in this.dbContext.Shelves.AsQueryable().ToEnumerable()
        //                                                                         join b in this.dbContext.Books.AsQueryable().ToEnumerable() on s.Id equals b.ShelfId into bookItems
        //                                                                         where s.BookCategory == (int)category
        //                                                                         && (bookItems.Sum(bi => bi.NumberOfCopies) + numberOfCopies) <= s.MaxQtyOfBooks
        //                                                                         select s);
        //        return items;
        //    });

        //    if (entities == null)
        //        return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(null, false, new[] { new Error($"get_available_shelves_failure", "No items found.") });

        //    IEnumerable<Librarian.Core.Domain.Entities.Shelf> results = this.mapper.Map<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(entities);

        //    if (results == null)
        //        return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(null, false, new[] { new Error($"get_available_shelves_failure", "No items found.") });

        //    return new GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(results, true);
        //}

        //public new async Task<GateawayResponse<string>> Add(Librarian.Core.Domain.Entities.Shelf model)
        //{
        //    int nbShelves = await (from s in this.dbContext.Shelves.AsQueryable()
        //                             where s.Floor == (int)model.Floor
        //                             && s.BookCategory == (int)model.BookCategory
        //                             select s).CountAsync();

        //    model.Name = $"F{(int)model.Floor}-BC{(int)model.Floor}-{nbShelves + 1}";

        //    return await base.Add(model);
        //}
    }
}
