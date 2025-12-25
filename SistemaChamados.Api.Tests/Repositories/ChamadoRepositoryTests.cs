using Microsoft.EntityFrameworkCore;
using Xunit;
using SistemaChamados.Api.Domain.Entities;
using SistemaChamados.Api.Domain.Enums;
using SistemaChamados.Api.Infrastructure.Data;
using SistemaChamados.Api.Infrastructure.Repositories;

namespace SistemaChamados.Api.Tests.Repositories
{
    public class ChamadoRepositoryTests
    {
        private readonly SistemaChamadosDbContext _context;
        private readonly ChamadoRepository _repository;

        public ChamadoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SistemaChamadosDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SistemaChamadosDbContext(options);
            _repository = new ChamadoRepository(_context);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarChamado()
        {
            // Arrange
            var chamado = new Chamado
            {
                Titulo = "Teste Chamado",
                Descricao = "Descrição de teste",
                Solicitante = "Usuario Teste",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Alta,
                DataCriacao = DateTime.UtcNow
            };

            // Act
            var resultado = await _repository.AddAsync(chamado);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0);
            
            var chamadoNoBanco = await _context.Chamados.FindAsync(resultado.Id);
            Assert.NotNull(chamadoNoBanco);
            Assert.Equal("Teste Chamado", chamadoNoBanco.Titulo);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodosChamados()
        {
            // Arrange
            var chamados = new List<Chamado>
            {
                new Chamado 
                { 
                    Titulo = "Chamado 1", 
                    Descricao = "Desc 1",
                    Solicitante = "Usuario 1",
                    Status = StatusChamado.Aberto,
                    Prioridade = PriorityChamado.Alta,
                    DataCriacao = DateTime.UtcNow
                },
                new Chamado 
                { 
                    Titulo = "Chamado 2", 
                    Descricao = "Desc 2",
                    Solicitante = "Usuario 2",
                    Status = StatusChamado.EmAndamento,
                    Prioridade = PriorityChamado.Média,
                    DataCriacao = DateTime.UtcNow
                }
            };

            await _context.Chamados.AddRangeAsync(chamados);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ComIdValido_DeveRetornarChamado()
        {
            // Arrange
            var chamado = new Chamado
            {
                Titulo = "Chamado Teste",
                Descricao = "Descrição",
                Solicitante = "Usuario",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Alta,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _repository.GetByIdAsync(chamado.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(chamado.Id, resultado.Id);
            Assert.Equal("Chamado Teste", resultado.Titulo);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarChamado()
        {
            // Arrange
            var chamado = new Chamado
            {
                Titulo = "Titulo Original",
                Descricao = "Descrição Original",
                Solicitante = "Usuario",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Baixa,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            // Atualizar
            chamado.Titulo = "Titulo Atualizado";
            chamado.Status = StatusChamado.EmAndamento;
            chamado.DataAtualizacao = DateTime.UtcNow;

            // Act
            var resultado = await _repository.UpdateAsync(chamado);

            // Assert
            Assert.Equal("Titulo Atualizado", resultado.Titulo);
            Assert.Equal(StatusChamado.EmAndamento, resultado.Status);
        }

        [Fact]
        public async Task DeleteAsync_ComIdValido_DeveDeletarChamado()
        {
            // Arrange
            var chamado = new Chamado
            {
                Titulo = "Chamado para deletar",
                Descricao = "Descrição",
                Solicitante = "Usuario",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Alta,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _repository.DeleteAsync(chamado.Id);

            // Assert
            Assert.True(resultado);
            var chamadoDeletado = await _context.Chamados.FindAsync(chamado.Id);
            Assert.Null(chamadoDeletado);
        }
    }
}
