using System;
using Master;

namespace share_master_page
{
	public partial class _Default : System.Web.UI.Page
	{
		protected override void OnPreInit(EventArgs e)
		{
			MasterPageFile = MasterPageVirtualPathProvider.MasterPageFileLocation;
			base.OnPreInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}
