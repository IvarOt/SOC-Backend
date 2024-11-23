using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using SOC_backend.logic.Pipelines;
using System.Net;


namespace SOC_backend.test
{
	[TestClass]
	public class ExceptionMiddlewareTests
	{
		[TestMethod]
		public async Task ExceptionMiddleware_GivesInternalServerError()
		{
			using var host = await new HostBuilder()
				.ConfigureWebHost(webBuilder =>
				{
					webBuilder
						.UseTestServer()
						.ConfigureServices(services =>
						{
						})
						.Configure(app =>
						{
							app.UseMiddleware<ExceptionMiddleware>();
							app.Run(context =>
							{
								throw new Exception("Test");
							});
						});
				})
				.StartAsync();
			var response = await host.GetTestClient().GetAsync("/");

			Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
		}
	}
}
