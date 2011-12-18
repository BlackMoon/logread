// JScript File

function OnTextEditKeyPress(sender, args)
{
    var text = sender.GetValue();

    if( text.length == 1 )
    {
        sender.SetValue( text.toUpperCase() );
    }
}