using ContactEase.Application.Common.Interfaces.Persistence;
using ContactEase.Domain.Entities;
using ContactEase.Infrastructure.Persistence;
using ContactEase.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactEase.Infrastructure.Persistence.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllPersonContacts(Guid personId)
    {
        return await _context
            .Set<Contact>()
            .Where(x => x.PersonId == personId)
            .ToListAsync();
    }
}
