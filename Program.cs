using Harmonix.Shared.Data;
using Harmonix.Shared.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<HarmonixDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HarmonixDb")));

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
    .AsSelf()
    .WithScopedLifetime());

builder.Services.AddScoped<PasswordHasher>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
