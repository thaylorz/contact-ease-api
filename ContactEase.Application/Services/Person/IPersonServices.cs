using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public interface IPersonServices
{
    ErrorOr<Person> Create(string name, string nickname, string notes);
    IEnumerable<Person> GetAll();
    ErrorOr<Person> Get(Guid id);
    ErrorOr<Person> Update(Guid id, string name, string nickname, string notes);
    ErrorOr<Deleted> Delete(Guid id);
}
