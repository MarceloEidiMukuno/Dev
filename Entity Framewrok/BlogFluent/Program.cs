// See https://aka.ms/new-console-template for more information
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var context = new BlogDataContext();

            // var user = new User
            // {
            //     Name = "Marcelo",
            //     Slug = "marceloeidi",
            //     Email = "marceloeidi@gmail.com",
            //     Bio = "dev",
            //     Image = "teste",
            //     PasswordHash = "123456",
            //     GitHub = "git"
            // };
            // var category = new Category
            // {
            //     Name = "Backend",
            //     Slug = "backend"
            // };

            // var post = new Post
            // {
            //     Author = user,
            //     Category = category,
            //     Body = "test",
            //     Slug = "teste",
            //     Summary = "teste de subconjunto",
            //     Title = "teste",
            //     CreateDate = DateTime.Now,
            //     LastUpdateDate = DateTime.Now
            // };

            // context.Posts.Add(post);
            // context.SaveChanges();

            var posts = await GetPosts(context);
            foreach (var p in posts)
                Console.WriteLine(p.Title);

            var query = context.PostWithTagsCount.FirstOrDefault();
            Console.WriteLine($"o nome é { query?.Name} quantidade { query?.Count}");

        }

        public static async Task<List<Post>> GetPosts(BlogDataContext context) => await context.Posts.ToListAsync();
    }
}


