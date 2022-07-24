using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public interface IFuncionarioRepositorio
    {
        List<FuncionarioModel> BuscarTodos();

        FuncionarioModel BuscarPorMatricula(int matricula);

        FuncionarioModel Adicionar(FuncionarioModel funcionario);
        FuncionarioModel Atualizar(FuncionarioModel funcionario);

        FuncionarioModel AlterarSenha(AlterarSenhaModel alterarSenha);

    }
}
