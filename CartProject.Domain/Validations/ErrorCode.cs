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
}
