#region Using

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

#endregion

/// <summary>
/// Summary description for CssCompressor
/// </summary>
public class CssCompressorHandler : IHttpHandler
{
	public bool IsReusable
	{
		get { return false; }
	}

	public void ProcessRequest(HttpContext context)
	{
		if (!string.IsNullOrEmpty(context.Request.QueryString["stylesheets"]))
		{
			string[] relativeFiles = context.Request.QueryString["stylesheets"].Split(',');
			string[] absoluteFiles = new string[relativeFiles.Length];

			for (int i = 0; i < relativeFiles.Length; i++)
			{
				string file = relativeFiles[i];
				if (file.EndsWith(".css"))
				{
                    file = file.Replace("#ThemePath#", "Resources_Design/" + ApplicationManager.GetProjectName());

					string absoluteFile = context.Server.MapPath(file);
					WriteContent(context, absoluteFile);
					absoluteFiles[i] = absoluteFile;
				}
			}

			SetHeaders(context, absoluteFiles);
			PageOptimizer.Compress(context);
		}
	}

	/// <summary>
	/// Writes the content of the individual stylesheets to the response stream.
	/// </summary>
	private void WriteContent(HttpContext context, string file)
	{
		using (StreamReader reader = new StreamReader(file))
		{
            //string body = reader.ReadToEnd();
            //body = StripWhitespace(body);
            //body = CheckUrl(body, file);
            //context.Response.Write(body);

            Miron.Web.MbCompression.CssMinifier cssMinifier = new Miron.Web.MbCompression.CssMinifier();
            string body = cssMinifier.Minify(reader);

            body = CheckUrl(body, file);
            context.Response.Write(body);
		}
	}

    private string CheckUrl(string body, string file)
    {
        if (file.Contains("Resources_Design"))
        {
            string ResourcesPath = "Resources_Design/" + ApplicationManager.GetProjectName();

            body = body.Replace("url('./", "url('" + ResourcesPath + "/");
        }

        return body;
    }

	/// <summary>
	/// Strips the whitespace from any .css file.
	/// </summary>
	private static string StripWhitespace(string body)
	{
		body = body.Replace("  ", " ");
		body = body.Replace(Environment.NewLine, String.Empty);
		body = body.Replace("\t", string.Empty);
		body = body.Replace(" {", "{");
		body = body.Replace(" :", ":");
		body = body.Replace(": ", ":");
		body = body.Replace(", ", ",");
		body = body.Replace("; ", ";");
		body = body.Replace(";}", "}");

		// sometimes found when retrieving CSS remotely
		body = body.Replace(@"?", string.Empty);

		//body = Regex.Replace(body, @"/\*[^\*]*\*+([^/\*]*\*+)*/", "$1");
		body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);

		//Remove comments from CSS
		body = Regex.Replace(body, @"/\*[\d\D]*?\*/", string.Empty);

		return body;
	}

	/// <summary>
	/// This will make the browser and server keep the output
	/// in its cache and thereby improve performance.
	/// </summary>
	private static void SetHeaders(HttpContext context, string[] files)
	{		
		context.Response.ContentType = "text/css";
		//return;
		context.Response.AddFileDependencies(files);
		context.Response.Cache.VaryByParams["stylesheets"] = true;
		context.Response.Cache.SetETagFromFileDependencies();
		context.Response.Cache.SetLastModifiedFromFileDependencies();
		context.Response.Cache.SetValidUntilExpires(true);
		context.Response.Cache.SetExpires(DateTime.Now.AddDays(7));
		context.Response.Cache.SetCacheability(HttpCacheability.Public);
	}
}