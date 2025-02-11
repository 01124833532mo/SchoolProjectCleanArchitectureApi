using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instractors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instractors.Queries.Handlers
{
    public class InstructorQueryHandler : ResponseHandler, IRequestHandler<GetSummationSalaryOfInstructorQuery, Response<decimal>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IInstractorService _instractorService;

        public InstructorQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, IInstractorService instractorService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _instractorService = instractorService;
        }

        public async Task<Response<decimal>> Handle(GetSummationSalaryOfInstructorQuery request, CancellationToken cancellationToken)
        {

            var result = await _instractorService.GetSalarySummationOfInstructor();
            return Success(result);

        }
    }
}
