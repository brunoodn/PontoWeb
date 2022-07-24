using Microsoft.AspNetCore.Mvc;
using PontoWeb.Helpers;
using PontoWeb.Models;
using PontoWeb.Repositorio;

namespace PontoWeb.Controllers
{
    public class BatidaController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IBatidaRepositorio _batidaRepositorio;
        private readonly ISessao _sessao;
        
        
        public BatidaController(IFuncionarioRepositorio funcionarioRepositorio, IBatidaRepositorio batidaRepositorio, ISessao sessao)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _batidaRepositorio = batidaRepositorio;
            _sessao = sessao;
        }
        
        public IActionResult Index()
        {
            FuncionarioModel funcionario = _sessao.BuscarSessaoFuncionarioLogado();
            List<BatidaModel> batidas = _batidaRepositorio.MinhasBatidas(funcionario.Matricula);

            return View(batidas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult RegistrarBatida(int matricula)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(matricula);
            

            if(funcionario != null)
            {
                BatidaModel batida = new BatidaModel
                {
                    Registro = DateTime.Now,
                    MatriculaEmpregado = funcionario.Matricula,
                    TipoBatida = Enums.TipoBatidaEnum.Manual,
                    MatriculaSupervisorAjuste = null,
                    DataCriacao = DateTime.Now,
                };
                _batidaRepositorio.Adicionar(batida);
                TempData["MensagemSucesso"] = "Batida registrada com sucesso.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult AjustarBatida(BatidaModel batidaModel)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(batidaModel.MatriculaEmpregado);
            FuncionarioModel supervisor = _sessao.BuscarSessaoFuncionarioLogado();

            if (funcionario != null)
            {
                BatidaModel batida = new BatidaModel
                {
                    Registro = batidaModel.Registro,
                    MatriculaEmpregado = batidaModel.MatriculaEmpregado,
                    TipoBatida = Enums.TipoBatidaEnum.Ajustado,
                    Observacao = batidaModel.Observacao,
                    MatriculaSupervisorAjuste = supervisor.Matricula,
                    DataCriacao = DateTime.Now,
                };
                if(funcionario.Ativo == false)
                {
                    TempData["MensagemErro"] = "Funcionario inválido(a).";
                    return View("Criar", batidaModel);
                }
                _batidaRepositorio.Adicionar(batida);
                TempData["MensagemSucesso"] = "Batida registrada com sucesso.";
                return RedirectToAction("Criar", "Batida");
                
            }
            return View();
        }
    }
}
