using ContactEase.Domain.Common.Entites;
using ErrorOr;

namespace ContactEase.Domain.Entities;

public partial class Person : BaseAuditableEntity
{
    public Person(string name, string nickname, string notes)
    {
        Name = name;
        Nickname = nickname;
        Notes = notes;
        Created = DateTime.UtcNow;
    }

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;
    public const int MinNicknameLengh = 2;
    public const int MaxNicknameLengh = 50;
    public const int MaxNotesLengh = 250;

    public string Name { get; private set; } = string.Empty;
    public string Nickname { get; private set; } = string.Empty;
    public string Notes { get; private set; } = string.Empty;

    public ICollection<Contact> Contacts { get; private set; } = new List<Contact>();

    public static List<Error> Errors { get; private set; } = null!;

    public static ErrorOr<Person> Create(string name, string nickname, string notes)
    {
        Errors = new List<Error>();

        ValidateNameLength(name);
        ValidateNicknameLength(nickname);
        ValidateNotesLength(notes);

        if(Errors.Count > 0)
        {
            return Errors;
        }

        var person = new Person(name, nickname, notes);

        return person;
    }

    public ErrorOr<Person> Update(string name, string nickname, string notes)
    {
        Errors = new List<Error>();

        ValidateNameLength(name);
        ValidateNicknameLength(nickname);
        ValidateNotesLength(notes);

        if(Errors.Count > 0)
        {
            return Errors;
        }

        Name = name;
        Nickname = nickname;
        Notes = notes;
        LastModified = DateTime.UtcNow;

        return this;
    }
}
