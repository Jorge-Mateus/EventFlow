using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using EventFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EventFlow.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"));
            });
            services.AddScoped<IPropostaRepository,PropostaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<ICategoriaOrcamentoRepository, CategoriaOrcamentoRepository>();
            services.AddScoped<IVisitaTecnicaRepository, VisitaTecnicaRepository>();
            services.AddScoped<IProjetoDecoracaoRepository, ProjetoDecoracaoRepository>();
            services.AddScoped<IFuncaoRepository, FuncaoRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            return services;
        }
    }
}
