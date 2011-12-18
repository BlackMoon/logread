// JScript File

//На создании грида устанавливаем высоту
function ClientHeightGridCreated(grid, height)
{
    var scrollArea = document.getElementById(grid.ClientID + "_GridData");
    scrollArea.style.height = height + "px";
};


//На создании грида регулируем его высоту
function GridCreated()
{
    var scrollArea = document.getElementById(this.ClientID + "_GridData");
    var dataHeight = this.MasterTableView.Control.clientHeight;

    if( dataHeight > 300 )
    {
        scrollArea.style.height = dataHeight + "px";
    }
    else
    {
        scrollArea.style.height = "300px";
    }
};

// Обновление таблицы
function RebindGrid(gridClientID)
{
    var grid = window[gridClientID]
    
    grid.AjaxRequest( grid.UniqueID, 'Rebind');
};

// Обработка двойного щелчка
function RowDblClick(index)
{
    var grid = window[this.OwnerID];
    
    grid.AjaxRequest(grid.UniqueID, "SelectItem:" + index);
};

// Обработчик запроса к гриду
function gridRequestStart(grid, eventArgs)
{
	try
	{
		if( (eventArgs.EventTargetElement != null && eventArgs.EventTargetElement.attributes["Exporting"]) != null && (eventArgs.EventTargetElement.attributes["Exporting"].nodeValue == "true") )
		{
			eventArgs.EnableAjax = false;
		}
	}
	catch( exc )
	{
	}
};

// Обработчик показа формы редкатирования
function ShowEditForm(Url, Index, SessionParam, gridClientID)
{
    var id = generateGuid();

    var oWnd = window.radopen("MainForm.aspx?ID=" + id + "&Form=" + escape(Url) + "&Index=" + Index + "&SessionParam=" + escape(SessionParam) + "&gridClientID=" + escape(gridClientID) );
    
    oWnd.Argument = id;
    
    SetSavedSize(oWnd, Url);
};

// Обработчик показа формы редактирования нового объекта
function ShowNewForm(Url, SessionParam, gridClientID)
{
    var id = generateGuid();

    var oWnd = window.radopen("MainForm.aspx?ID=" + id + "&Form=" + escape(Url) + "&SessionParam=" + escape(SessionParam) + "&gridClientID=" + escape(gridClientID) );
    
    oWnd.Argument = id;
    
    SetSavedSize(oWnd, Url);
};

function SetSavedSize(oWnd, Url)
{
    var width = ReadCookie(Url + "_width");
    var heigth = ReadCookie(Url + "_height");
    
    if( width != null && heigth != null && width != "" && heigth != "")
    {
        oWnd.SetSize(width,heigth);
        oWnd.Restore();
    }
};

//KeyCode - the code of the key pressed 
//IsShiftPressed - contains true or false, depending on whether the Shift key was held down, when the event fired 
//IsCtrlPressed - contains true or false, depending on whether the Ctrl key was held down, when the event fired 
//IsAltPressed - contains true or false, depending on whether the Alt key was held down, when the event fired 
//Event - the original browser event object
function OnKeyPress(Key) 
{
    if (Key.KeyCode == 45) // ins pressed
    {  
        var grid = window[this.ClientID];
        grid.AjaxRequest(grid.UniqueID, "InitInsert");
    }
};