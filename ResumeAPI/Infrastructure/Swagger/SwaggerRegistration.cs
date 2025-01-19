using Microsoft.OpenApi.Models;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;


namespace ResumeAPI.Infrastructure.Swagger;

public static class SwaggerRegistration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API для работы с резюме",
                Description = "API для работы с резюме",
                Contact = new OpenApiContact
                {
                    Name = "Andrei Baksheev",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}
