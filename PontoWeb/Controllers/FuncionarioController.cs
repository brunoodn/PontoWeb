using Microsoft.AspNetCore.Mvc;
using PontoWeb.Filters;
using PontoWeb.Models;
using PontoWeb.Repositorio;

namespace PontoWeb.Controllers
{
    [PaginaSupervisor]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }
        public IActionResult Index()
        {
            List<FuncionarioModel> funcionarios = _funcionarioRepositorio.BuscarTodos();

            return View(funcionarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(FuncionarioModel funcionario)
        {
            if (ModelState.IsValid)
            {
                FuncionarioModel funcionarioDb = _funcionarioRepositorio.BuscarPorMatricula(funcionario.Matricula);
                string funcionarioNIS = _funcionarioRepositorio.BuscaPorNIS(funcionario.NIS);


                if (funcionarioDb == null && funcionarioNIS == null)
                {
                    _funcionarioRepositorio.Adicionar(funcionario);
                    TempData["MensagemSucesso"] = "Funcionario criado com sucesso..";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "Matricula ou NIS já existe..";
                return View();

            }
            return View(funcionario);
        }

        public IActionResult ConfirmarExclusao(int matricula)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(matricula);
            
            return View(funcionario);
        }

        public IActionResult Excluir(int matricula)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(matricula);
            try
            {
                funcionario.Ativo = false;
                funcionario.DataAtualizacao = DateTime.Now;
                _funcionarioRepositorio.Atualizar(funcionario);
                TempData["MensagemSucesso"] = "Funcionario excluido com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao excluir o Funcionario {erro.Message}";

                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int matricula)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorMatricula(matricula);

            return View(funcionario);
        }
        [HttpPost]
        public IActionResult Editar(FuncionarioModelSemSenha funcionario)
        {
            FuncionarioModel funcionarioDB = _funcionarioRepositorio.BuscarPorMatricula(funcionario.Matricula);

            if (ModelState.IsValid)
            {
                FuncionarioModel funcionarioDb = _funcionarioRepositorio.BuscarPorMatricula(funcionario.Matricula);
                string funcionarioNIS = _funcionarioRepositorio.BuscaPorNIS(funcionario.NIS);


                if (funcionarioDb == null && funcionarioNIS == null)
                {
                    funcionarioDB.Matricula = funcionario.Matricula;
                    funcionarioDB.Nome = funcionario.Nome;
                    funcionarioDB.NIS = funcionario.NIS;
                    funcionarioDB.Tipo = funcionario.Tipo;
                    funcionarioDB.Senha = funcionarioDB.Senha;
                    funcionarioDB.DataCriacao = funcionarioDB.DataCriacao;
                    funcionarioDB.DataAtualizacao = DateTime.Now;
                    funcionarioDB.Ativo = funcionarioDB.Ativo;

                    _funcionarioRepositorio.Atualizar(funcionarioDB);
                    TempData["MensagemSucesso"] = "Funcionario atualizado com sucesso";

                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "Matricula ou NIS já existe..";
                return View();
            }
            return View(funcionario);
        }
    }
}
