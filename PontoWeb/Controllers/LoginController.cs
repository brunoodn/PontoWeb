using Microsoft.AspNetCore.Mvc;
using PontoWeb.Helpers;
using PontoWeb.Models;
using PontoWeb.Repositorio;

namespace PontoWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IFuncionarioRepositorio funcionarioRepositorio, ISessao sessao)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _sessao = sessao;
        } 
        public IActionResult Index()
        {
            if(_sessao.BuscarSessaoFuncionarioLogado() != null) return RedirectToAction("Index", "Home");
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.ExcluirSessaoFuncionarioLogado();

            return RedirectToAction("Index");
        }
        public IActionResult Entrar(LoginModel login)
        {
            FuncionarioModel funcionario = null;
            if (ModelState.IsValid)
            {
                funcionario = _funcionarioRepositorio.BuscarPorMatricula(login.Matricula);
                if(funcionario != null && funcionario.Ativo == true)
                {
                    if (funcionario.SenhaValida(login.Senha))
                    {
                        _sessao.CriarSessaoFuncionarioLogado(funcionario);
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["MensagemErro"] = "Senha inválida.. tente novamente.";
                    return  RedirectToAction("Index", "Login");
                    
                }
                TempData["MensagemErro"] = "Usuário ou senha invalido(s). Tente novamente..";
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }

    }
}
