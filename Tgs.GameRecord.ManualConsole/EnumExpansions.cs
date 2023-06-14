using System.ComponentModel;
using System.Reflection;

namespace Tgs.GameRecord.ManualConsole;

public static class EnumExpansions
{
    public static string Description(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString())!;

        return fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any()
            ? attributes.First().Description
            : value.ToString();
    }
}
