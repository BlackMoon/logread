// General

function escapeString(value)
{
	return value.replace("'", "\\'");
}

function deriveID(clientID, elementName)
{
	return clientID + "_" + elementName;
}

function evalSharedState(elementID)
{
	element = document.getElementById(elementID);
	
	return eval(String(element.value));
}

function associateObjectWithEvent(object, methodName)
{
	return (function(event) {event = event || window.event; return object[methodName] (event, this);})
}

function getObjectByID(ID)
{
	try
	{
		instance = eval(ID);
	}
	catch(e)
	{
		instance = null;
	}
	
	return instance;
}

function isInElement(element, container)
{
	inElement = (element == container);
	
	if (!inElement)
	{
		owner = element.parentNode;
		
		while (owner != null)
		{
			inElement = (owner == container);
			 
			if (inElement)
			{
				owner = null;
			}
			else
			{
				owner = owner.parentNode;
			}
		}
	}
	
	return inElement;
}

function elementHasMarkerClass(element)
{
	markerClassName = "ribbonPart";
	hasMarker = (element.className.indexOf(markerClassName) != -1);
		
	return hasMarker;
}

function cancelEventBubble(evt)
{
	evt.cancelBubble = true;
	
	if (evt.stopPropagation)
	{
		evt.stopPropagation();
	}
}

// Application Bar

function ApplicationBar(clientID)
{
	this.clientID = clientID;
	this.sharedStateID = deriveID(clientID, "SharedState");
	this.textID = deriveID(clientID, "Text");
	this.tabStrip = null;

	this.refreshSharedState();
}

ApplicationBar.prototype.getApplicationName = function() 
{ 
	return String(this.sharedState[0]); 
}

ApplicationBar.prototype.setApplicationName = function(name) 
{ 
	this.sharedState[0] = name; 
	this.updateSharedState(); 
	this.refresh();
}

ApplicationBar.prototype.getDocumentName = function() 
{ 
	return String(this.sharedState[1]); 
}

ApplicationBar.prototype.setDocumentName = function(name) 
{ 
	this.sharedState[1] = name; 
	this.updateSharedState(); 
	this.refresh();
}

ApplicationBar.prototype.getApplicationMenuID = function() 
{ 
	return String(this.sharedState[2]); 
}

ApplicationBar.prototype.getRibbonTabStripID = function() 
{ 
	return String(this.sharedState[3]); 
}

ApplicationBar.prototype.getRibbonMultiPageViewID = function() 
{ 
	return String(this.sharedState[4]); 
}

ApplicationBar.prototype.getCustomizeMenuID = function() 
{ 
	return String(this.sharedState[5]); 
}

ApplicationBar.prototype.getMinimizeRibbon = function() 
{ 
	return Boolean(this.sharedState[6]); 
}

ApplicationBar.prototype.setMinimizeRibbon = function(minimize) 
{ 
	this.setRibbonOpen(!minimize);	
	this.sharedState[6] = minimize; 
	this.updateSharedState(); 
	this.setAppearance();
}

ApplicationBar.prototype.getRibbonOpen = function() 
{ 
	return Boolean(this.sharedState[7]); 
}

ApplicationBar.prototype.setRibbonOpen = function(open) 
{ 
	this.sharedState[7] = open; 
	this.updateSharedState(); 
}

ApplicationBar.prototype.getOriginallySelectedTabIndex = function()
{
	return Number(this.sharedState[8]); 
}

ApplicationBar.prototype.getOriginallySelectedTab = function()
{
	tab = null;
	
	if (this.tabStrip != null)
	{
		tab = this.tabStrip.GetAllTabs()[this.getOriginallySelectedTabIndex()];
	}
	
	return tab;
}

ApplicationBar.prototype.setOriginallySelectedTab = function(tab)
{
 	index = -1;
	
	if (tab != null)
	{
		index = tab.Index;
	}
	
	this.sharedState[8] = index;
	this.updateSharedState();
}

ApplicationBar.prototype.refresh = function() 
{
	text = ""; 
	applicationName = this.getApplicationName(); 
	documentName = this.getDocumentName(); 

	if ((documentName != null) && (documentName != "")) 
	{
		text += documentName;
	}
	
	if ((documentName != null) && (documentName != "") && (applicationName != null) && (applicationName != "")) 
	{
		text += " - ";
	}
	
	if ((applicationName != null) && (applicationName != "")) 
	{ 
		text += applicationName; 
	} 
	
	document.getElementById(this.textID).innerHTML = text; 
}

