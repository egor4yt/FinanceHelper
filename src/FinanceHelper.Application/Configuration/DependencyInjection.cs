using System.Reflection;
using FinanceHelper.Application.Behaviours;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Application.Services.Localization;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceHelper.Application.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigureApplication(this IHostApplicationBuilder app)
    {
        var assembly = Assembly.GetExecutingAssembly();

        app.Services.AddValidatorsFromAssembly(assembly);
        app.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(assembly); });
        app.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        app.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        app.Services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
        app.Services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

        return app;
    }
}