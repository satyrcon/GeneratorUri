using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GeneratorUri.Areas.Identity.IdentityHostingStartup))]
namespace GeneratorUri.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}