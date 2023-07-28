using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movement
    {
        public Guid IdMovimento { get; set; }

        public Guid IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public MovementTypeEnum? TipoMovimento { get; set; }

        public string? Valor { get; set; }

        public Guid Chave_IdEmpotencia { get; set; }
    }
}