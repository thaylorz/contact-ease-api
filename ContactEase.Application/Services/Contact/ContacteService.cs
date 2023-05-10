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

    public ErrorOr<Contact> Create(Guid personId, string type, string value)
    {
        var person = _personRepository.GetById(personId).Result;

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

        _contactRepository.Add(contact);
        _contactRepository.Save(_cancellationToken);

        return result;
    }

    public IEnumerable<Contact> GetAll(Guid personId)
    {
        var contacts = _contactRepository.GetAllPersonContacts(personId).Result;

        return contacts;
    }

    public ErrorOr<Contact> Get(Guid id)
    {
        var contact = _contactRepository.GetById(id).Result;

        if(contact is null)
        {
            return ContactErrors.NotFound;
        }

        return contact;
    }

    public ErrorOr<Contact> Update(Guid id, string type, string value)
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
        _contactRepository.Save(_cancellationToken);

        return contact;
    }

    public ErrorOr<Deleted> Delete(Guid id)
    {
        var contact = _contactRepository.GetById(id).Result;

        if(contact is null)
        {
            return ContactErrors.NotFound;
        }

        _contactRepository.Delete(contact);
        _contactRepository.Save(_cancellationToken);

        return Result.Deleted;
    }
}
