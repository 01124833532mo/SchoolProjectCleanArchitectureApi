using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuthorizationService authorizationService;

        public ClaimsCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, RoleManager<Role> roleManager, IAuthorizationService authorizationService) : base(localizer)
        {
            _localizer = localizer;
            _roleManager = roleManager;
            this.authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.UpdateUserClaim(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUpdateClaims]);
            }
            return Success<string>(_localizer[SharedResourcesKeys.Success]);
        }
    }
}
