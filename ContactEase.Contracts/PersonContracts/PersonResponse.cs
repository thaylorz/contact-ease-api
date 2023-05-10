namespace ContactEase.Contracts.PersonContracts;

public record PersonResponse(
    Guid Id,
    string Name,
    string Nickname,
    string Notes);
