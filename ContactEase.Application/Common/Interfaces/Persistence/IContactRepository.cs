using ContactEase.Domain.Entities;

namespace ContactEase.Application.Common.Interfaces.Persistence;

public interface IContactRepository : IBaseRepository<Contact>
{
    Task<IEnumerable<Contact>> GetAllPersonContacts(Guid personId);
}
