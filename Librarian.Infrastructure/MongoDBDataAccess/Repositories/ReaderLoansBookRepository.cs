using AutoMapper;
using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderLoansBookRepository : Repository<Librarian.Infrastructure.Entities.ReaderLoansBook, Librarian.Core.Domain.Entities.ReaderLoansBook>, IReaderLoansBookRepository
    {
        public ReaderLoansBookRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public async Task<GateawayResponse<string>> Close(string id)
        {
            Librarian.Infrastructure.Entities.ReaderLoansBook entity = (await this.dbContext.ReaderLoansBook.FindAsync(item => item.Id == id)).FirstOrDefault();

            if (entity == null)
                return new GateawayResponse<string>(null, false, new[] { new Error($"close_loan_failure", "No loan found.") });

            Librarian.Core.Domain.Entities.ReaderLoansBook result = this.mapper.Map<Librarian.Core.Domain.Entities.ReaderLoansBook>(entity);

            result.EndDateOfLoaning = DateTime.Now;
            result.IsLost = false;

            return await this.Update(id, result);
        }

        public async Task<GateawayResponse<string>> CloseAndDeclareAsLost(string id)
        {
            Librarian.Infrastructure.Entities.ReaderLoansBook entity = (await this.dbContext.ReaderLoansBook.FindAsync(item => item.Id == id)).FirstOrDefault();

            if (entity == null)
                return new GateawayResponse<string>(null, false, new[] { new Error($"close_loan_and_declare_as_lost_failure", "No loan found.") });

            Librarian.Core.Domain.Entities.ReaderLoansBook result = this.mapper.Map<Librarian.Core.Domain.Entities.ReaderLoansBook>(entity);

            result.EndDateOfLoaning = DateTime.Now;
            result.IsLost = true;

            return await this.Update(id, result);
        }
    }
}
