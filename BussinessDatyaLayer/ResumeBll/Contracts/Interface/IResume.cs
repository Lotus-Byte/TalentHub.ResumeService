using ResumeBll.Contracts.Command;
using ResumeBll.Contracts.Dto;

namespace ResumeBll.Contracts.Interface;

public interface IResume
{
    Task<ResumeDataDto> GetResumeDataAsync(int id, CancellationToken cancellationToken = default);

    Task<CreateResumeDto> CreateResumeAsync(CreateResumeCommand command, CancellationToken cancellationToken = default);

    Task UpdateAsync(CancellationToken cancellationToken = default);

    Task RemoveResumeAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ResumeDataDto>> SearchAsync(SearchCommand command, CancellationToken cancellationToken = default);
}
