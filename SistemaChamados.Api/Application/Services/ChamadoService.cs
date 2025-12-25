using SistemaChamados.Api.Application.DTOs;
using SistemaChamados.Api.Application.Interfaces;
using SistemaChamados.Api.Domain.Entities;
using SistemaChamados.Api.Infrastructure.Repositories;

namespace SistemaChamados.Api.Application.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoService(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        public async Task<IEnumerable<ChamadoDTO>> GetAllAsync()
        {
            var chamados = await _chamadoRepository.GetAllAsync();
            return chamados.Select(MapToDTO);
        }

        public async Task<ChamadoDTO?> GetByIdAsync(int id)
        {
            var chamado = await _chamadoRepository.GetByIdAsync(id);
            return chamado == null ? null : MapToDTO(chamado);
        }

        public async Task<ChamadoDTO> CreateAsync(CreateChamadoDTO dto)
        {
            var chamado = new Chamado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Prioridade = dto.Prioridade,
                Solicitante = dto.Solicitante,
                Status = Domain.Enums.StatusChamado.Aberto,
                DataCriacao = DateTime.UtcNow
            };

            var created = await _chamadoRepository.AddAsync(chamado);
            return MapToDTO(created);
        }

        public async Task<ChamadoDTO> UpdateAsync(int id, UpdateChamadoDTO dto)
        {
            var chamado = await _chamadoRepository.GetByIdAsync(id);
            if (chamado == null)
                throw new Exception("Chamado não encontrado");

            chamado.Titulo = dto.Titulo;
            chamado.Descricao = dto.Descricao;
            chamado.Status = dto.Status;
            chamado.Prioridade = dto.Prioridade;
            chamado.Responsavel = dto.Responsavel;
            chamado.Observacoes = dto.Observacoes;
            chamado.DataAtualizacao = DateTime.UtcNow;

            if (dto.Status == Domain.Enums.StatusChamado.Fechado)
                chamado.DataFechamento = DateTime.UtcNow;

            var updated = await _chamadoRepository.UpdateAsync(chamado);
            return MapToDTO(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _chamadoRepository.DeleteAsync(id);
        }

        private static ChamadoDTO MapToDTO(Chamado chamado)
        {
            return new ChamadoDTO
            {
                Id = chamado.Id,
                Titulo = chamado.Titulo,
                Descricao = chamado.Descricao,
                Status = chamado.Status,
                Prioridade = chamado.Prioridade,
                Solicitante = chamado.Solicitante,
                Responsavel = chamado.Responsavel,
                DataCriacao = chamado.DataCriacao,
                DataAtualizacao = chamado.DataAtualizacao,
                DataFechamento = chamado.DataFechamento,
                Observacoes = chamado.Observacoes
            };
        }
    }
}
