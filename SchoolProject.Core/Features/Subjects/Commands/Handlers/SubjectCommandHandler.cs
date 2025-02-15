using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;
using subjects = SchoolProject.Data.Entities.Subjects;

namespace SchoolProject.Core.Features.Subjects.Commands.Handlers
{
    public class SubjectCommandHandler : ResponseHandler,
        IRequestHandler<CreateSubjectCommand, Response<string>>,
        IRequestHandler<EditSubjectCommand, Response<string>>
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public SubjectCommandHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _subjectService = subjectService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            // Mapping Between request and subject
            var subjectMapper = _mapper.Map<subjects>(request);

            // Add
            var result = await _subjectService.AddAsync(subjectMapper);

            if (result == "Success") return Created("");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetByIdAsync(request.Id);

            if (subject == null) return NotFound<string>("Subject is Not Found");

            var SubjectMapper = _mapper.Map(request, subject);

            var result = await _subjectService.EditAsync(SubjectMapper);

            if (result == "Success") return Success($"Edit Successfully ({SubjectMapper.Id})");
            else return BadRequest<string>();
        }
    }
}
