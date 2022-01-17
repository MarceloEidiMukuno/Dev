// See https://aka.ms/new-console-template for more information
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

using var context = new BlogDataContext();

// var user = new User
// {
//     Name = "Marcelo",
//     Slug = "marceloeidi",
//     Email = "marceloeidi@gmail.com",
//     Bio = "dev",
//     Image = "teste",
//     PasswordHash = "123456"
// };
// var category = new Category
// {
//     Name = "Backend",
//     Slug = "backend"
// };

// var post = new Post
// {
//     Author = user,
//     category = category,
//     Body = "test",
//     Slug = "teste",
//     Summary = "teste de subconjunto",
//     Title = "teste",
//     CreateDate = DateTime.Now,
//     LastUpdateDate = DateTime.Now
// };

// context.Posts.Add(post);
// context.SaveChanges();

// Create
// var tag = new Tag { Name = "Asp.Net", Slug = "aspnet" };
// context.Tags.Add(tag);
// context.SaveChanges();

// Update
// var tag = context.Tags.FirstOrDefault(x => x.Id == 1);
// tag.Name = ".Net";
// tag.Slug = "dotnet";
// context.Update(tag);
// context.SaveChanges();

// Delete
// var tag = context.Tags.FirstOrDefault(x => x.Id == 1);
// context.Remove(tag);
// context.SaveChanges();

//Select
// var tag = context
//         .Tags
//         .AsNoTracking() //Sem Metadata --> Apneas no select
//         .Where(x => x.Name.Contains("Asp"))
//         .ToList(); //Sempre deixar no final

// foreach (var t in tag)
// {
//     Console.WriteLine(t.Name);
// }

var posts = context
            .Posts
            .AsNoTracking()
            .Include(x => x.Author) // JOIN (Eager Loading)
            	.ThenInclude( x => x.Roles ) // SUBSELECT (Evitar, não perfomatico)
	    .Include(x => x.category) // JOIN	
            .OrderByDescending(x => x.LastUpdateDate)
            .ToList();

foreach (var p in posts)
    Console.WriteLine($"{p.Title} escrito por {p.Author?.Name} em {p.category?.Name}");

