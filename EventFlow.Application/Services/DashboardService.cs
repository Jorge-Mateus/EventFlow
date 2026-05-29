using EventFlow.Application.DTOs.Dashboard;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;
using EventFlow.Domain.Interfaces;


namespace EventFlow.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMovimentacaoFinanceiraRepository _financeiroRepository;

        public DashboardService(IEventoRepository eventoRepository, IMovimentacaoFinanceiraRepository financeiroRepository)
        {
            _eventoRepository = eventoRepository;
            _financeiroRepository = financeiroRepository;
        }

        public async Task<DashboardDto> ObterAsync()
        {
            var eventos =
                await _eventoRepository.ObterTodosAsync();

            var movimentacoes =
                await _financeiroRepository.ObterTodosAsync();

            var financeiroMensal = movimentacoes
                .GroupBy(x => new
                {
                    x.DataMovimentacao.Year,
                    x.DataMovimentacao.Month
                })
                .OrderBy(x => x.Key.Year)
                .ThenBy(x => x.Key.Month)
                .Select(g => new DashboardFinanceiroMensalDto
                {
                    Mes = $"{g.Key.Month:00}/{g.Key.Year}",

                    Entradas = g
                        .Where(x => x.Tipo == TipoMovimentacao.Entrada)
                        .Sum(x => x.Valor),

                    Saidas = g
                        .Where(x => x.Tipo == TipoMovimentacao.Saida)
                        .Sum(x => x.Valor)
                })
                .ToList();

            return new DashboardDto
            {
                TotalEventos = eventos.Count(),

                EventosFuturos = eventos.Count(x =>
                    x.DataEvento > DateTime.Now),

                EventosMes = eventos.Count(x =>
                    x.DataEvento.Month == DateTime.Now.Month &&
                    x.DataEvento.Year == DateTime.Now.Year),

                EventosSemEquipe = eventos.Count(x =>
                    !x.TemEquipe),

                EventosSemFornecedor = eventos.Count(x =>
                    !x.TemFornecedor),

                ReceitaTotal = movimentacoes
                    .Where(x => x.Tipo == TipoMovimentacao.Entrada)
                    .Sum(x => x.Valor),

                DespesaTotal = movimentacoes
                    .Where(x => x.Tipo == TipoMovimentacao.Saida)
                    .Sum(x => x.Valor),
                
                ProximosEventos = eventos
                    .Where(x => x.DataEvento >= DateTime.Now)
                    .OrderBy(x => x.DataEvento)
                    .Take(5)
                    .Select(x => new DashboardEventoDto
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        DataEvento = x.DataEvento,
                        LocalEvento = x.LocalEvento,
                        TemEquipe = x.TemEquipe,
                        TemFornecedor = x.TemFornecedor,
                        StatusOperacional = ObterStatusOperacional(x)
                    })
                    .ToList(),

                FinanceiroMensal = financeiroMensal
            };
        }
        private string ObterStatusOperacional(Evento evento)
        {
            if (!evento.TemEquipe)
                return "Aguardando montagem da equipe";

            if (!evento.TemFornecedor)
                return "Aguardando fornecedores";

            return "Operação completa";
        }
    }

}
