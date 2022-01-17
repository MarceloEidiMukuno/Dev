using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("v0/posts")]
        public async Task<IActionResult> Get(
            [FromServices] BlogDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context
                                .Posts
                                .AsNoTracking()
                                .Include(x => x.Category)
                                .Include(x => x.Author)
                                .Select(x => new ListPostViewModel
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Slug = x.Slug,
                                    LastUpdateDate = x.LastUpdateDate,
                                    Category = x.Category.Name,
                                    Author = x.Author.Name
                                })
                                .Skip(page * pageSize)
                                .Take(pageSize)
                                .OrderByDescending(x => x.LastUpdateDate)
                                .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }

        [HttpGet("v0/posts/{id:int}")]
        public async Task<IActionResult> GetDetails(
            [FromServices] BlogDataContext context,
            [FromRoute] int id
        )
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context
                                .Posts
                                .AsNoTracking()
                                .Include(x => x.Category)
                                .Include(x => x.Author)
                                    .ThenInclude(x => x.Roles)
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (posts == null)
                    return NotFound(new ResultViewModel<Post>("Não encontrado"));

                return Ok(new ResultViewModel<Post>(posts));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }


        [HttpGet("v0/posts/category/{category}")]
        public async Task<IActionResult> GetCategory(
           [FromServices] BlogDataContext context,
           [FromRoute] string category,
           [FromQuery] int page = 0,
           [FromQuery] int pageSize = 25
       )
        {
            try
            {
                var posts = await context
                                .Posts
                                .AsNoTracking()
                                .Include(x => x.Author)
                                .Include(x => x.Category)
                                .Where(x => x.Category.Slug == category)
                                .Select(x => new ListPostViewModel
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Slug = x.Slug,
                                    LastUpdateDate = x.LastUpdateDate,
                                    Category = x.Category.Name,
                                    Author = x.Author.Name
                                })
                                .Skip(page * pageSize)
                                .Take(pageSize)
                                .OrderByDescending(x => x.LastUpdateDate)
                                .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = posts.Count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }

    }

}