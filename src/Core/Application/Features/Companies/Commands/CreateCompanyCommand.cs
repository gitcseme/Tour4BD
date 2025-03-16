using Application.Features.Companies.Models;
using SharedKarnel.Contracts;
using SharedKarnel.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Companies.Commands;

public class CreateCompanyCommand : CompanyBaseModel, ICommand<CompanyDetailModel>
{
}

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyDetailModel>
{
    public Task<Result<CompanyDetailModel>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}