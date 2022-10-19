using System.ComponentModel;

namespace CartProject.Domain.Enums;

public enum CartStatus
{
    [Description("Carrinho aberto")]
    OPENED,
    [Description("Carrinho finalizado")]
    CLOSED,
}
