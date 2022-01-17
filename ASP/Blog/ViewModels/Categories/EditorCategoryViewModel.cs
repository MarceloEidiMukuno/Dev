using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "Campo name é obrigatorio.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O campo name deve ser entre 3 e 30 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo slug é obrigatorio.")]
        public string Slug { get; set; }
    }
}