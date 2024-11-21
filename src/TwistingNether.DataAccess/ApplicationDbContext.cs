using Microsoft.EntityFrameworkCore;
using TwistingNether.DataAccess.TwistingNether.Tables;

namespace TwistingNether.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<tbl_Classes> tbl_Classes { get; set; }
    }
}
