using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Entities = ResumeDataAccess.Entities;
using ResumeDataAccess.Internal;


namespace ResumeDataAccess.Fluents;

internal sealed class Resume : IEntityTypeConfiguration<Entities.Resume>
{
    public void Configure(EntityTypeBuilder<Entities.Resume> builder)
    {
        builder
            .ToTable("job");

        builder
             .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
             .Property(b => b.ObjectId)
             .IsRequired()
             // почему-то не находит функцию в схеме по умолчанию. Поэтому сделаю пока так
             .HasDefaultValueSql($"{DbConfig.SCHEMA_NAME}.uuid_generate_v4()");

        builder
             .Property(b => b.Name)
             .HasMaxLength(300)
             .IsRequired();

        builder
             .Property(b => b.Expertise)
             .HasMaxLength(1000)
             .IsRequired();

        builder
             .Property(b => b.Skills)
             .HasMaxLength(1000)
             .IsRequired();

        builder
             .Property(b => b.Deleted)
             .IsRequired()
             .HasDefaultValue(false);

        builder
             .Property(b => b.Created)
             .IsRequired()
             .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
             .Property(b => b.CreateUserId)
             .IsRequired();

        // Indexes

        builder
            .HasIndex(b => b.ObjectId)
            .IsUnique();

        builder
            .HasIndex(b => b.Name);

        builder
            .HasIndex(b => b.Created);

        builder
            .HasIndex(b => b.CreateUserId);

    }
}
