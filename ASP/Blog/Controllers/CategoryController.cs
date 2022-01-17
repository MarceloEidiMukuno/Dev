using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet("categories")]
        public IActionResult Get(
            [FromServices] BlogDataContext context)
        {
            return Ok(context.Categories.ToList());
        }

        [HttpGet("v0/categories")]
        public async Task<IActionResult> Get(
            [FromServices] IMemoryCache cache,
            [FromServices] BlogDataContext context)
        {

            var categories = cache.GetOrCreate("CategoriesCache", entry =>
           {
               entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
               return GetCategories(context);

           });

            return Ok(new ResultViewModel<List<Category>>(categories));
        }

        private List<Category> GetCategories(BlogDataContext context)
        {
            return context.Categories.ToList();
        }

        [HttpGet("v0/categories/{id=int}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<string>("Category não encontrado."));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha ao consultar a category."));
            }
        }

        [HttpPost("v0/categories/")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel categoryviewmodel,
            [FromServices] BlogDataContext context)
        {
            // Não é obrigatorio, já faz por padrão
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErros()));

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = categoryviewmodel.Name,
                    Slug = categoryviewmodel.Slug
                };
                await context.Categories.AddAsync(category);
                context.SaveChanges();

                return Created($"vo/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha ao incluir a category"));
            }
        }

        [HttpPut("v0/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {
            // Não é obrigatorio, já faz por padrão
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErros()));

            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<string>("Category não encontrado."));

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha ao atualizar a category"));
            }
        }

        [HttpDelete("v0/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] int id,
           [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<string>("Category não encontrado."));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha ao excluir a category"));
            }
        }
    }
}