ApplicationBar.prototype.showRibbon = function()
{
	this.setRibbonOpen(true);
	this.updateSharedState(); 
	this.setAppearance();
}

ApplicationBar.prototype.hideRibbon = function()
{
	if (this.getMinimizeRibbon())
	{
		this.setRibbonOpen(false);
		this.updateSharedState(); 
		this.setAppearance();
	}
}

ApplicationBar.prototype.showCustomizeMenu = function(evt) 
{		
	menu = getObjectByID(this.getCustomizeMenuID());

	if ((!evt.relatedTarget) || (!menu.IsChildOf(menu.DomElement, evt.relatedTarget)))
	{
		menu.Show(evt);
	}
	
	cancelEventBubble(evt);
}

ApplicationBar.prototype.setAppearance = function()
{	
	if (this.tabStrip != null)
	{
		if (this.getMinimizeRibbon())
		{
			tab = this.tabStrip.SelectedTab;
			
			if (tab == null)
			{
				tab = this.getOriginallySelectedTab();
			}
		
			if (tab != null)
			{
				multiPageView = document.getElementById(this.getRibbonMultiPageViewID());
				
				if (this.getRibbonOpen())
				{					
					multiPageView.className = "ribbonBarFloat";
				}
				else
				{	
					this.setOriginallySelectedTab(tab);
					
					tab.UnSelect();
					multiPageView.className = "ribbonBarClosed";					
				}
			}
		}
		else
		{
			originallySelectedTab = this.getOriginallySelectedTab();
			
			if (originallySelectedTab != null)
			{
				multiPageView = document.getElementById(this.getRibbonMultiPageViewID());
				
				multiPageView.className = "ribbonBar";
				originallySelectedTab.Select();
			}
		}
	}
}

ApplicationBar.prototype.tabSelected = function(sender, eventArgs)
{
	if ((this.getMinimizeRibbon()) && (!this.getRibbonOpen()))
	{
		this.showRibbon();
	}
	
	this.setOriginallySelectedTab(this.tabStrip.SelectedTab);
}

ApplicationBar.prototype.dismissRibbon = function(evt)
{
	if ((this.isClickAway(evt)) && (this.getMinimizeRibbon()) && (this.getRibbonOpen()))
	{
		this.hideRibbon();
	}
}

ApplicationBar.prototype.isClickAway = function(evt)
{	
	clickedAway = false;
	tabStripId = this.getRibbonTabStripID();
	
	if (tabStripId != null)
	{
		tabStripElement = document.getElementById(tabStripId);
		tab = this.tabStrip.SelectedTab;
		
		if (tab != null)
		{
			eventTarget = (typeof(evt.target) != 'undefined') ? evt.target : evt.srcElement;  // FF:IE
			clickedOn = this.isPartOfRibbon(eventTarget, tabStripElement);
			clickedAway = !clickedOn;
		}
	}
	
	return clickedAway;
}

ApplicationBar.prototype.isPartOfRibbon = function(eventTarget, tabStripElement)
{	
	isRibbonPart = ((isInElement(eventTarget, tabStripElement)) || 
		(elementHasMarkerClass(eventTarget)));
	
	return isRibbonPart;
}

ApplicationBar.prototype.refreshSharedState = function()
{
	this.sharedState = evalSharedState(this.sharedStateID);
	
	this.tabStrip = getObjectByID(this.getRibbonTabStripID());

	if (this.tabStrip != null)
	{
		this.tabStrip.OnClientTabSelected = associateObjectWithEvent(this, "tabSelected");
		document.onclick = associateObjectWithEvent(this, "dismissRibbon");
	}
}

ApplicationBar.prototype.updateSharedState = function() 
{ 
	value = "['" + escapeString(this.sharedState[0]) + "','" + escapeString(this.sharedState[1]) + "','" + escapeString(this.sharedState[2]) + "','" + escapeString(this.sharedState[3]) + "','" + escapeString(this.sharedState[4]) + "','" + escapeString(this.sharedState[5]) + "'," + this.sharedState[6] + "," + this.sharedState[7] + "," + this.sharedState[8] + "]"; 
	document.getElementById(this.sharedStateID).value = value; 
}


// Quick Access Ribbon Button

function QuickAccessRibbonButton(clientID)
{
	this.clientID = clientID;
	this.sharedStateID = deriveID(clientID, "SharedState");
	this.imageID = deriveID(clientID, "Image");
	
	this.refreshSharedState();
}

