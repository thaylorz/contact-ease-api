namespace ContactEase.Contracts.ContactContracts;

public record ContactResponse(
    Guid Id,
    Guid PersonId,
    string Type,
    string Value);
