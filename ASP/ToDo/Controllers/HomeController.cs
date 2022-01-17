using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        // Atributo Get        
        // [Route("/")] // Definir a rota
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
        => Ok(context.Todos.ToList());


        [HttpGet("/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int Id,
            [FromServices] AppDbContext context)
        {
            var todos = context.Todos.FirstOrDefault(x => x.Id == Id);

            return todos == null ? NotFound() : Ok(todos);
        }

        // Atributo Post
        [HttpPost("/")]
        public IActionResult Post(
            [FromBody] TodoModel model,
            [FromServices] AppDbContext context)
        {
            context.Todos.Add(model);
            context.SaveChanges();

            return Created($"/{model.Id}", model);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel model,
            [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound();

            todo.Title = model.Title;
            todo.Done = model.Done;

            context.Todos.Update(todo);
            context.SaveChanges();

            return Ok(todo);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
                return NotFound();

            context.Todos.Remove(todo);
            context.SaveChanges();

            return Ok(todo);
        }
    }

}