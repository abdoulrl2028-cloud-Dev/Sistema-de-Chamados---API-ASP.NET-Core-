using SistemaChamados.Api.Domain.Entities;

namespace SistemaChamados.Api.Infrastructure.Repositories
{
    public interface IChamadoRepository
    {
        Task<IEnumerable<Chamado>> GetAllAsync();
        Task<Chamado?> GetByIdAsync(int id);
        Task<Chamado> AddAsync(Chamado chamado);
        Task<Chamado> UpdateAsync(Chamado chamado);
        Task<bool> DeleteAsync(int id);
    }
}
