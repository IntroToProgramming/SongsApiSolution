using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SongsApiIntegrationTests
{
    public class ResourceSmokeTests : IClassFixture<BasicWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ResourceSmokeTests(BasicWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("/employees")]
        [InlineData("/status")]
        [InlineData("/whoami")]
        public async Task IsTheResourceAlive(string resource)
        {
            var response = await _client.GetAsync(resource);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
