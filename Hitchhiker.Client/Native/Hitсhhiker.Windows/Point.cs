using MapControl;

namespace ViewModel
{
	public class Point : Base
	{
		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				RaisePropertyChanged("Name");
			}
		}

		private Location location;
		public Location Location
		{
			get { return location; }
			set
			{
				location = value;
				RaisePropertyChanged("Location");
			}
		}
	}
}