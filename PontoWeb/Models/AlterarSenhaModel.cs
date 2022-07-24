using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class AlterarSenhaModel
    {
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Digite a senha atual.")]
        public string Senha { get; set;}

        [Required(ErrorMessage = "Digite a nova senha.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [Compare("NovaSenha", ErrorMessage = "A senha não confere com a nova senha.")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}
