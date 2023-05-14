using System;
using GoldMineGuide.Areas.Identity.Data;
using GoldMineGuide.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GoldMineGuide.Areas.Identity.IdentityHostingStartup))]
namespace GoldMineGuide.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<GoldMineGuideContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("GoldMineGuideContextConnection")));

                services.AddDefaultIdentity<GoldMineGuideUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<GoldMineGuideContext>();
            });
        }
    }
}