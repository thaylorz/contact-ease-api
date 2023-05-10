using ContactEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ContactEase.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Person> Person { get; set; }
    public DbSet<Contact> Contact { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
