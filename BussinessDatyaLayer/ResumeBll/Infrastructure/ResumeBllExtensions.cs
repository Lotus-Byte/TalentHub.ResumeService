using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ResumeBll.Contracts.Interface;
using ResumeBll.Service;

using ResumeDataAccess.Infrastructure;

namespace ResumeBll.Infrastructure;

public static class ResumeBllExtensions
{
    public static IServiceCollection AddResumeBllService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddResumeDataAccessContext(configuration);

        services
            .AddScoped<IResume, ResumeService>();

        return services;
    }
}
