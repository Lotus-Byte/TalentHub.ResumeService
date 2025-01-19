using ResumeDataAccess.Contracts.Query;
using ResumeDataAccess.Entities;


namespace ResumeDataAccess.Contracts.Repository;

public interface IResumeRepository : IRepository<Resume, long>
{
    IQueryable<Resume> SearchResumesQuery(SearchResumeQueryRequest request);

    Task<Resume?> GetAsync(Guid objectId, CancellationToken cancellationToken);
}
