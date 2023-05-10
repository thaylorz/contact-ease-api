using ContactEase.Application.Common.Interfaces.Persistence;
using ContactEase.Domain.Common.Errors;
using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public class PersonService : IPersonServices
{
    private readonly IPersonRepository _personRepository;
    private readonly CancellationToken _cancellationToken;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
        _cancellationToken = new CancellationToken();
    }

    public ErrorOr<Person> Create(string name, string nickname, string notes)
    {
        var result = Person.Create(name, nickname, notes);
        var person = result.Value;

        if(result.IsError)
        {
            return result;
        }

        _personRepository.Add(person);
        _personRepository.Save(_cancellationToken);

        return result;
    }

    public IEnumerable<Person> GetAll()
    {
        var persons = _personRepository.GetAll().Result;
        return persons;
    }

    public ErrorOr<Person> Get(Guid id)
    {
        var person = _personRepository.GetById(id).Result;

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        return person;
    }

    public ErrorOr<Person> Update(Guid id, string name, string nickname, string notes)
    {
        var person = _personRepository.GetById(id).Result;

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        person.Update(name, nickname, notes);

        _personRepository.Edit(person);
        _personRepository.Save(_cancellationToken);

        return person;
    }

    public ErrorOr<Deleted> Delete(Guid id)
    {
        var person = _personRepository.GetById(id).Result;

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        _personRepository.Delete(person);
        _personRepository.Save(_cancellationToken);

        return Result.Deleted;
    }
}
