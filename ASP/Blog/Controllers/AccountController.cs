using System.Text.RegularExpressions;
using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Blog.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        // private readonly TokenService _tokenService;
        // public AccountController(TokenService tokenService){
        //     _tokenService = tokenService;

        // }

        [HttpPost("v0/accounts/")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] EmailService email,
            [FromServices] BlogDataContext context
        )
        {
            if (!ModelState.IsValid)
                return Ok(new ResultViewModel<string>(ModelState.GetErros()));

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Slug = model.Email.Replace("@", "-").Replace(".", "-")
            };

            var password = PasswordGenerator.Generate(25);
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                // email.Send(
                //     user.Name,
                //     user.Email,
                //     "Bem Vindo ao Blog",
                //     $"Sua Senha é <strong>{password}</strong>"
                // );

                return Created($"vo/accounts/{user.Id}", new ResultViewModel<dynamic>(new
                {
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("Este e-mail já está cadastrado"));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna do servidor"));

            }
        }

        [HttpPost("v0/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] BlogDataContext context,
            [FromServices] TokenService tokenService)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

            var user = await context.Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuario ou senha invalidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuario ou senha invalidos"));

            try
            {

                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));

            }

        }

        [HttpGet("vo/accounts/{id:int}")]
        public IActionResult Get(
            [FromRoute] int id,
            [FromServices] BlogDataContext context
        )
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound(new ResultViewModel<string>("Usuario não encontrado."));

            return Ok(new ResultViewModel<User>(user));
        }

        [Authorize]
        [HttpPost("v0/accounts/upload-image")]
        public async Task<IActionResult> UploadImage(
            [FromBody] UploadImageViewModel model,
            [FromServices] BlogDataContext context
        )
        {
            var fileName = $"{Guid.NewGuid().ToString()}.jpg"; //Nome do arquivo em Guid, para não repetir
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, ""); // Remover a identificação do FrontEnd
            var bytes = Convert.FromBase64String(data); // Convert para base64

            try
            {
                await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{fileName}", bytes); // Cria o arquivo estatico 
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha Interna do servidor"));
            }

            // Busca o usuario para atualizar na BD
            var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if (user == null)
                return NotFound(new ResultViewModel<User>("Usuario não encontrado"));

            user.Image = $"https://localhost:0000/images/{fileName}";

            try
            {
                // Atualiza o cadastro do usuario com a imagem
                context.Users.Update(user);
                await context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna do servidor"));
            }

            return Ok(new ResultViewModel<User>("Imagem atualizada"));

        }
    }
}