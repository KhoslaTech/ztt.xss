using ZTT.XSS.Prevention.Full.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ASPSecurityKit;
using ASPSecurityKit.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace ZTT.XSS.Prevention.Full.Controllers
{
	[AllowAnonymous]
	public class HomeController : SiteControllerBase
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			var message = string.Empty;
			if (exceptionHandlerPathFeature?.Error is AuthFailedException authEx)
			{
				message = authEx.Message;
			}

			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
		}
	}
}
