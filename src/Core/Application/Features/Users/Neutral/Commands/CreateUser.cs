using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Application.Features.Users.Neutral.Commands
{
    public record CreateUserRequest : IRequest<IResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class CredentialValidator : AbstractValidator<CreateUserRequest>
    {
        public CredentialValidator()
        {
            RuleFor(r => r.Email).NotEmpty()
                                 .EmailAddress();
            RuleFor(r => r.Password).NotEmpty()
                                    .MinimumLength(6);
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserRequest, IResult>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService<IdentityUser> _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUserCommandHandler(ITokenService tokenService, IUserService<IdentityUser> userService, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return default!;
            }
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };
            var result = await _userService.CreateAsync(user, request.Password!);
            if (!result.Succeeded)
            {
                return Results.ValidationProblem(
                    detail: "One or more validation errors occurred.",
                    instance: _httpContextAccessor!.HttpContext!.Request.Path,
                    statusCode: (int)HttpStatusCode.BadRequest,
                    title: HttpStatusCode.BadRequest.ToString(),
                    type: RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    errors: new Dictionary<string, string[]>
                        {
                            { "error", result.Errors.Select(error => error.Description).ToArray()},
                        }
                    );
            }
            var token = await _tokenService.GetTokenAsync(request!.Email!, request!.Password!, cancellationToken);
            var entity = new TokenResponse
            {
                AccessToken = token,
                Expiration = _tokenService.Expiration
            };
            return TypedResults.CreatedAtRoute(
                                    routeName: ProjectConstants.ENDPOINT_NAME_GET_USER,
                                    routeValues: new { userId = user.Id },
                                    value: entity);
        }
    }
}
