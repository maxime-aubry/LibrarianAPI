﻿using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class UserHasRightRepository : Repository<Librarian.Infrastructure.Entities.UserHasRight, Librarian.Core.Domain.Entities.UserHasRight>, IUserHasRightRepository
    {
        public UserHasRightRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
