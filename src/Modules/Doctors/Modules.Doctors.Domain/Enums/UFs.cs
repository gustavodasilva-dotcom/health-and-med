using System.Runtime.Serialization;

namespace Modules.Doctors.Domain.Enums;

public enum UFs
{
    [EnumMember(Value = "AC")]
    Acre = 1,

    [EnumMember(Value = "AL")]
    Alagoas = 2,

    [EnumMember(Value = "AP")]
    Amapa = 3,

    [EnumMember(Value = "AM")]
    Amazonas = 4,

    [EnumMember(Value = "BA")]
    Bahia = 5,

    [EnumMember(Value = "CE")]
    Ceara = 6,

    [EnumMember(Value = "DF")]
    DistritoFederal = 7,

    [EnumMember(Value = "ES")]
    EspiritoSanto = 8,

    [EnumMember(Value = "GO")]
    Goias = 9,

    [EnumMember(Value = "MA")]
    Maranhao = 10,

    [EnumMember(Value = "MT")]
    MatoGrosso = 11,

    [EnumMember(Value = "MS")]
    MatoGrossoDoSul = 12,

    [EnumMember(Value = "MG")]
    MinasGerais = 13,

    [EnumMember(Value = "PA")]
    Para = 14,

    [EnumMember(Value = "PB")]
    Paraiba = 15,

    [EnumMember(Value = "PR")]
    Parana = 16,

    [EnumMember(Value = "PE")]
    Pernambuco = 17,

    [EnumMember(Value = "PI")]
    Piaui = 18,

    [EnumMember(Value = "RJ")]
    RioDeJaneiro = 19,

    [EnumMember(Value = "RN")]
    RioGrandeDoNorte = 20,

    [EnumMember(Value = "RS")]
    RioGrandeDoSul = 21,

    [EnumMember(Value = "RO")]
    Rondonia = 22,

    [EnumMember(Value = "RR")]
    Roraima = 23,

    [EnumMember(Value = "SC")]
    SantaCatarina = 24,

    [EnumMember(Value = "SP")]
    SaoPaulo = 25,

    [EnumMember(Value = "SE")]
    Sergipe = 26,

    [EnumMember(Value = "TO")]
    Tocantins = 27
}
