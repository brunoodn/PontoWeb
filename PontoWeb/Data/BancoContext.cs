using Microsoft.EntityFrameworkCore;
using PontoWeb.Models;

namespace PontoWeb.Data
{
    public class BancoContext: DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<BatidaModel> Batidas { get; set; }
    }
}
