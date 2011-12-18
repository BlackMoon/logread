using System;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for PageCompressor
/// </summary>
public class PageCompressorModule : IHttpModule
{

	#region IHttpModule Members

	public void Dispose()
	{
		// Nothing to dispose
	}

	public void Init(HttpApplication context)
	{
		context.PostRequestHandlerExecute += new EventHandler(context_PostRequestHandlerExecute);
	}

	void context_PostRequestHandlerExecute(object sender, EventArgs e)
	{
		HttpContext context = ((HttpApplication)sender).Context;
		if (context.CurrentHandler is Page)
		{
            PageOptimizer.Compress(context);
		}
	}

	#endregion
}
