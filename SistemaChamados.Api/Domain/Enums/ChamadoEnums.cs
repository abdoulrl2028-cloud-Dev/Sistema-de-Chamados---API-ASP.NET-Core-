namespace SistemaChamados.Api.Domain.Enums
{
    public enum StatusChamado
    {
        Aberto = 1,
        EmAndamento = 2,
        Resolvido = 3,
        Fechado = 4
    }

    public enum PriorityChamado
    {
        Baixa = 1,
        Média = 2,
        Alta = 3,
        Urgente = 4
    }
}
