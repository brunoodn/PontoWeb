using Microsoft.AspNetCore.Mvc;
using PontoWeb.Filters;
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
            return View();
        }
        public IActionResult MinhasBatidas()
        {
            FuncionarioModel funcionario = _sessao.BuscarSessaoFuncionarioLogado();
            List<BatidaModel> batidas = _batidaRepositorio.MinhasBatidas(funcionario.Matricula);

            return View(batidas);
        }
        public IActionResult Lista()
        {
            List<BatidaModel> batidas = _batidaRepositorio.ListaBatidas();
            return View(batidas);
        }

        [PaginaSupervisor]
        public IActionResult ExportarBatidas(BatidaPorDataModel batidas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _batidaRepositorio.ExportarBatidasPorData(batidas);
                    TempData["MensagemSucesso"] = "Arquivo exportado com sucesso..";
                    return RedirectToAction("Index", "Batida");
                }
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Erro ao exportar arquivo.. {erro.Message}";
                return RedirectToAction("Index", "Batida");
            }
            TempData["MensagemErro"] = $"Erro ao exportar arquivo.";
            return RedirectToAction("Index", "Batida");
        }

        [PaginaSupervisor]
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            
            BatidaModel batida = _batidaRepositorio.BuscarPorID(id);
            if (batida == null)
            {
                TempData["MensagemErro"] = "Batida não encontrada..";
                return View();
            }
            if (batida.Ativo == false)
            {
                TempData["MensagemErro"] = "Batida está inativa..";
                return View();
            }
            return View(batida);
        }

        [HttpPost]
        [PaginaSupervisor]
        public IActionResult Atualizar(BatidaModel batida)
        {
            BatidaModel batidaDB = _batidaRepositorio.BuscarPorID(batida.Id);
            batidaDB.DataAtualizacao = DateTime.Now;
            batidaDB.MatriculaEmpregado = batida.MatriculaEmpregado;
            batidaDB.Registro = batida.Registro;
            batidaDB.TipoBatida = Enums.TipoBatidaEnum.Ajustado;
            batidaDB.Observacao = batida.Observacao;
            batidaDB.MatriculaSupervisorAjuste = _sessao.BuscarSessaoFuncionarioLogado().Matricula;

            if (ModelState.IsValid)
            {
                TempData["MensagemSucesso"] = "Batida atualizada com sucesso.";
                _batidaRepositorio.Atualizar(batidaDB);
                return RedirectToAction("Lista", "Batida");
            }
            return RedirectToAction("Editar", "Batida",batida);
        }

        [PaginaSupervisor]
        public IActionResult ConfirmarExclusao(int id)
        {
            BatidaModel batida = _batidaRepositorio.BuscarPorID(id);
            if(batida == null)
            {
                TempData["MensagemErro"] = "Batida não encontrada..";
                return View();
            }
            return View(batida);

        }
        [PaginaSupervisor]
        public IActionResult Remover(int id)
        {
            BatidaModel batidaDB = _batidaRepositorio.BuscarPorID(id);
            if (batidaDB == null)
            {
                TempData["MensagemErro"] = "Batida não encontrada..";
                return RedirectToAction("Lista", "Batida");
            }
            batidaDB.Ativo = false;
            
            TempData["MensagemSucesso"] = "Batida removida com sucesso.";
            _batidaRepositorio.Atualizar(batidaDB);

            return RedirectToAction("Lista", "Batida");
        }

        public IActionResult RegistrarBatida(int matricula)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(matricula);
            

            if(funcionario != null && funcionario.Ativo == true)
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

        [PaginaSupervisor]
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
                if(batidaModel.Registro > DateTime.Now)
                {
                    TempData["MensagemErro"] = "O registro nao pode ser maior que a data atual";
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
