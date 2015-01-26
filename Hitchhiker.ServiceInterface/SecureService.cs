using Hitchhiker.ServiceModel;
using ServiceStack;

namespace Hitchhiker.ServiceInterface
{
	public class SecureService : Service
	{
		[Authenticate]
		public object Any(Secure request)
		{
			return new SecureResponse() { Name = "Hello, {0}! You are {1}".Fmt(request.Name, this.GetSession().IsAuthenticated) };
		}
	}
}