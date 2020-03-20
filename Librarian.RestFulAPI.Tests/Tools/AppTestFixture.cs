using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Librarian.RestFulAPI.Tests.Tools
{
    public class AppTestFixture : WebApplicationFactory<Program>
    {
        public ITestOutputHelper Output { get; set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            IWebHostBuilder builder = base.CreateWebHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                //logging.ClearProviders(); // Remove other loggers
                //logging.AddXUnit(Output); // Use the ITestOutputHelper instance
            });
            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Don't run IHostedServices when running as a test
            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(IHostedService));
            });
        }
    }
}
