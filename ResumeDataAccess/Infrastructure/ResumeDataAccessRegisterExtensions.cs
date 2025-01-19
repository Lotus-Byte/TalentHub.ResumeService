using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ResumeDataAccess.Context;
using ResumeDataAccess.Contracts.Repository;
using ResumeDataAccess.Internal;
using ResumeDataAccess.Repository;

namespace ResumeDataAccess.Infrastructure;

public static class ResumeDataAccessRegisterExtensions
{
    public static IServiceCollection AddResumeDataAccessContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var connectionStringSection = configuration.GetConnectionString(DbConfig.CONNECTION_STRING_KEY);
        services
            .AddDbContext<ResumeDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionStringSection)
                    .UseSnakeCaseNamingConvention();
            });

        services
            .AddTransient<IResumeRepository, ResumeRepository>();

        return services;
    }
}