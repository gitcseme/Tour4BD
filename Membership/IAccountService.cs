namespace Membership;

public interface IAccountService
{
    Task<int> CreateUser(string email, string password, int tenantId);
}
