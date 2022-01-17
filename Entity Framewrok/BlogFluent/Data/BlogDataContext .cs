using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PostWithTagsCount> PostWithTagsCount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=BlogMigration;User ID=sa;Password=kakashi123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.Entity<PostWithTagsCount>(x =>
            {
                x.HasNoKey();
                x.ToSqlQuery(@"SELECT 
                                [Title] AS [Name], 
                                (SELECT COUNT(*) FROM [Tag] WHERE Id = [Post].[Id]) AS [COUNT] 
                                FROM [Post] ");
            });

        }
    }
}