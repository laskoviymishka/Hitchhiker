using Google.Maps.Direction;

namespace Google.Maps
{
	public class LatLng
		: Location
	{
		public virtual double Latitude { get; set; }
		public virtual double Longitude { get; set; }
	}

	public class Location
	{
	}

	public enum TravelMode
	{
		driving,
		walking,
		bicycling,
	}

	public class ValueText
	{
		public virtual string Value { get; set; }
		public virtual string Text { get; set; }
	}
}

namespace Google.Maps.Direction
{
	public class DirectionLeg
	{
		public DirectionLeg()
		{
			Steps = new DirectionStep[] {};
		}

		public virtual DirectionStep[] Steps { get; set; }
		public virtual ValueText Duration { get; set; }
		public virtual ValueText Distance { get; set; }
		public virtual LatLng StartLocation { get; set; }
		public virtual LatLng EndLocation { get; set; }
		public virtual string StartAddress { get; set; }
		public virtual string EndAddress { get; set; }
	}

	public class DirectionRoute
	{
		public DirectionRoute()
		{
			Legs = new DirectionLeg[] {};
		}

		public virtual string Summary { get; set; }
		public virtual DirectionLeg[] Legs { get; set; }
		public virtual string Copyrights { get; set; }
		public virtual Polyline OverviewPolyline { get; set; }
	}

	public class DirectionStep
	{
		public virtual TravelMode TravelMode { get; set; }
		public virtual LatLng StartLocation { get; set; }
		public virtual LatLng EndLocation { get; set; }
		public virtual Polyline Polyline { get; set; }
		public virtual ValueText Duration { get; set; }
		public virtual string Maneuver { get; set; }
		public virtual string HtmlInstructions { get; set; }
		public virtual ValueText Distance { get; set; }
	}

	public class Polyline
	{
		public virtual string Points { get; set; }
		public virtual string Levels { get; set; }
	}
}

namespace Hitchhiker.ServiceModel
{
	public class Hello
	{
		public virtual string Name { get; set; }
	}

	public class HelloResponse
	{
		public virtual string Result { get; set; }
	}

	public class Route
	{
		public virtual string Origination { get; set; }
		public virtual string Destination { get; set; }
	}

	public class RouteResponse
	{
		public virtual DirectionRoute Route { get; set; }
	}

	public class Secure
	{
		public virtual string Name { get; set; }
	}

	public class SecureResponse
	{
		public virtual string Name { get; set; }
	}
}