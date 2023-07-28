using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public enum ExceptionMessages
    {
        [Description("Conta corrente não cadastrada no banco.")]
        INVALID_ACCOUNT,

        [Description("Conta corrente não esta ativo no banco")]
        INACTIVE_ACCOUNT,

        [Description("O valor da movimentação não pode ser negativo: valor da movimentação: {0}")]
        INVALID_VALUE,

        [Description("O tipo da movimentação esta inválido: tipo da movimentação: {0}")]
        INVALID_TYPE,

        [Description("Não é possivel completar essa movimentação: Saldo disponivel: {0}")]
        INVALID_MOVEMENT
    }
}