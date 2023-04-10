using Core.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.Database;

public class CoreBaseRepository<TEntity, TAuditLogEntity>
    where TEntity : CoreBaseEntity
    where TAuditLogEntity : CoreAuditLogEntity
{
    private readonly CoreDbContext<TAuditLogEntity> context;
    private readonly DbSet<TEntity> dbSet;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CoreBaseRepository(CoreDbContext<TAuditLogEntity> context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
        this.httpContextAccessor = httpContextAccessor;
    }

    public virtual IQueryable<TEntity> QueryableAll =>
        dbSet;

    public virtual IQueryable<TEntity> Queryable =>
        dbSet.Where(x => x.IsDeleted == false);

    public virtual async Task<TEntity> GetByID(object id)
    {
        var record = await dbSet.FindAsync(id);
        if (record == null || record.IsDeleted == false)
            return null;
        return record;
    }

    public virtual async Task Insert(TEntity entity)
    {
        entity.CreatedBy = GetExecutorId();
        await dbSet.AddAsync(entity);
    }

    public virtual async Task Insert(List<TEntity> entities)
    {
        var executorId = GetExecutorId();
        _ = entities.Select(x =>
        {
            x.CreatedBy = executorId;
            return x;
        });
        await dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(TEntity entity)
    {
        entity.UpdatedBy = GetExecutorId();
        entity.UpdatedAt = DateTime.Now;
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Update(List<TEntity> entities)
    {
        var executorId = GetExecutorId();
        _ = entities.Select(x =>
        {
            x.UpdatedBy = executorId;
            x.UpdatedAt = DateTime.Now;
            return x;
        });
        dbSet.AttachRange(entities);
        context.Entry(entities).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        entity.DeletedBy = GetExecutorId();
        entity.DeletedAt = DateTime.Now;
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(List<TEntity> entities)
    {
        var executorId = GetExecutorId();
        _ = entities.Select(x =>
        {
            x.DeletedBy = executorId;
            x.DeletedAt = DateTime.Now;
            return x;
        });
        dbSet.AttachRange(entities);
        context.Entry(entities).State = EntityState.Modified;
    }

    private int? GetExecutorId()
    {
        int? creatorId = null;
        var httpContextUserId = httpContextAccessor.HttpContext?.Items["userId"];
        if (httpContextUserId != null)
            creatorId = (int?)httpContextUserId;
        return creatorId;
    }
}