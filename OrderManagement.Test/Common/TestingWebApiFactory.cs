using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Test.Common;

public class TestingWebApiFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    "appsettings.json",
                    optional: true,
                    reloadOnChange: true)
                .Build(); 
            services.AddHttpClient();
        });
    }
}