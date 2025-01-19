

using Microsoft.EntityFrameworkCore;

using System.Text.RegularExpressions;

using ResumeDataAccess.Context;
using ResumeDataAccess.Contracts.Query;
using ResumeDataAccess.Contracts.Repository;
using ResumeDataAccess.Entities;

namespace ResumeDataAccess.Repository;

internal sealed class ResumeRepository : Repository<Resume, long>, IResumeRepository
{
    public ResumeRepository(ResumeDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сущность по ObjectId.
    /// </summary>
    /// <param name="objectId"> Бизнес-ключ сущности. </param>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Cущность. </returns>
    public async Task<Resume?> GetAsync(Guid objectId, CancellationToken cancellationToken)
    {
        return await Context.Set<Resume>()
            .Where(j => j.ObjectId == objectId)
            .Where(j => j.Deleted == false)
            .FirstOrDefaultAsync(cancellationToken);
    }


    /// <summary>
    /// Пометить сущность удаленной.
    /// </summary>
    /// <param name="entity"> Сущность для удаления. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public override bool Delete(Resume? entity)
    {
        if (entity == null)
        {
            return false;
        }

        entity.Deleted = true;

        return true;
    }

    public IQueryable<Resume> SearchResumesQuery(SearchResumeQueryRequest request)
    {
        var commonSearchQuery = Context.Set<Resume>()
            .Where(r => Regex.IsMatch(r.Name, request.Name, RegexOptions.IgnoreCase))
            .Where(j => j.Deleted == false);

        if (request.MinSalary != null)
        {
            commonSearchQuery = commonSearchQuery
                .Where(r => r.Salary >= request.MinSalary);
        }

        if (request.MaxSalary != null)
        {
            commonSearchQuery = commonSearchQuery
                .Where(r => r.Salary <= request.MaxSalary);
        }

        var zz= commonSearchQuery.ToQueryString();

        return commonSearchQuery;
    }
}
