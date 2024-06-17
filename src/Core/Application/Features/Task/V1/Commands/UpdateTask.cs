using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1.Commands
{
    public record UpdateTask
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    public record UpdateTaskRequest : UpdateTask, IRequest<IResult>
    {
        public int TaskId { get; set; }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(r => r.TaskId).Must(x => x > 0)
                                  .WithMessage("Invalid ID");
            RuleFor(r => r.Title).NotEmpty()
                                 .MaximumLength(50);
            RuleFor(r => r.Description).MaximumLength(100);
            RuleFor(r => r.ExpiryDate).Must(x => x >= DateTime.Today)
                                      .WithMessage("Invalid expiry date");
        }
    }

    public class UpdateTaskCommandHandler(ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<UpdateTaskRequest, IResult>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IResult> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
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

            task.Title = request.Title;
            task.Description = request.Description;
            task.IsChecked = request.IsChecked;
            task.ExpiryDate = request.ExpiryDate;

            if (!await _taskService.UpdateAsync(task))
            {
                return Results.ValidationProblem(
                    detail: "One or more validation errors occurred.",
                    instance: _httpContextAccessor!.HttpContext!.Request.Path,
                    statusCode: (int)HttpStatusCode.BadRequest,
                    title: HttpStatusCode.BadRequest.ToString(),
                    type: RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    errors: new Dictionary<string, string[]>
                        {
                            { "error", new[]{"It wasn't possible update the task" } },
                        }
                    );
            }
            return TypedResults.NoContent();
        }
    }
}
