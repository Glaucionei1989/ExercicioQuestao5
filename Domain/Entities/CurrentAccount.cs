namespace Questao5.Domain.Entities
{
    public class CurrentAccount
    {
        public string? IdContaCorrente { get; set; }

        public long Numero { get; set; }

        public string? Nome { get; set; }

        public bool Ativo { get; set; }
    }
}