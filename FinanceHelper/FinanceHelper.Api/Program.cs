using FinanceHelper.Api.Configuration;
using FinanceHelper.Api.Services;
using FinanceHelper.Application.Configuration;
using FinanceHelper.Persistence.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureApi();
builder.ConfigurePersistence();
builder.ConfigureApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRequestLocalization();

app.UseInitializeDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = HealthCheckService.WriterHealthCheckResponse,
    AllowCachingResponses = false
});

app.MapControllers();
app.Run();