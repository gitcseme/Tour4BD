namespace Application.Features.TenantFeatures.Responses;

public class TenantResponseDto
{
    public int Id { get; set; }
    public required string OrganizationName { get; set; }
    public required string ConnectionString { get; set; }
}
