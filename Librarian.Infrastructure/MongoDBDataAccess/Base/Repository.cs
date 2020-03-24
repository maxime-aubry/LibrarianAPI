using AutoMapper;
using Librarian.Infrastructure.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class Repository<TEntity, TResult>// : Librarian.Infrastructure.MongoDBDataAccess.Base.IRepository<TEntity, TResult>
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

        public async Task<IEnumerable<TResult>> Get()
        {
            IEnumerable<TEntity> entities = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => true)).ToEnumerable();
            IEnumerable<TResult> results = this.mapper.Map<IEnumerable<TResult>>(entities);
            return results;
        }

        public async Task<TResult> Get(string id)
        {
            TEntity entity = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => item.Id == id)).FirstOrDefault();
            TResult result = this.mapper.Map<TResult>(entity);
            return result;
        }

        public async Task<string> Add(TResult model)
        {
            TEntity entity = this.mapper.Map<TEntity>(model);
            await this.dbContext.GetCollection<TEntity>().InsertOneAsync(entity);
            return entity.Id;
        }

        public async Task<string> Update(string id, TResult model)
        {
            TEntity entity = this.mapper.Map<TEntity>(model);
            ReplaceOneResult result = await this.dbContext.GetCollection<TEntity>().ReplaceOneAsync(obj => obj.Id == id, entity, new ReplaceOptions() { IsUpsert = true });
            return entity.Id;
        }

        public async Task Delete(string id)
        {
            await this.dbContext.GetCollection<TEntity>().DeleteOneAsync(item => item.Id == id);
        }
    }
}
