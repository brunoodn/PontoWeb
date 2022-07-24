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
        public string Senha { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório..")]
        public string CPF { get; set; }
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
    }
}
