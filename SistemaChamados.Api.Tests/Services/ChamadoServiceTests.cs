using Moq;
using Xunit;
using SistemaChamados.Api.Application.DTOs;
using SistemaChamados.Api.Application.Services;
using SistemaChamados.Api.Domain.Entities;
using SistemaChamados.Api.Domain.Enums;
using SistemaChamados.Api.Infrastructure.Repositories;

namespace SistemaChamados.Api.Tests.Services
{
    public class ChamadoServiceTests
    {
        private readonly Mock<IChamadoRepository> _mockRepository;
        private readonly ChamadoService _service;

        public ChamadoServiceTests()
        {
            _mockRepository = new Mock<IChamadoRepository>();
            _service = new ChamadoService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodosChamados()
        {
            // Arrange
            var chamados = new List<Chamado>
            {
                new Chamado 
                { 
                    Id = 1, 
                    Titulo = "Chamado 1", 
                    Descricao = "Descrição 1",
                    Solicitante = "Usuario 1",
                    Status = StatusChamado.Aberto,
                    Prioridade = PriorityChamado.Alta,
                    DataCriacao = DateTime.UtcNow
                },
                new Chamado 
                { 
                    Id = 2, 
                    Titulo = "Chamado 2", 
                    Descricao = "Descrição 2",
                    Solicitante = "Usuario 2",
                    Status = StatusChamado.EmAndamento,
                    Prioridade = PriorityChamado.Média,
                    DataCriacao = DateTime.UtcNow
                }
            };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(chamados);

            // Act
            var resultado = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ComIdValido_DeveRetornarChamado()
        {
            // Arrange
            var chamado = new Chamado 
            { 
                Id = 1, 
                Titulo = "Chamado Teste",
                Descricao = "Descrição Teste",
                Solicitante = "Usuário Teste",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Alta,
                DataCriacao = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(chamado);

            // Act
            var resultado = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Chamado Teste", resultado.Titulo);
        }

        [Fact]
        public async Task GetByIdAsync_ComIdInvalido_DeveRetornarNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Chamado?)null);

            // Act
            var resultado = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task CreateAsync_DeveCreateChamado()
        {
            // Arrange
            var dto = new CreateChamadoDTO
            {
                Titulo = "Novo Chamado",
                Descricao = "Descrição do novo chamado",
                Prioridade = PriorityChamado.Alta,
                Solicitante = "Novo Usuário"
            };

            var chamado = new Chamado
            {
                Id = 1,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Solicitante = dto.Solicitante,
                Prioridade = dto.Prioridade,
                Status = StatusChamado.Aberto,
                DataCriacao = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Chamado>())).ReturnsAsync(chamado);

            // Act
            var resultado = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Novo Chamado", resultado.Titulo);
            Assert.Equal(StatusChamado.Aberto, resultado.Status);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Chamado>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarChamado()
        {
            // Arrange
            var chamadoExistente = new Chamado
            {
                Id = 1,
                Titulo = "Chamado Antigo",
                Descricao = "Descrição Antiga",
                Status = StatusChamado.Aberto,
                Prioridade = PriorityChamado.Baixa,
                Solicitante = "Usuário",
                DataCriacao = DateTime.UtcNow
            };

            var updateDto = new UpdateChamadoDTO
            {
                Titulo = "Chamado Atualizado",
                Descricao = "Descrição Atualizada",
                Status = StatusChamado.EmAndamento,
                Prioridade = PriorityChamado.Alta,
                Responsavel = "Novo Responsável"
            };

            var chamadoAtualizado = new Chamado
            {
                Id = 1,
                Titulo = updateDto.Titulo,
                Descricao = updateDto.Descricao,
                Status = updateDto.Status,
                Prioridade = updateDto.Prioridade,
                Solicitante = chamadoExistente.Solicitante,
                Responsavel = updateDto.Responsavel,
                DataAtualizacao = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(chamadoExistente);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Chamado>())).ReturnsAsync(chamadoAtualizado);

            // Act
            var resultado = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Chamado Atualizado", resultado.Titulo);
            Assert.Equal(StatusChamado.EmAndamento, resultado.Status);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Chamado>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ComIdValido_DeveDeletarChamado()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var resultado = await _service.DeleteAsync(1);

            // Assert
            Assert.True(resultado);
            _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ComIdInvalido_DeveFalhar()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var resultado = await _service.DeleteAsync(999);

            // Assert
            Assert.False(resultado);
        }
    }
}
