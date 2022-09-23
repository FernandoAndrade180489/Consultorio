using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models.Dtos
{
    public class PacienteAdicionarDto
    {
        // Data Annotation com mensagem de retorno personalizada
        // Valida o recebimento dos dados antes de tentar salvar no banco de dados
        [Required(ErrorMessage = "O campo nome é obrigatório")] 
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome precisa ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
    }
}
