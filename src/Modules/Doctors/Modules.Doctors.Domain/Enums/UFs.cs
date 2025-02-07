using System.ComponentModel.DataAnnotations;

namespace Modules.Doctors.Domain.Enums;

public enum UFs
{
    [Display(ShortName = "AC")]
    Acre = 1,

    [Display(ShortName = "AL")]
    Alagoas = 2,

    [Display(Name = "Amapá", ShortName = "AP")]
    Amapa = 3,

    [Display(ShortName = "AM")]
    Amazonas = 4,

    [Display(ShortName = "BA")]
    Bahia = 5,

    [Display(Name = "Ceará", ShortName = "CE")]
    Ceara = 6,

    [Display(Name = "Distrito Federal", ShortName = "DF")]
    DistritoFederal = 7,

    [Display(Name = "Espírito Santo", ShortName = "ES")]
    EspiritoSanto = 8,

    [Display(Name = "Goiás", ShortName = "GO")]
    Goias = 9,

    [Display(Name = "Maranhão", ShortName = "MA")]
    Maranhao = 10,

    [Display(Name = "Mato Grosso", ShortName = "MT")]
    MatoGrosso = 11,

    [Display(Name = "Mato Grosso do Sul", ShortName = "MS")]
    MatoGrossoDoSul = 12,

    [Display(Name = "Minas Gerais", ShortName = "MG")]
    MinasGerais = 13,

    [Display(Name = "Pará", ShortName = "PA")]
    Para = 14,

    [Display(Name = "Paraíba", ShortName = "PB")]
    Paraiba = 15,

    [Display(Name = "Paraná", ShortName = "PR")]
    Parana = 16,

    [Display(ShortName = "PE")]
    Pernambuco = 17,

    [Display(Name = "Piauí", ShortName = "PI")]
    Piaui = 18,

    [Display(Name = "Rio de Janeiro", ShortName = "RJ")]
    RioDeJaneiro = 19,

    [Display(Name = "Rio Grande do Norte", ShortName = "RN")]
    RioGrandeDoNorte = 20,

    [Display(Name = "Rio Grande do Sul", ShortName = "RS")]
    RioGrandeDoSul = 21,

    [Display(Name = "Rondônia", ShortName = "RO")]
    Rondonia = 22,

    [Display(ShortName = "RR")]
    Roraima = 23,

    [Display(Name = "Santa Catarina", ShortName = "SC")]
    SantaCatarina = 24,

    [Display(Name = "São Paulo", ShortName = "SP")]
    SaoPaulo = 25,

    [Display(ShortName = "SE")]
    Sergipe = 26,

    [Display(ShortName = "TO")]
    Tocantins = 27
}
