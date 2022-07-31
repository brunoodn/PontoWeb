using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public interface IBatidaRepositorio
    {
        BatidaModel Adicionar(BatidaModel batida);
        List<BatidaModel> MinhasBatidas(int matricula);
        List<BatidaModel> ListaBatidas();
        BatidaModel Atualizar(BatidaModel batida);
        BatidaModel BuscarPorID(int id);
        List<BatidaModel> ExportarBatidasPorData(BatidaPorDataModel batidas);
    }
}
