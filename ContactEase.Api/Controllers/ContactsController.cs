using ContactEase.Application.Services;
using ContactEase.Contracts.ContactContracts;
using ContactEase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactEase.Api.Controllers
{
    [ApiController]
    [Route("persons/{personId}/[controller]")]
    public class ContactsController : ApiController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{contactId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetContact([FromRoute] Guid contactId)
        {
            var result = await _contactService.Get(contactId);

            return result.Match(created => Ok(MapContactResponse(result.Value)), Problem);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateContact([FromRoute] Guid personId, [FromBody] CreateContactRequest request)
        {
            var result = await _contactService.Create(personId, request.Type, request.Value);

            return result.Match(created => CreatedAtGetContact(result.Value), Problem);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllContact([FromRoute] Guid personId)
        {
            var result = await _contactService.GetAll(personId);

            return Ok(result.Select(MapContactResponse));
        }

        [HttpPut("{contactId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid contactId, [FromBody] UpdateContactRequest request)
        {
            var result = await _contactService.Update(contactId, request.Type, request.Value);

            return result.Match(created => Ok(MapContactResponse(result.Value)), Problem);
        }

        [HttpDelete("{contactId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid contactId)
        {
            var result = await _contactService.Delete(contactId);

            return result.Match(deleted => NoContent(), Problem);
        }

        private CreatedAtActionResult CreatedAtGetContact(Contact contact)
        {
            return CreatedAtAction(
                actionName: nameof(GetContact),
                routeValues: new { personId = contact.PersonId, contactId = contact.Id },
                value: MapContactResponse(contact));
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
}
