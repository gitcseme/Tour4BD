namespace Domain.Entities;

public class Comment : BaseEntity<int>
{
    public string Message { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; }

    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
}
