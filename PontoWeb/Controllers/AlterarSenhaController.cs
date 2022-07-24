using Microsoft.AspNetCore.Mvc;
using PontoWeb.Helpers;
using PontoWeb.Models;
using PontoWeb.Repositorio;

namespace PontoWeb.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IFuncionarioRepositorio funcionarioRepositorio, ISessao sessao)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(AlterarSenhaModel alterarSenha)
        {
            FuncionarioModel funcionarioLogado = _sessao.BuscarSessaoFuncionarioLogado();
            alterarSenha.Matricula = funcionarioLogado.Matricula;

            if (ModelState.IsValid)
            {
                _funcionarioRepositorio.AlterarSenha(alterarSenha);

                TempData["MensagemSucesso"] = "Senha alterada com sucesso.";
                return RedirectToAction("Index", "Home");
            }
            TempData["MensagemErro"] = "Erro ao atualizar a senha.";
            return RedirectToAction("Index", "Home");
        }
    }
}
