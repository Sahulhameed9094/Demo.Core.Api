using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Api.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options) 
        {

        }

        DbSet<Brand> Brands { get; set; }
        
    }
}
