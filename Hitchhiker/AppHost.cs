using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using Hitchhiker.ServiceInterface;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Razor;
using ServiceStack;

namespace Hitchhiker
{
	public class AppHost : AppHostBase
	{
		/// <summary>
		/// Default constructor.
		/// Base constructor requires a name and assembly to locate web service classes. 
		/// </summary>
		public AppHost()
			: base("Hitchhiker", typeof(MyServices).Assembly)
		{
		}

		/// <summary>
		/// Application specific configuration
		/// This method should initialize any IoC resources utilized by your web service classes.
		/// </summary>
		/// <param name="container"></param>
		public override void Configure(Container container)
		{
			//Config examples
			//this.Plugins.Add(new PostmanFeature());
			//this.Plugins.Add(new CorsFeature());

			this.Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[]
			{
				new BasicAuthProvider(),
 				new CredentialsAuthProvider(), 
			}));

			this.Plugins.Add(new RegistrationFeature());
			var userRepository = new InMemoryAuthRepository();
			container.Register<IAuthRepository>(userRepository);
			container.Register<ICacheClient>(new MemoryCacheClient());

			this.Plugins.Add(new RazorFormat());
		}
	}
}