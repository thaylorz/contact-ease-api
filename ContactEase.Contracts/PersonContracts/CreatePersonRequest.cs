namespace ContactEase.Contracts.PersonContracts;

public record CreatePersonRequest(
    string Name,
    string Nickname,
    string Notes);
