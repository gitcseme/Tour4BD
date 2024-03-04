using Application.Interfaces.Repositories;

namespace Application.Interfaces;

public interface ITenantUnitOfWork
{
    ITenantRepository TenantRepository { get; }
}
