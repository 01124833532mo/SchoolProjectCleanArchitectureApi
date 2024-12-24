using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExsist]);


            var userByUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userByUsername is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsExsist]);


            var MappedUser = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(MappedUser, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }

            return Created<string>("");
        }
    }
}
