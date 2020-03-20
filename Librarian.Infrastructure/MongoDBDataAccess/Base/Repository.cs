using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Infrastructure.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class Repository<TEntity, TResult> : Librarian.Infrastructure.MongoDBDataAccess.Base.IRepository<TEntity, TResult>
        where TEntity : BaseObject
        where TResult : class
    {
        protected readonly IMongoDbContext dbContext;
        protected readonly IMapper mapper;

        public Repository(IMongoDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GateawayResponse<IEnumerable<TResult>>> Get()
        {
            IEnumerable<TEntity> entities = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => true)).ToEnumerable();

            if (entities == null)
                return new GateawayResponse<IEnumerable<TResult>>(null, false, new[] { new Error($"get_items_of_{typeof(TResult).FullName.ToLower()}_failure", "No items found.") });

            IEnumerable<TResult> results = this.mapper.Map<IEnumerable<TResult>>(entities);

            if (results == null)
                return new GateawayResponse<IEnumerable<TResult>>(null, false, new[] { new Error($"get_items_of_{typeof(TResult).FullName.ToLower()}_failure", "No items found.") });

            return new GateawayResponse<IEnumerable<TResult>>(results, true);
        }

        public async Task<GateawayResponse<TResult>> Get(string id)
        {
            TEntity entity = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => item.Id == id)).FirstOrDefault();

            if (entity == null)
                return new GateawayResponse<TResult>(null, false, new[] { new Error($"get_item_of_{typeof(TResult).FullName.ToLower()}_failure", "No item found.") });

            TResult result = this.mapper.Map<TResult>(entity);

            if (entity == null)
                return new GateawayResponse<TResult>(null, false, new[] { new Error($"get_item_of_{typeof(TResult).FullName.ToLower()}_failure", "No item found.") });

            return new GateawayResponse<TResult>(result, true);
        }

        public async Task<GateawayResponse<string>> Add(TResult model)
        {
            TEntity entity = this.mapper.Map<TEntity>(model);
            await this.dbContext.GetCollection<TEntity>().InsertOneAsync(entity);

            if (string.IsNullOrEmpty(entity.Id))
                return new GateawayResponse<string>(null, false, new[] { new Error($"add_item_of_{typeof(TResult).FullName.ToLower()}_failure", "No added items.") });

            return new GateawayResponse<string>(entity.Id, true);
        }

        public async Task<GateawayResponse<string>> Update(string id, TResult model)
        {
            TEntity entity = this.mapper.Map<TEntity>(model);
            ReplaceOneResult result = await this.dbContext.GetCollection<TEntity>().ReplaceOneAsync(obj => obj.Id == id, entity, new ReplaceOptions() { IsUpsert = true });

            if (result.ModifiedCount > 0)
                return new GateawayResponse<string>(null, false, new[] { new Error($"update_item_of_{typeof(TResult).FullName.ToLower()}_failure", "No updated items.") });

            return new GateawayResponse<string>(entity.Id, true);
        }

        public async Task<GateawayResponse<string>> Delete(string id)
        {
            DeleteResult result = await this.dbContext.GetCollection<TEntity>().DeleteOneAsync(item => item.Id == id);

            if (result.DeletedCount > 0)
                return new GateawayResponse<string>(null, false, new[] { new Error($"delete_item_of_{typeof(TResult).FullName}_failure", "No deleted item.") });

            return new GateawayResponse<string>(null, true);
        }
    }
}