QuickAccessRibbonButton.prototype.mouseOver = function(event, element) 
{ 
	this.highlight(true);
}

QuickAccessRibbonButton.prototype.mouseOut = function(event, element) 
{ 
	this.highlight(false);
}	
	
QuickAccessRibbonButton.prototype.getEnabled = function() 
{ 
	return Boolean(this.sharedState[0]);
}

QuickAccessRibbonButton.prototype.setEnabled = function(enabled) 
{ 
	this.sharedState[0] = enabled; 
	this.updateSharedState(); 	
	this.setAppearance(false);
}

QuickAccessRibbonButton.prototype.getChecked = function() 
{ 
	return Boolean(this.sharedState[1]); 
}

QuickAccessRibbonButton.prototype.setChecked = function(checked) 
{ 
	this.sharedState[1] = checked; 
	this.updateSharedState(); 	
	this.setAppearance(false);
}

QuickAccessRibbonButton.prototype.getEnabledImageUrl = function() 
{ 
	return String(this.sharedState[2]); 
}

QuickAccessRibbonButton.prototype.getDisabledImageUrl = function() 
{ 
	return String(this.sharedState[3]); 
}

QuickAccessRibbonButton.prototype.highlight = function(over)
{
	this.setAppearance(over);
}

QuickAccessRibbonButton.prototype.setAppearance = function(over)
{
	element = document.getElementById(this.clientID);
	imageElement = document.getElementById(this.imageID);
	
	enabled = this.getEnabled();
	checked = this.getChecked();
	
	if ((enabled) && (over))
	{
		element.className = "quickAccessButtonHover";
	}
	else if ((enabled) && (checked))
	{
		element.className = "quickAccessButtonChecked";
	}
	else
	{
		element.className = "quickAccessButton";
	}
	
	if (enabled)
	{
		imageElement.src = this.getEnabledImageUrl();
	}
	else
	{
		imageElement.src = this.getDisabledImageUrl();
	}
}

QuickAccessRibbonButton.prototype.refreshSharedState = function()
{
	this.sharedState = evalSharedState(this.sharedStateID);
	
	element = document.getElementById(this.clientID);
	element.onmouseover = associateObjectWithEvent(this, "mouseOver");
	element.onmouseout = associateObjectWithEvent(this, "mouseOut");
}

QuickAccessRibbonButton.prototype.updateSharedState = function() 
{ 
	value = "[" + this.sharedState[0] + "," + this.sharedState[1] + ",'" + escapeString(this.sharedState[2]) + "','" + escapeString(this.sharedState[3]) + "']"; 
	document.getElementById(this.sharedStateID).value = value; 
}


// Ribbon Group

function RibbonGroup(clientID)
{
	this.clientID = clientID;
	this.sharedStateID = deriveID(clientID, "SharedState");
	this.textID = deriveID(clientID, "Text");
	
	this.refreshSharedState();
}
	
RibbonGroup.prototype.getText = function() 
{ 
	return String(this.sharedState[0]); 
}

RibbonGroup.prototype.setText = function(text) 
{ 
	this.sharedState[0] = text;
	this.updateSharedState();
	
	element = document.getElementById(this.textID);
	element.innerHTML = text;
}

RibbonGroup.prototype.refreshSharedState = function()
{
	this.sharedState = evalSharedState(this.sharedStateID);
}

RibbonGroup.prototype.updateSharedState = function() 
{ 
	value = "['" + escapeString(this.sharedState[0]) + "']"; 
	document.getElementById(this.sharedStateID).value = value; 
}


// Large Ribbon Button

function LargeRibbonButton(clientID)
{
	this.clientID = clientID;
	this.sharedStateID = deriveID(clientID, "SharedState");
	this.startID = deriveID(clientID, "Start");
	this.itemID = deriveID(clientID, "Item");
	this.endID = deriveID(clientID, "End");
	this.imageID = deriveID(clientID, "Image");
	this.dropDownImageID = deriveID(clientID, "DropDownMenuImage");
	this.textID = deriveID(clientID, "Text");
	
	this.refreshSharedState();
}

LargeRibbonButton.prototype.mouseOver = function(event, element) 
{ 
	this.highlight(true);
}

LargeRibbonButton.prototype.mouseOut = function(event, element) 
{ 
	this.highlight(false);
}

LargeRibbonButton.prototype.getEnabled = function() 
{ 
	return Boolean(this.sharedState[0]); 
}

LargeRibbonButton.prototype.setEnabled = function(enabled) 
{ 
	this.sharedState[0] = enabled; 
	this.updateSharedState(); 
	this.setAppearance(false);
}

