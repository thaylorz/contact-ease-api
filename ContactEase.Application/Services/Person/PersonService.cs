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

    public async Task<ErrorOr<Person>> Create(string name, string nickname, string notes)
    {
        var result = Person.Create(name, nickname, notes);
        var person = result.Value;

        if(result.IsError)
        {
            return result;
        }

        await _personRepository.Add(person);
        await _personRepository.Save(_cancellationToken);

        return result;
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        var persons = await _personRepository.GetAll();
        return persons;
    }

    public async Task<ErrorOr<Person>> Get(Guid id)
    {
        var person = await _personRepository.GetById(id);

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        return person;
    }

    public async Task<ErrorOr<Person>> Update(Guid id, string name, string nickname, string notes)
    {
        var person = await _personRepository.GetById(id);

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        var result = person.Update(name, nickname, notes);

        if(result.IsError)
        {
            return result;
        }

        _personRepository.Edit(person);

        await _personRepository.Save(_cancellationToken);

        return person;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        var person = await _personRepository.GetById(id);

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        await _personRepository.Delete(person);
        await _personRepository.Save(_cancellationToken);

        return Result.Deleted;
    }
}
