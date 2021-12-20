namespace ZTT.XSS.Prevention.BasicDetection.Models
{
	public class Constants
	{
		public const string XssPattern = @"^(?:(?!(\<[a-zA-Z/!?])|(&[a-zA-Z#]))[\w\W])*$";
	}
}
