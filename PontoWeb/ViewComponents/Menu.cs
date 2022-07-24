using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PontoWeb.Models;

namespace PontoWeb.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoFuncionario = HttpContext.Session.GetString("SessaoFuncionarioLogado");

            if (String.IsNullOrEmpty(sessaoFuncionario)) return null;

            FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
            return View(funcionario);

        }
    }
}
