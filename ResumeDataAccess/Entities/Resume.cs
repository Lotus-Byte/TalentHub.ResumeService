
namespace ResumeDataAccess.Entities;

public sealed class Resume : IAggregateRoot<long>
{
    public long Id { get; set; }

    public Guid ObjectId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public long Salary { get; set; }

    public string City { get; set; } = null!;

    /// <summary>
    /// Опыт работы
    /// </summary>
    public string Expertise { get; set; } = null!;

    public string Skills { get; set; } = null!;

    public bool Deleted { get; set; }

    public DateTime Created { get; set; }

    public Guid CreateUserId { get; set; }

    public ICollection<ContactData> Contacts { get; set; } = new List<ContactData>();
}
