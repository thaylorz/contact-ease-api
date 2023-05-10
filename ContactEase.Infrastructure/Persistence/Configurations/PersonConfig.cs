using ContactEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactEase.Infrastructure.Persistence.Configurations;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Person.MaxNotesLengh);

        builder
            .Property(x => x.Nickname)
            .IsRequired()
            .HasMaxLength(Person.MaxNicknameLengh);

        builder
            .Property(x => x.Notes)
            .IsRequired()
            .HasMaxLength(Person.MaxNotesLengh);

        builder
            .HasMany(x => x.Contacts)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
