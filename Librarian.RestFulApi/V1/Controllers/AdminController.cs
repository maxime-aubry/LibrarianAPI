using Librarian.Infrastructure.MongoDBDataAccess.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.V1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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