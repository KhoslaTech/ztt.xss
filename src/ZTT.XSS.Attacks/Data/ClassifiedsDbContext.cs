using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZTT.XSS.Attack.Data;

namespace ZTT.XSS.Attack.Data
{
	public class ClassifiedsDbContext : IdentityDbContext
	{
		public ClassifiedsDbContext(DbContextOptions<ClassifiedsDbContext> options)
			: base(options)
		{
		}
		public DbSet<ZTT.XSS.Attack.Data.DbProduct> DbProduct { get; set; }
	}


	[Table("Product")]
	public class DbProduct
	{
		[Key]
		public Guid Id { get; set; }

		[MaxLength(60)]
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }
		
		[Required]
		public double Cost { get; set; }
	}
}
