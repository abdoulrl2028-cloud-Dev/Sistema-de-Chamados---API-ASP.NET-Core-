using SistemaChamados.Api.Domain.Enums;

namespace SistemaChamados.Api.Application.DTOs
{
    public class ChamadoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusChamado Status { get; set; }
        public PriorityChamado Prioridade { get; set; }
        public string Solicitante { get; set; } = string.Empty;
        public string? Responsavel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Observacoes { get; set; }
    }

    public class CreateChamadoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public PriorityChamado Prioridade { get; set; }
        public string Solicitante { get; set; } = string.Empty;
    }

    public class UpdateChamadoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusChamado Status { get; set; }
        public PriorityChamado Prioridade { get; set; }
        public string? Responsavel { get; set; }
        public string? Observacoes { get; set; }
    }
}
