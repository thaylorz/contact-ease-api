using ContactEase.Application.Common.Interfaces.Persistence;
using ContactEase.Domain.Entities;

namespace ContactEase.Infrastructure.Persistence.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }
}
