using Google.Maps;
using Google.Maps.Direction;
using Hitchhiker.ServiceModel;
using ServiceStack;

namespace Hitchhiker.ServiceInterface
{
	public class RouteService : Service
	{
		public object Get(Route route)
		{
			var directionRequest = new DirectionRequest();
			directionRequest.Language = "ru-RU";
			directionRequest.Origin = new Location(route.Origination);
			directionRequest.Destination = new Location(route.Destination);
			directionRequest.Region = "Беларусь";
			directionRequest.Sensor = false;
			DirectionService service = new DirectionService();

			var response = service.GetResponse(directionRequest);
			return new RouteResponse
			{
				Route = response.Routes[0]
			};
		}
	}
}