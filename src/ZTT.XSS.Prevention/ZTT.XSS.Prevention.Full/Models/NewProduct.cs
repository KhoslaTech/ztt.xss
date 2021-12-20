using System.ComponentModel.DataAnnotations;

namespace ZTT.XSS.Prevention.Full.Models
{
	public class NewProduct
	{
		[MaxLength(60)]
		[Required]
		public string Name { get; set; }
		
		public string Description { get; set; }

		[Required]
		public double Cost { get; set; }
	}
}
