using Carter;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Application.Features.Task.V1.Commands;
using TaskManagement.Application.Features.Task.V1.Queries;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Task.V1
{
    public class TaskRouting : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(ProjectConstants.ENDPOINT_CREATE_TASK, CreateTaskAsync)
               .WithTags(ProjectConstants.TAG_TASK)
               .WithName(ProjectConstants.ENDPOINT_NAME_CREATE_TASK)
               .Produces<TaskResponse>(StatusCodes.Status201Created)
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .MapToApiVersion(1, 0);

            app.MapGet(ProjectConstants.ENDPOINT_GET_TASK, GetTaskAsync)
               .WithTags(ProjectConstants.TAG_TASK)
               .WithName(ProjectConstants.ENDPOINT_NAME_GET_TASK)
               .Produces<TaskResponse>()
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .MapToApiVersion(1, 0);

            app.MapGet(ProjectConstants.ENDPOINT_GET_TASKS, GetTasksAsync)
               .WithTags(ProjectConstants.TAG_TASK)
               .WithName(ProjectConstants.ENDPOINT_NAME_GET_TASKS)
               .Produces<IEnumerable<TaskResponse>>()
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .MapToApiVersion(1, 0);

            app.MapPut(ProjectConstants.ENDPOINT_UPDATE_TASK, UpdateTaskAsync)
               .WithTags(ProjectConstants.TAG_TASK)
               .WithName(ProjectConstants.ENDPOINT_NAME_UPDATE_TASK)
               .Produces(StatusCodes.Status204NoContent)
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .MapToApiVersion(1, 0);

            app.MapDelete(ProjectConstants.ENDPOINT_DELETE_TASK, DeleteTaskAsync)
               .WithTags(ProjectConstants.TAG_TASK)
               .WithName(ProjectConstants.ENDPOINT_NAME_DELETE_TASK)
               .Produces(StatusCodes.Status204NoContent)
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .MapToApiVersion(1, 0);
        }

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_CREATE_TASK, Description = ProjectConstants.DESCRIPTION_CREATE_TASK)]
        [SwaggerResponse(StatusCodes.Status201Created, ProjectConstants.STATUS_201_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> CreateTaskAsync(ISender mediator, CreateTaskRequest createTask) => await mediator.Send(createTask);

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_GET_TASK, Description = ProjectConstants.DESCRIPTION_GET_TASK)]
        [SwaggerResponse(StatusCodes.Status200OK, ProjectConstants.STATUS_200_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status404NotFound, ProjectConstants.STATUS_404_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> GetTaskAsync(ISender mediator, int taskId) => await mediator.Send(new TaskByIdRequest { TaskId = taskId });

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_GET_TASKS, Description = ProjectConstants.DESCRIPTION_GET_TASKS)]
        [SwaggerResponse(StatusCodes.Status200OK, ProjectConstants.STATUS_200_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> GetTasksAsync(ISender mediator) => await mediator.Send(new AllTaskRequest());


        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_UPDATE_TASK, Description = ProjectConstants.DESCRIPTION_UPDATE_TASK)]
        [SwaggerResponse(StatusCodes.Status200OK, ProjectConstants.STATUS_200_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> UpdateTaskAsync(ISender mediator, IMapper mapper, int taskId, [FromBody] UpdateTask updateTask)
        {
            var request = mapper.Map<UpdateTaskRequest>(updateTask);
            request.TaskId = taskId;
            return await mediator.Send(request);
        }

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_DELETE_TASK, Description = ProjectConstants.DESCRIPTION_DELETE_TASK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, ProjectConstants.STATUS_204_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status404NotFound, ProjectConstants.STATUS_404_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> DeleteTaskAsync(ISender mediator, int taskId) => await mediator.Send(new DeleteTaskRequest { TaskId = taskId });
    }
}
