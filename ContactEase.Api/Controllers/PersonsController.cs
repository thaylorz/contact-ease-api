using ContactEase.Application.Services;
using ContactEase.Contracts.PersonContracts;
using ContactEase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactEase.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ApiController
{
    private readonly IPersonServices _personService;

    public PersonsController(IPersonServices personService)
    {
        _personService = personService;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
    {
        var result = await _personService.Create(request.Name, request.Nickname, request.Notes);

        return result.Match(created => CreatedAtGetPerson(result.Value), Problem);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllPerson()
    {
        var result = await _personService.GetAll();

        return Ok(result.Select(MapPersonResponse));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPerson([FromRoute] Guid id)
    {
        var result = await _personService.Get(id);

        return result.Match(created => Ok(MapPersonResponse(result.Value)), Problem);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePerson([FromRoute] Guid id, [FromBody] UpdatePersonRequest request)
    {
        var result = await _personService.Update(id, request.Name, request.Nickname, request.Notes);

        return result.Match(created => Ok(MapPersonResponse(result.Value)), Problem);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
    {
        var result = await _personService.Delete(id);

        return result.Match(deleted => NoContent(), Problem);
    }

    private CreatedAtActionResult CreatedAtGetPerson(Person person)
    {
        return CreatedAtAction(
            actionName: nameof(GetPerson),
            routeValues: new { id = person.Id },
            value: MapPersonResponse(person));
    }

    private static PersonResponse MapPersonResponse(Person person)
    {
        return new PersonResponse(
            person.Id,
            person.Name,
            person.Nickname,
            person.Notes);
    }
}
