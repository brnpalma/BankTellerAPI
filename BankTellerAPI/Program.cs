using BankTellerAPI.Api;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference("/docs", options =>
{
    options.Title = Constants.ApiTitle;
    options.Theme = ScalarTheme.Kepler;
});

await app.RunAsync();
