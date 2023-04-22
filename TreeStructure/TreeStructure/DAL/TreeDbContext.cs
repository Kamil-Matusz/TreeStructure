using Microsoft.EntityFrameworkCore;
using TreeStructure.Entities;

namespace TreeStructure.DAL
{
    public class TreeDbContext : DbContext
    {
        public DbSet<Tree> Trees { get; set; }

        public TreeDbContext(DbContextOptions<TreeDbContext> options) : base(options) { }
    }
}
