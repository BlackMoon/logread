// JScript File

function CloseAndRebindButtonEdit(newValue)
{
	GetRadWindow().BrowserWindow.RebindButtonEdit(newValue);
	GetRadWindow().Close();
};

function CloseAndRebindGrid(gridClientID)
{
	GetRadWindow().BrowserWindow.RebindGrid(gridClientID);
	GetRadWindow().Close();
};

function Close(sessionParam)
{
    var radWindow = GetRadWindow();
    
    try
    {
	    var sessionParam = radWindow.Argument;
        
        if( radWindow.BrowserWindow.AjaxMethods != null )
        {
            radWindow.BrowserWindow.AjaxMethods.RemoveListFromSession( sessionParam );
        }
	}
	finally
	{
	    radWindow.Argument = null;
    	
        radWindow.Close();
	}
};

function CloseWithConfirm()
{
    if( !confirm('В случае закрытия окна все данные будут потеряны.Продолжить редактирование?') )
    {
        Close();
    }
};

function OnClientClose(radWindow)
{
	if( radWindow != null && radWindow.Argument != null )
	{
		var sessionParam = radWindow.Argument;
	    
	    try
	    {
		    AjaxMethods.RemoveListFromSession( sessionParam );
		}
		catch(e)
		{
		}
		
		try
	    {
		    AjaxMethods.RemoveBlock( sessionParam );
		}
		catch(e)
		{
		}
    }
};

function GetRadWindow()
{
    var oWindow = null;
    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)
		
    return oWindow;
};

function ResizeWindow(Width, Height)
{
    var oWnd = GetRadWindow();
    
    if( oWnd.IsVisible() )
    {
        oWnd.SetSize(Width,Height);
        oWnd.Restore();
        oWnd.Center();
    }
};

function generateGuid()
{
    var result, i, j;
    result = '';
    for(j=0; j<32; j++)
    {
        i = Math.floor(Math.random()*16).toString(16).toUpperCase();
        result = result + i;
    }
    return result
};

function CreateConfirmOnWinClose()
{
    var oWnd = GetRadWindow();

	//onclick event
	var CloseButton = oWnd.BrowserWindow.document.getElementById("CloseButton" + oWnd.Id);

    if( CloseButton != null )
    {
        CloseButton.onclick = function()
            {
                CloseWithConfirm();
            }
    }
};

function ShowForm(Url, SessionParam, Params)
{
    var id = generateGuid();
    
    var oWnd = window.radopen("MainForm.aspx?ID=" + id + "&Form=" + escape(Url) + "&SessionParam=" + escape(SessionParam) + "&Params=" + escape(Params) );
    oWnd.Argument = id;
    
    SetSavedSize(oWnd, Url);
}

function Redirect( Url )
{
    var oWnd = GetRadWindow();
    
    oWnd.SetUrl( Url );
};