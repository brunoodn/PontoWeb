using PontoWeb.Models;

namespace PontoWeb.Repositorio
{
    public interface IBatidaRepositorio
    {
        BatidaModel Adicionar(BatidaModel batida);
        List<BatidaModel> MinhasBatidas(int matricula);
    }
}
