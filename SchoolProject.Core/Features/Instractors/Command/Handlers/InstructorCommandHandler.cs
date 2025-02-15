using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instractors.Command.Modles;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instractors.Command.Handlers
{
    public class InstructorCommandHandler :
        ResponseHandler, IRequestHandler<AddInstractorCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IInstractorService _instractorService;

        public InstructorCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, IInstractorService instractorService) : base(localizer)
        {
            _localizer = localizer;
            _instractorService = instractorService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddInstractorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instractorService.AddInstractorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_localizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInAdd": return BadRequest<string>(_localizer[SharedResourcesKeys.AddFaild]);
            }
            return Success("");
        }
    }

}
