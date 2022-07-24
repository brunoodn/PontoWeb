using PontoWeb.Data;
using PontoWeb.Helpers;
using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public class BatidaRepositorio : IBatidaRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;

        public BatidaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;

        }
        public BatidaModel Adicionar(BatidaModel batida)
        {
            _bancoContext.Batidas.Add(batida);
            _bancoContext.SaveChanges();

            return batida;
        }

        public List<BatidaModel> MinhasBatidas(int matricula)
        {
            return _bancoContext.Batidas.Where(f => f.MatriculaEmpregado == matricula).OrderByDescending(f => f.Registro).ToList();
        }
    }
}
