// JScript File
var ButtonEditID;

function RebindButtonEdit( newValue )
{
    var ButtonEdit = window[ ButtonEditID ];
    
    // Для отключения постбака
    ButtonEdit.InitialValue = newValue;
    ButtonEdit.SetValue( newValue );
}

function OnButtonEditClick( TypeName, SessionParam, ButtonEdit )
{
    if( TypeName != null )
    {
        var id = generateGuid();
        
        ButtonEditID = ButtonEdit;
        var oWnd = window.radopen( "MainForm.aspx?IsChoose=" + escape( SessionParam ) + "&ClassName=" + escape( TypeName ), "" );
        oWnd.Argument = id;
    }
}

function OnButtonClearClick( ButtonEditClientID )
{
    var ButtonEdit = window[ ButtonEditClientID ];
    ButtonEdit.InitialValue = "";
    ButtonEdit.SetValue( "" );
}