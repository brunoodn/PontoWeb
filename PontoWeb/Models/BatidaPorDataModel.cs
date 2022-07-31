using System.ComponentModel.DataAnnotations;

namespace PontoWeb.Models
{
    public class BatidaPorDataModel
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime DataFinal { get; set; }

    }
}
