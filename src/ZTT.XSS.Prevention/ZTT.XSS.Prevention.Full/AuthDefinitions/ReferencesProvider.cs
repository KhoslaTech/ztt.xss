using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZTT.XSS.Prevention.Full.DataModels;
using ASPSecurityKit;
using ASPSecurityKit.Authorization;

namespace ZTT.XSS.Prevention.Full.AuthDefinitions
{
	public class IdMemberReference : IdMemberReference<IdReference>
	{
		public IdMemberReference(string memberName, object value, string parentName, List<IdReference> references = null) : base(memberName, value, parentName, references)
		{
		}
	}

	public class IdReference : IdReference<Guid>
	{
		public IdReference(Guid entityId) : base(entityId)
		{
		}

		public static implicit operator IdReference(Guid id) => new IdReference(id);
	}

	public sealed class ReferencesProvider : ReferencesProviderBase<IdMemberReference, IdReference>
	{
		private readonly AppDbContext dbContext;

		public ReferencesProvider(ILogger logger, AppDbContext dbContext) : base(logger)
		{
			this.dbContext = dbContext;
		}

		public override IdMemberReference NewIdMemberReference(string memberName, object value, string parentName) => new IdMemberReference(memberName, value, parentName);

		public override bool AddSelfAsReference(IdMemberReference idMember)
		{
			// Only actual entityIds can be added
			if (idMember.Value is Guid id)
			{
				idMember.References.Add(new IdReference(id));
				return true;
			}

			return false;
		}

		public async Task<List<IdReference>> UserPermitId(Guid id)
		{
			return await dbContext.UserPermits
				.Where(x => x.Id == id)
				.Select(x => new List<Guid> { x.UserPermitGroup.UserId })
				.ToIdReferenceListAsync();
		}

		public async Task<List<IdReference>> Username(string username)
		{
			return await dbContext.Users
				.Where(x => x.Username == username)
				.Select(x => new List<Guid> { x.Id })
				.ToIdReferenceListAsync();
		}

		public async Task<List<IdReference>> ProductId(Guid productId)
		{
			return await dbContext.Products
				.Where(x => x.Id == productId)
				.Select(x => new List<Guid> { x.Id, x.OwnerId })
				.ToIdReferenceListAsync();
		}
	}
}
