using ContactEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactEase.Infrastructure.Persistence.Configurations;

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .IsRequired();

        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(Contact.MaxTypeLength);

        builder
            .Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(Contact.MaxValueLengh);

        builder
            .HasOne(x => x.Person)
            .WithMany(x => x.Contacts)
            .HasForeignKey(x => x.PersonId);
    }
}
