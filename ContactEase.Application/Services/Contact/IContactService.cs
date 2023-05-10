using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public interface IContactService
{
    ErrorOr<Contact> Create(Guid personId, string type, string value);
    IEnumerable<Contact> GetAll(Guid personId);
    ErrorOr<Contact> Get(Guid id);
    ErrorOr<Contact> Update(Guid id, string type, string value);
    ErrorOr<Deleted> Delete(Guid id);
}
