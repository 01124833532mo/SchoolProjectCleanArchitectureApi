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
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
                IRequestHandler<DeleteUserCommand, Response<string>>

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

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());

            // If Not Exist notfound
            if (oldUser == null) return NotFound<string>();

            // Mapping
            var newUser = _mapper.Map(request, oldUser);

            // Update
            var result = await _userManager.UpdateAsync(newUser);

            // Result is not success
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

            // Message
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            // If Not Exist notfound
            if (user == null) return NotFound<string>();

            // Delete the User
            var result = await _userManager.DeleteAsync(user);

            // In case of Failure
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);

            // Success
            return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
    }
}
