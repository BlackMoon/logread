// JScript File

// fixPNG(); http://www.tigir.com/js/fixpng.js (author Tigirlas Igor)
function fixPNG(element)
{
	if (/MSIE (5\.5|6).+Win/.test(navigator.userAgent))
	{
		var src;
		
		if (element.tagName=='IMG')
		{
			if (/\.png$/.test(element.src))
			{
				src = element.src;
				element.src = "";
				//element.src = "Resources/Images/Common/blank.gif";
			}
		}
		
		if (src)
		{
		    element.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "',sizingMethod='scale')";
		}
	}
}