using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Modles;
using SchoolProject.Core.Features.Subjects.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Subjects.Queries.Handlers
{
    public class SubjectQueryHandler : ResponseHandler, IRequestHandler<GetSubjectListQuery, Response<List<GetSubjectListResponse>>>
    {

        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public SubjectQueryHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _subjectService = subjectService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Response<List<GetSubjectListResponse>>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            var SubjectsList = await _subjectService.GetSubjectsAsync();
            var SubjectListMapper = _mapper.Map<List<GetSubjectListResponse>>(SubjectsList);
            var result = Success(SubjectListMapper);

            result.Meta = new { Count = SubjectListMapper.Count() };

            return result;
        }
    }
}
