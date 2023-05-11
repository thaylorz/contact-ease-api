using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public interface IPersonServices
{
    Task<ErrorOr<Person>> Create(string name, string nickname, string notes);
    Task<IEnumerable<Person>> GetAll();
    Task<ErrorOr<Person>> Get(Guid id);
    Task<ErrorOr<Person>> Update(Guid id, string name, string nickname, string notes);
    Task<ErrorOr<Deleted>> Delete(Guid id);
}
