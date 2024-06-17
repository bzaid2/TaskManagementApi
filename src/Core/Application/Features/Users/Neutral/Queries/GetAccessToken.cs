using FluentValidation;
using MediatR;
using TaskManagement.Domain.Entities.Dtos;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Features.Users.Neutral.Queries
{
    public record TokenRequest : IRequest<TokenResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class CredentialValidator : AbstractValidator<TokenRequest>
    {
        public CredentialValidator()
        {
            RuleFor(r => r.Email).NotEmpty()
                                .EmailAddress();
            RuleFor(r => r.Password).NotEmpty()
                                    .MinimumLength(6);
        }
    }

    public class TokenQueryHandler(ITokenService tokenService) : IRequestHandler<TokenRequest, TokenResponse>
    {
        private readonly ITokenService _tokenService = tokenService;
        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return default!;
            }
            var token = await _tokenService.GetTokenAsync(request!.Email!, request!.Password!, cancellationToken);
            return new TokenResponse
            {
                AccessToken = token,
                Expiration = _tokenService.Expiration
            };
        }
    }
}
