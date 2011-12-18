using System;
using System.Web;
using System.Collections.Generic;
using System.IO.Compression;
using System.Web.UI.HtmlControls;
using System.Web.UI;

/// <summary>
/// Summary description for HttpCompressor
/// </summary>
public static class PageOptimizer
{

	#region HTTP compression

	private const string GZIP = "gzip";
	private const string DEFLATE = "deflate";

	public static void Compress(HttpContext context)
	{
		if (IsEncodingAccepted(GZIP))
		{
			context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
			SetEncoding(GZIP);
		}
		else if (IsEncodingAccepted(DEFLATE))
		{
			context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
			SetEncoding(DEFLATE);
		}
	}

	/// <summary>
	/// Checks the request headers to see if the specified
	/// encoding is accepted by the client.
	/// </summary>
	private static bool IsEncodingAccepted(string encoding)
	{
		return HttpContext.Current.Request.Headers["Accept-encoding"] != null && HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
	}

	/// <summary>
	/// Adds the specified encoding to the response headers.
	/// </summary>
	/// <param name="encoding"></param>
	private static void SetEncoding(string encoding)
	{
		HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
	}

	#endregion

	#region Script and CSS urls

    public static void CombineCss(Page Page)
    {
        List<HtmlLink> stylesheets = new List<HtmlLink>();
        foreach (Control control in Page.Header.Controls)
        {
            HtmlLink c = control as HtmlLink;

            if (c != null && c.Attributes["rel"] != null && c.Attributes["rel"].Equals("stylesheet", StringComparison.OrdinalIgnoreCase))
            {
                if (!c.Href.StartsWith("http://"))
                    stylesheets.Add(c);
            }
        }

        string[] paths = new string[stylesheets.Count];
        for (int i = 0; i < stylesheets.Count; i++)
        {
            Page.Header.Controls.Remove(stylesheets[i]);
            paths[i] = stylesheets[i].Href;
        }

        AddStylesheetsToHeader(Page, paths);
    }

    private static void AddStylesheetsToHeader(Page Page, string[] paths)
    {
        HtmlLink link = new HtmlLink();
        link.Attributes["rel"] = "stylesheet";
        link.Attributes["type"] = "text/css";
        link.Href = "~/css.axd?stylesheets=" + Page.Server.UrlEncode(string.Join(",", paths));
        Page.Header.Controls.Add(link);
    }

    public static void AddScriptsToHeader(Page Page, string[] paths)
    {
        HtmlGenericControl link = new HtmlGenericControl("script");
        link.Attributes["type"] = "text/javascript";
        link.Attributes["src"] = "js.axd?path=" + HttpUtility.UrlEncode(string.Join(",", paths));

        Page.Header.Controls.Add(link);
    }
	#endregion

}
