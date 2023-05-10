using ContactEase.Domain.Common.Entites;
using ErrorOr;

namespace ContactEase.Domain.Entities;

public partial class Contact : BaseAuditableEntity
{
    private Contact(Guid personId, string type, string value)
    {
        PersonId = personId;
        Type = type;
        Value = value;
        Created = DateTime.UtcNow;
    }

    public const int MinTypeLength = 1;
    public const int MaxTypeLength = 50;
    public const int MinValueLengh = 5;
    public const int MaxValueLengh = 50;

    public Guid PersonId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public Person Person { get; private set; } = null!;

    public static List<Error> Errors { get; private set; } = null!;

    public static ErrorOr<Contact> Create(Guid personId, string type, string value)
    {
        Errors = new List<Error>();

        ValidateTypeLength(type);
        ValidateValueLength(value);

        if(Errors.Count is > 0)
        {
            return Errors;
        }

        var contact = new Contact(personId, type, value);

        return contact;
    }

    public ErrorOr<Contact> Update(string type, string value)
    {
        Errors = new List<Error>();

        ValidateTypeLength(type);
        ValidateValueLength(value);

        if(Errors.Count is > 0)
        {
            return Errors;
        }

        Type = type;
        Value = value;
        LastModified = DateTime.UtcNow;

        return this;
    }
}
