using Demo.Core.Api.TenantService;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Api.Models
{
    public class BrandContext : DbContext
    {
        private readonly TenantProvider _tenantProvider;
        private readonly int _tenantId;


        public BrandContext(DbContextOptions<BrandContext> options,
            TenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
            _tenantId = tenantProvider.GetTenantId();
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // CHANGE CONNECTION STRING BASED ON TENANT ID 
            optionsBuilder.UseSqlServer(_tenantProvider.GetTenantConnection());
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<profinity> Profinity { get; set; }

        public DbSet<bthinq> Bthinq { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Tenant id in queryfilter for global Brand Table
            //sinbgle
            //modelBuilder.Entity<Brand>().HasQueryFilter(o => o.TenantId == _tenantId);
        }

    }
}
