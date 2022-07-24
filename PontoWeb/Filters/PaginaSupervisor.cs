using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PontoWeb.Models;

namespace PontoWeb.Filters
{
    public class PaginaSupervisor : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoFuncionario = context.HttpContext.Session.GetString("SessaoFuncionarioLogado");

            if (string.IsNullOrEmpty(sessaoFuncionario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);

                if (funcionario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if (funcionario.Tipo != Enums.TipoFuncionarioEnum.Supervisor)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });

                }
            }
            base.OnActionExecuting(context);
        }
    }
}
