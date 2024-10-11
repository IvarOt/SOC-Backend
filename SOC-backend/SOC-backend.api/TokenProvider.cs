using Microsoft.AspNetCore.Authentication;
using SOC_backend.logic.Interfaces.Api;
using System.IdentityModel.Tokens.Jwt;

namespace SOC_backend.api
{
	public class TokenProvider : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public TokenProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public async Task<string> GetAccessTokenAsync()
		{
			var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

			if (string.IsNullOrEmpty(authorizationHeader))
			{
				return null; // No token found
			}

			// The header value is expected to be in the format "Bearer {token}"
			var token = authorizationHeader.StartsWith("Bearer ")
				? authorizationHeader.Substring("Bearer ".Length).Trim()
				: null;

			return token; // Return the token if found, or null if the format was incorrect
		}

		public async Task<DateTime> GetExpirationAsync()
		{
			var token =  GetAccessTokenAsync();
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token.ToString());
			var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
			var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;
			return expirationTime;
		}
	}
}
