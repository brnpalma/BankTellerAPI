using BankTeller.API;
using BankTeller.Infrastructure.DependencyInjection;
using BankTeller.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = Constantes.ApiTitle,
            Version = Constantes.ApiVersion,
            Description = Constantes.ApiDescription
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BancoContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference("/docs", options =>
{
    options.Title = Constantes.ApiTitle;
    options.Theme = ScalarTheme.Kepler;
});

await app.RunAsync();
