
using ResumeDataAccess.Context;
using ResumeDataAccess.Contracts.Repository;
using ResumeDataAccess.Entities;

namespace ResumeDataAccess.Repository;

internal sealed class ResumeRepository : Repository<Resume, long>, IResumeRepository
{
    public ResumeRepository(ResumeDbContext context) : base(context)
    {
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
}
