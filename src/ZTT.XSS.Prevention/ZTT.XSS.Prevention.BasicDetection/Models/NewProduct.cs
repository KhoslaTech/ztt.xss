using System.ComponentModel.DataAnnotations;

namespace ZTT.XSS.Prevention.BasicDetection.Models
{
	public class NewProduct
	{
		[MaxLength(60)]
		[Required]
		[RegularExpression(Constants.XssPattern, ErrorMessage = "Warning: XSS injection detected!")]
		public string Name { get; set; }

		[RegularExpression(Constants.XssPattern, ErrorMessage = "Warning: XSS injection detected!")]
		public string Description { get; set; }

		[Required]
		public double Cost { get; set; }
	}
}
