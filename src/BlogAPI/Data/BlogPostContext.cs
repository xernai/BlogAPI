using Microsoft.EntityFrameworkCore;
using BlogAPI.Models;

namespace BlogAPI.Data
{
    public class BlogPostsContext : DbContext
{
    public BlogPostsContext (DbContextOptions<BlogPostsContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
}
}