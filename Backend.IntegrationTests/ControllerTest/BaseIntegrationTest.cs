using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.IO;
using System.Net.Http;

namespace Backend.IntegrationTests.ControllerTest
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient client;
        protected readonly TestServer server;


        public BaseIntegrationTest()
        {
            server = new TestServer(new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<StartupTest>());

            client = server.CreateClient();
        }
    }
}