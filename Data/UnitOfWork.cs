using Core.Database;
using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Data;

public class UnitOfWork : CoreUnitOfWork<AuditLogEntity>
{

    public UnitOfWork(
        CoreDbContext<AuditLogEntity> context,
        IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }

    private BaseRepository<UserEntity> userRepository;
    public BaseRepository<UserEntity> UserRepository
    {
        get
        {
            userRepository ??= new BaseRepository<UserEntity>(context, httpContextAccessor);
            //if (userRepository == null)
            //    userRepository = new BaseRepository<User>(context, httpContextAccessor);
            return userRepository;
        }
    }
}