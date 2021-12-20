using System.Threading.Tasks;
using ZTT.XSS.Prevention.Full.Security;
using ASPSecurityKit;
using Microsoft.AspNetCore.Http;

namespace ZTT.XSS.Prevention.Full.Middlewares
{
	public class AuthSessionCachingMiddleware
	{
		private readonly ILogger logger;
		private readonly RequestDelegate next;

		public AuthSessionCachingMiddleware(RequestDelegate next, ILogger logger)
		{
			this.next = next;
			this.logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, IUserService userService, CachedAuthSessionProvider authSessionProvider)
		{
			await next(context);

			if (userService.IsAuthenticated)
			{
				await authSessionProvider.SaveSessionAsync();
			}
		}
	}
}