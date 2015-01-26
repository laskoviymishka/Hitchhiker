using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using Hitchhiker.ServiceModel;

namespace Hitchhiker.ServiceInterface
{
	public class MyServices : Service
	{
		public object Any(Hello request)
		{
			return new HelloResponse { Result = "Hello, {0}! You are {1}".Fmt(request.Name, this.GetSession().DisplayName) };
		}
	}
}