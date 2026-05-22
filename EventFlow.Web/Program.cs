using EventFlow.Application.Interfaces;
using EventFlow.Application.Services;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.DependencyInjection;
using EventFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPropostaService, PropostaService>();
builder.Services.AddScoped<ICategoriaOrcamentoService, CategoriaOrcamentoService>();
builder.Services.AddScoped<IEventoService, EventoService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Home}/{action=Index}/{id?}");

app.Run();