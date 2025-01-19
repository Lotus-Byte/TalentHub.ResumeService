
namespace ResumeDataAccess.Contracts.Query;

public sealed record SearchResumeQueryRequest
{
    /// <summary>
    ///  Наименование вакансии
    /// </summary>
    public required string Name { get; init; }

    public long? MinSalary { get; init; }

    public long? MaxSalary { get; init; }
}
