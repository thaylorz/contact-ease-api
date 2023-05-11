using ContactEase.Application;
using ContactEase.Infrastructure;
using ContactEase.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
}

var app = builder.Build();
{
    if(app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.SeedAsync();
    }

    app.UseCors(builder => builder
        .WithOrigins(
            "http://localhost:3000", 
            "https://localhost:3000",
            "https://contact-ease-web-thaylorz.vercel.app/")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
