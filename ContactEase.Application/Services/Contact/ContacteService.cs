using ContactEase.Application.Common.Interfaces.Persistence;
using ContactEase.Domain.Common.Errors;
using ContactEase.Domain.Entities;
using ErrorOr;

namespace ContactEase.Application.Services;

public class ContacteService : IContactService
{
    private readonly IPersonRepository _personRepository;
    private readonly IContactRepository _contactRepository;
    private readonly CancellationToken _cancellationToken;

    public ContacteService(
        IPersonRepository personRepository,
        IContactRepository contactRepository)
    {
        _personRepository = personRepository;
        _contactRepository = contactRepository;
        _cancellationToken = new CancellationToken();
    }

    public async Task<ErrorOr<Contact>> Create(Guid personId, string type, string value)
    {
        var person = await _personRepository.GetById(personId);

        if(person is null)
        {
            return PersonErrors.NotFound;
        }

        var result = Contact.Create(person.Id, type, value);
        var contact = result.Value;

        if(result.IsError)
        {
            return result;
        }

        await _contactRepository.Add(contact);
        await _contactRepository.Save(_cancellationToken);

        return result;
    }

    public async Task<IEnumerable<Contact>> GetAll(Guid personId)
    {
        var contacts = await _contactRepository.GetAllPersonContacts(personId);

        return contacts;
    }

    public async Task<ErrorOr<Contact>> Get(Guid id)
    {
        var contact = await _contactRepository.GetById(id);

        if(contact is null)
        {
            return ContactErrors.NotFound;
        }

        return contact;
    }

    public async Task<ErrorOr<Contact>> Update(Guid id, string type, string value)
    {
        var contact = _contactRepository.GetById(id).Result;

        if(contact is null)
        {
            return ContactErrors.NotFound;
        }

        var result = contact.Update(type, value);

        if(result.IsError)
        {
            return result;
        }

        _contactRepository.Edit(contact);

        await _contactRepository.Save(_cancellationToken);

        return contact;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        var contact = await _contactRepository.GetById(id);

        if(contact is null)
        {
            return ContactErrors.NotFound;
        }

        await _contactRepository.Delete(contact);
        await _contactRepository.Save(_cancellationToken);

        return Result.Deleted;
    }
}
