    function OnClientFocus(sender)
    {
        sender.InputDomElement.style["backgroundColor"] = "white";
    }

    function OnClientBlur(sender)
    {
        if(sender.SelectedItem.Value == null)
        {
            sender.InputDomElement.style["backgroundColor"] = "yellow";
        }
        else
        {
            sender.InputDomElement.style["backgroundColor"] = "white";  
        }
    }