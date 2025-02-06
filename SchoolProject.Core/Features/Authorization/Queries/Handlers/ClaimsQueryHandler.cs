using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<User> userManager;

        public ClaimsQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService, UserManager<User> userManager) : base(localizer)
        {
            _localizer = localizer;
            this.authorizationService = authorizationService;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return NotFound<ManageUserClaimsResult>(_localizer[SharedResourcesKeys.NotFound]);

            var result = await authorizationService.ManageUserClaimData(user);

            return Success(result);
        }
    }
}
