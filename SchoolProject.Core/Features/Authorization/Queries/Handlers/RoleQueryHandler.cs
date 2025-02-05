using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<ManageUserRoleQuery, Response<ManageUserRolesResult>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<User> userManager;

        public RoleQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService, UserManager<User> userManager) : base(localizer)
        {
            _localizer = localizer;
            this.authorizationService = authorizationService;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            _mapper = mapper;
        }




        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return NotFound<ManageUserRolesResult>(_localizer[SharedResourcesKeys.NotFound]);

            var result = await authorizationService.GetManageUserRolesData(user);

            return Success(result);



            // ... rest of the code ...        }
        }
    }
 }
