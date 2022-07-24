using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
    }
}
