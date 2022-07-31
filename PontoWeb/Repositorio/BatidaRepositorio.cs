using PontoWeb.Data;
using PontoWeb.Helpers;
using PontoWeb.Models;
using System.IO;

namespace PontoWeb.Repositorio
{
    public class BatidaRepositorio : IBatidaRepositorio
    {
        private readonly BancoContext _bancoContext;
        //private readonly ISessao _sessao;
        private readonly IFuncionarioRepositorio _funcionario;

        public BatidaRepositorio(BancoContext bancoContext, IFuncionarioRepositorio funcionario)
        {
            _bancoContext = bancoContext;
            _funcionario = funcionario;

        }
        public BatidaModel Adicionar(BatidaModel batida)
        {
            _bancoContext.Batidas.Add(batida);
            _bancoContext.SaveChanges();

            return batida;
        }

        public BatidaModel Atualizar(BatidaModel batida)
        {
            _bancoContext.Batidas.Update(batida);
            _bancoContext.SaveChanges();

            return batida;
        }

        public BatidaModel BuscarPorID(int id)
        {
            return _bancoContext.Batidas.FirstOrDefault(b => b.Id == id);
        }

        public List<BatidaModel> MinhasBatidas(int matricula)
        {
            return _bancoContext.Batidas.Where(b => (b.MatriculaEmpregado == matricula) && (b.Ativo == true) && (b.Registro.Year == DateTime.Now.Year) && (b.Registro.Month == DateTime.Now.Month)).OrderByDescending(b => b.Registro).ToList();
        }

        public List<BatidaModel> ListaBatidas()
        {
            return _bancoContext.Batidas.Where(b => b.Ativo == true).OrderByDescending(b => b.Registro).ToList();
        }

        public List<BatidaModel> ExportarBatidasPorData(BatidaPorDataModel batidas)
        {
            List<BatidaModel> batidasData = _bancoContext.Batidas.Where(b => (b.Registro.Date >= batidas.DataInicial.Date) && (b.Registro.Date <= batidas.DataFinal.Date) && (b.Ativo == true)).OrderByDescending(f => f.Registro).ToList();
            string path = $"C:\\TEMP\\batidas-"+ DateTime.Now.ToString("ddMMyyyyHHmm") + ".txt";
            
            using (FileStream fs =  File.Create(path))
            {
                fs.Close();
                using(StreamWriter sw = File.AppendText(path))
                {
                    foreach(BatidaModel batida in batidasData)
                    {
                        sw.WriteLine("999999999" + (batida.TipoBatida == PontoWeb.Enums.TipoBatidaEnum.Ajustado ? "5" : "3") + batida.Registro.ToString("ddMMyyyyHHmm") + _funcionario.BuscaNIS(batida.MatriculaEmpregado) );
                    }
                    
                }
            }
            return batidasData;
        }

    }
}
