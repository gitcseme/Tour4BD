namespace Domain.Entities;

public class UserPermission : EntityBase<int>
{
    public int UserId { get; set; }
    public int PermissionId { get; set; }

    public SystemUser SystemUser { get; set; }
    public Permission Permission { get; set; }
}