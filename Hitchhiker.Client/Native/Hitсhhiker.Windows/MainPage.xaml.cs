using Hitchhiker.ServiceModel;
using MapControl;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			//TileImageLoader.Cache = new MapControl.Caching.ImageFileCache();
			//TileImageLoader.Cache = new MapControl.Caching.FileDbCache();
			//BingMapsTileLayer.ApiKey = "...";

			this.InitializeComponent();
		}

		private void ImageOpacitySliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (mapImage != null)
			{
				mapImage.Opacity = e.NewValue / 100;
			}
		}

		private void SeamarksChecked(object sender, RoutedEventArgs e)
		{
			map.TileLayers.Add(((TileLayerCollection)Resources["TileLayers"])["Seamarks"]);
		}

		private void SeamarksUnchecked(object sender, RoutedEventArgs e)
		{
			map.TileLayers.Remove(((TileLayerCollection)Resources["TileLayers"])["Seamarks"]);
		}

		private void Map_OnTapped(object sender, TappedRoutedEventArgs e)
		{
		}

		private void Map_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
		{
		}
	}
}
