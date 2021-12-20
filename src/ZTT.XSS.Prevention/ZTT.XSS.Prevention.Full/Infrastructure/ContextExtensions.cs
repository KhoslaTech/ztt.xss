using ASPSecurityKit;
using Microsoft.AspNetCore.Http;
using ZTT.XSS.Prevention.Full.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZTT.XSS.Prevention.Full.Infrastructure
{
	public static class ContextExtensions
	{
		public static IUserService<Guid, Guid, DbUser> UserService(this HttpContext context)
		{
			return (IUserService<Guid, Guid, DbUser>)(context.Items[Constants.CX_UserService] ??
			                                          (context.Items[Constants.CX_UserService] = context.RequestServices
				                                          .GetService(typeof(IUserService<Guid, Guid, DbUser>))));
		}
	}
}
