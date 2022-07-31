using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public interface IFuncionarioRepositorio
    {
        List<FuncionarioModel> BuscarTodos();

        FuncionarioModel BuscarPorMatricula(int matricula);
        string BuscaNIS(int matricula);
        string BuscaPorNIS(string nis);
        FuncionarioModel Adicionar(FuncionarioModel funcionario);
        FuncionarioModel Atualizar(FuncionarioModel funcionario);

        FuncionarioModel AlterarSenha(AlterarSenhaModel alterarSenha);

    }
}
