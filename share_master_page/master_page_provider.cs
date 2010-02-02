using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Master
{
	public class MasterPageVirtualFile : VirtualFile
	{
		private string virPath;

		public MasterPageVirtualFile(string virtualPath)
			: base(virtualPath)
		{
			this.virPath = virtualPath;
		}

		public override Stream Open() {
			if (!(HttpContext.Current == null))
			{
				if (HttpContext.Current.Cache[virPath] == null)
				{
					HttpContext.Current.Cache.Insert(virPath, ReadResource(virPath));
				}
				return (Stream)HttpContext.Current.Cache[virPath];
			}
			else
			{
				return ReadResource(virPath);
			}
		}

		private static Stream ReadResource(string embeddedFileName)
		{
			string resourceFileName = VirtualPathUtility.GetFileName(embeddedFileName);
			Assembly assembly = Assembly.GetExecutingAssembly();
			return assembly.GetManifestResourceStream(MasterPageVirtualPathProvider.VirtualPathProviderResourceLocation + "." + resourceFileName);
		}
	}

	public class MasterPageVirtualPathProvider : System.Web.Hosting.VirtualPathProvider
	{
		public const string MasterPageFileLocation = "~/MasterPageDir/Site1.master";
		public const string VirtualPathProviderResourceLocation = "share_master_page";
		public const string VirtualMasterPagePath = "~/MasterPageDir/";

		public override bool FileExists(string virtualPath)
		{
			if (IsPathVirtual(virtualPath))
			{
				MasterPageVirtualFile file = (MasterPageVirtualFile)GetFile(virtualPath);
				return (file == null) ? false : true;
			}
			else
			{
				return Previous.FileExists(virtualPath);
			}
		}

		public override VirtualFile GetFile(string virtualPath)
		{
			if (IsPathVirtual(virtualPath))
			{
				return new MasterPageVirtualFile(virtualPath);
			}
			else
			{
				return Previous.GetFile(virtualPath);
			}
		}

		public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return null;
		}

		private static bool IsPathVirtual(string virtualPath)
		{
			String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
			return checkPath.StartsWith(VirtualMasterPagePath, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}