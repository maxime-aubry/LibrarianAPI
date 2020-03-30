using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Infrastructure.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class Repository<TEntity, TResult>
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
            try
            {
                IEnumerable<TEntity> entities = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => true)).ToEnumerable();
                IEnumerable<TResult> results = this.mapper.Map<IEnumerable<TResult>>(entities);
                return new GateawayResponse<IEnumerable<TResult>>(results, true);
            }
            catch (Exception e)
            {
                return new GateawayResponse<IEnumerable<TResult>>(null, false, new List<Error>() { new Error("get_mongodb_error", e.Message) });
            }
        }

        public async Task<GateawayResponse<TResult>> Get(string id)
        {
            try
            {
                TEntity entity = (await this.dbContext.GetCollection<TEntity>().FindAsync<TEntity>(item => item.Id == id)).FirstOrDefault();
                TResult result = this.mapper.Map<TResult>(entity);

                if (result == null)
                    return new GateawayResponse<TResult>(result, false);
                return new GateawayResponse<TResult>(result, true);
            }
            catch (Exception e)
            {
                return new GateawayResponse<TResult>(null, false, new List<Error>() { new Error("getbyid_mongodb_error", e.Message) });
            }
        }

        public async Task<GateawayResponse<string>> Add(TResult model)
        {
            try
            {
                TEntity entity = this.mapper.Map<TEntity>(model);
                await this.dbContext.GetCollection<TEntity>().InsertOneAsync(entity);
                return new GateawayResponse<string>(entity.Id, true);
            }
            catch (MongoQueryException e)
            {
                return new GateawayResponse<string>(null, false, e.ErrorLabels.Select(el => new Error("create_mongodb_validation_error", el)));
            }
            catch (Exception e)
            {
                return new GateawayResponse<string>(null, false, new List<Error>() { new Error("create_mongodb_error", e.Message) });
            }
        }

        public async Task<GateawayResponse<string>> Update(string id, TResult model)
        {
            try
            {
                TEntity entity = this.mapper.Map<TEntity>(model);
                ReplaceOneResult result = await this.dbContext.GetCollection<TEntity>().ReplaceOneAsync(obj => obj.Id == id, entity, new ReplaceOptions() { IsUpsert = true });
                return new GateawayResponse<string>(entity.Id, true);
            }
            catch (MongoQueryException e)
            {
                return new GateawayResponse<string>(null, false, e.ErrorLabels.Select(el => new Error("update_mongodb_validation_error", el)));
            }
            catch (Exception e)
            {
                return new GateawayResponse<string>(null, false, new List<Error>() { new Error("update_mongodb_error", e.Message) });
            }
        }

        public async Task<GateawayResponse<string>> Delete(string id)
        {
            try
            {
                await this.dbContext.GetCollection<TEntity>().DeleteOneAsync(item => item.Id == id);
                return new GateawayResponse<string>(null, true);
            }
            catch (Exception e)
            {
                return new GateawayResponse<string>(null, false, new List<Error>() { new Error("delete_mongodb_error", e.Message) });
            }
        }

        public async Task<GateawayResponse<string>> DeleteMany(IEnumerable<string> ids)
        {
            try
            {
                DeleteResult result = await this.dbContext.GetCollection<TEntity>().DeleteManyAsync(item => ids.Contains(item.Id));
                return new GateawayResponse<string>(null, true);
            }
            catch (Exception e)
            {
                return new GateawayResponse<string>(null, false, new List<Error>() { new Error("delete_many_mongodb_error", e.Message) });
            }
        }
    }
}
