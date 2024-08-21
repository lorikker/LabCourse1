using LabCourse.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabCourse.DbContexts
{
    public class StokuDbContext : DbContext
    {
        public DbSet<Stoku> Stoqet { get; set; }

        public StokuDbContext(DbContextOptions<StokuDbContext> options) : base(options) {
            
        }



    }
}
