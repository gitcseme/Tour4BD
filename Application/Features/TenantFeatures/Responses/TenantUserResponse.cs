using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TenantFeatures.Responses;

public class TenantUserResponse
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}
