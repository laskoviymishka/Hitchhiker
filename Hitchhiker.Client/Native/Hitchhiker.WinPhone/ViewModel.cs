using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Hitchhiker.ServiceModel;
using MapControl;
using PortableRest;

namespace Hitchhiker.WinPhone
{
	public class ViewModel : INotifyPropertyChanged
	{
		private readonly CoreDispatcher dispatcher;
		private readonly Geolocator geoLocator;
		private double accuracy;
		private Location location;

		public event PropertyChangedEventHandler PropertyChanged;

		public ViewModel(CoreDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;

			geoLocator = new Geolocator
			{
				DesiredAccuracy = PositionAccuracy.High,
				MovementThreshold = 1d
			};

			geoLocator.StatusChanged += GeoLocatorStatusChanged;
			geoLocator.PositionChanged += GeoLocatorPositionChanged;
		}

		public double Accuracy
		{
			get { return accuracy; }
			private set
			{
				accuracy = value;
				RaisePropertyChanged("Accuracy");
			}
		}

		public Location Location
		{
			get { return location; }
			private set
			{
				location = value;
				RaisePropertyChanged("Location");
			}
		}

		private void RaisePropertyChanged(string propertyName)
		{
			var propertyChanged = PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private async void GeoLocatorStatusChanged(Geolocator sender, StatusChangedEventArgs args)
		{
			if (args.Status != PositionStatus.Initializing &&
				args.Status != PositionStatus.Ready)
			{
				await dispatcher.RunAsync(CoreDispatcherPriority.Low, () => Location = null);
			}
		}

		private string origination;
		public string Origination
		{
			get { return origination; }
			set
			{
				origination = value;
				RaisePropertyChanged("Origination");
			}
		}

		private string destination;
		public string Destination
		{
			get { return destination; }
			set
			{
				destination = value;
				RaisePropertyChanged("Destination");
			}
		}

		public ICommand GetRoute
		{
			get { return new ActionCommand(GetRouteAction); }
		}

		private async void GetRouteAction()
		{
			var client = new HitchhikerClient();
			RestRequest request = new RestRequest(
				string.Format("route?Origination={0}&Destination={1}", this.Origination, this.Destination), new HttpMethod("GET"));

			try
			{
				var result = await client.ExecuteAsync<RouteResponse>(request);
			}
			catch (Exception exception)
			{
			}
		}

		private async void GeoLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
		{
			await dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
			{
				Accuracy = args.Position.Coordinate.Accuracy;
				Location = new Location(
					args.Position.Coordinate.Point.Position.Latitude,
					args.Position.Coordinate.Point.Position.Longitude);
			});
		}
	}
}