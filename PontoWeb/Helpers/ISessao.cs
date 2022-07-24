using PontoWeb.Models;

namespace PontoWeb.Helpers
{
    public interface ISessao 
    {
        FuncionarioModel BuscarSessaoFuncionarioLogado();

        void CriarSessaoFuncionarioLogado(FuncionarioModel funcionario);

        void ExcluirSessaoFuncionarioLogado();
    }
}
