using Microsoft.EntityFrameworkCore;

namespace BlogCMS.Models
{
    public class BlogDbContext: DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) 
        { 
        }
    }
}
