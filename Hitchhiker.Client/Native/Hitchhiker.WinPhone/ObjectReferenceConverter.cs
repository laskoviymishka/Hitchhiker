using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Hitchhiker.WinPhone
{
	public class ObjectReferenceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (targetType == typeof(Visibility))
			{
				return value != null ? Visibility.Visible : Visibility.Collapsed;
			}

			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotSupportedException();
		}
	}
}