using Microsoft.EntityFrameworkCore;
using PontoWeb.Data;
using PontoWeb.Helpers;
using PontoWeb.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>(o => o.UseSqlServer("Server=./;Database=DB_PontoWeb;User Id=sa;Password=Albodn@1223"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<IBatidaRepositorio, BatidaRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSession(s =>{
    s.Cookie.HttpOnly = true;
    s.Cookie.IsEssential = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
