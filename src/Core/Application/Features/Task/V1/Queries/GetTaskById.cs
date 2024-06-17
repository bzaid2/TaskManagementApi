using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1.Queries
{

    public record TaskByIdRequest : IRequest<IResult>
    {
        public int TaskId { get; set; }
    }

    public class GetTaskByIdQueryHandler(ITaskService taskService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : IRequestHandler<TaskByIdRequest, IResult>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;

        public async Task<IResult> Handle(TaskByIdRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains(ProjectConstants.USER_ID)).Value;

            if (await _taskService.GetByIdAsync(request.TaskId, userId) is not { } task)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(_mapper.Map<TaskResponse>(task));
        }
    }
}