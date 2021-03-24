using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SongsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsApiIntegrationTests
{
    public class BasicWebApplicationFactory : WebApplicationFactory<Startup>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // ask for the service that is implement IProvideServerStatus
                var serviceDescription = services.Single(service =>
                    service.ServiceType == typeof(IProvideServerStatus));
                // remove it.
                services.Remove(serviceDescription);
                // Replace it with folger's chrystals. 
                services.AddScoped<IProvideServerStatus, DummyService>();
            });
        }
    }

    public class DummyService : IProvideServerStatus
    {
        public SongsApi.Controllers.GetStatusResponse GetMyStatus()
        {
            return new SongsApi.Controllers.GetStatusResponse
            {
                Message = "Dummy says Howdy!",
                LastChecked = new DateTime(1969, 4, 20, 23, 59, 00)
            };
        }
    }
}
