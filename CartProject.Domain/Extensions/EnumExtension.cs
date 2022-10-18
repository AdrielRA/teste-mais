using System.ComponentModel;

namespace CartProject.Domain.Extensions;

public static class EnumExtension
{
    public static string? GetDescription<T>(this T value)
    {
        var enumType = value?.GetType();
        var field = enumType?.GetField(value?.ToString() ?? "");
        var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes?.Length == 0 ? value?.ToString() : (attributes?[0] as DescriptionAttribute)?.Description;
    }

    public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

}
