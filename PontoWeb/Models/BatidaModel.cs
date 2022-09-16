using PontoWeb.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class BatidaModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Campo obrigatório..")]
        public DateTime Registro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório..")]
        public int FuncionarioMatricula { get; set; }

        [Required(ErrorMessage = "Campo obrigatório..")]
        public TipoBatidaEnum TipoBatida { get; set; }
        public int? MatriculaSupervisorAjuste { get; set; }

        [StringLength(500)]
        public string? Observacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public bool Ativo { get; set; } = true;

        /* EF Relations */
        public FuncionarioModel Funcionario { get; set; }
    }
}
