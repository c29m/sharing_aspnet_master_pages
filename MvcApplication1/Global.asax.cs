using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Master;

namespace MvcApplication1
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			MasterPageVirtualPathProvider vpp = new MasterPageVirtualPathProvider();
			HostingEnvironment.RegisterVirtualPathProvider(vpp);

			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new ThemedViewEngine());

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}
	}

	public class ThemedViewEngine : WebFormViewEngine
	{
		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			masterName = MasterPageVirtualPathProvider.MasterPageFileLocation;
			return base.FindView(controllerContext, viewName, masterName, useCache);
		}
	}

}