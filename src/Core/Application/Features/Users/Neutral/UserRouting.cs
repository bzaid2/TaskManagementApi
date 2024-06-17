using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Application.Features.Users.Neutral.Commands;
using TaskManagement.Application.Features.Users.Neutral.Queries;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Users.Neutral
{
    public class UserRouting : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(ProjectConstants.ENDPOINT_CREATE_USER, CreateUserAsync)
               .WithTags(ProjectConstants.TAG_USER)
               .WithName(ProjectConstants.ENDPOINT_NAME_CREATE_USER)
               .Produces<TokenResponse>(StatusCodes.Status201Created)
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .IsApiVersionNeutral();

            app.MapGet(ProjectConstants.ENDPOINT_GET_USER, GetUserAsync)
               .WithTags(ProjectConstants.TAG_USER)
               .WithName(ProjectConstants.ENDPOINT_NAME_GET_USER)
               .Produces<TokenResponse>(StatusCodes.Status200OK)
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status401Unauthorized)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .IsApiVersionNeutral();

            app.MapPost(ProjectConstants.ENDPOINT_GET_TOKEN, TokenAsync)
               .WithTags(ProjectConstants.TAG_TOKEN)
               .WithName(ProjectConstants.ENDPOINT_NAME_GET_TOKEN)
               .Accepts<TokenRequest>(ProjectConstants.CONTENT_TYPE_APPLICATION_X_WWW_FORM)
               .Produces<TokenResponse>()
               .ProducesValidationProblem()
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithApiVersionSet(ApplicationVersion.VersionSet!)
               .IsApiVersionNeutral();
        }

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_CREATE_USER, Description = ProjectConstants.DESCRIPTION_CREATE_USER)]
        [SwaggerResponse(StatusCodes.Status201Created, ProjectConstants.STATUS_201_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        internal static async Task<IResult> CreateUserAsync(ISender mediator, CreateUserRequest createUser) => await mediator.Send(createUser);

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_GET_USER, Description = ProjectConstants.DESCRIPTION_GET_USER)]
        [SwaggerResponse(StatusCodes.Status201Created, ProjectConstants.STATUS_201_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, ProjectConstants.STATUS_401_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        [Authorize(AuthenticationSchemes = ProjectConstants.BEARER)]
        internal static async Task<IResult> GetUserAsync(ISender mediator, string userId) => await mediator.Send(new UserRequest { UserId = userId });

        [SwaggerOperation(Summary = ProjectConstants.SUMMARY_GET_TOKEN, Description = ProjectConstants.DESCRIPTION_GET_TOKEN)]
        [SwaggerResponse(StatusCodes.Status200OK, ProjectConstants.STATUS_200_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, ProjectConstants.STATUS_400_DESCRIPTION)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, ProjectConstants.STATUS_500_DESCRIPTION)]
        internal static async Task<TokenResponse> TokenAsync(ISender mediator, HttpContext httpContext)
        {
            var request = new TokenRequest
            {
                Email = httpContext.Request.Form["email"],
                Password = httpContext.Request.Form["password"]
            };
            return await mediator.Send(request);
        }
    }
}
