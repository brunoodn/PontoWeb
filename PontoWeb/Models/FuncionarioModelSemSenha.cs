using PontoWeb.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class FuncionarioModelSemSenha
    {
        [Key]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório..")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]
        public string NIS { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]
        public TipoFuncionarioEnum Tipo { get; set; }

        public DateTime? DataAtualizacao { get; set; }
    }
}
