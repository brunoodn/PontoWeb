using Microsoft.AspNetCore.Mvc;
using PontoWeb.Filters;

namespace PontoWeb.Controllers
{
    [PaginaFuncionarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}