LargeRibbonButton.prototype.getChecked = function() 
{ 
	return Boolean(this.sharedState[1]); 
}

LargeRibbonButton.prototype.setChecked = function(checked) 
{ 
	this.sharedState[1] = checked; 
	this.updateSharedState(); 	
	this.setAppearance(false);
}

LargeRibbonButton.prototype.getEnabledImageUrl = function() 
{ 
	return String(this.sharedState[2]); 
}

LargeRibbonButton.prototype.getDisabledImageUrl = function() 
{ 
	return String(this.sharedState[3]); 
}

LargeRibbonButton.prototype.getText = function() 
{ 
	return String(this.sharedState[4]); 
}

LargeRibbonButton.prototype.setText = function(text) 
{ 
	this.sharedState[4] = text;
	this.updateSharedState();
	this.setAppearance(false);
}

LargeRibbonButton.prototype.getDropDownMenuID = function() 
{ 
	return String(this.sharedState[5]); 
}

LargeRibbonButton.prototype.highlight = function(over)
{
	this.setAppearance(over);
}

LargeRibbonButton.prototype.showDropDownMenu = function(evt) 
{		
	menu = getObjectByID(this.getDropDownMenuID());

	if ((!evt.relatedTarget) || (!menu.IsChildOf(menu.DomElement, evt.relatedTarget)))
	{
		menu.Show(evt);
	}
	
	cancelEventBubble(evt);
}

LargeRibbonButton.prototype.setAppearance = function(over)
{	
	imageElement = document.getElementById(this.imageID);
	textElement = document.getElementById(this.textID);
	dropDownImageElement = document.getElementById(this.dropDownImageID);
	startElement = document.getElementById(this.startID);
	itemElement = document.getElementById(this.itemID);
	endElement = document.getElementById(this.endID);
	
	enabled = this.getEnabled();
	checked = this.getChecked();
	
	textElement.innerHTML = this.getText();
	
	if (enabled)
	{
		imageElement.src = this.getEnabledImageUrl();
		textElement.className = "largeRibbonButtonText";
		
		if (dropDownImageElement)
		{
			dropDownImageElement.className = "largeRibbonButtonDropDownMenuArrow";
		}
	}
	else
	{
		imageElement.src = this.getDisabledImageUrl();
		textElement.className = "largeRibbonButtonTextDisabled";
		
		if (dropDownImageElement)
		{
			dropDownImageElement.className = "largeRibbonButtonDropDownMenuArrowDisabled";
		}
	}
	
	if ((enabled) && (over))
	{
		startElement.className = "largeRibbonButtonStartHover";
		itemElement.className = "largeRibbonButtonItemHover";
		endElement.className = "largeRibbonButtonEndHover";
	}
	else if ((enabled) && (checked))
	{
		startElement.className = "largeRibbonButtonStartChecked";
		itemElement.className = "largeRibbonButtonItemChecked";
		endElement.className = "largeRibbonButtonEndChecked";
	}
	else
	{
		startElement.className = "largeRibbonButtonStart";
		itemElement.className = "largeRibbonButtonItem";
		endElement.className = "largeRibbonButtonEnd";
	}
}

LargeRibbonButton.prototype.refreshSharedState = function()
{	
	this.sharedState = evalSharedState(this.sharedStateID);
	
	element = document.getElementById(this.clientID);
	element.onmouseover = associateObjectWithEvent(this, "mouseOver");
	element.onmouseout = associateObjectWithEvent(this, "mouseOut");
}

LargeRibbonButton.prototype.updateSharedState = function() 
{ 
	value = "[" + this.sharedState[0] + "," + this.sharedState[1] + ",'" + escapeString(this.sharedState[2]) + "','" + escapeString(this.sharedState[3]) + "','" + escapeString(this.sharedState[4]) + "','" + escapeString(this.sharedState[5]) + "']";
	document.getElementById(this.sharedStateID).value = value; 
}
	

// Small Ribbon Button

function SmallRibbonButton(clientID)
{
	this.clientID = clientID;
	this.sharedStateID = deriveID(clientID, "SharedState");
	this.startID = deriveID(clientID, "Start");
	this.itemID = deriveID(clientID, "Item");
	this.endID = deriveID(clientID, "End");
	this.imageID = deriveID(clientID, "Image");
	this.dropDownImageID = deriveID(clientID, "DropDownMenuImage");
	this.textID = deriveID(clientID, "Text");
	
	this.refreshSharedState();
}

