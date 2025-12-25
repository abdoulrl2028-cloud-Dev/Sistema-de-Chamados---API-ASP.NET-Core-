using Microsoft.EntityFrameworkCore;
using SistemaChamados.Api.Domain.Entities;
using SistemaChamados.Api.Infrastructure.Data;

namespace SistemaChamados.Api.Infrastructure.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly SistemaChamadosDbContext _context;

        public ChamadoRepository(SistemaChamadosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chamado>> GetAllAsync()
        {
            return await _context.Chamados.OrderByDescending(c => c.DataCriacao).ToListAsync();
        }

        public async Task<Chamado?> GetByIdAsync(int id)
        {
            return await _context.Chamados.FindAsync(id);
        }

        public async Task<Chamado> AddAsync(Chamado chamado)
        {
            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();
            return chamado;
        }

        public async Task<Chamado> UpdateAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
            return chamado;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
                return false;

            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
