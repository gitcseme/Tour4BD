namespace Domain.Entities;

public class Comment : EntityBase<int>
{
    public string Message { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; }

    public int VisitorId { get; set; }
    public Visitor Visitor { get; set; }
}
