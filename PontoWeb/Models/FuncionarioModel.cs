using PontoWeb.Enums;
using PontoWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório..")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Minimo 6 caracteres para a senha.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]
        
        [StringLength(12,MinimumLength = 12, ErrorMessage = "O NIS deve conter 12 caracteres. caso o seu nao tenha coloque um zero no começo.")]
        public string NIS { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]
        public TipoFuncionarioEnum Tipo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();

            return novaSenha;
        }

        /* EF Relations */
        public IEnumerable<BatidaModel> Batidas { get; set; }

    }
}