SmallRibbonButton.prototype.mouseOver = function(event, element) 
{ 
	this.highlight(true);
}

SmallRibbonButton.prototype.mouseOut = function(event, element) 
{ 
	this.highlight(false);
}

SmallRibbonButton.prototype.getEnabled = function() 
{ 
	return Boolean(this.sharedState[0]); 
}

SmallRibbonButton.prototype.setEnabled = function(enabled) 
{ 
	this.sharedState[0] = enabled; 
	this.updateSharedState(); 
	this.setAppearance(false);
}

SmallRibbonButton.prototype.getChecked = function() 
{ 
	return Boolean(this.sharedState[1]); 
}

SmallRibbonButton.prototype.setChecked = function(checked) 
{ 
	this.sharedState[1] = checked; 
	this.updateSharedState(); 	
	this.setAppearance(false);
}

SmallRibbonButton.prototype.getEnabledImageUrl = function() 
{ 
	return String(this.sharedState[2]); 
}

SmallRibbonButton.prototype.getDisabledImageUrl = function() 
{ 
	return String(this.sharedState[3]); 
}

SmallRibbonButton.prototype.getText = function() 
{ 
	return String(this.sharedState[4]); 
}

SmallRibbonButton.prototype.setText = function(text) 
{ 
	this.sharedState[4] = text;
	this.updateSharedState();
	this.setAppearance(false);
}

SmallRibbonButton.prototype.getDropDownMenuID = function() 
{ 
	return String(this.sharedState[5]); 
}

SmallRibbonButton.prototype.highlight = function(over)
{
	this.setAppearance(over);
}

SmallRibbonButton.prototype.showDropDownMenu = function(evt) 
{		
	menu = getObjectByID(this.getDropDownMenuID());

	if ((!evt.relatedTarget) || (!menu.IsChildOf(menu.DomElement, evt.relatedTarget)))
	{
		menu.Show(evt);
	}
	
	cancelEventBubble(evt);
}

SmallRibbonButton.prototype.setAppearance = function(over)
{	
	imageElement = document.getElementById(this.imageID);
	textElement = document.getElementById(this.textID);
	dropDownImageElement = document.getElementById(this.dropDownImageID);
	startElement = document.getElementById(this.startID);
	itemElement = document.getElementById(this.itemID);
	endElement = document.getElementById(this.endID);
	
	enabled = this.getEnabled();
	checked = this.getChecked();
	
	textElement.innerHTML = this.getText();
	
	if (enabled)
	{
		imageElement.src = this.getEnabledImageUrl();
		textElement.className = "smallRibbonButtonText";
		
		if (dropDownImageElement)
		{
			dropDownImageElement.className = "smallRibbonButtonDropDownMenuArrow";
		}
	}
	else
	{
		imageElement.src = this.getDisabledImageUrl();
		textElement.className = "smallRibbonButtonTextDisabled";
		
		if (dropDownImageElement)
		{
			dropDownImageElement.className = "smallRibbonButtonDropDownMenuArrowDisabled";
		}
	}
	
	if ((enabled) && (over))
	{
		startElement.className = "smallRibbonButtonStartHover";
		itemElement.className = "smallRibbonButtonItemHover";
		endElement.className = "smallRibbonButtonEndHover";
	}
	else if ((enabled) && (checked))
	{
		startElement.className = "smallRibbonButtonStartChecked";
		itemElement.className = "smallRibbonButtonItemChecked";
		endElement.className = "smallRibbonButtonEndChecked";
	}
	else
	{
		startElement.className = "smallRibbonButtonStart";
		itemElement.className = "smallRibbonButtonItem";
		endElement.className = "smallRibbonButtonEnd";
	}
}

SmallRibbonButton.prototype.refreshSharedState = function()
{	
	this.sharedState = evalSharedState(this.sharedStateID);
	
	element = document.getElementById(this.clientID);
	element.onmouseover = associateObjectWithEvent(this, "mouseOver");
	element.onmouseout = associateObjectWithEvent(this, "mouseOut");
}

SmallRibbonButton.prototype.updateSharedState = function() 
{ 
	value = "[" + this.sharedState[0] + "," + this.sharedState[1] + ",'" + escapeString(this.sharedState[2]) + "','" + escapeString(this.sharedState[3]) + "','" + escapeString(this.sharedState[4]) + "','" + escapeString(this.sharedState[5]) + "']";
	document.getElementById(this.sharedStateID).value = value; 
}
	
