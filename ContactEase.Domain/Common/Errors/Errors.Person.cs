using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Domain.Common.Errors;

public static class PersonErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Person.NotFound",
        description: "Pessoa não foi encontrada.");

    public static Error InvalidName => Error.Validation(
        code: "Person.InvalidName",
        description: $"O nome da pessoa deve ter no mínimo {Person.MinNameLength} caracteres e no máximo {Entities.Person.MaxNameLength} caracteres.");

    public static Error InvalidNickname => Error.Validation(
        code: "Person.InvalidNickname",
        description: $"O apelido da pessoa deve ter no mínimo {Person.MinNameLength} caracteres e no máximo {Entities.Person.MaxNicknameLengh} caracteres.");

    public static Error InvalidNotes => Error.Validation(
        code: "Person.InvalidNotes",
        description: $"As notas da pessoa deve ter no máximo {Person.MaxNotesLengh} caracteres.");
}
