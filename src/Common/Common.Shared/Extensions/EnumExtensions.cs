using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Common.Shared.Contracts;

namespace Common.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName<TEnum>(this TEnum enumValue)
        where TEnum : Enum
        => enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? enumValue.ToString();

    public static EnumResponse ToEnumResponse<TEnum>(this TEnum enumValue)
        where TEnum : Enum
        => new()
        {
            Id = Convert.ToInt32(enumValue),
            Description = enumValue.GetDisplayName()
        };
}
