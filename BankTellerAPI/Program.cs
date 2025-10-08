using BankTellerAPI.Api;
using BankTellerAPI.Infrastructure.Context;
using BankTellerAPI.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = Constants.ApiTitle,
            Version = Constants.ApiVersion,
            Description = Constants.ApiDescription
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference("/docs", options =>
{
    options.Title = Constants.ApiTitle;
    options.Theme = ScalarTheme.Kepler;
});

await app.RunAsync();
