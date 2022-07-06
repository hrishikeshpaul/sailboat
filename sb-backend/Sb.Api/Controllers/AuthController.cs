using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Sb.Api.Configuration;
using Sb.Api.Models;
using Sb.Api.Services;
using Sb.Data;
using Sb.Data.Models;
using Sb.OAuth2;

using System.Security.Claims;

namespace Sb.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly OAuth2ClientFactory _clientFactory;
        private readonly JwtConfig _jwtConfig;

        public AuthController(IOptions<JwtConfig> jwtOptions, OAuth2ClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jwtConfig = jwtOptions.Value;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public string Login(IdentityProvider provider, [FromQuery] string redirectUri, [FromQuery] string state)
        {
            return _clientFactory.GetClient(provider).GetAuthorizationEndpoint(redirectUri, null, null, state);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginEmailPassword(
            [FromBody] EmailPasswordLogin login,
            [FromServices] IUserService userService)
        {
            return Ok(await userService.AuthenticateAsync(login.Email, login.Password));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(
            [FromServices] IUserService userService,
            CreateUser user)
        {
            user.EmailConfirmationToken = Guid.NewGuid();
            JwtTokensResponse tokens = await userService.CreateUserAsync(user);
            return Ok(tokens);
        }

        [HttpGet("authorize")]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize(
            IdentityProvider provider,
            [FromQuery] string code,
            [FromQuery] string redirectUri,
            [FromQuery] string state,
            [FromServices] IRepository<User> userRepository,
            [FromServices] ITokenService tokenService)
        {
            OAuth2Client client = _clientFactory.GetClient(provider);
            OAuthTokens providerTokens = await client.GenerateAccessTokensAsync(code, redirectUri, state);
            AuthorizedUser user = await client.GetAuthorizedUserAsync(providerTokens.AccessToken);

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Invalid email");

            User existingUser = await userRepository
                .FirstOrDefaultAsync(u => u.ConnectedAccounts.Any(ca => ca.Id == user.Id));

            if (existingUser is null)
            {
                existingUser = await userRepository.InsertAsync(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    DateCreated = DateTime.UtcNow,
                    ConnectedAccounts = new List<ConnectedAccount>
                        {
                            new ConnectedAccount
                            {
                                Id = user.Id,
                                Provider = provider.ToString(),
                                PictureUrl = user.GetProfilePicture()
                            }
                        }
                });
            }

            return Ok(await tokenService.GenerateTokens(existingUser.Id, GenerateUserClaims(existingUser)));
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(
            [FromBody] TokenBase token,
            [FromServices] ITokenService tokenService)
        {
            AuthorizedUser u = HttpContext.GetUserFromClaims();
            if (await tokenService.IsTokenValid(u.Id, token.Value, TokenType.Refresh))
            {
                return Ok(new JwtTokensResponse
                {
                    AccessToken = await tokenService.GenerateToken(u.Id, TokenType.Access, HttpContext.User.Claims),
                    RefreshToken = await tokenService.GenerateToken(u.Id, TokenType.Refresh, HttpContext.User.Claims)
                });
            }
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromServices] ITokenService tokenService)
        {
            string token = HttpContext.GetAccessToken();
            await tokenService.RevokeToken(HttpContext.GetUserId(), token, TokenType.Access);
            return Ok();
        }

        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutAll([FromServices] ITokenService tokenService)
        {
            await tokenService.RevokeAllTokens(HttpContext.GetUserId());
            return Ok();
        }

        private IEnumerable<Claim> GenerateUserClaims(User user)
            => new List<Claim>()
                .AddIfValid(ClaimTypes.Name, user.Name)
                .AddIfValid(ClaimTypes.Email, user.Email)
                .AddIfValid(CustomClaimTypes.Id, user.Id);
    }
}
