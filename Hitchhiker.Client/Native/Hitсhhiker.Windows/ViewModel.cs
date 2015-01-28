using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using Windows.UI.Xaml;
using Hitchhiker.ServiceModel;
using MapControl;
using RestSharp.Portable;
using ServiceStack;
using Yandex.IO;
using Yandex.MVVM;
using IRestClient = RestSharp.Portable.IRestClient;
using Path = System.IO.Path;

namespace ViewModel
{
	public class ViewModel : Base
	{
		public ObservableCollection<Point> Points { get; set; }
		public ObservableCollection<Point> Pushpins { get; set; }
		public ObservableCollection<Polyline> Polylines { get; set; }

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

		private RouteResponse routeResponse;
		public RouteResponse RouteResponse
		{
			get { return routeResponse; }
			set
			{
				routeResponse = value;
				RaisePropertyChanged("RouteResponse");
			}
		}

		private Location mapCenter;
		public Location MapCenter
		{
			get { return mapCenter; }
			set
			{
				mapCenter = value;
				RaisePropertyChanged("MapCenter");
			}
		}

		public ICommand GetRoute
		{
			get
			{
				return new RelayCommand(() =>
				{
					var routeRequest = new Route
					{
						Origination = this.Origination,
						Destination = this.Destination
					};
					try
					{
						var serviceClient = new JsonServiceClient("http://localhost:13011/");

						var response = serviceClient.Get(routeRequest);
						this.MapCenter = new Location
						{
							Latitude = response.Route.Legs[0].StartLocation.Latitude,
							Longitude = response.Route.Legs[0].StartLocation.Longitude
						};

						foreach (var directionStep in response.Route.Legs[0].Steps)
						{
							this.Polylines.Add(new Polyline
							{
								Locations = new LocationCollection(this.DecodePolylinePoints(directionStep.Polyline.Points))
							});
						}
						var asd = response.Route;
					}
					catch (Exception exception)
					{
						var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message);
						messageDialog.ShowAsync();
					}
				});
			}
		}

		public ViewModel()
		{
			MapCenter = new Location(53.5, 8.2);

			Points = new ObservableCollection<Point>();
			Points.Add(
				new Point
				{
					Name = "Steinbake Leitdamm",
					Location = new Location(53.51217, 8.16603)
				});
			Points.Add(
				new Point
				{
					Name = "Buhne 2",
					Location = new Location(53.50926, 8.15815)
				});
			Points.Add(
				new Point
				{
					Name = "Buhne 4",
					Location = new Location(53.50468, 8.15343)
				});
			Points.Add(
				new Point
				{
					Name = "Buhne 6",
					Location = new Location(53.50092, 8.15267)
				});
			Points.Add(
				new Point
				{
					Name = "Buhne 8",
					Location = new Location(53.49871, 8.15321)
				});
			Points.Add(
				new Point
				{
					Name = "Buhne 10",
					Location = new Location(53.49350, 8.15563)
				});
			Points.Add(
				new Point
				{
					Name = "Moving",
					Location = new Location(53.5, 8.25)
				});

			Pushpins = new ObservableCollection<Point>();
			Pushpins.Add(
				new Point
				{
					Name = "WHV - Eckwarderhörne",
					Location = new Location(53.5495, 8.1877)
				});
			Pushpins.Add(
				new Point
				{
					Name = "JadeWeserPort",
					Location = new Location(53.5914, 8.14)
				});
			Pushpins.Add(
				new Point
				{
					Name = "Kurhaus Dangast",
					Location = new Location(53.447, 8.1114)
				});
			Pushpins.Add(
				new Point
				{
					Name = "Eckwarderhörne",
					Location = new Location(53.5207, 8.2323)
				});

			Polylines = new ObservableCollection<Polyline>();
			Polylines.Add(
				new Polyline
				{
					Locations = LocationCollection.Parse("53.5140,8.1451 53.5123,8.1506 53.5156,8.1623 53.5276,8.1757 53.5491,8.1852 53.5495,8.1877 53.5426,8.1993 53.5184,8.2219 53.5182,8.2386 53.5195,8.2387")
				});
			Polylines.Add(
				new Polyline
				{
					Locations = LocationCollection.Parse("53.5978,8.1212 53.6018,8.1494 53.5859,8.1554 53.5852,8.1531 53.5841,8.1539 53.5802,8.1392 53.5826,8.1309 53.5867,8.1317 53.5978,8.1212")
				});

			var timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(0.1)
			};

			timer.Tick += (s, e) =>
			{
				var p = Points.Last();
				p.Location = new Location(p.Location.Latitude + 0.001, p.Location.Longitude + 0.002);

				if (p.Location.Latitude > 54d)
				{
					p.Name = "Stopped";
					((DispatcherTimer)s).Stop();
				}
			};

			timer.Start();
		}

		private List<Location> DecodePolylinePoints(string encodedPoints)
		{
			if (encodedPoints == null || encodedPoints == "") return null;
			List<Location> poly = new List<Location>();
			char[] polylinechars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			try
			{
				while (index < polylinechars.Length)
				{
					// calculate next latitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length)
						break;

					currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

					//calculate next longitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length && next5bits >= 32)
						break;

					currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
					Location p = new Location();
					p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
					p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
					poly.Add(p);
				}
			}
			catch (Exception ex)
			{
				// logo it
			}
			return poly;
		}
	}
}
