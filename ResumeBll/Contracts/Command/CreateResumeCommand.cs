

namespace ResumeBll.Contracts.Command;

public sealed record CreateResumeCommand
{
    public string Name { get; init; }

    public DateOnly BirthDate { get; init; }

    public long Salary { get; set; }

    public string City { get; set; } = null!;

    /// <summary>
    /// Опыт работы
    /// </summary>
    public string Expertise { get; set; } = null!;

    public string Skills { get; set; } = null!;

    public Guid CreateUserId { get; set; }
}
