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

        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasQueryFilter(o => o.TenantId == _tenantId);
        }

    }
}
