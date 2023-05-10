using ContactEase.Domain.Common.Errors;

namespace ContactEase.Domain.Entities
{
    public partial class Person
    {
        public static void ValidateNameLength(string name)
        {
            if(name.Length is < MinNameLength or > MaxNameLength)
            {
                Errors.Add(PersonErrors.InvalidName);
            }
        }

        public static void ValidateNicknameLength(string nickname)
        {
            if(nickname.Length is < MinNicknameLengh or > MaxNameLength)
            {
                Errors.Add(PersonErrors.InvalidNickname);
            }
        }

        public static void ValidateNotesLength(string notes)
        {
            if(notes.Length is > MaxNotesLengh)
            {
                Errors.Add(PersonErrors.InvalidNotes);
            }
        }
    }
}
