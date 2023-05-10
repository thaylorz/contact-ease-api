using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Domain.Common.Errors;

public static class ContactErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Contact.NotFound",
        description: "Contato não foi encontrado.");

    public static Error InvalidType => Error.Validation(
        code: "Contact.InvalidType",
        description: $"O tipo do contato deve ter no mínimo {Contact.MinTypeLength} caracteres e no máximo {Contact.MaxTypeLength} caracteres.");

    public static Error InvalidValue => Error.Validation(
        code: "Contact.InvalidValue",
        description: $"O contato deve ter no mínimo {Contact.MinValueLengh} caracteres e no máximo {Contact.MaxValueLengh} caracteres.");
}
