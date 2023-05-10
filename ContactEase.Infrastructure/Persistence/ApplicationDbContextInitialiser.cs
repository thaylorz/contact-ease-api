using ContactEase.Domain.Entities;

namespace ContactEase.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if(!_context.Person.Any())
        {
            var newPerson = Person.Create(name: "Bruno Oliveira", nickname: "Oliveira", notes: "Nota padrão para a pessoa Bruno Oliveira").Value;

            _context.Person.Add(newPerson);

            var contacts = new List<Contact> {
                Contact.Create(personId: newPerson.Id, type: "Telefone", value: "48991225879").Value,
                Contact.Create(personId: newPerson.Id, type: "Email", value: "brunooliveira@email.com.br").Value,
                Contact.Create(personId: newPerson.Id, type: "Whats", value: "+5548991225879").Value,
            };

            _context.Contact.AddRange(contacts);

            await _context.SaveChangesAsync();
        }
    }
}
