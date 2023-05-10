using ContactEase.Domain.Common.Errors;

namespace ContactEase.Domain.Entities;

public partial class Contact
{
    public static void ValidateTypeLength(string type)
    {
        if(type.Length is < MinTypeLength or > MaxTypeLength)
        {
            Errors.Add(ContactErrors.InvalidType);
        }
    }

    public static void ValidateValueLength(string value)
    {
        if(value.Length is < MinValueLengh or > MaxValueLengh)
        {
            Errors.Add(ContactErrors.InvalidValue);
        }
    }
}
