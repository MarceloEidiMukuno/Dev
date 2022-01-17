using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Accounts
{

    public class LoginViewModel
    {


        [Required(ErrorMessage = "O e-mail é obrigatorio")]
        [EmailAddress(ErrorMessage = "e-mail é invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatoria.")]
        public string Password { get; set; }
    }
}