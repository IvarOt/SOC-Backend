using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net.Http.Headers;
using SOC_backend.logic.Interfaces.Api;
using System.Net.Http;

namespace SOC_backend.logic.Pipelines
{
	public class TokenMiddleware : DelegatingHandler
	{
		private readonly RequestDelegate _next;
		private readonly IHttpClientFactory _httpClientFactory;

		public TokenMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
		{
			_next = next;
			_httpClientFactory = httpClientFactory;
		}

		public async Task Invoke(HttpContext context, ITokenProvider _tokenProvider)
		{
			await _next(context);

			if (context.Response.StatusCode == StatusCodes.Status200OK)
			{
				var token = await _tokenProvider.GetAccessTokenAsync();
				if (!string.IsNullOrEmpty(token))
				{
					var client = _httpClientFactory.CreateClient();
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				}
			}
		}
	}
}
