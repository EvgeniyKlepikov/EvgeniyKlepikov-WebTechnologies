using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KLEPIKOV30323WEB.Domain.Entities;

namespace KLEPIKOV30323WEB.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KLEPIKOV30323WEB.Domain.Entities.Product> Product { get; set; } = default!;
    }
}
