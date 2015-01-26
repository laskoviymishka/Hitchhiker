using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using MapControl;

namespace Hitchhiker.WinPhone
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private bool mapCentered;

		public MainPage()
		{
			//BingMapsTileLayer.ApiKey = "...";

			InitializeComponent();

			DataContext = new ViewModel(Dispatcher);
			NavigationCacheMode = NavigationCacheMode.Required;
		}

		private void MapMenuItemClick(object sender, RoutedEventArgs e)
		{
			var tileLayers = (TileLayerCollection)Resources["TileLayers"];
			Map.TileLayer = tileLayers[(string)((FrameworkElement)sender).Tag];
		}

		private void SeamarksChecked(object sender, RoutedEventArgs e)
		{
			var tileLayers = (TileLayerCollection)Resources["TileLayers"];
			Map.TileLayers.Add(tileLayers["Seamarks"]);
		}

		private void SeamarksUnchecked(object sender, RoutedEventArgs e)
		{
			var tileLayers = (TileLayerCollection)Resources["TileLayers"];
			Map.TileLayers.Remove(tileLayers["Seamarks"]);
		}

		private void CenterButtonClick(object sender, RoutedEventArgs e)
		{
			Map.TargetCenter = ((ViewModel)DataContext).Location;
			mapCentered = true;
		}

		private void MapManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
		{
			mapCentered = false;
		}

		private void MapManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			if (mapCentered)
			{
				e.Complete();
			}
			else
			{
				Map.TransformMap(e.Position, e.Delta.Translation, e.Delta.Rotation, e.Delta.Scale);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
