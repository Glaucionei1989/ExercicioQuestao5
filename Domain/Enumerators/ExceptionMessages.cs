using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public enum ExceptionMessages
    {
        [Description("Conta corrente n�o cadastrada no banco.")]
        INVALID_ACCOUNT,

        [Description("Conta corrente n�o esta ativo no banco")]
        INACTIVE_ACCOUNT,

        [Description("O valor da movimenta��o n�o pode ser negativo: valor da movimenta��o: {0}")]
        INVALID_VALUE,

        [Description("O tipo da movimenta��o esta inv�lido: tipo da movimenta��o: {0}")]
        INVALID_TYPE,

        [Description("N�o � possivel completar essa movimenta��o: Saldo disponivel: {0}")]
        INVALID_MOVEMENT
    }
}