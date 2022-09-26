using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models.Dtos
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo precisa ter entre 6 e 50 carecteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo precisa ter entre 6 e 50 carecteres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
