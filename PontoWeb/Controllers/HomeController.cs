using Microsoft.AspNetCore.Mvc;
using PontoWeb.Helpers;
using PontoWeb.Models;
using System.Diagnostics;

namespace PontoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessao _sessao;
        public HomeController(ILogger<HomeController> logger, ISessao sessao)
        {
            _logger = logger;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoFuncionarioLogado() == null) return RedirectToAction("index", "Login");

            FuncionarioModel funcionario = _sessao.BuscarSessaoFuncionarioLogado();
            return View(funcionario);
        }

    }
}