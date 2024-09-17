using Application.DTOs;
using Application.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TenantFeatures.Queries;

public record GetUserDetailQuery(int TenantId, int UserId) : IRequest<UserDetailResponse>;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailResponse>
{
    private readonly IUnitOfWork _uow;

    public GetUserDetailQueryHandler([FromKeyedServices(AppConstants.ApplicationDbContextDIKey)] IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<UserDetailResponse> Handle(GetUserDetailQuery request, CancellationToken ctn)
    {
        var appUser = await _uow.Repository<ApplicationUser, int>()
            .Query()
            .Include(u => u.Companies)
            .Include(u => u.Ratings)
            .Include(u => u.Comments)
            .Where(u => u.MembershipId == request.TenantId && u.Id == request.UserId)
            .Select(appUser => new UserDetailResponse
            {
                Id = appUser.Id,
                MembershipId = appUser.MembershipId,
                Companies = appUser.Companies.Select(c => new CompanyResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    LisenceLink = c.LisenceLink
                }),
                Ratings = appUser.Ratings.Select(r => new RatingResponse
                {
                    Id = r.Id,
                    CompanyId = r.CompanyId,
                    PackageId = r.PackageId,
                }),
                Comments = appUser.Comments.Select(cmt => new CommentResponse
                {
                    Id = cmt.Id,
                    Message = cmt.Message,
                    PackageId = cmt.PackageId,
                })
            })
            .FirstOrDefaultAsync(ctn);

        return appUser;
    }
}

