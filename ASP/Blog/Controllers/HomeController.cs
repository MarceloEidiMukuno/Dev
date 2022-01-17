using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{

    [ApiController] // Controller de API, para retornar apenas Json. Não vai retornar Html, Javascript, Css
    [Route("")] // Define a rota para todos os metodos da classe
    public class HomeController : ControllerBase // Herdar da classe de Controllers, identificando que a classe é Controller
    {
        [HttpGet("")] // Definindo o metodo como Get(Obter)
        // Metodo de Health Check
        public IActionResult Get()
        {
            return Ok();
        }

    }
}