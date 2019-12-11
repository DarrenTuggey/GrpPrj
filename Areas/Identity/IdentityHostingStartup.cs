using GroupProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GroupProject.Areas.Identity.IdentityHostingStartup))]
namespace GroupProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        // Method used to configure the connection for the identity framework
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<GroupProjectAuthContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("GroupProjectContextConnection")));
            });
        }
    }
}