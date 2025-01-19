using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumeDataAccess.Fluents;

/*internal sealed class ContactData : IEntityTypeConfiguration<Entities.ContactData>
{
    public void Configure(EntityTypeBuilder<Entities.ContactData> builder)
    {
        builder
            .ToTable("contact_data");

        builder
             .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
             .Property(b => b.Value)
             .HasMaxLength(256)
             .IsRequired();
    }
}*/
