using Librarian.RestFulAPI.Tests.Tools;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Librarian.RestFulAPI.Tests
{
    public class BaseController : IClassFixture<AppTestFixture>, IDisposable
    {
        protected readonly AppTestFixture fixture;
        protected readonly HttpClient client;

        public BaseController(AppTestFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.fixture.Output = output;
            this.client = fixture.CreateClient();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            this.fixture.Output = null;
        }
    }
}
