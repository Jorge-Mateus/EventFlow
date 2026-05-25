using EventFlow.Application.DTOs.Cliente;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarClienteDto dto)
        {
            var cliente = new Cliente(dto.Nome, dto.Telefone, dto.Email);

            await _repository.AdicionarAsync(cliente);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<ClienteDto>>
            ObterTodosAsync()
        {
            var clientes = await _repository.ObterTodosAsync();

            return clientes.Select(x => new ClienteDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Telefone = x.Telefone,
                Email = x.Email
            });
        }

        public async Task<ClienteDto?> ObterPorIdAsync(Guid id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);

            if (cliente is null) return null;

            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };
        }
        public async Task<ClienteDetalheDto?> ObterDetalheAsync(Guid id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);

            if (cliente is null) return null;

            return new ClienteDetalheDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };
        }
        public async Task AtualizarAsync(AtualizarClienteDto dto)
        {
            var cliente = await _repository.ObterPorIdAsync(dto.Id);

            if (cliente is null) return;

            cliente.Atualizar(dto.Nome, dto.Telefone, dto.Email);

            await _repository.AtualizarAsync(cliente);

            await _repository.SalvarAlteracoesAsync();
        }
        public async Task ExcluirAsync(Guid id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);

            if (cliente is null) return;

            await _repository.RemoverAsync(cliente);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
