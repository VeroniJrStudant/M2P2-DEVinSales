using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevInSales.Context;
using DEVinSalesTest.Context;

namespace UnitTest
{
    public class DevInSalesFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();


            builder.ConfigureServices(services => {
                services.AddScoped(sp => {
                    return new DbContextOptionsBuilder<SqlContext>()
                        .UseInMemoryDatabase("DEVinSales", root)
                        .UseApplicationServiceProvider(sp)
                        .Options;
                });
            });

            return base.CreateHost(builder);
        }
    }
}
