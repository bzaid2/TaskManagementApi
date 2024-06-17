using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1.Commands
{
    public record DeleteTaskRequest : IRequest<IResult>
    {
        public int TaskId { get; set; }
    }

    public class DeleteTaskValidator : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteTaskValidator()
        {
            RuleFor(r => r.TaskId).Must(x => x > 0)
                                  .WithMessage("Invalid ID");
        }
    }

    public class DeleteTaskCommandHandler(ITaskService taskService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteTaskRequest, IResult>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IResult> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return default!;
            }
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains(ProjectConstants.USER_ID)).Value;
            if (await _taskService.GetByIdAsync(request.TaskId, userId) is not { } task)
            {
                return TypedResults.NotFound();
            }

            if (!await _taskService.DeleteAsync(task))
            {
                return Results.ValidationProblem(
                    detail: "One or more validation errors occurred.",
                    instance: _httpContextAccessor!.HttpContext!.Request.Path,
                    statusCode: (int)HttpStatusCode.BadRequest,
                    title: HttpStatusCode.BadRequest.ToString(),
                    type: RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    errors: new Dictionary<string, string[]>
                        {
                            { "error", new[]{"It wasn't possible delete the task" } },
                        }
                    );
            }
            return TypedResults.NoContent();
        }
    }
}
