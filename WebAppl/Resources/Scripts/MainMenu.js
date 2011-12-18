// JScript File

function OnMenuClick(sender,args)
{
    var item = args.Item;
    var url = item.Attributes["URL"];
    var ClassName = item.Attributes["ClassName"];
    
    if (url != null)
    {
        ShowURL(url);
    }
    else
    {
        if (ClassName != null)
        {
            ShowClass(ClassName);
        }
    }
}

function ShowURL(Url)
{
    var name = generateGuid();
    
    var UrlString = "MainForm.aspx?Form=" + Url + "&ID=" + name;
    
    var oWnd = window.radopen(UrlString, null);
    oWnd.Argument = name;
    
	return false;
}

function ShowClass(ClassName)
{
    var name = generateGuid();
    
    var UrlString = "MainForm.aspx?ClassName=" + escape(ClassName) + "&ID=" + name;
    
    var oWnd = window.radopen(UrlString, null);
    oWnd.Argument = name;
    
	return false;
}