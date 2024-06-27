using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1.Commands
{
    public record CreateTaskRequest : IRequest<IResult>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskValidator()
        {
            RuleFor(r => r.Title).NotEmpty()
                                 .MaximumLength(50);
            RuleFor(r => r.Description).MaximumLength(100);
        }
    }

    public class CreateTaskCommandHandler(ITaskService taskService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : IRequestHandler<CreateTaskRequest, IResult>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;

        public async Task<IResult> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return default!;
            }
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains(ProjectConstants.USER_ID)).Value;
            var task = _mapper.Map<Domain.Entities.Persistence.Task>(request);
            task.CreatedBy = userId;

            if (!await _taskService.CreateAsync(task))
            {
                return Results.ValidationProblem(
                    detail: "One or more validation errors occurred.",
                    instance: _httpContextAccessor!.HttpContext!.Request.Path,
                    statusCode: (int)HttpStatusCode.BadRequest,
                    title: HttpStatusCode.BadRequest.ToString(),
                    type: RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    errors: new Dictionary<string, string[]>
                        {
                            { "error", new[]{"It wasn't possible create the task" } },
                        }
                    );
            }
            var entity = _mapper.Map<TaskResponse>(task);
            return Results.CreatedAtRoute(
                                    routeName: ProjectConstants.ENDPOINT_NAME_GET_TASK,
                                    routeValues: new { taskId = task.Id },
                                    value: entity);
        }
    }
}
