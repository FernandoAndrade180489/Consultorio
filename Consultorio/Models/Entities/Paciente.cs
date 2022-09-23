using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models.Entities
{
    public class Paciente : Base
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")] // Data Annotation com mensagem de retorno personalizada
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome precisa ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
        public List<Consulta> Consultas { get; set; }
    }
}
