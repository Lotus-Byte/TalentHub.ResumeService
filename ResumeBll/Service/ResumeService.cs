using Microsoft.EntityFrameworkCore;

using ResumeBll.Contracts.Command;
using ResumeBll.Contracts.Dto;
using ResumeBll.Contracts.Interface;

using ResumeDataAccess.Contracts.Repository;
using ResumeDataAccess.Entities;
using ResumeDataAccess.Contracts.Query;


namespace ResumeBll.Service;

internal sealed class ResumeService : IResume
{
    private readonly IResumeRepository _resumeRepository;

    public ResumeService(IResumeRepository resumeRepository) =>
        _resumeRepository = resumeRepository;

    public async Task<ResumeDataDto> GetResumeDataAsync(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Нужны только неудаленные вакансии
        var entity = await _resumeRepository.GetAsync(id, cancellationToken);

        if (entity == null)
        {
            // TODO: need specialized exception
            throw new InvalidOperationException($"Resume {id} not found");
        }

        var ret = new ResumeDataDto
        {
            Id = entity.Id,
            ObjectId = entity.ObjectId,
            Name = entity.Name,
            Salary = entity.Salary,
            BirthDate = entity.BirthDate,
            City = entity.City,
            Expertise = entity.Expertise,
            Skills = entity.Skills,
            Created = entity.Created,
            CreateUserId = entity.CreateUserId,
        };

        return ret;
    }

    public async Task<CreateResumeDto> CreateResumeAsync(CreateResumeCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new Resume
        {
            Name = command.Name,
            BirthDate = command.BirthDate,
            Salary = command.Salary,
            City = command.City,
            Expertise = command.Expertise,
            Skills = command.Skills,
            Created = DateTime.UtcNow,
            CreateUserId = command.CreateUserId
        };

        var createResult = await _resumeRepository.AddAsync(entity, cancellationToken);
        await _resumeRepository.SaveChangesAsync(cancellationToken);

        var ret = new CreateResumeDto
        {
            Id = createResult.Id,
            ObjectId = createResult.ObjectId,
        };
        return ret;
    }

    public Task UpdateAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveResumeAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _resumeRepository.GetAsync(id, cancellationToken);
        var deleted = _resumeRepository.Delete(entity);

        if (deleted)
        {
            await _resumeRepository.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IReadOnlyCollection<ResumeDataDto>> SearchAsync(SearchCommand command, CancellationToken cancellationToken = default)
    {
        var request = new SearchResumeQueryRequest
        {
            Name = command.SearchText,
            // TODO: city
            // City = command.City,
            MinSalary = command.MinSalary,
            MaxSalary = command.MaxSalary,
        };

        var resumesQuery = _resumeRepository.SearchResumesQuery(request)
            .Select(resume => new ResumeDataDto
            {
                Id = resume.Id,
                ObjectId = resume.ObjectId,
                Name = resume.Name,
                Salary = resume.Salary,
                BirthDate = resume.BirthDate,
                City = resume.City,
                Expertise = resume.Expertise,
                Skills = resume.Skills,
                Created = resume.Created,
                CreateUserId = resume.CreateUserId,
            });

        var ret = await resumesQuery.ToArrayAsync(cancellationToken);

        return ret;
    }
}
