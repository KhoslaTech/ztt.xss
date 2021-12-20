using Microsoft.AspNetCore.Builder;

namespace ZTT.XSS.Prevention.Full.Middlewares
{
	public static class MiddlewareExtensions
	{

		public static void UseAuthSessionCaching(this IApplicationBuilder app)
		{
			app.UseMiddleware<AuthSessionCachingMiddleware>();
		}
	}
}