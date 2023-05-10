using ContactEase.Application.Services;
using ContactEase.Contracts.ContactContracts;
using ContactEase.Contracts.PersonContracts;
using ContactEase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactEase.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ApiController
{
    private readonly IPersonServices _personService;
    private readonly IContactService _contactService;

    public PersonsController(
        IPersonServices personService,
        IContactService contactService)
    {
        _personService = personService;
        _contactService = contactService;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreatePerson([FromBody] CreatePersonRequest request)
    {
        var result = _personService.Create(
            request.Name,
            request.Nickname,
            request.Notes);

        return result.Match(created => CreatedAtGetPerson(result.Value), Problem);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllPerson()
    {
        var result = _personService.GetAll();

        return Ok(result.Select(MapPersonResponse));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPerson([FromRoute] Guid id)
    {
        var result = _personService.Get(id);

        return result.Match(created => Ok(MapPersonResponse(result.Value)), Problem);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePerson([FromRoute] Guid id, [FromBody] UpdatePersonRequest request)
    {
        var result = _personService.Update(
            id,
            request.Name,
            request.Nickname,
            request.Notes);

        return result.Match(created => Ok(MapPersonResponse(result.Value)), Problem);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeletePerson([FromRoute] Guid id)
    {
        var result = _personService.Delete(id);

        return result.Match(deleted => NoContent(), Problem);
    }

    [HttpPost("{id:guid}/Contacts")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateContact([FromRoute] Guid id, [FromBody] CreateContactRequest request)
    {
        var result = _contactService.Create(
            id,
            request.Type,
            request.Value);

        return result.Match(created => CreatedAtGetContact(result.Value), Problem);
    }

    [HttpGet("{id:guid}/Contacts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllContact([FromRoute] Guid id)
    {
        var result = _contactService.GetAll(id);

        return Ok(result.Select(MapContactResponse));
    }

    [HttpGet("{id:guid}/Contacts/{contactId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetContact([FromRoute] Guid contactId)
    {
        var result = _contactService.Get(contactId);

        return result.Match(created => Ok(MapContactResponse(result.Value)), Problem);
    }

    [HttpPut("{id:guid}/Contacts/{contactId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateContact([FromRoute] Guid contactId, [FromBody] UpdateContactRequest request)
    {
        var result = _contactService.Update(
            contactId,
            request.Type,
            request.Value);

        return result.Match(created => Ok(MapContactResponse(result.Value)), Problem);
    }

    [HttpDelete("{id:guid}/Contacts/{contactId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteContact([FromRoute] Guid contactId)
    {
        var result = _contactService.Delete(contactId);

        return result.Match(deleted => NoContent(), Problem);
    }

    private CreatedAtActionResult CreatedAtGetPerson(Person person)
    {
        return CreatedAtAction(
            actionName: nameof(GetPerson),
            routeValues: new { id = person.Id },
            value: MapPersonResponse(person));
    }

    private CreatedAtActionResult CreatedAtGetContact(Contact contact)
    {
        return CreatedAtAction(
            actionName: nameof(GetContact),
            routeValues: new { id = contact.Id },
            value: MapContactResponse(contact));
    }

    private static PersonResponse MapPersonResponse(Person person)
    {
        return new PersonResponse(
            person.Id,
            person.Name,
            person.Nickname,
            person.Notes);
    }

    private static ContactResponse MapContactResponse(Contact contact)
    {
        return new ContactResponse(
            contact.Id,
            contact.PersonId,
            contact.Type,
            contact.Value);
    }
}
