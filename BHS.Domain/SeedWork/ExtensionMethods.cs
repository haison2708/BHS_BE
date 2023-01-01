namespace BHS.Domain.SeedWork;

public static class ExtensionMethods
{
    public static string? GetStringValue(this Enum value)
    {
        // Get the type
        var type = value.GetType();

        // Get fieldinfo for this type
        var fieldInfo = type.GetField(value.ToString());

        // Get the stringvalue attributes
        var attribs = fieldInfo!.GetCustomAttributes(
            typeof(StringValueAttribute), false) as StringValueAttribute[];

        // Return the first if there was a match.
        return attribs!.Length > 0 ? attribs[0].StringValue : null;
    }
    
    public static int ToInt(this Enum value)
    {
        return Convert.ToInt32(value);
    }
}