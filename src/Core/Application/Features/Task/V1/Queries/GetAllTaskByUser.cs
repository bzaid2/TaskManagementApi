using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1.Queries
{
    public record AllTaskRequest : IRequest<IResult>
    {

    }

    public class GetAllTaskByUserQueryHandler(ITaskService taskService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : IRequestHandler<AllTaskRequest, IResult>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;

        public async Task<IResult> Handle(AllTaskRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains(ProjectConstants.USER_ID)).Value;
            var result = await _taskService.GetAsync(userId);
            return TypedResults.Ok(_mapper.Map<IEnumerable<TaskResponse>>(result));
        }
    }
}
