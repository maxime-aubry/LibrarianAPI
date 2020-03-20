using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IMongoDbContext mongoDbContext;

        public AdminController(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }


        [HttpPost("cleanDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CleanDatabase()
        {
            await this.mongoDbContext.CleanDatase();
            return Ok();
        }
    }
}