using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ToDoListBk.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion.Default;
            options.ReportApiVersions = true;

        });
    }
}
