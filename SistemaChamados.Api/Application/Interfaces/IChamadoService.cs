using SistemaChamados.Api.Application.DTOs;

namespace SistemaChamados.Api.Application.Interfaces
{
    public interface IChamadoService
    {
        Task<IEnumerable<ChamadoDTO>> GetAllAsync();
        Task<ChamadoDTO?> GetByIdAsync(int id);
        Task<ChamadoDTO> CreateAsync(CreateChamadoDTO dto);
        Task<ChamadoDTO> UpdateAsync(int id, UpdateChamadoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
