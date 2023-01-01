namespace BHS.Domain.SeedWork;

public class StringValueAttribute : Attribute
{
    public StringValueAttribute(string? value)
    {
        StringValue = value;
    }

    public string? StringValue { get; set; }
}