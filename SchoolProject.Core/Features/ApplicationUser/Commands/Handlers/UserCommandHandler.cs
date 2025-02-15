using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
                IRequestHandler<DeleteUserCommand, Response<string>>,
          IRequestHandler<ChangePasswordCommand, Response<string>>


    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IApplicationUserService _applicationUserService;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IApplicationUserService applicationUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _applicationUserService = applicationUserService;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.FindByEmailAsync(request.Email);
            //if (user is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExsist]);


            //var userByUsername = await _userManager.FindByNameAsync(request.UserName);
            //if (userByUsername is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsExsist]);


            var MappedUser = _mapper.Map<User>(request);

            var result = await _applicationUserService.AddUserAsync(MappedUser, request.Password);

            switch (result)
            {
                case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExsist]);
                case "UserNameIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsExsist]);
                case "ErrorInCreateUser": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }

            //await _userManager.AddToRoleAsync(MappedUser, "User");

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(MappedUser);
            //var requestAccessor = _httpContextAccessor.HttpContext!.Request;
            //var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + $"/Api/V1/Authentication/ConfirmEmail?userId={MappedUser.Id}&code={code}";
            //var sendEmailResult = await _emailService.SendEmail(MappedUser.Email, returnUrl,"");

            //// Check if email sending was successful
            //if (sendEmailResult == "Success")
            //{
            //    return Created<string>(""); // Return success status
            //}

            //return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());

            // If Not Exist notfound
            if (oldUser == null) return NotFound<string>();

            // Mapping
            var newUser = _mapper.Map(request, oldUser);


            var userByUsername = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            if (userByUsername is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsExsist]);


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

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_stringLocalizer[SharedResourcesKeys.ChangePasswordSuccess]);
        }
    }
}
