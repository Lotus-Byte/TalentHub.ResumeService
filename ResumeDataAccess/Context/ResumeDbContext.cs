using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Npgsql;

using ResumeDataAccess.Entities;
using ResumeDataAccess.Internal;

namespace ResumeDataAccess.Context;

public class ResumeDbContext : DbContext
{
    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<ContactData> Contacts { get; set; }

    //static ResumeDbContext()
    //    => NpgsqlConnection.GlobalTypeMapper.MapEnum<ContactType>();

    public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(DbConfig.CONNECTION_STRING_KEY);

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.MapEnum<ContactType>();
            var dataSource = dataSourceBuilder.Build();

            optionsBuilder
                .UseNpgsql(dataSource)
                .UseSnakeCaseNamingConvention();
        }
    }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConfig.SCHEMA_NAME);

        modelBuilder.UseIdentityByDefaultColumns();

        modelBuilder.HasPostgresExtension(DbConfig.SCHEMA_NAME, "uuid-ossp");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResumeDbContext).Assembly);

        modelBuilder.HasPostgresEnum<ContactType>();
    }
    #endregion
}
