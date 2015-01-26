using PortableRest;

namespace Hitchhiker.WinPhone
{
	public class HitchhikerClient : RestClient
	{
		public HitchhikerClient()
		{
			this.BaseUrl = "http://localhost:13011/";
		}
	}
}