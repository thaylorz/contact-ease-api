using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public interface IContactService
{
    Task<ErrorOr<Contact>> Create(Guid personId, string type, string value);
    Task<IEnumerable<Contact>> GetAll(Guid personId);
    Task<ErrorOr<Contact>> Get(Guid id);
    Task<ErrorOr<Contact>> Update(Guid id, string type, string value);
    Task<ErrorOr<Deleted>> Delete(Guid id);
}
