using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SongsApiIntegrationTests
{
    public class GettingTheServerStatus : IClassFixture<BasicWebApplicationFactory>
    {

        private readonly HttpClient _client;
        public GettingTheServerStatus(BasicWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/status");
            Assert.True(response.IsSuccessStatusCode);
            // alternately
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProperMediaType()
        {
            var response = await _client.GetAsync("/status");

            Assert.Equal("application/json",
                response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task HasCorrectRepresentation()
        {
            var response = await _client.GetAsync("/status");

            var representation = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("Dummy says Howdy!", representation.message);
            Assert.Equal(new DateTime(1969,4,20,23,59,00), representation.lastChecked);
        }
    }


    public class GetStatusResponse
    {
        public string message { get; set; }
        public DateTime lastChecked { get; set; }
    }

}
