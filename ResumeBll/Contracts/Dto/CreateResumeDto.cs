
namespace ResumeBll.Contracts.Dto;

public sealed record CreateResumeDto
{
    public long Id { get; init; }

    public Guid ObjectId { get; init; }
}
