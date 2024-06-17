using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Features.Users.Neutral.Queries
{
    public record UserRequest : IRequest<IResult>
    {
        public string? UserId { get; set; }
    }

    public class GetUserQueryHandler(IUserService<IdentityUser> userService, IMapper mapper) : IRequestHandler<UserRequest, IResult>
    {
        private readonly IUserService<IdentityUser> _userService = userService;
        private readonly IMapper _mapper = mapper;

        public async Task<IResult> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return default!;
            }
            if (await _userService.GetUserAsync(request.UserId!) is not { } user)
            {
                return TypedResults.NotFound($"UserId {request.UserId} doesn't exist");
            }



            return TypedResults.Ok(_mapper.Map<UserResponse>(user));
        }
    }
}
