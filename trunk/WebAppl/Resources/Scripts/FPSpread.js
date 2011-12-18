// JScript File

var ActiveSheetViewID;

function RebindSheets()
{
    var шапка = document.getElementById("ctl02_вебЭкраннаяФорма_наборТаблицШапка");

    шапка.CallBack("Update");
    
    var вкладки = document.getElementById("ctl02_вебЭкраннаяФорма_наборТаблицФормы");

    вкладки.CallBack("Update");
}

function OpenDictionary(Url, SessionParam, Params, SheetViewID)
{
    ActiveSheetViewID = SheetViewID;
    
    var id = "DictionaryForm";
    
    var oWnd = window.radopen("Forms/Dictionary/Dictionary.aspx?ID=" + id + "&SessionParam=" + escape( SessionParam ) + "&Params=" + escape( Params ) );
    oWnd.Argument = id;
    
    SetSavedSize(oWnd, Url);
}

function CloseAndRebindSheet()
{
    var wind = GetRadWindow().BrowserWindow;
   
    GetRadWindow().Close();

    wind.RebindSheets();
}