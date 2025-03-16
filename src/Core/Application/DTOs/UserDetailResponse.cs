using System.Collections.Generic;

namespace Application.DTOs;

public class UserDetailResponse
{
    public int Id { get; set; }
    public int MembershipId { get; set; }

    public IEnumerable<CompanyResponse> Companies { get; set; } = [];
    public IEnumerable<RatingResponse> Ratings { get; set; } = [];
    public IEnumerable<CommentResponse> Comments { get; set; } = [];
}
