using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authntecation.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authntecation.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<string>>
    {

        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            // Return The UserName Not Found
            if (user == null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);

            //try To Sign in
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if Failed Return Passord is wrong
            if (!signInResult.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            //Generate Token
            var accessToken = await _authenticationService.GetJWTToken(user);


            //return Token
            //
            return Success(accessToken);
        }
    }
}
