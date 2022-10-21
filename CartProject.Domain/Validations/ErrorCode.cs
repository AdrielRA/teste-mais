using System.ComponentModel;

namespace CartProject.Domain.Validations;

public enum ErrorCode
{
    //Sempre que adicionar um novo tipo nesse enum, favor adicionar também a descrição
    [Description("Algo deu errado")]
    EX00000,
    [Description("Id não pode ser nulo")]
    EX00001,
    [Description("Id não deve estar vazio")]
    EX00002,
    [Description("Id informado é inválido")]
    EX00003,
    [Description("Id informado não foi encontrado")]
    EX00004,
    [Description("Email não pode ser nulo")]
    EX00005,
    [Description("Email não deve estar vazio")]
    EX00006,
    [Description("Email informado é inválido")]
    EX00007,
    [Description("Email informado não foi encontrado")]
    EX00008,
    [Description("Senha não pode ser nula")]
    EX00009,
    [Description("Senha não deve estar vazia")]
    EX00010,
    [Description("Senha deve ter pelo menos 6 dígitos")]
    EX00011,
    [Description("Senha informada está incorreta")]
    EX00012,
    [Description("Conta desativada")]
    EX00013,
    [Description("Nome não pode ser nulo")]
    EX00014,
    [Description("Nome não deve estar vazio")]
    EX00015,
    [Description("Nome informado é inválido")]
    EX00016,
    [Description("Nome deve ter pelo menos 10 caracteres")]
    EX00017,
    [Description("Nome deve ter no máximo 100 caracteres")]
    EX00018,
    [Description("Valor não pode ser nulo")]
    EX00019,
    [Description("Valor não deve estar vazio")]
    EX00020,
    [Description("Valor deve ser maior que zero")]
    EX00021,
    [Description("Código não pode ser nulo")]
    EX00022,
    [Description("Código não deve estar vazio")]
    EX00023,
    [Description("Código deve conter apenas letras e números")]
    EX00024,
    [Description("Código deve ter pelo menos 6 caracteres")]
    EX00025,
}
