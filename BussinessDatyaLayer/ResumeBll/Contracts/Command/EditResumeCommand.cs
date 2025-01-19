namespace ResumeBll.Contracts.Command;

public sealed class EditResumeCommand
{
    /// <summary>
    /// Идентификатор вакансии
    /// </summary>
    public long Id { get; set; }
}
