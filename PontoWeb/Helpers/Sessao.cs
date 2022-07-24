using Newtonsoft.Json;
using PontoWeb.Models;

namespace PontoWeb.Helpers
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public FuncionarioModel BuscarSessaoFuncionarioLogado()
        {
            string sessaoFuncionario = _httpContextAccessor.HttpContext.Session.GetString("SessaoFuncionarioLogado");

            if (String.IsNullOrEmpty(sessaoFuncionario)) return null;

            return JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
        }

        public void CriarSessaoFuncionarioLogado(FuncionarioModel funcionario)
        {
            string valor = JsonConvert.SerializeObject(funcionario);
            _httpContextAccessor.HttpContext.Session.SetString("SessaoFuncionarioLogado", valor);
        }

        public void ExcluirSessaoFuncionarioLogado()
        {
            _httpContextAccessor.HttpContext.Session.Remove("SessaoFuncionarioLogado");
        }
    }
}
