using Core.Database.Entities;
using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Database;

public class CoreUnitOfWork<TAuditLogEntity> : IDisposable where TAuditLogEntity : CoreAuditLogEntity
{
    protected readonly CoreDbContext<TAuditLogEntity> context;
    protected readonly IHttpContextAccessor httpContextAccessor;

    public CoreUnitOfWork(CoreDbContext<TAuditLogEntity> context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Save()
    {
        int? createdBy = null;
        var httpContextUserId = httpContextAccessor.HttpContext?.Items["executorId"];
        if (httpContextUserId != null)
            createdBy = (int?)httpContextUserId;
        var auditLogCreateDto = new AuditLogCreateDto
        {
            Method = httpContextAccessor.HttpContext?.Request.Method ?? string.Empty,
            CreatedBy = createdBy
        };
        return await context.SaveChangesAsync(auditLogCreateDto) > 0;
    }

    // Dispose
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}