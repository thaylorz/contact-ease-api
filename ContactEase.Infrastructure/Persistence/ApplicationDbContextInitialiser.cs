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
            List<Person> people = new()
            {
                Person.Create(name: "Alice Garcia", nickname: "Alicia", notes: "Nota padrão para a pessoa Alice Garcia").Value,
                Person.Create(name: "Bianca Santos", nickname: "Bia", notes: "Nota padrão para a pessoa Bianca Santos").Value,
                Person.Create(name: "Caio Ribeiro", nickname: "Caião", notes: "Nota padrão para a pessoa Caio Ribeiro").Value,
                Person.Create(name: "Daniel Oliveira", nickname: "Dan", notes: "Nota padrão para a pessoa Daniel Oliveira").Value,
                Person.Create(name: "Eduarda Pereira", nickname: "Duda", notes: "Nota padrão para a pessoa Eduarda Pereira").Value,
                Person.Create(name: "Fernanda Alves", nickname: "Fer", notes: "Nota padrão para a pessoa Fernanda Alves").Value,
                Person.Create(name: "Gabriel Martins", nickname: "Gabi", notes: "Nota padrão para a pessoa Gabriel Martins").Value,
                Person.Create(name: "Helena Souza", nickname: "Lena", notes: "Nota padrão para a pessoa Helena Souza").Value,
                Person.Create(name: "Isabel Silva", nickname: "Bel", notes: "Nota padrão para a pessoa Isabel Silva").Value,
                Person.Create(name: "João Oliveira", nickname: "Joãozinho", notes: "Nota padrão para a pessoa João Oliveira").Value,
                Person.Create(name: "Karina Rocha", nickname: "Kari", notes: "Nota padrão para a pessoa Karina Rocha").Value,
                Person.Create(name: "Luana Barbosa", nickname: "Lua", notes: "Nota padrão para a pessoa Luana Barbosa").Value,
                Person.Create(name: "Matheus Castro", nickname: "Matheus", notes: "Nota padrão para a pessoa Matheus Castro").Value,
                Person.Create(name: "Natália Ferreira", nickname: "Nati", notes: "Nota padrão para a pessoa Natália Ferreira").Value,
                Person.Create(name: "Oliver Santos", nickname: "Oliver", notes: "Nota padrão para a pessoa Oliver Santos").Value,
                Person.Create(name: "Paulo Oliveira", nickname: "Paulinho", notes: "Nota padrão para a pessoa Paulo Oliveira").Value,
                Person.Create(name: "Rafaela Costa", nickname: "Rafa", notes: "Nota padrão para a pessoa Rafaela Costa").Value,
                Person.Create(name: "Samantha Vieira", nickname: "Sam", notes: "Nota padrão para a pessoa Samantha Vieira").Value,
                Person.Create(name: "Thiago Almeida", nickname: "Thi", notes: "Nota padrão para a pessoa Thiago Almeida").Value,
                Person.Create(name: "Ulisses Ribeiro", nickname: "Ully", notes: "Nota padrão para a pessoa Ulisses Ribeiro").Value,
                Person.Create(name: "Victor Pereira", nickname: "Vitinho", notes: "Nota padrão para a pessoa Victor Pereira").Value,
                Person.Create(name: "William Costa", nickname: "Will", notes: "Nota padrão para a pessoa William Costa").Value,
                Person.Create(name: "Yasmin Fernandes", nickname: "Yas", notes: "Nota padrão para a pessoa Yasmin Fernandes").Value,
            };

            await _context.Person.AddRangeAsync(people);

            var contacts = people.SelectMany(person => new List<Contact>
            {
                Contact.Create(personId: person.Id, type: "Telefone", value: $"48991225879").Value,
                Contact.Create(personId: person.Id, type: "E-mail", value: $"{person.Name}@exemplo.com").Value,
                Contact.Create(personId: person.Id, type: "Endereço", value: $"Rua Exemplo, 123").Value,
                Contact.Create(personId: person.Id, type: "LinkedIn", value: $"linkedin.com/in/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Facebook", value: $"facebook.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Instagram", value: $"instagram.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Skype", value: $"{person.Name}.skype").Value,
                Contact.Create(personId: person.Id, type: "Telegram", value: $"@{person.Name}_telegram").Value,
                Contact.Create(personId: person.Id, type: "Whatsapp", value: $"+5511999999999").Value,
                Contact.Create(personId: person.Id, type: "Twitter", value: $"@{person.Name}_twitter").Value,
                Contact.Create(personId: person.Id, type: "Discord", value: $"{person.Name}#1234").Value,
                Contact.Create(personId: person.Id, type: "Twitch", value: $"twitch.tv/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Reddit", value: $"reddit.com/user/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Github", value: $"github.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Stack Overflow", value: $"stackoverflow.com/users/1234567/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Discord", value: $"{person.Name}#1234").Value,
                Contact.Create(personId: person.Id, type: "Medium", value: $"medium.com/@{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Quora", value: $"quora.com/profile/{person.Name}-Oliveira").Value,
                Contact.Create(personId: person.Id, type: "AngelList", value: $"angel.co/u/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Dribbble", value: $"dribbble.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Behance", value: $"behance.net/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "YouTube", value: $"youtube.com/user/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Vimeo", value: $"vimeo.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Pinterest", value: $"pinterest.com/{person.Name}").Value,
                Contact.Create(personId: person.Id, type: "Snapchat", value: $"snapchat.com/add/{person.Name}").Value,
            });

            _context.Contact.AddRange(contacts);

            await _context.SaveChangesAsync();
        }
    }
}
