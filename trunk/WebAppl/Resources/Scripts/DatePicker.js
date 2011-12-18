// JScript File

    function OnFocus(sender)
    {
        sender.TextBoxElement.style["backgroundColor"] = "white";
    }

    function OnBlur(sender)
    {
        if(sender.TextBoxElement.value == "")
        {
            sender.TextBoxElement.style["backgroundColor"] = "yellow";
        }
        else
        {
            sender.TextBoxElement.style["backgroundColor"] = "white";  
        }
    }
    
    function OnLoad(sender)
    {
        if(sender.TextBoxElement.value == "")
        {
            sender.TextBoxElement.style["backgroundColor"] = "yellow";
        }
        else
        {
            sender.TextBoxElement.style["backgroundColor"] = "white";  
        }
    }