using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Infrastructure.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly AppDbContext _context;

        public ContratoRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Contrato contrato)
        {
            await _context.Contratos.AddAsync(contrato);
        }

        public Task AtualizarAsync(Contrato contrato)
        {
            _context.Contratos.Update(contrato);

            return Task.CompletedTask;
        }

        public async Task<Contrato?> ObterPorIdAsync(Guid id)
        {
            return await _context.Contratos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Contrato>> ObterPorEventoAsync(Guid eventoId)
        {
            return await _context.Contratos
                .Where(x => x.EventoId == eventoId)
                .ToListAsync();
        }

        public Task RemoverAsync(Contrato contrato)
        {
            _context.Contratos.Remove(contrato);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
