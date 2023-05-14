using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldMineGuide.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoldMineGuide.Data
{
    public class GoldMineGuideContext : IdentityDbContext<GoldMineGuideUser>
    {
        public GoldMineGuideContext(DbContextOptions<GoldMineGuideContext> options)
            : base(options)
        {
        }

        public DbSet<GoldMineGuide.Models.Flower> Flower { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
