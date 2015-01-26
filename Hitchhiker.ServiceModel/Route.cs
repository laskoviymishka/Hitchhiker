using Google.Maps.Direction;
using ServiceStack;

namespace Hitchhiker.ServiceModel
{
	[Route("/route")]
	public class Route : IReturn<RouteResponse>
	{
		public string Origination { get; set; }

		public string Destination { get; set; }
	}

	public class RouteResponse
	{
		public DirectionRoute Route { get; set; }
	}
}