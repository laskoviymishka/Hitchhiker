using ServiceStack;

namespace Hitchhiker.ServiceModel
{
	[Route("/secure/{Name}")]
	public class Secure : IReturn<SecureResponse>
	{
		public string Name { get; set; }
	}

	public class SecureResponse
	{
		public string Name { get; set; }
	}
}