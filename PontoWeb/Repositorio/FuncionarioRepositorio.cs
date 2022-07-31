using PontoWeb.Data;
using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public FuncionarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public FuncionarioModel Adicionar(FuncionarioModel funcionario)
        {
            funcionario.SetSenhaHash();
            _bancoContext.Funcionarios.Add(funcionario);
            _bancoContext.SaveChanges();

            return funcionario;
        }

        public FuncionarioModel Atualizar(FuncionarioModel funcionario)
        {
            _bancoContext.Funcionarios.Update(funcionario);
            _bancoContext.SaveChanges();
            
            return funcionario;
        }

        public FuncionarioModel BuscarPorMatricula(int matricula)
        {
            return _bancoContext.Funcionarios.FirstOrDefault(f => f.Matricula == matricula);
        }
        public string BuscaNIS(int matricula)
        {
            FuncionarioModel funcionario = _bancoContext.Funcionarios.FirstOrDefault(f => f.Matricula == matricula);

            return funcionario.NIS;
        }
        public string BuscaPorNIS(string nis)
        {
            FuncionarioModel funcionario = _bancoContext.Funcionarios.FirstOrDefault(f => f.NIS == nis);

            return funcionario.NIS;
        }

        public List<FuncionarioModel> BuscarTodos()
        {
            return _bancoContext.Funcionarios.Where(f => f.Ativo == true).ToList();
        }

        public FuncionarioModel AlterarSenha (AlterarSenhaModel alterarSenha)
        {
            FuncionarioModel funcionario = BuscarPorMatricula(alterarSenha.Matricula);

            if (funcionario == null) throw new Exception("houve um erro na atualização do usuario. Usuário não encontrado");

            if (!funcionario.SenhaValida(alterarSenha.Senha)) throw new Exception("Senha atual não confere..");

            if (funcionario.SenhaValida(alterarSenha.NovaSenha)) throw new Exception("A nova senha deve ser diferente da senha atual..");

            funcionario.SetNovaSenha(alterarSenha.NovaSenha);
            funcionario.DataAtualizacao = DateTime.Now;

            _bancoContext.Funcionarios.Update(funcionario);
            _bancoContext.SaveChanges();

            return funcionario;
        }
    }
}
