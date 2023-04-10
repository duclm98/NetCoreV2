using Core.Database;
using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Data;

public class BaseRepository<TEntity> : CoreBaseRepository<TEntity, AuditLogEntity> where TEntity : BaseEntity
{
    public BaseRepository(
        CoreDbContext<AuditLogEntity> context,
        IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }
}