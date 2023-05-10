namespace ContactEase.Contracts.PersonContracts;

public record UpdatePersonRequest(
    string Name,
    string Nickname,
    string Notes);
