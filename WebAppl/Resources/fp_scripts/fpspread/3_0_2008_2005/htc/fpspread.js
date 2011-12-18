//
//	Copyright?2005. FarPoint Technologies.	All rights reserved.
//
var the_fpSpread = new Fpoint_FPSpread();
function FpSpread_EventHandlers(){
var e4=the_fpSpread;
this.TranslateKey=function (event){
e4.TranslateKey(event);
}
this.SetActiveSpread=function (event){
e4.SetActiveSpread(event);
}
this.MouseDown=function (event){
e4.MouseDown(event);
}
this.MouseUp=function (event){
e4.MouseUp(event);
}
this.MouseMove=function (event){
e4.MouseMove(event);
}
this.DblClick=function (event){
e4.DblClick(event);
}
this.HandleFirstKey=function (event){
e4.HandleFirstKey(event);
}
this.DoPropertyChange=function (event){
e4.DoPropertyChange(event);
}
this.CmdbarMouseOver=function (event){
e4.CmdbarMouseOver(event);
}
this.CmdbarMouseOut=function (event){
e4.CmdbarMouseOut(event);
}
this.ScrollViewport=function (event){
e4.ScrollViewport(event);
}
e4.AttachEvent(document,"keydown",this.TranslateKey,true);
e4.AttachEvent(document,"mousedown",this.SetActiveSpread,false);
e4.AttachEvent(document,"keyup",this.HandleFirstKey,true);
e4.AttachEvent(window,"resize",e4.DoResize,false);
this.AttachEvents=function (ss){
e4.AttachEvent(ss,"mousedown",this.MouseDown,false);
e4.AttachEvent(ss,"mouseup",this.MouseUp,false);
e4.AttachEvent(document,"mouseup",this.MouseUp,false);
e4.AttachEvent(ss,"mousemove",this.MouseMove,false);
e4.AttachEvent(ss,"dblclick",this.DblClick,false);
var e5=e4.GetViewport(ss);
if (e5!=null){
e4.AttachEvent(e4.GetViewport(ss).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e4.AttachEvent(e4.GetViewport(ss).parentNode,"scroll",this.ScrollViewport);
}
var e6=e4.GetCommandBar(ss);
if (e6!=null){
e4.AttachEvent(e6,"mouseover",this.CmdbarMouseOver,false);
e4.AttachEvent(e6,"mouseout",this.CmdbarMouseOut,false);
}
}
this.DetachEvents=function (ss){
e4.DetachEvent(ss,"mousedown",this.MouseDown,false);
e4.DetachEvent(ss,"mouseup",this.MouseUp,false);
e4.DetachEvent(document,"mouseup",this.MouseUp,false);
e4.DetachEvent(ss,"mousemove",this.MouseMove,false);
e4.DetachEvent(ss,"dblclick",this.DblClick,false);
var e5=e4.GetViewport(ss);
if (e5!=null){
e4.DetachEvent(e4.GetViewport(ss).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e4.DetachEvent(e4.GetViewport(ss).parentNode,"scroll",this.ScrollViewport);
}
var e6=e4.GetCommandBar(ss);
if (e6!=null){
e4.DetachEvent(e6,"mouseover",this.CmdbarMouseOver,false);
e4.DetachEvent(e6,"mouseout",this.CmdbarMouseOut,false);
}
}
}
function Fpoint_FPSpread(){
this.a7=false;
this.a8=false;
this.a9=null;
this.b0=null;
this.b1=null;
this.b2=-1;
this.b3=null;
this.b4=null;
this.b5=null;
this.b6=null;
this.b7=-1;
this.b8=-1;
this.b9=null;
this.c0=null;
this.c1=new Array();
this.error=false;
this.InitFields=function (ss){
if (this.b4==null)
this.b4=new this.Margin();
ss.c9=null;
ss.groupBar=null;
ss.d0=null;
ss.d1=null;
ss.d2=null;
ss.d3=null;
ss.d4=null;
ss.d5=null;
ss.d6=null;
ss.d7=null;
ss.d8="";
ss.d9=null;
ss.e3=false;
ss.slideLeft=0;
ss.slideRight=0;
ss.setAttribute("rowCount",0);
ss.setAttribute("colCount",0);
ss.e0=new Array();
ss.e1=new Array();
ss.e2=new Array();
this.activePager=null;
this.dragSlideBar=false;
ss.allowColMove=(ss.getAttribute("colMove")=="true");
ss.allowGroup=(ss.getAttribute("allowGroup")=="true");
}
this.RegisterSpread=function (ss){
var e7=this.GetTopSpread(ss);
if (e7!=ss)return ;
if (this.spreads==null){
this.spreads=new Array();
}
var e8=this.spreads.length;
for (var e9=0;e9<e8;e9++){
if (this.spreads[e9]==ss)return ;
}
this.spreads.length=e8+1;
this.spreads[e8]=ss;
}
this.Init=function (ss){
if (ss==null)alert("spread is not defined!");
ss.initialized=false;
this.b3=null;
this.c1=new Array();
this.RegisterSpread(ss);
this.InitFields(ss);
this.InitMethods(ss);
ss.c2=document.getElementById(ss.id+"_XMLDATA");
if (ss.c2==null){
ss.c2=document.createElement('XML');
ss.c2.id=ss.id+"_XMLDATA";
ss.c2.style.display="none";
document.body.insertBefore(ss.c2,null);
}
var f0=document.getElementById(ss.id+"_data");
if (f0!=null&&f0.getAttribute("data")!=null){
ss.c2.innerHTML=f0.getAttribute("data");
f0.value="";
}
this.SaveData(ss);
ss.c3=document.getElementById(ss.id+"_viewport");
if (ss.c3!=null){
ss.c4=ss.c3.parentNode;
}
ss.c5=document.getElementById(ss.id+"_corner");
if (ss.c5!=null&&ss.c5.childNodes.length>0){
ss.c5=ss.c5.getElementsByTagName("TABLE")[0];
}
ss.c6=document.getElementById(ss.id+"_rowHeader");
if (ss.c6!=null)ss.c6=ss.c6.getElementsByTagName("TABLE")[0];
ss.c7=document.getElementById(ss.id+"_colHeader");
if (ss.c7!=null)ss.c7=ss.c7.getElementsByTagName("TABLE")[0];
var c8=ss.c8=document.getElementById(ss.id+"_commandBar");
var f1=this.GetViewport(ss);
if (f1!=null){
ss.setAttribute("rowCount",f1.rows.length);
if (f1.rows.length==1)ss.setAttribute("rowCount",0);
ss.setAttribute("colCount",f1.getAttribute("cols"));
}
var e0=ss.e0;
var e2=ss.e2;
var e1=ss.e1;
this.InitSpan(ss,this.GetViewport(ss),e0);
this.InitSpan(ss,this.GetColHeader(ss),e2);
this.InitSpan(ss,this.GetRowHeader(ss),e1);
ss.style.overflow="hidden";
if (this.GetParentSpread(ss)==null){
this.LoadScrollbarState(ss);
var f2=this.GetData(ss);
var f3=f2.getElementsByTagName("root")[0];
var f4=f3.getElementsByTagName("activespread")[0];
if (f4!=null&&f4.innerHTML!=""){
this.SetPageActiveSpread(document.getElementById(this.Trim(f4.innerHTML)));
}
}
this.InitLayout(ss);
ss.e3=true;
if (this.GetPageActiveSpread()==ss&&(ss.getAttribute("AllowInsert")=="false"||ss.getAttribute("IsNewRow")=="true")){
var f5=this.GetCmdBtn(ss,"Insert");
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(ss,"Add");
this.UpdateCmdBtnState(f5,true);
}
this.CreateTextbox(ss);
this.CreateFocusBorder(ss);
this.InitSelection(ss);
ss.initialized=true;
if (this.GetPageActiveSpread()==ss)
{
try {
this.Focus(ss);
}catch (e){}
}
this.SaveData(ss);
if (this.handlers==null)
this.handlers=new FpSpread_EventHandlers();
this.handlers.DetachEvents(ss);
this.handlers.AttachEvents(ss);
if (c8!=null&&ss.style.position==""){
c8.parentNode.style.backgroundColor=c8.style.backgroundColor;
c8.parentNode.style.borderTop=c8.style.borderTop;
}
}
this.Dispose=function (ss){
if (this.handlers==null)
this.handlers=new FpSpread_EventHandlers();
this.handlers.DetachEvents(ss);
}
this.CmdbarMouseOver=function (event){
var f6=this.GetTarget(event);
if (f6!=null&&f6.tagName=="IMG"&&f6.getAttribute("disabled")!="true"){
f6.style.backgroundColor="cyan";
}
}
this.CmdbarMouseOut=function (event){
var f6=this.GetTarget(event);
if (f6!=null&&f6.tagName=="IMG"){
f6.style.backgroundColor="";
}
}
this.DoPropertyChange=function (event){
if (event.attrName=="curpos"){
this.ScrollViewport(event);
}else if (this.b5==null&&this.b6==null&&event.attrName=="pageincrement"&&event.ctrlKey){
var f7=this.GetSpread(this.GetTarget(event));
if (f7!=null)
this.SizeAll(this.GetTopSpread(f7));
}
}
this.HandleFirstKey=function (){
var f7=this.GetPageActiveSpread();
if (f7==null)return ;
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null&&f8.value!=""){
f8.value="";
}
}
this.IsXHTML=function (f7){
var e7=this.GetTopSpread(f7);
var f9=e7.getAttribute("strictMode");
return (f9!=null&&f9=="true");
}
this.GetData=function (f7){
return f7.c2;
}
this.AttachEvent=function (target,event,handler,useCapture){
if (target.addEventListener!=null){
target.addEventListener(event,handler,useCapture);
}else if (target.attachEvent!=null){
target.attachEvent("on"+event,handler);
}
}
this.DetachEvent=function (target,event,handler,useCapture){
if (target.removeEventListener!=null){
target.removeEventListener(event,handler,useCapture);
}else if (target.detachEvent!=null){
target.detachEvent("on"+event,handler);
}
}
this.CancelDefault=function (e){
if (e.preventDefault!=null){
e.preventDefault();
e.stopPropagation();
}else {
e.cancelBubble=false;
e.returnValue=false;
}
return false;
}
this.CreateEvent=function (name){
var g0=document.createEvent("Events")
g0.initEvent(name,true,true);
return g0;
}
this.Refresh=function (f7){
var f6=f7.style.display;
f7.style.display="none";
f7.style.display=f6;
}
this.InitMethods=function (f7){
var e4=this;
f7.Update=function (){e4.Update(this);}
f7.Cancel=function (){e4.Cancel(this);}
f7.Clear=function (){e4.Clear(this);}
f7.Copy=function (){e4.Copy(this);}
f7.Paste=function (){e4.Paste(this);}
f7.Prev=function (){e4.Prev(this);}
f7.Next=function (){e4.Next(this);}
f7.Add=function (){e4.Add(this);}
f7.Insert=function (){e4.Insert(this);}
f7.Delete=function (){e4.Delete(this);}
f7.Print=function (){e4.Print(this);}
f7.StartEdit=function (cell){e4.StartEdit(this,cell);}
f7.EndEdit=function (){e4.EndEdit(this);}
f7.ClearSelection=function (){e4.ClearSelection(this);}
f7.GetSelectedRange=function (){return e4.GetSelectedRange(this);}
f7.SetSelectedRange=function (r,c,rc,cc){e4.SetSelectedRange(this,r,c,rc,cc);}
f7.GetSelectedRanges=function (){return e4.GetSelectedRanges(this);}
f7.AddSelection=function (r,c,rc,cc){e4.AddSelection(this,r,c,rc,cc);}
f7.AddSpan=function (r,c,rc,cc,spans){e4.AddSpan(this,r,c,rc,cc,spans);}
f7.RemoveSpan=function (r,c,spans){e4.RemoveSpan(this,r,c,spans);}
f7.GetActiveRow=function (){var f6=e4.GetRowFromCell(this,this.d2);if (f6<0)return f6;return e4.GetSheetIndex(this,f6);}
f7.GetActiveCol=function (){return e4.GetColFromCell(this,this.d2);}
f7.SetActiveCell=function (r,c){e4.SetActiveCell(this,r,c);}
f7.GetCellByRowCol=function (r,c){return e4.GetCellByRowCol(this,r,c);}
f7.GetValue=function (r,c){return e4.GetValue(this,r,c);}
f7.SetValue=function (r,c,v,noEvent,recalc){e4.SetValue(this,r,c,v,noEvent,recalc);}
f7.GetFormula=function (r,c){return e4.GetFormula(this,r,c);}
f7.SetFormula=function (r,c,f,recalc,clientOnly){e4.SetFormula(this,r,c,f,recalc,clientOnly);}
f7.GetHiddenValue=function (r,colName){return e4.GetHiddenValue(this,r,colName);}
f7.GetSheetRowIndex=function (r){return e4.GetSheetRowIndex(this,r);}
f7.GetSheetColIndex=function (c){return e4.GetSheetColIndex(this,c);}
f7.GetRowCount=function (){return e4.GetRowCount(this);}
f7.GetColCount=function (){return e4.GetColCount(this);}
f7.GetTotalRowCount=function (){return e4.GetTotalRowCount(this);}
f7.GetPageCount=function (){return e4.GetPageCount(this);}
f7.GetParentSpread=function (){return e4.GetParentSpread(this);}
f7.GetChildSpread=function (r,ri){return e4.GetChildSpread(this,r,ri);}
f7.GetChildSpreads=function (){return e4.GetChildSpreads(this);}
f7.GetParentRowIndex=function (){return e4.GetParentRowIndex(this);}
f7.GetSpread=function (f6){return e4.GetSpread(f6);}
f7.UpdatePostbackData=function (){e4.UpdatePostbackData(this);}
f7.SizeToFit=function (c){e4.SizeToFit(this,c);}
f7.SetColWidth=function (c,w){e4.SetColWidth(this,c,w);}
f7.GetPreferredRowHeight=function (r){return e4.GetPreferredRowHeight(this,r);}
f7.SetRowHeight2=function (r,h){e4.SetRowHeight2(this,r,h);}
f7.CallBack=function (cmd,asyncCallBack){e4.SyncData(this.getAttribute("name"),cmd,this,asyncCallBack);}
f7.AddKeyMap=function (keyCode,ctrl,shift,alt,action){e4.AddKeyMap(this,keyCode,ctrl,shift,alt,action);}
f7.RemoveKeyMap=function (keyCode,ctrl,shift,alt){e4.RemoveKeyMap(this,keyCode,ctrl,shift,alt);}
f7.MoveToPrevCell=function (){e4.MoveToPrevCell(this);}
f7.MoveToNextCell=function (){e4.MoveToNextCell(this);}
f7.MoveToNextRow=function (){e4.MoveToNextRow(this);}
f7.MoveToPrevRow=function (){e4.MoveToPrevRow(this);}
f7.MoveToFirstColumn=function (){e4.MoveToFirstColumn(this);}
f7.MoveToLastColumn=function (){e4.MoveToLastColumn(this);}
f7.ScrollTo=function (r,c){e4.ScrollTo(this,r,c);}
f7.focus=function (){e4.Focus(this);}
f7.ProcessKeyMap=function (event){
if (this.keyMap!=null){
var e8=this.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=this.keyMap[e9];
if (event.keyCode==g1.key&&event.ctrlKey==g1.ctrl&&event.shiftKey==g1.shift&&event.altKey==g1.alt){
var g2=false;
if (typeof(g1.action)=="function")
g2=g1.action();
else 
g2=eval(g1.action);
return g2;
}
}
}
return true;
}
}
this.CreateTextbox=function (f7){
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (f8==null)
{
f8=document.createElement('INPUT');
f8.type="text";
f8.setAttribute("autocomplete","off");
f8.style.position="absolute";
f8.style.borderWidth=0;
f8.style.top="-10px";
f8.style.left="-100px";
f8.style.width="0px";
f8.style.height="1px";
if (f7.tabIndex!=null)
f8.tabIndex=f7.tabIndex;
f8.id=f7.id+"_textBox";
f7.insertBefore(f8,f7.firstChild);
}
}
this.CreateLineBorder=function (f7,id){
var g3=document.getElementById(id);
if (g3==null)
{
g3=document.createElement('div');
g3.style.position="absolute";
g3.style.left="-1000px";
g3.style.top="0px";
g3.style.overflow="hidden";
g3.style.border="1px solid black";
if (f7.getAttribute("FocusBorderColor")!=null)
g3.style.borderColor=f7.getAttribute("FocusBorderColor");
if (f7.getAttribute("FocusBorderStyle")!=null)
g3.style.borderStyle=f7.getAttribute("FocusBorderStyle");
g3.id=id;
var g4=this.GetViewport(f7).parentNode;
g4.insertBefore(g3,null);
}
return g3;
}
this.CreateFocusBorder=function (f7){
if (this.GetTopSpread(f7).getAttribute("hierView")=="true")return ;
if (this.GetTopSpread(f7).getAttribute("showFocusRect")=="false")return ;
if (this.GetViewport(f7)==null)return ;
var g3=this.CreateLineBorder(f7,f7.id+"_focusRectT");
g3.style.height=0;
g3=this.CreateLineBorder(f7,f7.id+"_focusRectB");
g3.style.height=0;
g3=this.CreateLineBorder(f7,f7.id+"_focusRectL");
g3.style.width=0;
g3=this.CreateLineBorder(f7,f7.id+"_focusRectR");
g3.style.width=0;
}
this.GetPosIndicator=function (f7){
var g5=f7.posIndicator;
if (g5==null)
g5=this.CreatePosIndicator(f7);
else if (g5.parentNode!=f7)
f7.insertBefore(g5,null);
return g5;
}
this.CreatePosIndicator=function (f7){
var g5=document.createElement("img");
g5.style.position="absolute";
g5.style.top="0px";
g5.style.left="-400px";
g5.style.width="10px";
g5.style.height="10px";
g5.style.zIndex=1000;
g5.id=f7.id+"_posIndicator";
if (f7.getAttribute("clienturl")!=null)
g5.src=f7.getAttribute("clienturl")+"down.gif";
else 
g5.src=f7.getAttribute("clienturlres");
f7.insertBefore(g5,null);
f7.posIndicator=g5;
return g5;
}
this.InitSpan=function (f7,e5,spans){
if (e5!=null){
var g6=0;
if (e5==this.GetViewport(f7))
g6=e5.rows.length;
var g7=e5.rows;
var g8=this.GetColCount(f7);
for (var g9=0;g9<g7.length;g9++){
if (this.IsChildSpreadRow(f7,e5,g9)){
if (e5==this.GetViewport(f7))g6--;
}else {
var h0=g7[g9].cells;
for (var h1=0;h1<h0.length;h1++){
var h2=h0[h1];
if (h2!=null&&((h2.rowSpan!=null&&h2.rowSpan>1)||(h2.colSpan!=null&&h2.colSpan>1))){
var h3=this.GetRowFromCell(f7,h2);
var h4=parseInt(h2.getAttribute("col"));
if (h4<g8){
this.AddSpan(f7,h3,h4,h2.rowSpan,h2.colSpan,spans);
}
}
}
}
}
if (e5==this.GetViewport(f7))f7.setAttribute("rowCount",g6);
}
}
this.AddSpan=function (f7,g9,h1,rc,g8,spans){
if (spans==null)spans=f7.e0;
var h5=new this.Range();
this.SetRange(h5,"Cell",g9,h1,rc,g8);
spans.push(h5);
this.PaintFocusRect(f7);
}
this.RemoveSpan=function (f7,g9,h1,spans){
if (spans==null)spans=f7.e0;
for (var e9=0;e9<spans.length;e9++){
var h5=spans[e9];
if (h5.row==g9&&h5.col==h1){
var h6=spans.length-1;
for (var h7=e9;h7<h6;h7++){
spans[h7]=spans[h7+1];
}
spans.length=spans.length-1;
break ;
}
}
this.PaintFocusRect(f7);
}
this.Focus=function (f7){
if (this.a8)return ;
this.SetPageActiveSpread(f7);
if (f7.d2==null&&f7.GetRowCount()>0&&f7.GetColCount()>0){
var h8=this.FireActiveCellChangingEvent(f7,0,0);
if (!h8){
f7.SetActiveCell(0,0);
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=f7.id;
g0.row=g0.Row=0;
g0.col=g0.Col=0;
this.FireEvent(f7,g0);
}
}
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (f7.d2!=null){
var h9=this.GetEditor(f7.d2);
if (h9==null){
if (f8!=null){
if (this.b9!=f8){
try {f8.focus();}catch (g0){}
}
}
}else {
if (h9.tagName!="SELECT")h9.focus();
this.SetEditorFocus(h9);
}
}else {
if (f8!=null){
try {f8.focus();}catch (g0){}
}
}
this.EnableButtons(f7);
}
this.GetTotalRowCount=function (f7){
var f6=parseInt(f7.getAttribute("totalRowCount"));
if (isNaN(f6))f6=0;
return f6;
}
this.GetPageCount=function (f7){
var f6=parseInt(f7.getAttribute("pageCount"));
if (isNaN(f6))f6=0;
return f6;
}
this.GetColCount=function (f7){
var f6=parseInt(f7.getAttribute("colCount"));
if (isNaN(f6))f6=0;
return f6;
}
this.GetRowCount=function (f7){
var f6=parseInt(f7.getAttribute("rowCount"));
if (isNaN(f6))f6=0;
return f6;
}
this.GetRowCountInternal=function (f7){
var f6=parseInt(this.GetViewport(f7).rows.length);
if (isNaN(f6))f6=0;
return f6;
}
this.IsChildSpreadRow=function (f7,view,g9){
if (f7==null||view==null)return false;
if (g9>=1&&g9<view.rows.length){
if (view.rows[g9].cells.length>0&&view.rows[g9].cells[0]!=null&&view.rows[g9].cells[0].firstChild!=null){
var f6=view.rows[g9].cells[0].firstChild;
if (f6.nodeName!="#text"&&f6.getAttribute("FpSpread")=="Spread")return true;
}
}
return false;
}
this.GetChildSpread=function (f7,row,rindex){
var i0=this.GetViewport(f7);
if (i0!=null){
var g9=this.GetDisplayIndex(f7,row)+1;
if (typeof(rindex)=="number")g9+=rindex;
if (g9>=1&&g9<i0.rows.length){
if (i0.rows[g9].cells.length>0&&i0.rows[g9].cells[0]!=null&&i0.rows[g9].cells[0].firstChild!=null){
var f6=i0.rows[g9].cells[0].firstChild;
if (f6.nodeName!="#text"&&f6.getAttribute("FpSpread")=="Spread"){
return f6;
}
}
}
}
return null;
}
this.GetChildSpreads=function (f7){
var e9=0;
var g2=new Array();
var i0=this.GetViewport(f7);
if (i0!=null){
for (var g9=1;g9<i0.rows.length;g9++){
if (i0.rows[g9].cells.length>0&&i0.rows[g9].cells[0]!=null&&i0.rows[g9].cells[0].firstChild!=null){
var f6=i0.rows[g9].cells[0].firstChild;
if (f6.nodeName!="#text"&&f6.getAttribute("FpSpread")=="Spread"){
g2.length=e9+1;
g2[e9]=f6;
e9++;
}
}
}
}
return g2;
}
this.GetDisplayIndex=function (f7,row){
if (row<0)return -1;
var e9=0;
var g9=0;
var i0=this.GetViewport(f7);
if (i0!=null){
for (e9=0;e9<i0.rows.length;e9++){
if (this.IsChildSpreadRow(f7,i0,e9))continue ;
if (g9==row)break ;
g9++;
}
}
return e9;
}
this.GetSheetIndex=function (f7,row,c3){
var e9=0
var g9=0;
var i0=c3;
if (i0==null)i0=this.GetViewport(f7);
if (i0!=null){
if (row<0||row>=i0.rows.length)return -1;
for (e9=0;e9<row;e9++){
if (this.IsChildSpreadRow(f7,i0,e9))continue ;
g9++;
}
}
return g9;
}
this.GetParentRowIndex=function (f7){
var i1=this.GetParentSpread(f7);
if (i1==null)return -1;
var i0=this.GetViewport(i1);
if (i0==null)return -1;
var i2=f7.parentNode.parentNode;
var e9=i2.rowIndex-1;
for (;e9>0;e9--){
if (this.IsChildSpreadRow(i1,i0,e9))continue ;
else 
break ;
}
return this.GetSheetIndex(i1,e9,i0);
}
this.CreateTestBox=function (f7){
var i3=document.getElementById(f7.id+"_testBox");
if (i3==null)
{
i3=document.createElement("span");
i3.style.position="absolute";
i3.style.borderWidth=0;
i3.style.top="-500px";
i3.style.left="-100px";
i3.id=f7.id+"_testBox";
f7.insertBefore(i3,f7.firstChild);
}
return i3;
}
this.SizeToFit=function (f7,h1){
if (h1==null||h1<0)h1=0;
var e5=this.GetViewport(f7);
if (e5!=null){
var i3=this.CreateTestBox(f7);
var g7=e5.rows;
var i4=0;
for (var g9=0;g9<g7.length;g9++){
if (!this.IsChildSpreadRow(f7,e5,g9)){
var i5=this.GetCellFromRowCol(f7,g9,h1);
if (i5.colSpan>1)continue ;
var i6=this.GetPreferredCellWidth(f7,i5,i3);
if (i6>i4)i4=i6;
}
}
this.SetColWidth(f7,h1,i4);
}
}
this.GetPreferredCellWidth=function (f7,i5,i3){
if (i3==null)i3=this.CreateTestBox(f7);
var i7=this.GetRender(f7,i5);
if (i7!=null){
i3.style.fontFamily=i7.style.fontFamily;
i3.style.fontSize=i7.style.fontSize;
i3.style.fontWeight=i7.style.fontWeight;
i3.style.fontStyle=i7.style.fontStyle;
}
i3.innerHTML=i5.innerHTML;
var i6=i3.offsetWidth+8;
if (i5.style.paddingLeft!=null&&i5.style.paddingLeft.length>0)
i6+=parseInt(i5.style.paddingLeft);
if (i5.style.paddingRight!=null&&i5.style.paddingRight.length>0)
i6+=parseInt(i5.style.paddingRight);
return i6;
}
this.GetHierBar=function (f7){
if (f7.c9==null)f7.c9=document.getElementById(f7.id+"_hierBar");
return f7.c9;
}
this.GetGroupBar=function (f7){
if (f7.groupBar==null)f7.groupBar=document.getElementById(f7.id+"_groupBar");
return f7.groupBar;
}
this.GetPager1=function (f7){
if (f7.d0==null)f7.d0=document.getElementById(f7.id+"_pager1");
return f7.d0;
}
this.GetPager2=function (f7){
if (f7.d1==null)f7.d1=document.getElementById(f7.id+"_pager2");
return f7.d1;
}
this.SynRowHeight=function (f7,c6,e5,g9,updateParent,header){
if (c6==null||e5==null)return ;
var i8=c6.rows[g9].offsetHeight;
var g4=e5.rows[g9].offsetHeight;
if (i8==g4&&g9>0)return ;
var i9=this.IsXHTML(f7);
if (g9==0&&!i9){
i8+=c6.rows[g9].offsetTop;
g4+=e5.rows[g9].offsetTop;
}
if (i9)e5.rows[g9].style.height="";
var j0=Math.max(i8,g4);
if (c6.rows[g9].style.height=="")c6.rows[g9].style.height=""+i8+"px";
if (e5.rows[g9].style.height=="")e5.rows[g9].style.height=""+g4+"px";
if (this.IsChildSpreadRow(f7,e5,g9)){
c6.rows[g9].style.height=j0;
return ;
}
var i9=this.IsXHTML(f7);
if (j0>0){
if (i9){
if (j0==i8)
e5.rows[g9].style.height=""+(parseInt(e5.rows[g9].style.height)+(j0-g4))+"px";
else 
c6.rows[g9].style.height=""+(parseInt(c6.rows[g9].style.height)+(j0-i8))+"px";
}else {
if (header&&e5.rows.length>=2&&e5.cellSpacing=="0"){
if (g9==0)
if (j0==i8)
e5.rows[g9].style.height=parseInt(e5.rows[g9].style.height)+(j0-g4);
else 
c6.rows[g9].style.height=parseInt(c6.rows[g9].style.height)+(j0-i8);
else 
{
c6.rows[g9].style.height=j0;
e5.rows[g9].style.height=j0;
}
}else {
if (j0==i8)
e5.rows[g9].style.height=parseInt(e5.rows[g9].style.height)+(j0-g4);
else 
c6.rows[g9].style.height=parseInt(c6.rows[g9].style.height)+(j0-i8);
}
}
}
if (updateParent){
var i1=this.GetParentSpread(f7);
if (i1!=null)this.UpdateRowHeight(i1,f7);
}
}
this.SizeAll=function (f7){
var j1=this.GetChildSpreads(f7);
if (j1!=null&&j1.length>0){
for (var e9=0;e9<j1.length;e9++){
this.SizeAll(j1[e9]);
}
}
this.SizeSpread(f7);
if (this.GetParentSpread(f7)!=null)
this.Refresh(f7);
}
this.SizeSpread=function (f7){
var i9=this.IsXHTML(f7);
var c3=this.GetViewport(f7);
if (c3==null)return ;
var c7=this.GetColHeader(f7);
var j2=this.GetColGroup(c3);
var j3=this.GetColGroup(c7);
if (j2!=null&&j2.childNodes.length>0&&j3!=null&&j3.childNodes.length>0){
var j4=-1;
if (this.b5!=null)j4=parseInt(this.b5.getAttribute("index"));
if (this.b5!=null&&j4!=0)
{
j2.childNodes[0].width=(j3.childNodes[0].offsetWidth-j2.childNodes[0].offsetLeft);
}
else 
{
j3.childNodes[0].width=(j2.childNodes[0].offsetLeft+j2.childNodes[0].offsetWidth);
}
}
var c6=this.GetRowHeader(f7);
if (c6!=null){
for (var e9=0;e9<c3.rows.length&&e9<c6.rows.length;e9++){
this.SynRowHeight(f7,c6,c3,e9,false,true);
}
}
var i1=this.GetParentSpread(f7);
if (i1!=null)this.UpdateRowHeight(i1,f7);
var j0=f7.clientHeight;
var j5=this.GetCommandBar(f7);
if (j5!=null)
{
j5.style.width=""+f7.clientWidth+"px";
if (f7.style.position!="absolute"&&f7.style.position!="relative"){
j5.parentNode.style.borderTop="1px solid white";
j5.parentNode.style.backgroundColor=j5.style.backgroundColor;
}
var j6=this.GetElementById(j5,f7.id+"_cmdTable");
if (j6!=null){
if (f7.style.position!="absolute"&&f7.style.position!="relative"&&(j6.style.height==""||parseInt(j6.style.height)<27)){
j6.style.height=""+(j6.offsetHeight+3)+"px";
}
if (!i9&&parseInt(c3.cellSpacing)>0)
j6.parentNode.style.height=""+(j6.offsetHeight+3)+"px";
j0-=Math.max(j6.parentNode.offsetHeight,j6.offsetHeight);
}
if (f7.style.position!="absolute"&&f7.style.position!="relative")
j5.style.position="";
}
var c7=this.GetColHeader(f7);
if (c7!=null)
{
j0-=c7.offsetHeight;
c7.parentNode.style.height=""+(c7.offsetHeight-parseInt(c7.cellSpacing))+"px";
if (i9)
j0+=parseInt(c7.cellSpacing);
}
var c9=this.GetHierBar(f7);
if (c9!=null)
{
j0-=c9.offsetHeight;
}
var j7=this.GetGroupBar(f7);
if (j7!=null){
j0-=j7.offsetHeight;
}
var d0=this.GetPager1(f7);
if (d0!=null)
{
j0-=d0.offsetHeight;
this.InitSlideBar(f7,d0);
}
var j8=(f7.getAttribute("cmdTop")=="true");
var d1=this.GetPager2(f7);
if (d1!=null)
{
d1.style.width=""+(f7.clientWidth-10)+"px";
j0-=Math.max(d1.offsetHeight,28);
this.InitSlideBar(f7,d1);
}
var j9=null;
if (c6!=null)j9=c6.parentNode;
var k0=null;
if (c7!=null)k0=c7.parentNode;
var k1=c3.parentNode;
var c5=this.GetCorner(f7);
if (k0!=null)
{
k0.style.height=c7.offsetHeight-parseInt(c3.cellSpacing);
if (c5!=null){
c5.parentNode.style.height=k0.style.height;
}
}
if (c5!=null&&!i9)
c5.width=""+(c5.parentNode.offsetWidth+parseInt(c3.cellSpacing))+"px";
if (k1!=null){
if (j9!=null){
k1.style.width=""+Math.max(f7.clientWidth-j9.offsetWidth+parseInt(c3.cellSpacing),1)+"px";
k1.style.height=""+Math.max(j0,1)+"px";
k1.style.width=""+Math.max(f7.clientWidth-j9.offsetWidth+parseInt(c3.cellSpacing),1)+"px";
}else {
k1.style.width=""+Math.max(f7.clientWidth,1)+"px";
k1.style.height=""+Math.max(j0,1)+"px";
k1.style.width=""+Math.max(f7.clientWidth,1)+"px";
}
}
if (j5!=null&&!j8){
if (d1!=null){
if (f7.style.position=="absolute"||f7.style.position=="relative"){
j5.style.position="absolute";
j5.style.top=""+(f7.clientHeight-Math.max(d1.offsetHeight,28)-j5.offsetHeight)+"px";
}else {
j5.style.position="absolute";
j5.style.top=""+(c3.parentNode.offsetTop+c3.parentNode.offsetHeight)+"px";
}
}else {
if (f7.style.position=="absolute"||f7.style.position=="relative"){
j5.style.position="absolute";
j5.style.top=""+(f7.clientHeight-j5.offsetHeight)+"px";
}
}
}
if (d1!=null)
{
if (f7.style.position=="absolute"||f7.style.position=="relative"){
d1.style.position="absolute";
d1.style.top=""+(f7.clientHeight-Math.max(d1.offsetHeight,28))+"px";
}else {
d1.style.position="absolute";
if (j5!=null&&!j8)
d1.style.top=""+(c3.parentNode.offsetTop+c3.parentNode.offsetHeight+j5.offsetHeight)+"px";
else 
d1.style.top=""+(c3.parentNode.offsetTop+c3.parentNode.offsetHeight)+"px";
}
}
if (j9!=null){
j9.style.position="relative";
if (i9)j9.style.height=""+Math.max(k1.offsetHeight,1)+"px";
else j9.style.height=Math.max(k1.offsetHeight,1);
}
if (this.GetParentSpread(f7)==null&&k0!=null){
var i6=0;
if (j9!=null){
i6=Math.max(f7.clientWidth-j9.offsetWidth,1);
}else {
i6=Math.max(f7.clientWidth,1);
}
k0.style.width=i6;
k0.parentNode.style.width=i6;
}
if (i9)
{
if (c3!=null){
c3.style.posTop=-c3.cellSpacing;
var k2=f7.clientWidth;
if (c6!=null)k2-=c6.parentNode.offsetWidth;
c3.parentNode.style.width=""+k2+"px";
}
if (c6!=null){
c6.style.position="relative";
c6.parentNode.style.position="relative";
c6.style.posTop=-c3.cellSpacing;
c6.width=""+(c6.parentNode.offsetWidth)+"px";
}
}else {
if (c3!=null){
var k2=f7.clientWidth;
if (c6!=null){
k2-=c6.parentNode.offsetWidth;
c6.width=""+(c6.parentNode.offsetWidth+parseInt(c3.cellSpacing))+"px";
}
c3.parentNode.style.width=""+k2+"px";
}
}
this.ScrollView(f7);
this.PaintFocusRect(f7);
}
this.InitSlideBar=function (f7,pager){
var k3=this.GetElementById(pager,f7.id+"_slideBar");
if (k3!=null){
var i9=this.IsXHTML(f7);
if (i9)
k3.style.height=Math.max(pager.offsetHeight,28)+"px";
else 
k3.style.height=(pager.offsetHeight-2)+"px";
var f6=pager.getElementsByTagName("TABLE");
if (f6!=null&&f6.length>0){
var k4=f6[0].rows[0];
var h4=k4.cells[0];
var k5=k4.cells[2];
f7.slideLeft=Math.max(107,h4.offsetWidth+1);
if (h4.style.paddingRight!="")f7.slideLeft+=parseInt(h4.style.paddingRight);
f7.slideRight=pager.offsetWidth-k5.offsetWidth-k3.offsetWidth-3;
if (k5.style.paddingRight!="")f7.slideRight-=parseInt(k5.style.paddingLeft);
var k6=parseInt(pager.getAttribute("curPage"));
var k7=parseInt(pager.getAttribute("totalPage"))-1;
if (k7==0)k7=1;
var k2=Math.max(107,f7.slideLeft)+(k6/k7)*(f7.slideRight-f7.slideLeft);
if (pager.id.indexOf("pager1")>=0&&f7.style.position!="absolute"&&f7.style.position!="relative"){
k2+=this.GetOffsetLeft(f7,pager,document);
var k8=(this.GetOffsetTop(f7,h4,pager)+this.GetOffsetTop(f7,pager,document));
k3.style.top=k8+"px";
}
if (k2>f7.slideRight)k2=f7.slideRight;
k3.style.left=k2+"px";
}
}
}
this.InitLayout=function (f7){
this.SizeSpread(f7);
this.SizeSpread(f7);
}
this.GetRowByKey=function (f7,key){
if (key=="-1")
return -1;
var k9=this.GetViewport(f7);
if (k9!=null){
for (var i2=0;i2<k9.rows.length;i2++){
if (k9.rows[i2].getAttribute("FpKey")==key){
return i2;
}
}
}
if (k9!=null)
return 0;
else 
return -1;
}
this.GetColByKey=function (f7,key){
if (key=="-1")
return -1;
var k9=this.GetViewport(f7);
var l0=this.GetColGroup(k9);
if (l0==null||l0.childNodes.length==0)
l0=this.GetColGroup(this.GetColHeader(f7));
if (l0!=null){
for (var l1=0;l1<l0.childNodes.length;l1++){
var f6=l0.childNodes[l1];
if (f6.getAttribute("FpCol")==key){
return l1;
}
}
}
return 0;
}
this.IsRowSelected=function (f7,i2){
var l2=this.GetSelection(f7);
if (l2!=null){
var l3=l2.firstChild;
while (l3!=null){
var g9=parseInt(l3.getAttribute("rowIndex"));
var l4=parseInt(l3.getAttribute("rowcount"));
if (g9<=i2&&i2<g9+l4)
return true;
l3=l3.nextSibling;
}
}
}
this.InitSelection=function (f7){
var g9=0;
var h1=0;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var l2=l5.getElementsByTagName("selection")[0];
var l6=l5.firstChild;
while (l6!=null&&l6.tagName!="activerow"&&l6.tagName!="ACTIVEROW"){
l6=l6.nextSibling;
}
if (l6!=null)
g9=this.GetRowByKey(f7,l6.innerHTML);
if (g9>=this.GetRowCount(f7))g9=0;
var l7=l5.firstChild;
while (l7!=null&&l7.tagName!="activecolumn"&&l7.tagName!="ACTIVECOLUMN"){
l7=l7.nextSibling;
}
if (l7!=null)
h1=this.GetColByKey(f7,l7.innerHTML);
if (g9<0)g9=0;
if (g9>=0||h1>=0){
var l8=f2;
if (this.GetParentSpread(f7)!=null){
var l9=this.GetTopSpread(f7);
if (l9.initialized)l8=this.GetData(l9);
f3=l8.getElementsByTagName("root")[0];
}
var m0=f3.getElementsByTagName("activechild")[0];
f7.d4=g9;f7.d5=h1;
if ((this.GetParentSpread(f7)==null&&(m0==null||m0.innerHTML==""))||(m0!=null&&f7.id==this.Trim(m0.innerHTML))){
this.UpdateAnchorCell(f7,g9,h1);
}else {
f7.d2=this.GetCellFromRowCol(f7,g9,h1);
}
}
var l3=l2.firstChild;
while (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
l3.setAttribute("rowIndex",g9);
l3.setAttribute("colIndex",h1);
this.PaintSelection(f7,g9,h1,l4,g8,true);
l3=l3.nextSibling;
}
this.PaintFocusRect(f7);
}
this.TranslateKey=function (event){
event=this.GetEvent(event);
var m1=this.GetTarget(event);
try {
if (document.readyState!=null&&document.readyState!="complete")return ;
var f7=this.GetPageActiveSpread();
if (f7!=null){
if (event.keyCode==229){
this.CancelDefault(event);
return ;
}
if (!this.IsChild(m1,this.GetTopSpread(f7)))return ;
this.KeyDown(f7,event);
var m2=false;
if (event.keyCode==event.DOM_VK_TAB){
var m3=this.GetProcessTab(f7);
m2=(m3=="true"||m3=="True");
}
if (m2)
this.CancelDefault(event);
}
}catch (g0){}
}
this.KeyAction=function (key,ctrl,shift,alt,action){
this.key=key;
this.ctrl=ctrl;
this.shift=shift;
this.alt=alt;
this.action=action;
}
this.RemoveKeyMap=function (f7,keyCode,ctrl,shift,alt,action){
if (f7.keyMap==null)f7.keyMap=new Array();
var e8=f7.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=f7.keyMap[e9];
if (g1!=null&&g1.key==keyCode&&g1.ctrl==ctrl&&g1.shift==shift&&g1.alt==alt){
for (var h7=e9+1;h7<e8;h7++){
f7.keyMap[h7-1]=f7.keyMap[h7];
}
f7.keyMap.length=f7.keyMap.length-1;
break ;
}
}
}
this.AddKeyMap=function (f7,keyCode,ctrl,shift,alt,action){
if (f7.keyMap==null)f7.keyMap=new Array();
var g1=this.GetKeyAction(f7,keyCode,ctrl,shift,alt);
if (g1!=null){
g1.action=action;
}else {
var e8=f7.keyMap.length;
f7.keyMap.length=e8+1;
f7.keyMap[e8]=new this.KeyAction(keyCode,ctrl,shift,alt,action);
}
}
this.GetKeyAction=function (f7,keyCode,ctrl,shift,alt){
if (f7.keyMap==null)f7.keyMap=new Array();
var e8=f7.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=f7.keyMap[e9];
if (g1!=null&&g1.key==keyCode&&g1.ctrl==ctrl&&g1.shift==shift&&g1.alt==alt){
return g1;
}
}
return null;
}
this.MoveToPrevCell=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
var h1=f7.GetActiveCol();
this.MoveLeft(f7,g9,h1);
}
this.MoveToNextCell=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
var h1=f7.GetActiveCol();
this.MoveRight(f7,g9,h1);
}
this.MoveToNextRow=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
var h1=f7.GetActiveCol();
this.MoveDown(f7,g9,h1);
}
this.MoveToPrevRow=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
var h1=f7.GetActiveCol();
if (g9>0)
this.MoveUp(f7,g9,h1);
}
this.MoveToFirstColumn=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
if (f7.d2.parentNode.rowIndex>=0)
this.UpdateLeadingCell(f7,g9,0);
}
this.MoveToLastColumn=function (f7){
var m4=this.EndEdit(f7);
if (!m4)return ;
var g9=f7.GetActiveRow();
if (f7.d2.parentNode.rowIndex>=0){
h1=this.GetColCount(f7)-1;
this.UpdateLeadingCell(f7,g9,h1);
}
}
this.UpdatePostbackData=function (f7){
this.SaveData(f7);
}
this.PrepareData=function (l3){
var g2="";
if (l3!=null){
if (l3.nodeName=="#text")
g2=l3.nodeValue;
else {
g2=this.GetBeginData(l3);
var f6=l3.firstChild;
while (f6!=null){
var m5=this.PrepareData(f6);
if (m5!="")g2+=m5;
f6=f6.nextSibling;
}
g2+=this.GetEndData(l3);
}
}
return g2;
}
this.GetBeginData=function (l3){
var g2="<"+l3.nodeName.toLowerCase();
if (l3.attributes!=null){
for (var e9=0;e9<l3.attributes.length;e9++){
var m6=l3.attributes[e9];
if (m6.nodeName!=null&&m6.nodeName!=""&&m6.nodeName!="style"&&m6.nodeValue!=null&&m6.nodeValue!="")
g2+=(" "+m6.nodeName+"=\""+m6.nodeValue+"\"");
}
}
g2+=">";
return g2;
}
this.GetEndData=function (l3){
return "</"+l3.nodeName.toLowerCase()+">";
}
this.SaveData=function (f7){
if (f7==null)return ;
try {
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var f6=this.PrepareData(f3);
var m7=document.getElementById(f7.id+"_data");
m7.value=encodeURIComponent(f6);
}catch (g0){
alert("e "+g0);
}
}
this.SetActiveSpread=function (event){
try {
event=this.GetEvent(event);
var m1=this.GetTarget(event);
var m8=this.GetSpread(m1,false);
var m9=this.GetPageActiveSpread();
if (this.a8&&(m8==null||m8!=m9)){
if (m1!=this.a9&&this.a9!=null){
if (this.a9.blur!=null)this.a9.blur();
}
var m4=this.EndEdit();
if (!m4)return ;
}
var n0=false;
if (m8==null){
m8=this.GetSpread(m1,true);
n0=(m8!=null);
}
var h2=this.GetCell(m1,true);
if (h2==null&&m9!=null&&m9.e3){
this.SaveData(m9);
m9.e3=false;
}
if (m9!=null&&m9.e3&&(m8!=m9||m8==null||n0)){
this.SaveData(m9);
m9.e3=false;
}
if (m9!=null&&m9.e3&&m8==m9&&m1.tagName=="INPUT"&&(m1.type=="submit"||m1.type=="button"||m1.type=="image")){
this.SaveData(m9);
m9.e3=false;
}
if (m8!=null&&this.GetOperationMode(m8)=="ReadOnly")return ;
var l9=null;
if (m8==null){
if (m9==null)return ;
l9=this.GetTopSpread(m9);
this.SetActiveSpreadID(l9,"",null,false);
this.SetPageActiveSpread(null);
}else {
if (m8!=m9){
if (m9!=null){
l9=this.GetTopSpread(m9);
this.SetActiveSpreadID(l9,"",null,false);
}
if (n0){
l9=this.GetTopSpread(m8);
var n1=this.GetTopSpread(m9);
if (l9!=n1){
this.SetActiveSpreadID(l9,m8.id,m8.id,true);
this.SetPageActiveSpread(m8);
}else {
this.SetActiveSpreadID(l9,m9.id,m9.id,true);
this.SetPageActiveSpread(m9);
}
}else {
l9=this.GetTopSpread(m8);
this.SetPageActiveSpread(m8);
this.SetActiveSpreadID(l9,m8.id,m8.id,false);
}
}
}
}catch (g0){}
}
this.SetActiveSpreadID=function (f7,id,child,n0){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var f4=f3.getElementsByTagName("activespread")[0];
var n2=f3.getElementsByTagName("activechild")[0];
if (f4==null)return ;
if (n0&&n2!=null&&n2.nodeValue!=""){
f4.innerHTML=n2.innerHTML;
}else {
f4.innerHTML=id;
if (child!=null&&n2!=null)n2.innerHTML=child;
}
this.SaveData(f7);
f7.e3=false;
}
this.GetSpread=function (ele,incCmdBar){
var i6=ele;
while (i6!=null&&i6.tagName!="BODY"){
if (typeof(i6.getAttribute)!="function")break ;
var f7=i6.getAttribute("FpSpread");
if (f7==null)f7=i6.FpSpread;
if (f7=="Spread"){
if (!incCmdBar){
var f6=ele;
while (f6!=null&&f6!=i6){
if (f6.id==i6.id+"_commandBar"||f6.id==i6.id+"_pager1"||f6.id==i6.id+"_pager2")return null;
f6=f6.parentNode;
}
}
return i6;
}
i6=i6.parentNode;
}
return null;
}
this.ScrollViewport=function (event){
var f6=this.GetTarget(event);
var f7=this.GetTopSpread(f6);
if (f7!=null)this.ScrollView(f7);
}
this.ScrollTo=function (f7,i2,l1){
var h2=this.GetCellByRowCol(f7,i2,l1);
if (h2==null)return ;
var i0=this.GetViewport(f7).parentNode;
if (i0==null)return ;
i0.scrollTop=h2.offsetTop;
i0.scrollLeft=h2.offsetLeft;
}
this.ScrollView=function (f7){
var m8=this.GetTopSpread(f7);
var c6=this.GetParent(this.GetRowHeader(m8));
var c7=this.GetParent(this.GetColHeader(m8));
var i0=this.GetParent(this.GetViewport(m8));
var n3=false;
if (c6!=null){
n3=(c6.scrollTop!=i0.scrollTop);
c6.scrollTop=i0.scrollTop;
}
if (c7!=null){
if (!n3)n3=(c7.scrollLeft!=i0.scrollLeft);
c7.scrollLeft=i0.scrollLeft;
}
if (this.GetParentSpread(f7)==null)this.SaveScrollbarState(f7,i0.scrollTop,i0.scrollLeft);
if (n3){
var g0=this.CreateEvent("Scroll");
this.FireEvent(f7,g0);
}
if (i0.scrollTop>0&&i0.scrollTop+i0.offsetHeight>=this.GetViewport(m8).offsetHeight){
if (!this.a8&&f7.getAttribute("loadOnDemand")=="true"){
if (f7.LoadState!=null)return ;
f7.LoadState=true;
var l4=this.GetRowCount(f7);
this.SetActiveCell(f7,l4-1,Math.max(f7.d5,0));
this.SaveData(f7);
f7.CallBack("LoadOnDemand",true);
}
}
}
this.SaveScrollbarState=function (f7,scrollTop,scrollLeft){
if (this.GetParentSpread(f7)!=null)return ;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var n4=f3.getElementsByTagName("scrollTop")[0];
var n5=f3.getElementsByTagName("scrollLeft")[0];
if (n4!=null)n4.innerHTML=scrollTop;
if (n5!=null)n5.innerHTML=scrollLeft;
}
this.LoadScrollbarState=function (f7){
if (this.GetParentSpread(f7)!=null)return ;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var n4=f3.getElementsByTagName("scrollTop")[0];
var n5=f3.getElementsByTagName("scrollLeft")[0];
var n6=0;
if (n4!=null&&n4.innerHTML!=""){
n6=parseInt(n4.innerHTML);
}else {
n6=0;
}
var n7=0;
if (n5!=null&&n5.innerHTML!=""){
n7=parseInt(n5.innerHTML);
}else {
n7=0;
}
var i0=this.GetParent(this.GetViewport(f7));
if (i0!=null){
if (!isNaN(n6))i0.scrollTop=n6;
if (!isNaN(n7))i0.scrollLeft=n7;
var c6=this.GetParent(this.GetRowHeader(f7));
var c7=this.GetParent(this.GetColHeader(f7));
if (c6!=null){
c6.scrollTop=i0.scrollTop;
}
if (c7!=null){
c7.scrollLeft=i0.scrollLeft;
}
}
}
this.GetParent=function (g0){
if (g0==null)
return null;
else 
return g0.parentNode;
}
this.GetViewport=function (f7){
return f7.c3;
}
this.GetCommandBar=function (f7){
return f7.c8;
}
this.GetRowHeader=function (f7){
return f7.c6;
}
this.GetColHeader=function (f7){
return f7.c7;
}
this.GetCmdBtn=function (f7,id){
var m8=this.GetTopSpread(f7);
var n8=this.GetCommandBar(m8);
if (n8!=null)
return this.GetElementById(n8,m8.id+"_"+id);
else 
return null;
}
this.Range=function (){
this.type="Cell";
this.row=-1;
this.col=-1;
this.rowCount=0;
this.colCount=0;
}
this.SetRange=function (h5,type,i2,l1,l4,g8){
h5.type=type;
h5.row=i2;
h5.col=l1;
h5.rowCount=l4;
h5.colCount=g8;
if (type=="Row"){
h5.col=h5.colCount=-1;
}else if (type=="Column"){
h5.row=h5.rowCount=-1;
}else if (type=="Table"){
h5.col=h5.colCount=-1;h5.row=h5.rowCount=-1;
}
}
this.Margin=function (left,top,right,bottom){
this.left;
this.top;
this.right;
this.bottom;
}
this.GetRender=function (h2){
var f6=h2;
if (f6.firstChild!=null&&f6.firstChild.tagName!=null&&f6.firstChild.tagName!="BR")
return f6.firstChild;
if (f6.firstChild!=null&&f6.firstChild.value!=null){
f6=f6.firstChild;
}
return f6;
}
this.GetPreferredRowHeight=function (f7,g9){
var i3=this.CreateTestBox(f7);
g9=this.GetDisplayIndex(f7,g9);
var i0=this.GetViewport(f7);
var i4=0;
var n9=i0.rows[g9].offsetHeight;
var e8=i0.rows[g9].cells.length;
for (var e9=0;e9<e8;e9++){
var i5=i0.rows[g9].cells[e9];
var i7=this.GetRender(i5);
if (i7!=null){
i3.style.fontFamily=i7.style.fontFamily;
i3.style.fontSize=i7.style.fontSize;
i3.style.fontWeight=i7.style.fontWeight;
i3.style.fontStyle=i7.style.fontStyle;
}
var l1=this.GetColFromCell(f7,i5);
i3.style.posWidth=this.GetColWidthFromCol(f7,l1);
if (i7!=null&&i7.tagName=="SELECT"){
var f6="";
for (var h7=0;h7<i7.childNodes.length;h7++){
var o0=i7.childNodes[h7];
if (o0.text!=null&&o0.text.length>f6.length)f6=o0.text;
}
i3.innerHTML=f6;
}
else if (i7!=null&&i7.tagName=="INPUT")
i3.innerHTML=i7.value;
else 
{
i3.innerHTML=i5.innerHTML;
}
n9=i3.offsetHeight;
if (n9>i4)i4=n9;
}
return Math.max(0,i4)+3;
}
this.SetRowHeight2=function (f7,g9,height){
if (height<1){
height=1;
}
g9=this.GetDisplayIndex(f7,g9);
var b6=null;
if (this.GetRowHeader(f7)!=null)b6=this.GetRowHeader(f7).rows[g9];
if (b6!=null)b6.style.height=height;
var i0=this.GetViewport(f7);
if (b6!=null){
i0.rows[b6.rowIndex].style.height=b6.style.height;
}else {
if (i0!=null)i0.rows[g9].style.height=height;
b6=i0.rows[g9];
}
var o1=this.AddRowInfo(f7,b6.FpKey);
if (o1!=null){
this.SetRowHeight(f7,o1,b6.style.posHeight);
}
var i1=this.GetParentSpread(f7);
if (i1!=null)i1.UpdateRowHeight(f7);
this.SizeSpread(f7);
}
this.GetRowHeightInternal=function (f7,g9){
var b6=null;
if (this.GetRowHeader(f7)!=null)
b6=this.GetRowHeader(f7).rows[g9];
else if (this.GetViewport(f7)!=null)
b6=this.GetViewport(f7).rows[g9];
if (b6!=null)
return b6.offsetHeight;
else 
return 0;
}
this.GetCell=function (ele,noHeader,event){
var f6=ele;
while (f6!=null){
if (noHeader){
if ((f6.tagName=="TD"||f6.tagName=="TH")&&(f6.parentNode.getAttribute("FpSpread")=="r")){
return f6;
}
}else {
if ((f6.tagName=="TD"||f6.tagName=="TH")&&(f6.parentNode.getAttribute("FpSpread")=="r"||f6.parentNode.getAttribute("FpSpread")=="ch"||f6.parentNode.getAttribute("FpSpread")=="rh")){
return f6;
}
}
f6=f6.parentNode;
}
return null;
}
this.InRowHeader=function (f7,h2){
return (this.IsChild(h2,this.GetRowHeader(f7)));
}
this.InColHeader=function (f7,h2){
return (this.IsChild(h2,this.GetColHeader(f7)));
}
this.IsHeaderCell=function (f7,h2){
return (h2!=null&&(h2.tagName=="TD"||h2.tagName=="TH")&&(h2.parentNode.getAttribute("FpSpread")=="ch"||h2.parentNode.getAttribute("FpSpread")=="rh"));
}
this.GetSizeColumn=function (f7,ele,event){
if (ele.tagName!="TD"||(this.GetColHeader(f7)==null))return null;
var l1=-1;
var f6=ele;
var n7=this.GetViewport(this.GetTopSpread(f7)).parentNode.scrollLeft+window.scrollX;
while (f6!=null&&f6.parentNode!=null&&f6.parentNode!=document.documentElement){
if (f6.parentNode.getAttribute("FpSpread")=="ch"){
var o2=this.GetOffsetLeft(f7,f6);
var o3=o2+f6.offsetWidth;
if (event.clientX+n7<o2+3){
l1=this.GetColFromCell(f7,f6)-1;
}
else if (event.clientX+n7>o3-4){
l1=this.GetColFromCell(f7,f6);
var o4=this.GetSpanCell(f6.parentNode.rowIndex,l1,f7.e2);
if (o4!=null){
l1=o4.col+o4.colCount-1;
}
}else {
l1=this.GetColFromCell(f7,f6);
var o4=this.GetSpanCell(f6.parentNode.rowIndex,l1,f7.e2);
if (o4!=null){
var i6=o2;
l1=-1;
for (var e9=o4.col;e9<o4.col+o4.colCount&&e9<this.GetColCount(f7);e9++){
if (this.IsChild(f6,this.GetColHeader(f7)))
i6+=parseInt(this.GetElementById(this.GetColHeader(f7),f7.id+"col"+e9).width);
if (event.clientX>i6-3&&event.clientX<i6+3){
l1=e9;
break ;
}
}
}else {
l1=-1;
}
}
if (isNaN(l1)||l1<0)return null;
var o5=0;
var o6=this.GetColCount(f7);
var o7=true;
var e5=null;
var h1=l1+1;
while (h1<o6){
var l0=this.GetColGroup(this.GetColHeader(f7));
if (h1<l0.childNodes.length)
o5=parseInt(l0.childNodes[h1].width);
if (o5>1){
o7=false;
break ;
}
h1++;
}
if (o7){
h1=l1+1;
while (h1<o6){
if (this.GetSizable(f7,h1)){
l1=h1;
break ;
}
h1++;
}
}
if (!this.GetSizable(f7,l1))return null;
if (this.IsChild(f6,this.GetColHeader(f7))){
return this.GetElementById(this.GetColHeader(f7),f7.id+"col"+l1);
}
}
f6=f6.parentNode;
}
return null;
}
this.GetColGroup=function (f6){
if (f6==null)return null;
var l0=f6.getElementsByTagName("COLGROUP");
if (l0!=null&&l0.length>0){
if (f6.colgroup!=null)return f6.colgroup;
var n1=new Object();
n1.childNodes=new Array();
for (var e9=0;e9<l0[0].childNodes.length;e9++){
if (l0[0].childNodes[e9]!=null&&l0[0].childNodes[e9].tagName=="COL"){
var e8=n1.childNodes.length;
n1.childNodes.length++;
n1.childNodes[e8]=l0[0].childNodes[e9];
}
}
f6.colgroup=n1;
return n1;
}else {
return null;
}
}
this.GetSizeRow=function (f7,ele,event){
var l4=this.GetRowCount(f7);
if (l4==0)return null;
var h2=this.GetCell(ele);
if (h2==null){
if (ele.getAttribute("FpSpread")=="rowpadding"){
if (event.clientY<3){
var e8=ele.parentNode.rowIndex;
if (e8>1){
var i2=ele.parentNode.parentNode.rows[e8-1];
if (this.GetSizable(f7,i2))
return i2;
}
}
}
var c5=this.GetCorner(f7);
if (c5!=null&&this.IsChild(ele,c5)){
if (event.clientY>ele.offsetHeight-4){
var o8=null;
var e8=0;
o8=this.GetRowHeader(f7);
if (o8!=null){
while (e8<o8.rows.length&&o8.rows[e8].offsetHeight<2&&!this.GetSizable(f7,o8.rows[e8]))
e8++;
if (e8<o8.rows.length&&this.GetSizable(f7,o8.rows[e8])&&o8.rows[e8].offsetHeight<2)
return o8.rows[e8];
}
}else {
}
}
return null;
}
var e1=f7.e1;
var e0=f7.e0;
var f6=h2;
var n6=this.GetViewport(this.GetTopSpread(f7)).parentNode.scrollTop+window.scrollY;
while (f6!=null&&f6!=document.documentElement){
if (f6.getAttribute("FpSpread")=="rh"){
var e8=-1;
var o9=this.GetOffsetTop(f7,f6);
var p0=o9+f6.offsetHeight;
if (event.clientY+n6<o9+3){
if (f6.rowIndex>1)
e8=f6.rowIndex-1;
}
else if (event.clientY+n6>p0-4){
var o4=this.GetSpanCell(this.GetRowFromCell(f7,h2),this.GetColFromCell(f7,h2),e1);
if (o4!=null){
var j0=o9;
for (var e9=o4.row;e9<o4.row+o4.rowCount;e9++){
j0+=parseInt(this.GetRowHeader(f7).rows[e9].style.height);
if (event.clientY>j0-3&&event.clientY<j0+3){
e8=e9;
break ;
}
}
}else {
if (f6.rowIndex>=0)e8=f6.rowIndex;
}
}
else {
break ;
}
var j0=0;
var l4=this.GetRowHeader(f7).rows.length;
var p1=true;
var o8=null;
o8=this.GetRowHeader(f7);
var g9=e8+1;
while (g9<l4){
if (o8.rows[g9].style.height!=null)j0=parseInt(o8.rows[g9].style.height);
else j0=parseInt(o8.rows[g9].offsetHeight);
if (j0>1){
p1=false;
break ;
}
g9++;
}
if (p1){
g9=e8+1;
while (g9<l4){
if (this.GetSizable(f7,this.GetRowHeader(f7).rows[g9])){
e8=g9;
break ;
}
g9++;
}
}
if (e8>=0&&this.GetSizable(f7,o8.rows[e8])){
return o8.rows[e8];
}
else if (event.clientY<3){
while (e8>0&&o8.rows[e8].offsetHeight==0&&!this.GetSizable(f7,o8.rows[e8]))
e8--;
if (e8>=0&&this.GetSizable(f7,o8.rows[e8]))
return o8.rows[e8];
else 
return null;
}
}
f6=f6.parentNode;
}
return null;
}
this.GetElementById=function (i1,id){
if (i1==null)return null;
var f6=i1.firstChild;
while (f6!=null){
if (f6.id==id||(typeof(f6.getAttribute)=="function"&&f6.getAttribute("name")==id))return f6;
var n1=this.GetElementById(f6,id)
if (n1!=null)return n1;
f6=f6.nextSibling;
}
return null;
}
this.GetSizable=function (f7,ele){
if (typeof(ele)=="number"){
var h2=this.GetElementById(this.GetColHeader(f7),f7.id+"col"+ele);
return (h2!=null&&(h2.getAttribute("Sizable")==null||h2.getAttribute("Sizable")=="True"));
}
return (ele!=null&&(ele.getAttribute("Sizable")==null||ele.getAttribute("Sizable")=="True"));
}
this.GetSpanWidth=function (f7,l1,o6){
var i6=0;
var e5=this.GetViewport(f7);
if (e5!=null){
var l0=this.GetColGroup(e5);
if (l0!=null){
for (var e9=l1;e9<l1+o6;e9++){
i6+=parseInt(l0.childNodes[e9].width);
}
}
}
return i6;
}
this.GetCellType=function (h2){
if (h2!=null&&h2.getAttribute("FpCellType")!=null)return h2.getAttribute("FpCellType");
if (h2!=null&&h2.getAttribute("FpRef")!=null){
var f6=document.getElementById(h2.getAttribute("FpRef"));
return f6.getAttribute("FpCellType");
}
if (h2!=null&&h2.getAttribute("FpCellType")!=null)return h2.getAttribute("FpCellType");
return "text";
}
this.GetCellType2=function (h2){
if (h2!=null&&h2.getAttribute("FpRef")!=null){
h2=document.getElementById(h2.getAttribute("FpRef"));
}
var p2=null;
if (h2!=null){
p2=h2.getAttribute("FpCellType");
if (p2=="readonly")p2=h2.getAttribute("CellType");
}
if (p2!=null)return p2;
return "text";
}
this.GetCellEditorID=function (f7,h2){
if (h2!=null&&h2.getAttribute("FpRef")!=null){
var f6=document.getElementById(h2.getAttribute("FpRef"));
return f6.getAttribute("FpEditorID");
}
if (h2.getAttribute("FpEditorID")!=null)
return h2.getAttribute("FpEditorID");
return f7.getAttribute("FpDefaultEditorID");
}
this.EditorMap=function (editorID,a9){
this.id=editorID;
this.a9=a9;
}
this.ValidatorMap=function (validatorID,validator){
this.id=validatorID;
this.validator=validator;
}
this.GetCellEditor=function (f7,editorID,noClone){
var a9=null;
for (var e9=0;e9<this.c1.length;e9++){
var p3=this.c1[e9];
if (p3.id==editorID){
a9=p3.a9;
break ;
}
}
if (a9==null){
a9=document.getElementById(editorID);
this.c1[this.c1.length]=new this.EditorMap(editorID,a9);
}
if (noClone)
return a9;
return a9.cloneNode(true);
}
this.GetCellValidatorID=function (f7,h2){
return null;
}
this.GetCellValidator=function (f7,validatorID){
return null;
}
this.GetTableRow=function (f7,g9){
var f3=this.GetData(f7).getElementsByTagName("root")[0];
var f2=f3.getElementsByTagName("data")[0];
var f6=f2.firstChild;
while (f6!=null){
if (f6.getAttribute("key")==""+g9)return f6;
f6=f6.nextSibling;
}
return null;
}
this.GetTableCell=function (i2,h1){
if (i2==null)return null;
var f6=i2.firstChild;
while (f6!=null){
if (f6.getAttribute("key")==""+h1)return f6;
f6=f6.nextSibling;
}
return null;
}
this.AddTableRow=function (f7,g9){
if (g9==null)return null;
var l3=this.GetTableRow(f7,g9);
if (l3!=null)return l3;
var f3=this.GetData(f7).getElementsByTagName("root")[0];
var f2=f3.getElementsByTagName("data")[0];
if (document.all!=null){
l3=this.GetData(f7).createNode("element","row","");
}else {
l3=document.createElement("row");
l3.style.display="none";
}
l3.setAttribute("key",g9);
f2.appendChild(l3);
return l3;
}
this.AddTableCell=function (i2,h1){
if (i2==null)return null;
var l3=this.GetTableCell(i2,h1);
if (l3!=null)return l3;
if (document.all!=null){
l3=this.GetData(f7).createNode("element","cell","");
}else {
l3=document.createElement("cell");
l3.style.display="none";
}
l3.setAttribute("key",h1);
i2.appendChild(l3);
return l3;
}
this.GetCellValue=function (f7,h2){
if (h2==null)return null;
var g9=this.GetRowKeyFromCell(f7,h2);
var h1=this.GetColKeyFromCell(f7,h2);
var p4=this.AddTableCell(this.AddTableRow(f7,g9),h1);
return p4.innerHTML;
}
this.HTMLEncode=function (s){
var p5=new String(s);
var p6=new RegExp("&","g");
p5=p5.replace(p6,"&amp;");
p6=new RegExp("<","g");
p5=p5.replace(p6,"&lt;");
p6=new RegExp(">","g");
p5=p5.replace(p6,"&gt;");
p6=new RegExp("\"","g");
p5=p5.replace(p6,"&quot;");
return p5;
}
this.HTMLDecode=function (s){
var p5=new String(s);
var p6=new RegExp("&amp;","g");
p5=p5.replace(p6,"&");
p6=new RegExp("&lt;","g");
p5=p5.replace(p6,"<");
p6=new RegExp("&gt;","g");
p5=p5.replace(p6,">");
p6=new RegExp("&quot;","g");
p5=p5.replace(p6,'"');
return p5;
}
this.SetCellValue=function (f7,h2,val,noEvent,recalc){
if (h2==null)return ;
var p7=this.GetCellType(h2);
if (p7=="readonly")return ;
var g9=this.GetRowKeyFromCell(f7,h2);
var h1=this.GetColKeyFromCell(f7,h2);
var p4=this.AddTableCell(this.AddTableRow(f7,g9),h1);
val=this.HTMLEncode(val);
val=this.HTMLEncode(val);
p4.innerHTML=val;
if (!noEvent){
var g0=this.CreateEvent("DataChanged");
g0.cell=h2;
g0.cellValue=val;
g0.row=g9;
g0.col=h1;
this.FireEvent(f7,g0);
}
var f5=this.GetCmdBtn(f7,"Update");
if (f5!=null&&f5.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f5,false);
f5=this.GetCmdBtn(f7,"Cancel");
if (f5!=null&&f5.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f5,false);
f7.e3=true;
if (recalc){
this.UpdateValues(f7);
}
}
this.GetSelectedRanges=function (f7){
var l2=this.GetSelection(f7);
var g2=new Array();
var l3=l2.firstChild;
while (l3!=null){
var h5=new this.Range();
this.GetRangeFromNode(f7,l3,h5);
var f6=g2.length;
g2.length=f6+1;
g2[f6]=h5;
l3=l3.nextSibling;
}
return g2;
}
this.GetSelectedRange=function (f7){
var h5=new this.Range();
var l2=this.GetSelection(f7);
var l3=l2.lastChild;
if (l3!=null){
this.GetRangeFromNode(f7,l3,h5);
}
return h5;
}
this.GetRangeFromNode=function (f7,l3,h5){
if (l3==null||f7==null||h5==null)return ;
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
var i0=this.GetViewport(f7);
if (i0!=null){
var p8=this.GetDisplayIndex(f7,g9);
for (var e9=p8;e9<p8+l4;e9++){
if (this.IsChildSpreadRow(f7,i0,e9))l4--;
}
}
var p9=null;
if (g9<0&&h1<0&&l4!=0&&g8!=0)
p9="Table";
else if (g9<0&&h1>=0&&g8>0)
p9="Column";
else if (h1<0&&g9>=0&&l4>0)
p9="Row";
else 
p9="Cell";
this.SetRange(h5,p9,g9,h1,l4,g8);
}
this.GetSelection=function (f7){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var q0=l5.getElementsByTagName("selection")[0];
return q0;
}
this.GetRowKeyFromRow=function (f7,g9){
if (g9<0)return null;
var e5=null;
e5=this.GetViewport(f7);
return e5.rows[g9].getAttribute("FpKey");
}
this.GetColKeyFromCol=function (f7,h1){
if (h1<0)return null;
var e5=this.GetViewport(f7);
var l0=this.GetColGroup(e5);
if (l0==null||l0.childNodes.length==0)
l0=this.GetColGroup(this.GetColHeader(f7));
if (l0!=null&&h1>=0&&h1<l0.childNodes.length){
return l0.childNodes[h1].getAttribute("FpCol");
}
return null;
}
this.GetRowKeyFromCell=function (f7,h2){
var g9=h2.parentNode.getAttribute("FpKey");
return g9;
}
this.GetColKeyFromCell=function (f7,h2){
var l1=this.GetColFromCell(f7,h2);
var e5=this.GetViewport(f7);
var l0=this.GetColGroup(e5);
if (l0!=null&&l1>=0&&l1<l0.childNodes.length){
return l0.childNodes[l1].getAttribute("FpCol");
}
return null;
}
this.SetSelection=function (f7,i2,l1,rowcount,colcount,addSelection){
if (!f7.initialized)return ;
var q1=i2;
var q2=l1;
if (i2!=null&&parseInt(i2)>=0){
i2=this.GetRowKeyFromRow(f7,i2);
if (i2!="newRow")
i2=parseInt(i2);
}
if (l1!=null&&parseInt(l1)>=0){
l1=parseInt(this.GetColKeyFromCol(f7,l1));
}
var l3=this.GetSelection(f7);
if (l3==null)return ;
if (addSelection==null)
addSelection=(f7.getAttribute("multiRange")=="true"&&!this.a7);
var q3=l3.lastChild;
if (q3==null||addSelection){
if (document.all!=null){
q3=this.GetData(f7).createNode("element","range","");
}else {
q3=document.createElement('range');
q3.style.display="none";
}
l3.appendChild(q3);
}
q3.setAttribute("row",i2);
q3.setAttribute("col",l1);
q3.setAttribute("rowcount",rowcount);
q3.setAttribute("colcount",colcount);
q3.setAttribute("rowIndex",q1);
q3.setAttribute("colIndex",q2);
f7.e3=true;
this.PaintFocusRect(f7);
var f5=this.GetCmdBtn(f7,"Update");
this.UpdateCmdBtnState(f5,false);
var g0=this.CreateEvent("SelectionChanged");
this.FireEvent(f7,g0);
}
this.CreateSelectionNode=function (f7,i2,l1,rowcount,colcount,q1,q2){
var q3=document.createElement('range');
q3.style.display="none";
q3.setAttribute("row",i2);
q3.setAttribute("col",l1);
q3.setAttribute("rowcount",rowcount);
q3.setAttribute("colcount",colcount);
q3.setAttribute("rowIndex",q1);
q3.setAttribute("colIndex",q2);
return q3;
}
this.AddRowToSelection=function (f7,l3,i2){
var q1=i2;
if (typeof(i2)!="undefined"&&parseInt(i2)>=0){
i2=this.GetRowKeyFromRow(f7,i2);
if (i2!="newRow")
i2=parseInt(i2);
}
if (!this.IsRowSelected(f7,i2))
{
var q3=this.CreateSelectionNode(i2,-1,1,-1,q1,-1);
l3.appendChild(q3);
}
}
this.RemoveSelection=function (f7,i2,l1,rowcount,colcount){
var l3=this.GetSelection(f7);
if (l3==null)return ;
var q3=l3.firstChild;
while (q3!=null){
var g9=parseInt(q3.getAttribute("rowIndex"));
var l4=parseInt(q3.getAttribute("rowcount"));
if (g9<=i2&&i2<g9+l4){
l3.removeChild(q3);
for (var e9=g9;e9<g9+l4;e9++){
if (e9!=i2){
this.AddRowToSelection(f7,l3,e9);
}
}
break ;
}
q3=q3.nextSibling;
}
f7.e3=true;
var f5=this.GetCmdBtn(f7,"Update");
this.UpdateCmdBtnState(f5,false);
var g0=this.CreateEvent("SelectionChanged");
this.FireEvent(f7,g0);
}
this.GetColInfo=function (f7,h1){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var l1=l5.getElementsByTagName("colinfo")[0];
var f6=l1.firstChild;
while (f6!=null){
if (f6.getAttribute("key")==""+h1)return f6;
f6=f6.nextSibling;
}
return null;
}
this.GetColWidthFromCol=function (f7,h1){
var l0=this.GetColGroup(this.GetViewport(f7));
return parseInt(l0.childNodes[h1].width);
}
this.GetColWidth=function (colInfo){
if (colInfo==null)return null;
var l3=colInfo.getElementsByTagName("width")[0];
if (l3!=null)return l3.innerHTML;
return 0;
}
this.AddColInfo=function (f7,h1){
var l3=this.GetColInfo(f7,h1);
if (l3!=null)return l3;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var l1=l5.getElementsByTagName("colinfo")[0];
if (document.all!=null){
l3=this.GetData(f7).createNode("element","col","");
}else {
l3=document.createElement('col');
l3.style.display="none";
}
l3.setAttribute("key",h1);
l1.appendChild(l3);
return l3;
}
this.SetColWidth=function (f7,l1,width){
if (l1==null)return ;
l1=parseInt(l1);
var i9=this.IsXHTML(f7);
var q4=null;
if (this.GetViewport(f7)!=null){
var l0=this.GetColGroup(this.GetViewport(f7));
if (l0==null||l0.childNodes.length==0){
l0=this.GetColGroup(this.GetColHeader(f7));
}
q4=this.AddColInfo(f7,l0.childNodes[l1].getAttribute("FpCol"));
if (this.GetViewport(f7).cellSpacing=="0"&&this.GetColCount(f7)>1&&this.GetViewport(f7).rules!="rows"){
if (l1==0)width-=1;
}
if (width==0)width=1;
if (l0!=null)
l0.childNodes[l1].width=width;
this.SetWidthFix(this.GetViewport(f7),l1,width);
}
if (this.GetColHeader(f7)!=null){
if (this.GetViewport(f7)!=null){
if (this.GetViewport(f7).cellSpacing=="0"&&this.GetColCount(f7)>1&&this.GetViewport(f7).rules!="rows"){
if (i9){
if (l1==this.colCount-1)width-=1;
}
}
}
if (width<=0)width=1;
document.getElementById(f7.id+"col"+l1).width=width;
this.SetWidthFix(this.GetColHeader(f7),l1,width);
if (this.GetViewport(f7)!=null){
if (this.GetViewport(f7).cellSpacing=="0"&&this.GetColCount(f7)>1&&this.GetViewport(f7).rules!="rows"){
if (l1==this.GetColCount(f7)-1)width+=1;
}
}
}
var e7=this.GetTopSpread(f7);
this.SizeAll(e7);
this.Refresh(e7);
if (q4!=null){
var l3=q4.getElementsByTagName("width");
if (l3!=null&&l3.length>0)
l3[0].innerHTML=width;
else {
if (document.all!=null){
l3=this.GetData(f7).createNode("element","width","");
}else {
l3=document.createElement('width');
l3.style.display="none";
}
q4.appendChild(l3);
l3.innerHTML=width;
}
}
var f5=this.GetCmdBtn(f7,"Update");
if (f5!=null)this.UpdateCmdBtnState(f5,false);
f7.e3=true;
}
this.SetWidthFix=function (e5,l1,width){
if (e5==null||e5.rows.length==0)return ;
var e9=0;
var q5=0;
var i5=e5.rows[0].cells[0];
var q6=i5.colSpan;
if (q6==null)q6=1;
while (l1>q5+q6){
e9++;
q5=q5+q6;
i5=e5.rows[0].cells[e9];
q6=i5.colSpan;
if (q6==null)q6=1;
}
i5.width=width;
}
this.GetRowInfo=function (f7,g9){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var i2=l5.getElementsByTagName("rowinfo")[0];
var f6=i2.firstChild;
while (f6!=null){
if (f6.getAttribute("key")==""+g9)return f6;
f6=f6.nextSibling;
}
return null;
}
this.GetRowHeight=function (o1){
if (o1==null)return null;
var q7=o1.getElementsByTagName("height");
if (q7!=null&&q7.length>0)return q7[0].innerHTML;
return 0;
}
this.AddRowInfo=function (f7,g9){
var l3=this.GetRowInfo(f7,g9);
if (l3!=null)return l3;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var i2=l5.getElementsByTagName("rowinfo")[0];
if (document.all!=null){
l3=this.GetData(f7).createNode("element","row","");
}else {
l3=document.createElement('row');
l3.style.display="none";
}
l3.setAttribute("key",g9);
i2.appendChild(l3);
return l3;
}
this.GetTopSpread=function (g0)
{
if (g0==null)return null;
var g2=this.GetSpread(g0);
if (g2==null)return null;
var f6=g2.parentNode;
while (f6!=null&&f6.tagName!="BODY")
{
if (f6.getAttribute("FpSpread")=="Spread"){
if (f6.getAttribute("hierView")=="true")
g2=f6;
else 
break ;
}
f6=f6.parentNode;
}
return g2;
}
this.GetParentSpread=function (f7)
{
try {
var f6=f7.parentNode;
while (f6!=null&&f6.getAttribute("FpSpread")!="Spread")f6=f6.parentNode;
if (f6!=null&&f6.getAttribute("hierView")=="true")
return f6;
else 
return null;
}catch (g0){
return null;
}
}
this.SetRowHeight=function (f7,o1,height){
if (o1==null)return ;
var l3=o1.getElementsByTagName("height");
if (l3!=null&&l3.length>0)
l3[0].innerHTML=height;
else {
if (document.all!=null){
l3=this.GetData(f7).createNode("element","height","");
}else {
l3=document.createElement('height');
l3.style.display="none";
}
o1.appendChild(l3);
l3.innerHTML=height;
}
var f5=this.GetCmdBtn(f7,"Update");
if (f5!=null)this.UpdateCmdBtnState(f5,false);
f7.e3=true;
}
this.SetActiveRow=function (f7,i2){
if (this.GetRowCount(f7)<1)return ;
if (i2==null)i2=-1;
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var l6=l5.firstChild;
while (l6!=null&&l6.tagName!="activerow"&&l6.tagName!="ACTIVEROW"){
l6=l6.nextSibling;
}
if (l6!=null)
l6.innerHTML=""+i2;
if (i2!=null&&f7.getAttribute("IsNewRow")!="true"&&f7.getAttribute("AllowInsert")=="true"){
var f5=this.GetCmdBtn(f7,"Insert");
this.UpdateCmdBtnState(f5,false);
f5=this.GetCmdBtn(f7,"Add");
this.UpdateCmdBtnState(f5,false);
}else {
var f5=this.GetCmdBtn(f7,"Insert");
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(f7,"Add");
this.UpdateCmdBtnState(f5,true);
}
if (i2!=null&&f7.getAttribute("IsNewRow")!="true"&&(f7.getAttribute("AllowDelete")==null||f7.getAttribute("AllowDelete")=="true")){
var f5=this.GetCmdBtn(f7,"Delete");
this.UpdateCmdBtnState(f5,(i2==-1));
}else {
var f5=this.GetCmdBtn(f7,"Delete");
this.UpdateCmdBtnState(f5,true);
}
f7.e3=true;
}
this.SetActiveCol=function (f7,l1){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var l5=f3.getElementsByTagName("state")[0];
var l7=l5.firstChild;
while (l7!=null&&l7.tagName!="activecolumn"&&l7.tagName!="ACTIVECOLUMN"){
l7=l7.nextSibling;
}
if (l7!=null)
l7.innerHTML=""+parseInt(l1);
f7.e3=true;
}
this.GetEditor=function (h2){
if (h2==null)return null;
var p7=this.GetCellType(h2);
if (p7=="readonly")return null;
var h9=h2.getElementsByTagName("INPUT");
if (h9!=null&&h9.length>0){
var f6=h9[0];
while (f6!=null&&f6.getAttribute("FpEditor")==null)f6=f6.parentNode;
return f6;
}
h9=h2.getElementsByTagName("SELECT");
if (h9!=null&&h9.length>0){
var f6=h9[0];
return f6;
}
return null;
}
this.GetPageActiveSpread=function (){
var q8=document.documentElement.getAttribute("FpActiveSpread");
var f6=null;
if (q8!=null)f6=document.getElementById(q8);
return f6;
}
this.SetPageActiveSpread=function (f7){
if (f7==null)
document.documentElement.setAttribute("FpActiveSpread",null);
else 
document.documentElement.setAttribute("FpActiveSpread",f7.id);
}
this.DoResize=function (event){
if (the_fpSpread.spreads==null)return ;
var e8=the_fpSpread.spreads.length;
for (var e9=0;e9<e8;e9++){
if (the_fpSpread.spreads[e9]!=null)the_fpSpread.SizeSpread(the_fpSpread.spreads[e9]);
}
}
this.DblClick=function (event){
var h2=this.GetCell(this.GetTarget(event),true,event);
var f7=this.GetSpread(h2);
if (h2!=null&&!this.IsHeaderCell(h2)&&h2==f7.d2)this.StartEdit(f7,h2);
}
this.GetEvent=function (g0){
if (g0!=null)return g0;
return window.event;
}
this.GetTarget=function (g0){
g0=this.GetEvent(g0);
if (g0.target==document){
if (g0.currentTarget!=null)return g0.currentTarget;
}
if (g0.target!=null)return g0.target;
return g0.srcElement;
}
this.StartEdit=function (f7,editCell){
var q9=this.GetOperationMode(f7);
if (q9=="SingleSelect"||q9=="ReadOnly"||this.a8)return ;
var h2=editCell;
if (h2==null)h2=f7.d2;
if (h2==null)return ;
this.b2=-1;
var h9=this.GetEditor(h2);
if (h9!=null){
this.a8=true;
this.a9=h9;
this.b2=1;
}
var i9=this.IsXHTML(f7);
if (h2!=null){
var g9=this.GetRowFromCell(f7,h2);
var h1=this.GetColFromCell(f7,h2);
var g0=this.CreateEvent("EditStart");
g0.cell=h2;
g0.row=this.GetSheetIndex(f7,g9);
g0.col=h1;
g0.cancel=false;
this.FireEvent(f7,g0);
if (g0.cancel)return ;
var p7=this.GetCellType(h2);
if (p7=="readonly")return ;
if (f7.d2!=h2){
f7.d2=h2;
this.SetActiveRow(f7,this.GetRowKeyFromCell(f7,h2));
this.SetActiveCol(f7,this.GetColKeyFromCell(f7,h2));
}
if (h9==null){
var i7=this.GetRender(h2);
var r0=this.GetValueFromRender(f7,i7);
if (r0==" ")r0="";
this.b0=r0;
this.b1=this.GetFormulaFromCell(h2);
if (this.b1!=null)r0=this.b1;
try {
if (i7!=h2){
i7.style.display="none";
}
else {
i7.innerHTML="";
}
}catch (g0){
return ;
}
var r1=this.GetCellEditorID(f7,h2);
if (r1!=null&&r1.length>0){
this.a9=this.GetCellEditor(f7,r1,true);
this.a9.style.display="inline";
}else {
this.a9=document.createElement("INPUT");
this.a9.type="text";
}
this.a9.style.fontFamily=i7.style.fontFamily;
this.a9.style.fontSize=i7.style.fontSize;
this.a9.style.fontWeight=i7.style.fontWeight;
this.a9.style.fontStyle=i7.style.fontStyle;
this.a9.style.textDecoration=i7.style.textDecoration;
this.a9.style.position="";
if (i9){
var r2=h2.clientWidth-2;
var r3=parseInt(h2.style.paddingLeft);
if (!isNaN(r3))
r2-=r3;
r3=parseInt(h2.style.paddingRight);
if (!isNaN(r3))
r2-=r3;
this.a9.style.width=""+r2+"px";
}
else 
this.a9.style.width=h2.clientWidth-2;
this.SaveMargin(h2);
if (this.a9.tagName=="TEXTAREA")
this.a9.style.height=""+(h2.offsetHeight-4)+"px";
if ((this.a9.tagName=="INPUT"&&this.a9.type=="text")||this.a9.tagName=="TEXTAREA"){
if (this.a9.style.backgroundColor==""||this.a9.backColorSet!=null){
var r4="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)r4=document.defaultView.getComputedStyle(h2,'').getPropertyValue("background-color");
if (r4!="")
this.a9.style.backgroundColor=r4;
else 
this.a9.style.backgroundColor=h2.bgColor;
this.a9.backColorSet=true;
}
if (this.a9.style.color==""||this.a9.colorSet!=null){
var r5="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)r5=document.defaultView.getComputedStyle(h2,'').getPropertyValue("color");
this.a9.style.color=r5;
this.a9.colorSet=true;
}
this.a9.style.borderWidth="0px";
this.RestoreMargin(this.a9,false);
}
this.b2=0;
h2.insertBefore(this.a9,h2.firstChild);
this.SetEditorValue(this.a9,r0);
if (this.a9.offsetHeight<h2.clientHeight&&this.a9.tagName!="TEXTAREA"){
if (h2.vAlign=="middle")
this.a9.style.posTop+=(h2.clientHeight-this.a9.offsetHeight)/2;
else if (h2.vAlign=="bottom")
this.a9.style.posTop+=(h2.clientHeight-this.a9.offsetHeight);
}
this.SizeAll(this.GetTopSpread(f7));
}
this.SetEditorFocus(this.a9);
if (f7.getAttribute("EditMode")=="replace"){
if ((this.a9.tagName=="INPUT"&&this.a9.type=="text")||this.a9.tagName=="TEXTAREA")
this.a9.select();
}
this.a8=true;
var f5=this.GetCmdBtn(f7,"Update");
if (f5!=null&&f5.disabled)
this.UpdateCmdBtnState(f5,false);
f5=this.GetCmdBtn(f7,"Copy");
if (f5!=null&&!f5.disabled)
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(f7,"Paste");
if (f5!=null&&!f5.disabled)
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(f7,"Clear");
if (f5!=null&&!f5.disabled)
this.UpdateCmdBtnState(f5,true);
}
this.ScrollView(f7);
}
this.GetCurrency=function (validator){
var r6=validator.CurrencySymbol;
if (r6!=null)return r6;
var f6=document.getElementById(validator.id+"cs");
if (f6!=null){
return f6.innerText;
}
return "";
}
this.GetValueFromRender=function (f7,rd){
var p2=this.GetCellType2(this.GetCell(rd));
if (p2!=null){
if (p2=="text")p2="TextCellType";
var h8=this.GetFunction(p2+"_getValue");
if (h8!=null){
return h8(rd);
}
}
var f6=rd;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
var r0=f6.value;
if (r0==null){
r0=this.ReplaceAll(f6.innerHTML,"&nbsp;"," ");
r0=this.ReplaceAll(r0,"<br>"," ");
r0=this.HTMLDecode(r0);
}
return r0;
}
this.ReplaceAll=function (val,src,dest){
if (val==null)return val;
var r7=val.length;
while (true){
val=val.replace(src,dest);
if (val.length==r7)break ;
r7=val.length;
}
return val;
}
this.GetFormula=function (f7,g9,h1){
g9=this.GetDisplayIndex(f7,g9);
var h2=this.GetCellFromRowCol(f7,g9,h1);
var r8=this.GetFormulaFromCell(h2);
return r8;
}
this.SetFormula=function (f7,g9,h1,h8,recalc,clientOnly){
g9=this.GetDisplayIndex(f7,g9);
var h2=this.GetCellFromRowCol(f7,g9,h1);
h2.setAttribute("FpFormula",h8);
if (!clientOnly)
this.SetCellValue(f7,h2,h8,null,recalc);
}
this.GetFormulaFromCell=function (rd){
var r0=null;
if (rd.getAttribute("FpFormula")!=null){
r0=rd.getAttribute("FpFormula");
}
if (r0!=null)
r0=this.Trim(new String(r0));
return r0;
}
this.IsDouble=function (val,decimalchar,negsign,possign,minimumvalue,maximumvalue){
if (val==null||val.length==0)return true;
val=val.replace(" ","");
if (val.length==0)return true;
if (negsign!=null)val=val.replace(negsign,"-");
if (possign!=null)val=val.replace(possign,"+");
if (val.charAt(val.length-1)=="-")val="-"+val.substring(0,val.length-1);
var r9=new RegExp("^\\s*([-\\+])?(\\d+)?(\\"+decimalchar+"(\\d+))?([eE]([-\\+])?(\\d+))?\\s*$");
var s0=val.match(r9);
if (s0==null)
return false;
if ((s0[2]==null||s0[2].length==0)&&(s0[4]==null||s0[4].length==0))return false;
var s1="";
if (s0[1]!=null&&s0[1].length>0)s1=s0[1];
if (s0[2]!=null&&s0[2].length>0)
s1+=s0[2];
else 
s1+="0";
if (s0[4]!=null&&s0[4].length>0)
s1+=("."+s0[4]);
if (s0[6]!=null&&s0[6].length>0){
s1+=("E"+s0[6]);
if (s0[7]!=null)
s1+=(s0[7]);
else 
s1+="0";
}
var s2=parseFloat(s1);
if (isNaN(s2))return false;
var f6=true;
if (minimumvalue!=null){
var s3=parseFloat(minimumvalue);
f6=(!isNaN(s3)&&s2>=s3);
}
if (f6&&maximumvalue!=null){
var i4=parseFloat(maximumvalue);
f6=(!isNaN(i4)&&s2<=i4);
}
return f6;
}
this.GetFunction=function (fn){
if (fn==null||fn=="")return null;
try {
var f6=eval(fn);
return f6;
}catch (g0){
return null;
}
}
this.SetValueToRender=function (rd,val,valueonly){
var h8=null;
var p2=this.GetCellType2(this.GetCell(rd));
if (p2!=null){
if (p2=="text")p2="TextCellType";
h8=this.GetFunction(p2+"_setValue");
}
if (h8!=null){
h8(rd,val);
}else {
if (typeof(rd.value)!="undefined"){
if (val==null)val="";
rd.value=val;
}else {
var f6=rd;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
f6.innerHTML=this.ReplaceAll(val," ","&nbsp;");
}
}
if ((valueonly==null||!valueonly)&&rd.getAttribute("FpFormula")!=null){
rd.setAttribute("FpFormula",val);
}
}
this.Trim=function (p8){
var s0=p8.match(new RegExp("^\\s*(\\S+(\\s+\\S+)*)\\s*$"));
return (s0==null)?"":s0[1];
}
this.GetOffsetLeft=function (f7,h2,i1){
var e5=i1;
if (e5==null)e5=this.GetViewportFromCell(f7,h2);
var o2=0;
var f6=h2;
while (f6!=null&&f6!=e5){
o2+=f6.offsetLeft;
f6=f6.offsetParent;
}
return o2;
}
this.GetOffsetTop=function (f7,h2,i1){
var e5=i1;
if (e5==null)e5=this.GetViewportFromCell(f7,h2);
var s4=0;
var f6=h2;
while (f6!=null&&f6!=e5){
s4+=f6.offsetTop;
f6=f6.offsetParent;
}
return s4;
}
this.SetEditorFocus=function (f6){
if (f6==null)return ;
var s5=true;
var h2=this.GetCell(f6,true);
var p2=this.GetCellType(h2);
if (p2!=null){
var h8=this.GetFunction(p2+"_setFocus");
if (h8!=null){
h8(f6);
s5=false;
}
}
if (s5){
try {
f6.focus();
}catch (g0){}
}
}
this.SetEditorValue=function (f6,val){
var h2=this.GetCell(f6,true);
var p2=this.GetCellType(h2);
if (p2=="text")p2="TextCellType";
if (p2!=null){
var h8=this.GetFunction(p2+"_setEditorValue");
if (h8!=null){
h8(f6,val);
return ;
}
}
f6.value=val;
}
this.GetEditorValue=function (f6){
var h2=this.GetCell(f6,true);
var p2=this.GetCellType(h2);
if (p2!=null){
var h8=this.GetFunction(p2+"_getEditorValue");
if (h8!=null){
return h8(f6);
}
}
if (f6.type=="checkbox"){
if (f6.checked)
return "True";
else 
return "False";
}
else 
{
return f6.value;
}
}
this.CreateMsg=function (){
if (this.b3!=null)return ;
var f6=this.b3=document.createElement("div");
f6.style.position="absolute";
f6.style.background="yellow";
f6.style.color="red";
f6.style.border="1px solid black";
f6.style.display="none";
f6.style.width="120px";
}
this.SetMsg=function (msg){
this.CreateMsg();
this.b3.innerHTML=msg;
}
this.ShowMsg=function (show){
this.CreateMsg();
if (show){
this.b3.style.display="block";
}
else 
this.b3.style.display="none";
}
this.EndEdit=function (){
if (this.a9!=null&&this.a9.parentNode!=null){
var h2=this.GetCell(this.a9.parentNode);
var f7=this.GetSpread(h2,false);
if (f7==null)return true;
var s6=this.GetEditorValue(this.a9);
var s7=s6;
if (typeof(s6)=="string")s7=this.Trim(s6);
var s8=(f7.getAttribute("AcceptFormula")=="true"&&s7!=null&&s7.charAt(0)=='=');
var h9=(this.b2!=0);
if (!s8&&!h9){
var s9=null;
var p2=this.GetCellType(h2);
if (p2!=null){
var h8=this.GetFunction(p2+"_isValid");
if (h8!=null){
s9=h8(h2,s6);
}
}
if (s9!=null&&s9!=""){
this.SetMsg(s9);
this.GetViewport(f7).parentNode.insertBefore(this.b3,this.GetViewport(f7).parentNode.firstChild);
this.ShowMsg(true);
this.SetValidatorPos(f7);
this.a9.focus();
return false;
}else {
this.ShowMsg(false);
}
}
if (!h9){
h2.removeChild(this.a9);
this.a9.style.display="none";
this.GetViewport(f7).parentNode.appendChild(this.a9);
this.SetEditorValue(this.a9,"");
var t0=this.GetRender(h2);
if (t0.style.display=="none")t0.style.display="block";
if (this.b1!=null&&this.b1==s6){
this.SetValueToRender(t0,this.b0,true);
}else {
this.SetValueToRender(t0,s6);
}
this.RestoreMargin(h2);
}
if ((this.b1!=null&&this.b1!=s6)||(this.b1==null&&this.b0!=s6)){
this.SetCellValue(f7,h2,s6);
if (s6!=null&&s6.length>0&&s6.indexOf("=")==0)h2.setAttribute("FpFormula",s6);
}
if (!h9)
this.SizeAll(this.GetTopSpread(f7));
this.a9=null;
this.a8=false;
var g0=this.CreateEvent("EditStopped");
g0.cell=h2;
this.FireEvent(f7,g0);
this.Focus(f7);
var t1=f7.getAttribute("autoCalc");
if (t1!="false"){
if ((this.b1!=null&&this.b1!=s6)||(this.b1==null&&this.b0!=s6)){
this.UpdateValues(f7);
}
}
}
this.b2=-1;
return true;
}
this.SetValidatorPos=function (f7){
if (this.a9==null)return ;
var h2=this.GetCell(this.a9.parentNode);
if (h2==null)return ;
var f6=this.b3;
if (f6!=null&&f6.style.display!="none"){
if (f6!=null){
f6.style.left=""+(h2.offsetLeft)+"px";
f6.style.top=""+(h2.offsetTop+h2.offsetHeight)+"px";
if (h2.offsetTop+h2.offsetHeight+f6.offsetHeight+16>f6.parentNode.offsetHeight)
f6.style.top=""+(h2.offsetTop-f6.offsetHeight-1)+"px";
}
}
}
this.SaveMargin=function (editCell){
if (editCell.style.paddingLeft!=null&&editCell.style.paddingLeft!=""){
this.b4.left=editCell.style.paddingLeft;
editCell.style.paddingLeft=0;
}
if (editCell.style.paddingRight!=null&&editCell.style.paddingRight!=""){
this.b4.right=editCell.style.paddingRight;
editCell.style.paddingRight=0;
}
if (editCell.style.paddingTop!=null&&editCell.style.paddingTop!=""){
this.b4.top=editCell.style.paddingTop;
editCell.style.paddingTop=0;
}
if (editCell.style.paddingBottom!=null&&editCell.style.paddingBottom!=""){
this.b4.bottom=editCell.style.paddingBottom;
editCell.style.paddingBottom=0;
}
}
this.RestoreMargin=function (h2,reset){
if (this.b4.left!=null&&this.b4.left!=-1){
h2.style.paddingLeft=this.b4.left;
if (reset==null||reset)this.b4.left=-1;
}
if (this.b4.right!=null&&this.b4.right!=-1){
h2.style.paddingRight=this.b4.right;
if (reset==null||reset)this.b4.right=-1;
}
if (this.b4.top!=null&&this.b4.top!=-1){
h2.style.paddingTop=this.b4.top;
if (reset==null||reset)this.b4.top=-1;
}
if (this.b4.bottom!=null&&this.b4.bottom!=-1){
h2.style.paddingBottom=this.b4.bottom;
if (reset==null||reset)this.b4.bottom=-1;
}
}
this.PaintSelectedCell=function (f7,h2,select){
if (select){
if (h2.bgColor1==null)
h2.bgColor1=h2.style.backgroundColor;
h2.style.backgroundColor=f7.getAttribute("selectedBackColor");
}else {
if (h2.bgColor1!=null)
h2.style.backgroundColor="";
if (h2.bgColor1!=null&&h2.bgColor1!="")
h2.style.backgroundColor=h2.bgColor1;
}
}
this.PaintAnchorCell=function (f7){
var t2=this.GetOperationMode(f7);
if (f7.d2==null||(t2!="Normal"&&t2!="RowMode"))return ;
if (t2=="MultiSelect"||t2=="ExtendedSelect")return ;
if (!this.IsChild(f7.d2,f7))return ;
var t3=(this.GetTopSpread(f7).getAttribute("hierView")!="true");
if (f7.getAttribute("showFocusRect")=="false")t3=false;
if (t3){
this.PaintSelectedCell(f7,f7.d2,false);
this.PaintFocusRect(f7);
return ;
}
var f6=f7.d2.parentNode.cells[0].firstChild;
if (f6!=null&&f6.nodeName!="#text"&&f6.getAttribute("FpSpread")=="Spread")return ;
if (f7.d2.bgColor1==null)
f7.d2.bgColor1=f7.d2.style.backgroundColor;
f7.d2.style.backgroundColor=f7.getAttribute("anchorBackColor");
}
this.ClearSelection=function (f7,thisonly){
var t4=this.GetParentSpread(f7);
if (thisonly==null&&t4!=null&&t4.getAttribute("hierView")=="true"){
this.ClearSelection(t4);
return ;
}
var i0=this.GetViewport(f7);
var g6=this.GetRowCount(f7);
if (i0!=null&&i0.rows.length>g6){
for (var e9=0;e9<i0.rows.length;e9++){
if (i0.rows[e9].cells.length>0&&i0.rows[e9].cells[0]!=null&&i0.rows[e9].cells[0].firstChild!=null&&i0.rows[e9].cells[0].firstChild.nodeName!="#text"){
var f6=i0.rows[e9].cells[0].firstChild;
if (f6.getAttribute("FpSpread")=="Spread"){
this.ClearSelection(f6,true);
}
}
}
}
this.DoclearSelection(f7);
if (f7.d2!=null){
var q9=this.GetOperationMode(f7);
if (q9=="RowMode"||q9=="SingleSelect"||q9=="ExtendedSelect"||q9=="MultiSelect"){
var h3=this.GetRowFromCell(f7,f7.d2);
this.PaintSelection(f7,h3,-1,1,-1,false);
}
this.PaintSelectedCell(f7,f7.d2,false);
}else {
var h2=this.GetCellFromRowCol(f7,1,0);
if (h2!=null)this.PaintSelectedCell(f7,h2,false);
}
this.PaintFocusRect(f7);
f7.e3=true;
}
this.SetSelectedRange=function (f7,g9,h1,l4,g8){
this.ClearSelection(f7);
var g9=this.GetDisplayIndex(f7,g9);
var t5=0;
var t6=l4;
var i0=this.GetViewport(f7);
if (i0!=null){
for (e9=g9;e9<i0.rows.length&&t5<t6;e9++){
if (this.IsChildSpreadRow(f7,i0,e9)){;
l4++;
}else {
t5++;
}
}
}
this.PaintSelection(f7,g9,h1,l4,g8,true);
this.SetSelection(f7,g9,h1,l4,g8);
}
this.AddSelection=function (f7,g9,h1,l4,g8){
var g9=this.GetDisplayIndex(f7,g9);
var t5=0;
var t6=l4;
var i0=this.GetViewport(f7);
if (i0!=null){
for (e9=g9;e9<i0.rows.length&&t5<t6;e9++){
if (this.IsChildSpreadRow(f7,i0,e9)){;
l4++;
}else {
t5++;
}
}
}
this.PaintSelection(f7,g9,h1,l4,g8,true);
this.SetSelection(f7,g9,h1,l4,g8,true);
}
this.SelectRow=function (f7,index,t5,select,ignoreAnchor){
f7.d6=index;
f7.d7=-1;
if (!ignoreAnchor)this.UpdateAnchorCell(f7,index,0,false);
f7.d8="r";
this.PaintSelection(f7,index,-1,t5,-1,select);
if (select)
{
this.SetSelection(f7,index,-1,t5,-1);
}else {
this.RemoveSelection(f7,index,-1,t5,-1);
this.PaintFocusRect(f7);
}
}
this.SelectColumn=function (f7,index,t5,select,ignoreAnchor){
f7.d6=-1;
f7.d7=index;
if (!ignoreAnchor)this.UpdateAnchorCell(f7,0,index,false);
f7.d8="c";
this.PaintSelection(f7,-1,index,-1,t5,select);
if (select)
{
this.SetSelection(f7,-1,index,-1,t5);
}
}
this.SelectTable=function (f7,select){
if (select)this.UpdateAnchorCell(f7,0,0,false);
f7.d8="t";
this.PaintSelection(f7,-1,-1,-1,-1,select);
if (select)
{
this.SetSelection(f7,-1,-1,-1,-1);
}
}
this.GetSpanCell=function (g9,h1,span){
if (span==null){
return null;
}
var t5=span.length;
for (var e9=0;e9<t5;e9++){
var o4=span[e9];
var t7=(o4.row<=g9&&g9<o4.row+o4.rowCount&&o4.col<=h1&&h1<o4.col+o4.colCount);
if (t7)return o4;
}
return null;
}
this.IsCovered=function (f7,g9,h1,span){
var o4=this.GetSpanCell(g9,h1,span);
if (o4==null){
return false;
}else {
if (o4.row==g9&&o4.col==h1)return false;
return true;
}
}
this.IsSpanCell=function (f7,g9,h1){
var e0=f7.e0;
var t5=e0.length;
for (var e9=0;e9<t5;e9++){
var o4=e0[e9];
var t7=(o4.row==g9&&o4.col==h1);
if (t7)return o4;
}
return null;
}
this.SelectRange=function (f7,g9,h1,l4,g8,select){
f7.d8="";
this.UpdateRangeSelection(f7,g9,h1,l4,g8,select);
if (select){
this.SetSelection(f7,g9,h1,l4,g8);
this.PaintAnchorCell(f7);
}
}
this.UpdateRangeSelection=function (f7,g9,h1,l4,g8,select){
var i0=this.GetViewport(f7);
this.UpdateRangeSelection(f7,g9,h1,l4,g8,select,i0);
}
this.GetSpanCells=function (f7,i0){
if (i0==this.GetViewport(f7))
return f7.e0;
else if (i0==this.GetColHeader(f7))
return f7.e2;
else if (i0==this.GetRowHeader(f7))
return f7.e1;
return null;
}
this.UpdateRangeSelection=function (f7,g9,h1,l4,g8,select,i0){
if (i0==null)return ;
for (var e9=g9;e9<g9+l4&&e9<i0.rows.length;e9++){
if (this.IsChildSpreadRow(f7,i0,e9))continue ;
var t8=this.GetCellIndex(f7,e9,h1,this.GetSpanCells(f7,i0));
for (var h7=0;h7<g8;h7++){
if (this.IsCovered(f7,e9,h1+h7,this.GetSpanCells(f7,i0)))continue ;
if (t8<i0.rows[e9].cells.length){
this.PaintSelectedCell(f7,i0.rows[e9].cells[t8],select);
}
t8++;
}
}
}
this.GetColFromCell=function (f7,h2){
if (h2==null)return -1;
var g9=this.GetRowFromCell(f7,h2);
return this.GetColIndex(f7,g9,h2.cellIndex,this.GetSpanCells(f7,h2.parentNode.parentNode.parentNode),false,this.IsChild(h2,this.GetRowHeader(f7)));
}
this.GetRowFromCell=function (f7,h2){
if (h2==null||h2.parentNode==null)return -1;
var g9=h2.parentNode.rowIndex;
return g9;
}
this.GetColIndex=function (f7,e9,cellIndex,span,frozArea,c6){
var t9=false;
var e5=this.GetViewport(f7);
if (e5!=null)t9=e5.parentNode.getAttribute("hiddenCells");
if (t9&&span==f7.e0){
return cellIndex;
}
var u0=0;
var t5=this.GetColCount(f7);
var u1=0;
if (c6){
u1=0;
var l0=null;
if (this.GetRowHeader(f7)!=null)
l0=this.GetColGroup(this.GetRowHeader(f7));
if (l0!=null)
t5=l0.childNodes.length;
}
for (var h7=u1;h7<t5;h7++){
if (this.IsCovered(f7,e9,h7,span))continue ;
if (u0==cellIndex){
return h7;
}
u0++;
}
return t5;
}
this.GetCellIndex=function (f7,e9,q2,span){
var t9=false;
var e5=this.GetViewport(f7);
if (e5!=null)t9=e5.parentNode.getAttribute("hiddenCells");
if (t9&&span==f7.e0){
return q2;
}else {
var u1=0;
var t5=q2;
var u0=0;
for (var h7=0;h7<t5;h7++){
if (this.IsCovered(f7,e9,u1+h7,span))continue ;
u0++;
}
return u0;
}
}
this.NextCell=function (f7,event,key){
if (event.altKey)return ;
var u2=this.GetParent(this.GetViewport(f7));
if (f7.d2==null)return ;
if (event.shiftKey&&key!=event.DOM_VK_TAB){
var o0=this.GetOperationMode(f7);
if (o0=="RowMode"||o0=="SingleSelect"||o0=="MultiSelect")return ;
var o4=this.GetSpanCell(f7.d4,f7.d5,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
switch (key){
case event.DOM_VK_RIGHT:
var g9=f7.d4;
var h1=f7.d5+1;
if (o4!=null){
h1=o4.col+o4.colCount;
}
if (h1>this.GetColCount(f7)-1)return ;
f7.d5=h1;
f7.d3=this.GetCellFromRowCol(f7,g9,h1);
this.Select(f7,f7.d2,f7.d3);
break ;
case event.DOM_VK_LEFT:
var g9=f7.d4;
var h1=f7.d5-1;
if (o4!=null){
h1=o4.col-1;
}
o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
if (o4!=null){
if (this.IsSpanCell(f7,g9,o4.col))h1=o4.col;
}
if (h1<0)return ;
f7.d5=h1;
f7.d3=this.GetCellFromRowCol(f7,g9,h1);
this.Select(f7,f7.d2,f7.d3);
break ;
case event.DOM_VK_DOWN:
var g9=f7.d4+1;
var h1=f7.d5;
if (o4!=null){
g9=o4.row+o4.rowCount;
}
g9=this.GetNextRow(f7,g9);
if (g9>this.GetRowCountInternal(f7)-1)return ;
f7.d4=g9;
f7.d3=this.GetCellFromRowCol(f7,g9,h1);
this.Select(f7,f7.d2,f7.d3);
break ;
case event.DOM_VK_UP:
var g9=f7.d4-1;
var h1=f7.d5;
if (o4!=null){
g9=o4.row-1;
}
g9=this.GetPrevRow(f7,g9);
o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
if (o4!=null){
if (this.IsSpanCell(f7,o4.row,h1))g9=o4.row;
}
if (g9<0)return ;
f7.d4=g9;
f7.d3=this.GetCellFromRowCol(f7,g9,h1);
this.Select(f7,f7.d2,f7.d3);
break ;
case event.DOM_VK_HOME:
if (f7.d2.parentNode.rowIndex>=0){
f7.d5=0;
f7.d3=this.GetCellFromRowCol(f7,f7.d4,f7.d5);
this.Select(f7,f7.d2,f7.d3);
}
break ;
case event.DOM_VK_END:
if (f7.d2.parentNode.rowIndex>=0){
f7.d5=this.GetColCount(f7)-1;
f7.d3=this.GetCellFromRowCol(f7,f7.d4,f7.d5);
this.Select(f7,f7.d2,f7.d3);
}
break ;
case event.DOM_VK_PAGE_DOWN:
if (this.GetViewport(f7)!=null&&f7.d2.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(f7).rows.length;g9++){
if (this.GetViewport(f7).rows[g9].offsetTop+this.GetViewport(f7).rows[g9].offsetHeight>this.GetViewport(f7).parentNode.offsetHeight+this.GetViewport(f7).parentNode.scrollTop){
break ;
}
}
g9=this.GetNextRow(f7,g9);
if (g9<this.GetViewport(f7).rows.length){
this.GetViewport(f7).parentNode.scrollTop=this.GetViewport(f7).rows[g9].offsetTop;
f7.d4=g9;
}else {
g9=this.GetRowCountInternal(f7)-1;
f7.d4=g9;
}
f7.d3=this.GetCellFromRowCol(f7,f7.d4,f7.d5);
this.Select(f7,f7.d2,f7.d3);
}
break ;
case event.DOM_VK_PAGE_UP:
if (this.GetViewport(f7)!=null&&f7.d2.parentNode.rowIndex>0){
g9=0;
for (g9=0;g9<this.GetViewport(f7).rows.length;g9++){
if (this.GetViewport(f7).rows[g9].offsetTop+this.GetViewport(f7).rows[g9].offsetHeight>this.GetViewport(f7).parentNode.scrollTop){
break ;
}
}
if (g9<this.GetViewport(f7).rows.length){
var j0=0;
while (g9>0){
j0+=this.GetViewport(f7).rows[g9].offsetHeight;
if (j0>this.GetViewport(f7).parentNode.offsetHeight){
break ;
}
g9--;
}
g9=this.GetPrevRow(f7,g9);
if (g9>=0){
this.GetViewport(f7).parentNode.scrollTop=this.GetViewport(f7).rows[g9].offsetTop;
f7.d4=g9;
f7.d3=this.GetCellFromRowCol(f7,f7.d4,f7.d5);
this.Select(f7,f7.d2,f7.d3);
}
}
}
break ;
}
}else {
if (key==event.DOM_VK_TAB){
if (event.shiftKey)key=event.DOM_VK_LEFT;
else key=event.DOM_VK_RIGHT;
}
var u3=f7.d2;
var g9=f7.d4;
var h1=f7.d5;
switch (key){
case event.DOM_VK_RIGHT:
this.MoveRight(f7,g9,h1);
break ;
case event.DOM_VK_LEFT:
this.MoveLeft(f7,g9,h1);
break ;
case event.DOM_VK_DOWN:
this.MoveDown(f7,g9,h1);
break ;
case event.DOM_VK_UP:
this.MoveUp(f7,g9,h1);
break ;
case event.DOM_VK_HOME:
if (f7.d2.parentNode.rowIndex>=0){
this.UpdateLeadingCell(f7,g9,0);
}
break ;
case event.DOM_VK_END:
if (f7.d2.parentNode.rowIndex>=0){
h1=this.GetColCount(f7)-1;
this.UpdateLeadingCell(f7,g9,h1);
}
break ;
case event.DOM_VK_PAGE_DOWN:
if (this.GetViewport(f7)!=null&&f7.d2.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(f7).rows.length;g9++){
if (this.GetViewport(f7).rows[g9].offsetTop+this.GetViewport(f7).rows[g9].offsetHeight>this.GetViewport(f7).parentNode.offsetHeight+this.GetViewport(f7).parentNode.scrollTop){
break ;
}
}
g9=this.GetNextRow(f7,g9);
if (g9<this.GetViewport(f7).rows.length){
var f6=this.GetViewport(f7).rows[g9].offsetTop;
this.UpdateLeadingCell(f7,g9,f7.d5);
this.GetViewport(f7).parentNode.scrollTop=f6;
}else {
g9=this.GetPrevRow(f7,this.GetRowCount(f7)-1);
this.UpdateLeadingCell(f7,g9,f7.d5);
}
}
break ;
case event.DOM_VK_PAGE_UP:
if (this.GetViewport(f7)!=null&&f7.d2.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(f7).rows.length;g9++){
if (this.GetViewport(f7).rows[g9].offsetTop+this.GetViewport(f7).rows[g9].offsetHeight>this.GetViewport(f7).parentNode.scrollTop){
break ;
}
}
if (g9<this.GetViewport(f7).rows.length){
var j0=0;
while (g9>=0){
j0+=this.GetViewport(f7).rows[g9].offsetHeight;
if (j0>this.GetViewport(f7).parentNode.offsetHeight){
break ;
}
g9--;
}
g9=this.GetPrevRow(f7,g9);
if (g9>=0){
var f6=this.GetViewport(f7).rows[g9].offsetTop;
this.UpdateLeadingCell(f7,g9,f7.d5);
this.GetViewport(f7).parentNode.scrollTop=f6;
}
}
}
break ;
}
if (u3!=f7.d2){
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=f7.id;
g0.Row=g0.row=this.GetSheetIndex(f7,this.GetRowFromCell(f7,f7.d2));
g0.Col=g0.col=this.GetColFromCell(f7,f7.d2);
this.FireEvent(f7,g0);
}
}
var h2=this.GetCellFromRowCol(f7,f7.d4,f7.d5);
if (key==event.DOM_VK_LEFT&&h2.offsetLeft<u2.scrollLeft){
if (h2.cellIndex>0)
u2.scrollLeft=f7.d2.offsetLeft;
else 
u2.scrollLeft=0;
}else if (h2.cellIndex==0){
u2.scrollLeft=0;
}
if (key==event.DOM_VK_RIGHT&&h2.offsetLeft+h2.offsetWidth>u2.scrollLeft+u2.offsetWidth-10){
u2.scrollLeft+=h2.offsetWidth;
}
if (key==event.DOM_VK_UP&&h2.parentNode.offsetTop<u2.scrollTop){
if (h2.parentNode.rowIndex>1)
u2.scrollTop=h2.parentNode.offsetTop;
else 
u2.scrollTop=0;
}else if (h2.parentNode.rowIndex==1){
u2.scrollTop=0;
}
var u4=this.GetParent(this.GetViewport(f7));
u2=this.GetParent(this.GetViewport(f7));
if (key==event.DOM_VK_DOWN&&this.IsChild(h2,u2)&&h2.offsetTop+h2.offsetHeight>u2.scrollTop+u2.clientHeight){
u4.scrollTop+=h2.offsetHeight;
}
if (h2!=null&&h2.offsetWidth<u2.clientWidth){
if (this.IsChild(h2,u2)&&h2.offsetLeft+h2.offsetWidth>u2.scrollLeft+u2.clientWidth){
u4.scrollLeft=h2.offsetLeft+h2.offsetWidth-u2.clientWidth;
}
}
if (this.IsChild(h2,u2)&&h2.offsetTop+h2.offsetHeight>u2.scrollTop+u2.clientHeight&&h2.offsetHeight<u2.clientHeight){
u4.scrollTop=h2.offsetTop+h2.offsetHeight-u2.clientHeight;
}
if (h2.offsetTop<u2.scrollTop){
u4.scrollTop=h2.offsetTop;
}
this.ScrollView(f7);
this.EnableButtons(f7);
this.SaveData(f7);
if (f7.d2!=null){
var h9=this.GetEditor(f7.d2);
if (h9!=null){
if (h9.tagName!="SELECT")
h9.focus();
if (!h9.disabled&&(h9.type==null||h9.type=="checkbox"||h9.type=="radio"||h9.type=="text"||h9.type=="password")){
this.a8=true;
this.a9=h9;
this.b0=this.GetEditorValue(h9);
}
}
}
this.Focus(f7);
}
this.MoveUp=function (f7,g9,h1){
var l4=this.GetRowCountInternal(f7);
var g8=this.GetColCount(f7);
g9--;
g9=this.GetPrevRow(f7,g9);
if (g9>=0){
f7.d4=g9;
this.UpdateLeadingCell(f7,f7.d4,f7.d5);
}
}
this.MoveDown=function (f7,g9,h1){
var l4=this.GetRowCountInternal(f7);
var g8=this.GetColCount(f7);
var o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
if (o4!=null){
g9=o4.row+o4.rowCount;
}else {
g9++;
}
g9=this.GetNextRow(f7,g9);
if (g9<l4){
f7.d4=g9;
this.UpdateLeadingCell(f7,f7.d4,f7.d5);
}
}
this.MoveLeft=function (f7,g9,h1){
var l4=this.GetRowCountInternal(f7);
var g8=this.GetColCount(f7);
var o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
if (o4!=null){
h1=o4.col-1;
}else {
h1--;
}
if (h1<0){
h1=g8-1;
g9--;
if (g9<0){
g9=l4-1;
}
g9=this.GetPrevRow(f7,g9);
f7.d4=g9;
}
this.UpdateLeadingCell(f7,f7.d4,h1);
}
this.MoveRight=function (f7,g9,h1){
var l4=this.GetRowCountInternal(f7);
var g8=this.GetColCount(f7);
var o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,this.GetViewportFromCell(f7,f7.d2)));
if (o4!=null){
h1=o4.col+o4.colCount;
}else {
h1++;
}
if (h1>=g8){
h1=0;
g9++;
if (g9>=l4)g9=0;
f7.d4=this.GetNextRow(f7,g9);
}
this.UpdateLeadingCell(f7,f7.d4,h1);
}
this.UpdateLeadingCell=function (f7,g9,h1){
var h8=this.FireActiveCellChangingEvent(f7,g9,h1);
if (!h8){
this.ClearSelection(f7);
f7.d5=h1;
f7.d4=g9;
f7.d7=h1;
f7.d6=g9;
this.UpdateAnchorCell(f7,g9,h1);
}
}
this.GetPrevRow=function (f7,g9){
if (g9<0)return 0;
var i0=this.GetViewport(f7);
if (i0!=null){
while (g9>0&&i0.rows[g9].cells.length>0){
if (this.IsChildSpreadRow(f7,i0,g9))
g9--;
else 
break ;
}
}
return g9;
}
this.GetNextRow=function (f7,g9){
var i0=this.GetViewport(f7);
while (i0!=null&&g9<i0.rows.length){
if (this.IsChildSpreadRow(f7,i0,g9))g9++;
else 
break ;
}
return g9;
}
this.FireActiveCellChangingEvent=function (f7,i2,l1){
var g0=this.CreateEvent("ActiveCellChanging");
g0.cancel=false;
g0.cmdID=f7.id;
g0.row=this.GetSheetIndex(f7,i2);
g0.col=l1;
this.FireEvent(f7,g0);
return g0.cancel;
}
this.GetSheetRowIndex=function (f7,g9){
g9=this.GetDisplayIndex(f7,g9);
if (g9<0)return -1;
var k4=this.GetViewport(f7).rows[g9];
if (k4!=null){
return k4.getAttribute("FpKey");
}else {
return -1;
}
}
this.GetSheetColIndex=function (f7,h1){
var l1=-1;
var l0=null;
var u5=this.GetColHeader(f7);
if (u5!=null&&u5.rows.length>0){
l0=this.GetColGroup(u5);
}else {
var e5=this.GetViewport(f7);
if (e5!=null&&e5.rows.length>0){
l0=this.GetColGroup(e5);
}
}
if (l0!=null&&h1>=0&&h1<l0.childNodes.length){
l1=l0.childNodes[h1].getAttribute("FpCol");
}
return l1;
}
this.GetCellByRowCol=function (f7,g9,h1){
g9=this.GetDisplayIndex(f7,g9);
return this.GetCellFromRowCol(f7,g9,h1);
}
this.GetHeaderCellFromRowCol=function (f7,g9,h1,c7){
if (g9<0||h1<0)return null;
var e5=null;
if (c7){
e5=this.GetColHeader(f7);
}else {
e5=this.GetRowHeader(f7);
}
var o4=this.GetSpanCell(g9,h1,this.GetSpanCells(f7,e5));
if (o4!=null){
g9=o4.row;
h1=o4.col;
}
var u6=this.GetCellIndex(f7,g9,h1,this.GetSpanCells(f7,e5));
return e5.rows[g9].cells[u6];
}
this.GetCellFromRowCol=function (f7,g9,h1,prevCell){
if (g9<0||h1<0)return null;
var e5=null;
{
e5=this.GetViewport(f7);
}
var e0=f7.e0;
var o4=this.GetSpanCell(g9,h1,e0);
if (o4!=null){
g9=o4.row;
h1=o4.col;
}
var u6=0;
var t9=false;
if (e5!=null)t9=e5.parentNode.getAttribute("hiddenCells");
if (prevCell!=null&&!t9){
if (prevCell.cellIndex<prevCell.parentNode.cells.length-1)
u6=prevCell.cellIndex+1;
}
else 
{
u6=this.GetCellIndex(f7,g9,h1,e0);
}
if (g9>=0&&g9<e5.rows.length)
return e5.rows[g9].cells[u6];
else 
return null;
}
this.GetHiddenValue=function (f7,g9,colName){
if (colName==null)return ;
g9=this.GetDisplayIndex(f7,g9);
var r0=null;
var e5=null;
e5=this.GetViewport(f7);
if (e5!=null&&g9>=0&&g9<e5.rows.length){
var k4=e5.rows[g9];
r0=k4.getAttribute("hv"+colName);
}
return r0;
}
this.GetValue=function (f7,g9,h1){
g9=this.GetDisplayIndex(f7,g9);
var h2=this.GetCellFromRowCol(f7,g9,h1);
var i7=this.GetRender(h2);
var r0=this.GetValueFromRender(f7,i7);
if (r0!=null)r0=this.Trim(r0.toString());
return r0;
}
this.SetValue=function (f7,g9,h1,s6,noEvent,recalc){
g9=this.GetDisplayIndex(f7,g9);
if (s6!=null&&typeof(s6)!="string")s6=new String(s6);
var h2=this.GetCellFromRowCol(f7,g9,h1);
if (this.ValidateCell(f7,h2,s6)){
this.SetCellValueFromView(h2,s6);
if (s6!=null){
this.SetCellValue(f7,h2,""+s6,noEvent,recalc);
}else {
this.SetCellValue(f7,h2,"",noEvent,recalc);
}
}else {
if (f7.getAttribute("lcidMsg")!=null)
alert(f7.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
}
}
this.SetActiveCell=function (f7,g9,h1){
this.ClearSelection(f7,true);
g9=this.GetDisplayIndex(f7,g9);
this.UpdateAnchorCell(f7,g9,h1);
this.ResetLeadingCell(f7);
}
this.GetOperationMode=function (f7){
var t2=f7.getAttribute("OperationMode");
return t2;
}
this.SetOperationMode=function (f7,t2){
f7.setAttribute("OperationMode",t2);
}
this.UpdateAnchorCell=function (f7,g9,h1,select){
if (g9<0||h1<0)return ;
f7.d2=this.GetCellFromRowCol(f7,g9,h1);
if (f7.d2==null)return ;
this.SetActiveRow(f7,this.GetRowKeyFromCell(f7,f7.d2));
this.SetActiveCol(f7,this.GetColKeyFromCell(f7,f7.d2));
if (select==null||select){
var t2=this.GetOperationMode(f7);
if (t2=="RowMode"||t2=="SingleSelect"||t2=="ExtendedSelect"||t2=="MultiSelect")
this.SelectRow(f7,g9,1,true,true);
else 
this.SelectRange(f7,g9,h1,1,1,true);
}
}
this.ResetLeadingCell=function (f7){
if (f7.d2==null||!this.IsChild(f7.d2,f7))return ;
f7.d4=this.GetRowFromCell(f7,f7.d2);
f7.d5=this.GetColFromCell(f7,f7.d2);
this.SelectRange(f7.d4,f7.d5,1,1,true);
}
this.Update=function (f7){
if (this.a8)return ;
this.SaveData(f7);
var q8=f7.getAttribute("name");
__doPostBack(q8,"Update");
}
this.Cancel=function (f7){
var f6=document.getElementById(f7.id+"_data");
f6.value="";
this.SaveData(f7);
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Cancel",f7);
else 
__doPostBack(q8,"Cancel");
}
this.Add=function (f7){
if (this.a8)return ;
var q8=null;
var m9=this.GetPageActiveSpread();
if (m9!=null){
q8=m9.getAttribute("name");
}else {
q8=f7.getAttribute("name");
}
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Add",f7);
else 
__doPostBack(q8,"Add");
}
this.Insert=function (f7){
if (this.a8)return ;
var q8=null;
var m9=this.GetPageActiveSpread();
if (m9!=null){
q8=m9.getAttribute("name");
}else {
q8=f7.getAttribute("name");
}
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Insert",f7);
else 
__doPostBack(q8,"Insert");
}
this.Delete=function (f7){
if (this.a8)return ;
var q8=null;
var m9=this.GetPageActiveSpread();
if (m9!=null){
q8=m9.getAttribute("name");
}else {
q8=f7.getAttribute("name");
}
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Delete",f7);
else 
__doPostBack(q8,"Delete");
}
this.Print=function (f7){
if (this.a8)return ;
this.SaveData(f7);
if (document.printSpread==null){
var f6=document.createElement("IFRAME");
f6.name="printSpread";
f6.style.position="absolute";
f6.style.left="-10px";
f6.style.width="0px";
f6.style.height="0px";
document.printSpread=f6;
document.body.insertBefore(f6,null);
f6.addEventListener("load",the_fpSpread.PrintSpread,false);
}
var u8=this.GetForm(f7);
if (u8==null)return ;
{
var h8=u8;
h8.__EVENTTARGET.value=f7.getAttribute("name");
h8.__EVENTARGUMENT.value="Print";
var u9=h8.target;
h8.target="printSpread";
h8.submit();
h8.target=u9;
}
}
this.PrintSpread=function (){
document.printSpread.contentWindow.focus();
document.printSpread.contentWindow.print();
window.focus();
var m9=this.GetPageActiveSpread();
if (m9!=null)this.Focus(m9);
}
this.GotoPage=function (f7,e8){
if (this.a8)return ;
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Page,"+e8,f7);
else 
__doPostBack(q8,"Page,"+e8);
}
this.Next=function (f7){
if (this.a8)return ;
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Next",f7);
else 
__doPostBack(q8,"Next");
}
this.Prev=function (f7){
if (this.a8)return ;
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Prev",f7);
else 
__doPostBack(q8,"Prev");
}
this.GetViewportFromCell=function (f7,i5){
if (i5!=null){
var f6=i5;
while (f6!=null){
if (f6.tagName=="TABLE")break ;
f6=f6.parentNode;
}
if (f6==this.GetViewport(f7))
return f6;
}
return null;
}
this.IsChild=function (h2,i1){
if (h2==null||i1==null)return false;
var f6=h2.parentNode;
while (f6!=null){
if (f6==i1)return true;
f6=f6.parentNode;
}
return false;
}
this.GetCorner=function (f7){
return f7.c5;
}
this.Select=function (f7,cl1,cl2){
if (this.GetSpread(cl1)!=f7||this.GetSpread(cl2)!=f7)return ;
var h3=f7.d6;
var h4=f7.d7;
var v0=this.GetRowFromCell(f7,cl2);
var k5=0;
if (f7.d8=="r"){
k5=-1;
if (this.IsChild(cl2,this.GetColHeader(f7)))
v0=0;
}else if (f7.d8=="c"){
if (this.IsChild(cl2,this.GetRowHeader(f7)))
k5=0;
else 
k5=this.GetColFromCell(f7,cl2);
v0=-1;
}
else {
if (this.IsChild(cl2,this.GetColHeader(f7))){
v0=0;k5=this.GetColFromCell(f7,cl2);
}else if (this.IsChild(cl2,this.GetRowHeader(f7))){
k5=0;
}else {
k5=this.GetColFromCell(f7,cl2);
}
}
if (f7.d8=="t"){
h4=k5=h3=v0=-1;
}
var f6=Math.max(h3,v0);
h3=Math.min(h3,v0);
v0=f6;
f6=Math.max(h4,k5);
h4=Math.min(h4,k5);
k5=f6;
var h5=null;
var l2=this.GetSelection(f7);
var l3=l2.lastChild;
if (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
h5=new this.Range();
this.SetRange(h5,"cell",g9,h1,l4,g8);
}
if (h5!=null&&h5.col==-1&&h5.row==-1)return ;
if (h5!=null&&h5.col==-1&&h5.row>=0){
if (h5.row>v0||h5.row+h5.rowCount-1<h3){
this.PaintSelection(f7,h5.row,h5.col,h5.rowCount,h5.colCount,false);
this.PaintSelection(f7,h3,h4,v0-h3+1,k5-h4+1,true);
}else {
if (h3>h5.row){
var f6=h3-h5.row;
this.PaintSelection(f7,h5.row,h5.col,f6,h5.colCount,false);
if (v0<h5.row+h5.rowCount-1){
this.PaintSelection(f7,v0,h5.col,h5.row+h5.rowCount-v0,h5.colCount,false);
}else {
this.PaintSelection(f7,h5.row+h5.rowCount,h5.col,v0-h5.row-h5.rowCount+1,h5.colCount,true);
}
}else {
this.PaintSelection(f7,h3,h5.col,h5.row-h3,h5.colCount,true);
if (v0<h5.row+h5.rowCount-1){
this.PaintSelection(f7,v0+1,h5.col,h5.row+h5.rowCount-v0-1,h5.colCount,false);
}else {
this.PaintSelection(f7,h5.row+h5.rowCount,h5.col,v0-h5.row-h5.rowCount+1,h5.colCount,true);
}
}
}
}else if (h5!=null&&h5.row==-1&&h5.col>=0){
if (h5.col>k5||h5.col+h5.colCount-1<h4){
this.PaintSelection(f7,h5.row,h5.col,h5.rowCount,h5.colCount,false);
this.PaintSelection(f7,h3,h4,v0-h3+1,k5-h4+1,true);
}else {
if (h4>h5.col){
this.PaintSelection(f7,h5.row,h5.col,h5.rowCount,h4-h5.col,false);
if (k5<h5.col+h5.colCount-1){
this.PaintSelection(f7,h5.row,k5,h5.rowCount,h5.col+h5.colCount-k5,false);
}else {
this.PaintSelection(f7,h5.row,h5.col+h5.colCount,h5.rowCount,k5-h5.col-h5.colCount,true);
}
}else {
this.PaintSelection(f7,h5.row,h4,h5.rowCount,h5.col-h4,true);
if (k5<h5.col+h5.colCount-1){
this.PaintSelection(f7,h5.row,k5+1,h5.rowCount,h5.col+h5.colCount-k5-1,false);
}else {
this.PaintSelection(f7,h5.row,h5.col+h5.colCount,h5.rowCount,k5-h5.col-h5.colCount+1,true);
}
}
}
}else if (h5!=null&&h5.row>=0&&h5.col>=0){
this.ExtendSelection(f7,h5,h3,h4,v0-h3+1,k5-h4+1);
}else {
this.PaintSelection(f7,h3,h4,v0-h3+1,k5-h4+1,true);
}
this.SetSelection(f7,h3,h4,v0-h3+1,k5-h4+1,h5==null);
}
this.ExtendSelection=function (f7,h5,newRow,newCol,newRowCount,newColCount)
{
var o2=Math.max(h5.col,newCol);
var o3=Math.min(h5.col+h5.colCount-1,newCol+newColCount-1);
var s4=Math.max(h5.row,newRow);
var v1=Math.min(h5.row+h5.rowCount-1,newRow+newRowCount-1);
if (h5.row<s4){
this.PaintSelection(f7,h5.row,h5.col,s4-h5.row,h5.colCount,false);
}
if (h5.col<o2){
this.PaintSelection(f7,h5.row,h5.col,h5.rowCount,o2-h5.col,false);
}
if (h5.row+h5.rowCount-1>v1){
this.PaintSelection(f7,v1+1,h5.col,h5.row+h5.rowCount-v1-1,h5.colCount,false);
}
if (h5.col+h5.colCount-1>o3){
this.PaintSelection(f7,h5.row,o3+1,h5.rowCount,h5.col+h5.colCount-o3-1,false);
}
if (newRow<s4){
this.PaintSelection(f7,newRow,newCol,s4-newRow,newColCount,true);
}
if (newCol<o2){
this.PaintSelection(f7,newRow,newCol,newRowCount,o2-newCol,true);
}
if (newRow+newRowCount-1>v1){
this.PaintSelection(f7,v1+1,newCol,newRow+newRowCount-v1-1,newColCount,true);
}
if (newCol+newColCount-1>o3){
this.PaintSelection(f7,newRow,o3+1,newRowCount,newCol+newColCount-o3-1,true);
}
}
this.PaintSelection=function (f7,g9,h1,l4,g8,select){
if (g9<0&&h1<0){
this.PaintCornerSelection(f7,select);
}
var v2=false;
if (g9<0){
g9=0;
l4=this.GetRowCountInternal(f7);
v2=true;
}
if (h1<0){
h1=0;
g8=this.GetColCount(f7);
if (this.GetRowHeader(f7)!=null)this.PaintHeaderSelection(f7,g9,h1,l4,g8,select,false);
}
if (v2)
this.PaintHeaderSelection(f7,g9,h1,l4,g8,select,true);
this.PaintViewportSelection(f7,g9,h1,l4,g8,select);
this.PaintAnchorCell(f7);
}
this.PaintFocusRect=function (f7){
var g3=document.getElementById(f7.id+"_focusRectT");
if (g3==null)return ;
var v3=this.GetSelectedRange(f7);
if (f7.d2==null&&(v3==null||(v3.rowCount==0&&v3.colCount==0))){
g3.style.left="-1000px";
var q8=f7.id;
g3=document.getElementById(q8+"_focusRectB");
g3.style.left="-1000px";
g3=document.getElementById(q8+"_focusRectL");
g3.style.left="-1000px";
g3=document.getElementById(q8+"_focusRectR");
g3.style.left="-1000px";
return ;
}
var v4=this.GetOperationMode(f7);
if (v4=="RowMode"||v4=="SingleSelect"||v4=="MultiSelect"||v4=="ExtendedSelect"){
var g9=f7.GetActiveRow();
v3=new this.Range();
this.SetRange(v3,"Row",g9,-1,1,-1);
}else if (v3==null||(v3.rowCount==0&&v3.colCount==0)){
var g9=f7.GetActiveRow();
var h1=f7.GetActiveCol();
v3=new this.Range();
this.SetRange(v3,"Cell",g9,h1,f7.d2.rowSpan,f7.d2.colSpan);
}
if (v3.row<0){
v3.row=0;
v3.rowCount=this.GetRowCountInternal(f7);
}
if (v3.col<0){
v3.col=0;
v3.colCount=this.GetColCount(f7);
}
var h2=this.GetCellFromRowCol(f7,v3.row,v3.col);
if (h2==null)return ;
if (v3.rowCount==1&&v3.colCount==1){
v3.rowCount=h2.rowSpan;
v3.colCount=h2.colSpan;
if (h2.colSpan>1){
var v5=parseInt(h2.getAttribute("col"));
if (v5!=v3.col&&!isNaN(v5))v3.col=v5;
}
}
var f6=this.GetOffsetTop(f7,h2);
var v6=this.GetOffsetLeft(f7,h2);
if (h2.rowSpan>1){
v3.row=h2.parentNode.rowIndex;
var h4=this.GetCellFromRowCol(f7,v3.row,v3.col+v3.colCount-1);
if (h4!=null&&h4.parentNode.rowIndex>h2.parentNode.rowIndex){
f6=this.GetOffsetTop(f7,h4);
}
}
if (h2.colSpan>1){
var h4=this.GetCellFromRowCol(f7,v3.row+v3.rowCount-1,v3.col);
var n1=this.GetOffsetLeft(f7,h4);
if (n1>v6){
v6=n1;
h2=h4;
}
}
var j0=0;
var g7=this.GetViewport(f7).rows;
for (var g9=v3.row;g9<v3.row+v3.rowCount&&g9<g7.length;g9++){
j0+=g7[g9].offsetHeight;
if (g9>v3.row)j0+=parseInt(this.GetViewport(f7).cellSpacing);
}
var i6=0;
var l0=this.GetColGroup(this.GetViewport(f7));
if (l0.childNodes==null||l0.childNodes.length==0)return ;
for (var h1=v3.col;h1<v3.col+v3.colCount&&h1<l0.childNodes.length;h1++){
i6+=l0.childNodes[h1].offsetWidth;
if (h1>v3.col)i6+=parseInt(this.GetViewport(f7).cellSpacing);
}
if (v3.col>h2.cellIndex&&v3.type=="Column"){
var k5=parseInt(h2.getAttribute("col"));
for (var h1=k5;h1<v3.col;h1++){
v6+=l0.childNodes[h1].offsetWidth;
if (h1>k5)v6+=parseInt(this.GetViewport(f7).cellSpacing);
}
}
if (v3.row>0)f6-=2;
else j0-=2;
if (v3.col>0)v6-=2;
else i6-=2;
if (parseInt(this.GetViewport(f7).cellSpacing)>0){
f6+=1;v6+=1;
}else {
i6+=1;
j0+=1;
}
g3.style.left=""+v6+"px";
g3.style.top=""+f6+"px";
g3.style.width=""+i6+"px";
g3=document.getElementById(f7.id+"_focusRectB");
g3.style.left=""+v6+"px";
g3.style.top=""+(f6+j0)+"px";
g3.style.width=""+i6+"px";
g3=document.getElementById(f7.id+"_focusRectL");
g3.style.left=""+v6+"px";
g3.style.top=""+f6+"px";
g3.style.height=""+j0+"px";
g3=document.getElementById(f7.id+"_focusRectR");
g3.style.left=""+(v6+i6)+"px";
g3.style.top=""+f6+"px";
g3.style.height=""+j0+"px";
}
this.PaintCornerSelection=function (f7,select){
var k9=this.GetCorner(f7);
if (k9!=null&&k9.rows.length>0){
for (var e9=0;e9<k9.rows.length;e9++){
for (var h7=0;h7<k9.rows[0].cells.length;h7++){
this.PaintSelectedCell(f7,k9.rows[e9].cells[h7],select);
}
}
}
}
this.PaintHeaderSelection=function (f7,g9,h1,l4,g8,select,c7){
var v7=this.GetRowCountInternal(f7);
var v8=this.GetColCount(f7);
if (c7){
if (this.GetColHeader(f7)==null)return ;
g9=0;
l4=v7=this.GetColHeader(f7).rows.length;
}else {
if (this.GetRowHeader(f7)==null)return ;
h1=0;
g8=v8=this.GetColGroup(this.GetRowHeader(f7)).childNodes.length;
}
var v9=c7?f7.e2:f7.e1;
for (var e9=g9;e9<g9+l4&&e9<v7;e9++){
if (!c7&&this.IsChildSpreadRow(f7,this.GetViewport(f7),e9))continue ;
for (var h7=h1;h7<h1+g8&&h7<v8;h7++){
if (this.IsCovered(f7,e9,h7,v9))continue ;
var h2=this.GetHeaderCellFromRowCol(f7,e9,h7,c7);
if (h2!=null)this.PaintSelectedCell(f7,h2,select);
}
}
}
this.PaintViewportSelection=function (f7,g9,h1,l4,g8,select){
var v7=this.GetRowCountInternal(f7);
var v8=this.GetColCount(f7);
for (var e9=g9;e9<g9+l4&&e9<v7;e9++){
if (this.IsChildSpreadRow(f7,this.GetViewport(f7),e9))continue ;
var h2=null;
for (var h7=h1;h7<h1+g8&&h7<v8;h7++){
if (this.IsCovered(f7,e9,h7,f7.e0))continue ;
h2=this.GetCellFromRowCol(f7,e9,h7,h2);
this.PaintSelectedCell(f7,h2,select);
}
}
}
this.Copy=function (f7){
var m9=this.GetPageActiveSpread();
if (m9!=null&&m9!=f7&&this.GetTopSpread(m9)==f7){
this.Copy(m9);
return ;
}
var l2=this.GetSelection(f7);
var l3=l2.lastChild;
if (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
if (g9<0){
g9=0;
l4=this.GetRowCountInternal(f7);
}
if (h1<0){
h1=0;
g8=this.GetColCount(f7);
}
var f2="";
for (var e9=g9;e9<g9+l4;e9++){
if (this.IsChildSpreadRow(f7,this.GetViewport(f7),e9))continue ;
var h2=null;
for (var h7=h1;h7<h1+g8;h7++){
if (this.IsCovered(f7,e9,h7,f7.e0))
f2+="";
else 
{
h2=this.GetCellFromRowCol(f7,e9,h7,h2);
var p2=this.GetCellType(h2);
if (p2=="TextCellType"&&h2.getAttribute("password")!=null)
f2+="";
else 
f2+=this.GetCellValueFromView(f7,h2);
}
if (h7+1<h1+g8)f2+="\t";
}
f2+="\r\n";
}
this.c0=f2;
}else {
if (f7.d2!=null){
var f2=this.GetCellValueFromView(f7,f7.d2);
this.c0=f2;
}
}
}
this.GetCellValueFromView=function (f7,h2){
var s6=null;
if (h2!=null){
var w0=this.GetRender(h2);
s6=this.GetValueFromRender(f7,w0);
if (s6==null||s6==" ")s6="";
}
return s6;
}
this.SetCellValueFromView=function (h2,s6,ignoreLock){
if (h2!=null){
var w0=this.GetRender(h2);
var p7=this.GetCellType(h2);
if ((p7!="readonly"||ignoreLock)&&w0!=null&&w0.getAttribute("FpEditor")!="Button")
this.SetValueToRender(w0,s6);
}
}
this.Paste=function (f7){
var m9=this.GetPageActiveSpread();
if (m9!=null&&m9!=f7&&this.GetTopSpread(m9)==f7){
this.Paste(m9);
return ;
}
if (f7.d2==null)return ;
var f2=this.c0;
if (f2==null)return ;
var e5=this.GetViewportFromCell(f7,f7.d2);
var g9=this.GetRowFromCell(f7,f7.d2);
var h1=this.GetColFromCell(f7,f7.d2);
var g8=this.GetColCount(f7);
var l4=this.GetRowCountInternal(f7);
var w1=g9;
var t8=h1;
var w2=new String(f2);
if (w2.length==0)return ;
var e8=w2.lastIndexOf("\r\n");
if (e8>=0&&e8==w2.length-2)w2=w2.substring(0,e8);
var w3=0;
var w4=w2.split("\r\n");
for (var e9=0;e9<w4.length&&w1<l4;e9++){
if (typeof(w4[e9])=="string"){
w4[e9]=w4[e9].split("\t");
if (w4[e9].length>w3)w3=w4[e9].length;
}
w1++;
}
w1=this.GetSheetIndex(f7,g9);
for (var e9=0;e9<w4.length&&w1<l4;e9++){
var w5=w4[e9];
if (w5!=null){
t8=h1;
var h2=null;
var v0=this.GetDisplayIndex(f7,w1);
for (var h7=0;h7<w5.length&&t8<g8;h7++){
if (!this.IsCovered(f7,v0,t8,f7.e0)){
h2=this.GetCellFromRowCol(f7,v0,t8,h2);
if (h2==null)return ;
var w6=w5[h7];
if (!this.ValidateCell(f7,h2,w6)){
if (f7.getAttribute("lcidMsg")!=null)
alert(f7.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
return ;
}
}
t8++;
}
}
w1++;
}
if (w4.length==0)return ;
w1=this.GetSheetIndex(f7,g9);
for (var e9=0;e9<w4.length&&w1<l4;e9++){
t8=h1;
var w5=w4[e9];
var h2=null;
var v0=this.GetDisplayIndex(f7,w1);
for (var h7=0;h7<w3&&t8<g8;h7++){
if (!this.IsCovered(f7,v0,t8,f7.e0)){
h2=this.GetCellFromRowCol(f7,v0,t8,h2);
var p7=this.GetCellType(h2);
var w0=this.GetRender(h2);
if (p7!="readonly"&&w0.getAttribute("FpEditor")!="Button"){
var w6=null;
if (w5!=null&&h7<w5.length)w6=w5[h7];
this.SetCellValueFromView(h2,w6);
if (w6!=null){
this.SetCellValue(f7,h2,""+w6);
}else {
this.SetCellValue(f7,h2,"");
}
}
}
t8++;
}
w1++;
}
var t1=f7.getAttribute("autoCalc");
if (t1!="false"){
this.UpdateValues(f7);
}
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null){
f8.blur();
}
this.Focus(f7);
}
this.UpdateValues=function (f7){
if (f7.d9==null&&this.GetParentSpread(f7)==null&&f7.getAttribute("rowFilter")!="true"&&f7.getAttribute("hierView")!="true"&&f7.getAttribute("IsNewRow")!="true"){
this.SaveData(f7);
this.StorePostData(f7);
this.SyncData(f7.getAttribute("name"),"UpdateValues",f7);
}
}
this.ValidateCell=function (f7,h2,s6){
if (h2==null||s6==null||s6=="")return true;
var s9=null;
var p2=this.GetCellType(h2);
if (p2!=null){
var h8=this.GetFunction(p2+"_isValid");
if (h8!=null){
s9=h8(h2,s6);
}
}
return (s9==null||s9=="");
}
this.DoclearSelection=function (f7){
var l2=this.GetSelection(f7);
var l3=l2.firstChild;
while (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
this.PaintSelection(f7,g9,h1,l4,g8,false);
l2.removeChild(l3);
l3=l2.firstChild;
}
}
this.Clear=function (f7){
var m9=this.GetPageActiveSpread();
if (m9!=null&&m9!=f7&&this.GetTopSpread(m9)==f7){
this.Clear(m9);
return ;
}
var p7=this.GetCellType(f7.d2);
if (p7=="readonly")return ;
var l2=this.GetSelection(f7);
var l3=l2.lastChild;
if (this.AnyReadOnlyCell(f7,l3)){
return ;
}
this.Copy(f7);
if (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
if (g9<0){
g9=0;
l4=this.GetRowCountInternal(f7);
}
if (h1<0){
h1=0;
g8=this.GetColCount(f7);
}
for (var e9=g9;e9<g9+l4;e9++){
if (this.IsChildSpreadRow(f7,this.GetViewport(f7),e9))continue ;
var h2=null;
for (var h7=h1;h7<h1+g8;h7++){
if (!this.IsCovered(f7,e9,h7,f7.e0)){
h2=this.GetCellFromRowCol(f7,e9,h7,h2);
var p7=this.GetCellType(h2);
if (p7!="readonly"){
var w7=this.GetEditor(h2);
if (w7!=null&&w7.getAttribute("FpEditor")=="Button")continue ;
this.SetCellValueFromView(h2,null);
this.SetCellValue(f7,h2,"");
}
}
}
}
var t1=f7.getAttribute("autoCalc");
if (t1!="false"){
this.UpdateValues(f7);
}
}
}
this.AnyReadOnlyCell=function (f7,l3){
if (l3!=null){
var g9=this.GetRowByKey(f7,l3.getAttribute("row"));
var h1=this.GetColByKey(f7,l3.getAttribute("col"));
var l4=parseInt(l3.getAttribute("rowcount"));
var g8=parseInt(l3.getAttribute("colcount"));
if (g9<0){
g9=0;
l4=this.GetRowCountInternal(f7);
}
if (h1<0){
h1=0;
g8=this.GetColCount(f7);
}
for (var e9=g9;e9<g9+l4;e9++){
if (this.IsChildSpreadRow(f7,this.GetViewport(f7),e9))continue ;
var h2=null;
for (var h7=h1;h7<h1+g8;h7++){
if (!this.IsCovered(f7,e9,h7,f7.e0)){
h2=this.GetCellFromRowCol(f7,e9,h7,h2);
var p7=this.GetCellType(h2);
if (p7=="readonly"){
return true;
}
}
}
}
}
return false;
}
this.MouseMove=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var m1=this.GetTarget(event);
if (m1!=null&&m1.tagName=="scrollbar")
return ;
var f7=this.GetSpread(m1,true);
if (f7!=null&&this.dragSlideBar)
{
if (this.activePager!=null){
var k3=this.GetElementById(this.activePager,f7.id+"_slideBar");
var f6=0;
if (f7.style.position!="absolute"&&f7.style.position!="relative"){
f6=(event.clientX+window.scrollX-8);
}else {
f6=(event.clientX-this.GetOffsetLeft(f7,f7,document.body)+window.scrollX-8);
}
if (f6<f7.slideLeft)f6=f7.slideLeft;
if (f6>f7.slideRight)f6=f7.slideRight;
k3.style.left=f6+"px";
var k7=parseInt(this.activePager.getAttribute("totalPage"))-1;
var w8=parseInt(((f6-f7.slideLeft)/(f7.slideRight-f7.slideLeft))*k7)+1;
var w9=this.GetElementById(this.activePager,f7.id+"_posIndicator");
w9.innerHTML=this.activePager.getAttribute("pageText")+w8;
}
return ;
}
if (this.a7)f7=this.GetSpread(this.b9);
if (f7==null||(!this.a7&&this.HitCommandBar(m1)))return ;
if (f7.getAttribute("OperationMode")=="ReadOnly")return ;
var i9=this.IsXHTML(f7);
if (this.a7){
if (this.dragCol!=null&&this.dragCol>=0){
var x0=this.GetMovingCol(f7);
if (x0!=null){
if (x0.style.display=="none")x0.style.display="";
if (f7.style.position!="absolute"&&f7.style.position!="relative"){
x0.style.top=""+(event.clientY+window.scrollY)+"px";
x0.style.left=""+(event.clientX+window.scrollX+5)+"px";
}else {
x0.style.top=""+(event.clientY-this.GetOffsetTop(f7,f7,document.body)+window.scrollY)+"px";
x0.style.left=""+(event.clientX-this.GetOffsetLeft(f7,f7,document.body)+window.scrollX+5)+"px";
}
}
var e5=this.GetViewport(f7);
var x1=document.body;
var x2=this.GetGroupBar(f7);
var f6=-1;
var k2=event.clientX;
var s4=0;
var o2=0;
if (f7.style.position!="absolute"&&f7.style.position!="relative"){
s4=this.GetOffsetTop(f7,f7,document.body)-e5.parentNode.scrollTop;
o2=this.GetOffsetLeft(f7,f7,document.body)-e5.parentNode.scrollLeft;
k2+=Math.max(document.body.scrollLeft,document.documentElement.scrollLeft);
}else {
k2-=(this.GetOffsetLeft(f7,f7,document.body)-Math.max(document.body.scrollLeft,document.documentElement.scrollLeft));
}
var x3=false;
var i9=this.IsXHTML(f7);
var x4=i9?document.body.parentNode.scrollTop:document.body.scrollTop;
if (this.GetPager1(f7)!=null)x4-=this.GetPager1(f7).offsetHeight;
if (x2!=null&&event.clientY<this.GetOffsetTop(f7,f7,document.body)-e5.parentNode.scrollTop+x2.offsetHeight-x4){
if (f7.style.position!="absolute"&&f7.style.position!="relative")
o2=this.GetOffsetLeft(f7,f7,document.body);
s4+=10;
x3=true;
var x5=x2.getElementsByTagName("TABLE")[0];
if (x5!=null){
for (var e9=0;e9<x5.rows[0].cells[0].childNodes.length;e9++){
var i6=x5.rows[0].cells[0].childNodes[e9].offsetWidth;
if (i6==null)continue ;
if (o2<=k2&&k2<o2+i6){
f6=e9;
break ;
}
o2+=i6;
}
}
if (f6==-1&&k2>=o2)f6=-2;
f7.targetCol=f6;
}else {
if (f7.style.position=="absolute"||f7.style.position=="relative")
o2=-e5.parentNode.scrollLeft;
if (this.GetRowHeader(f7)!=null)o2+=this.GetRowHeader(f7).offsetWidth;
if (x2!=null)s4+=x2.offsetHeight;
if (k2<o2){
f6=0;
}else {
var l0=this.GetColGroup(this.GetColHeader(f7));
if (l0!=null){
for (var e9=0;e9<l0.childNodes.length;e9++){
var i6=l0.childNodes[e9].offsetWidth;
if (i6==null)continue ;
if (o2<=k2&&k2<o2+i6){
f6=e9;
break ;
}
o2+=i6;
}
}
}
if (f6>=0&&f6!=this.dragViewCol){
if (this.dragViewCol<f6){
f6++;
if (f6<l0.childNodes.length)
o2+=i6;
}
}
o2-=5;
var x6=parseInt(this.GetSheetColIndex(f7,f6));
if (x6<0)x6=f6;
f7.targetCol=x6;
}
if (this.GetPager1(f7)!=null)s4+=this.GetPager1(f7).offsetHeight;
var w9=this.GetPosIndicator(f7);
w9.style.left=""+o2+"px";
w9.style.top=""+s4+"px";
if (x2!=null&&x3&&x2.getElementsByTagName("TABLE").length==0){
w9.style.display="none";
}else {
if (x3||f7.allowColMove)
w9.style.display="";
else 
w9.style.display="none";
}
return ;
}
if (this.b5==null&&this.b6==null){
if (f7.d2!=null){
var i0=this.GetParent(this.GetViewport(f7));
if (i0!=null){
var p9=f7.offsetTop+i0.offsetTop+i0.offsetHeight-10;
if (event.clientY>p9){
i0.scrollTop=i0.scrollTop+10;
this.ScrollView(f7);
}else if (event.clientY<f7.offsetTop+i0.offsetTop+5){
i0.scrollTop=i0.scrollTop-10;
this.ScrollView(f7);
}
var x7=f7.offsetLeft+i0.offsetLeft+i0.offsetWidth-20;
if (event.clientX>x7){
i0.scrollLeft=i0.scrollLeft+10;
this.ScrollView(f7);
}else if (event.clientX<f7.offsetLeft+i0.offsetLeft+5){
i0.scrollLeft=i0.scrollLeft-10;
this.ScrollView(f7);
}
}
var h2=this.GetCell(m1,null,event);
if (h2!=null&&h2!=f7.d3){
var v4=this.GetOperationMode(f7);
if (v4!="MultiSelect"){
if (v4=="SingleSelect"||v4=="RowMode"){
this.ClearSelection(f7);
var h3=this.GetRowFromCell(f7,h2);
this.UpdateAnchorCell(f7,h3,0);
this.SelectRow(f7,h3,1,true,true);
}else {
this.Select(f7,f7.d2,h2);
}
f7.d3=h2;
}
}
}
}else if (this.b5!=null){
var x8=event.clientX-this.b7;
var x9=parseInt(this.b5.width)+x8;
var s3=0;
var y0=(x9>s3);
if (y0){
this.b5.width=x9;
var j4=parseInt(this.b5.getAttribute("index"));
this.SetWidthFix(this.GetColHeader(f7),j4,x9);
this.b7=event.clientX;
}
}else if (this.b6!=null){
var x8=event.clientY-this.b8;
var y1=parseInt(this.b6.style.height)+x8;
var s3=0;
var y0=(s3<y1);
if (y0){
this.b6.style.height=""+(parseInt(this.b6.style.height)+x8)+"px";
this.b8=event.clientY;
}
}
}else {
this.b9=m1;
if (this.b9==null||this.GetSpread(this.b9)!=f7)return ;
var m1=this.GetSizeColumn(f7,this.b9,event);
if (m1!=null){
this.b5=m1;
this.b9.style.cursor=this.GetResizeCursor(false);
}else {
var m1=this.GetSizeRow(f7,this.b9,event);
if (m1!=null){
this.b6=m1;
if (this.b9!=null&&this.b9.style!=null)this.b9.style.cursor=this.GetResizeCursor(true);
}else {
if (this.b9!=null&&this.b9.style!=null){
var h2=this.GetCell(this.b9);
if (h2!=null&&this.IsHeaderCell(f7,h2))this.b9.style.cursor="default";
}
}
}
}
}
this.GetResizeCursor=function (i2){
if (i2){
return "n-resize";
}else {
return "w-resize";
}
}
this.HitCommandBar=function (m1){
var f6=m1;
var f7=this.GetTopSpread(this.GetSpread(f6,true));
if (f7==null)return false;
var n8=this.GetCommandBar(f7);
while (f6!=null&&f6!=f7){
if (f6==n8)return true;
f6=f6.parentNode;
}
return false;
}
this.OpenWaitMsg=function (f7){
var h8=document.getElementById(f7.id+"_waitmsg");
if (h8==null)return ;
var i6=f7.offsetWidth;
var j0=f7.offsetHeight;
var i3=this.CreateTestBox(f7);
i3.style.fontFamily=h8.style.fontFamily;
i3.style.fontSize=h8.style.fontSize;
i3.style.fontWeight=h8.style.fontWeight;
i3.style.fontStyle=h8.style.fontStyle;
i3.innerHTML=h8.innerHTML;
h8.style.width=""+(i3.offsetWidth+2)+"px";
var v6=Math.max(10,(i6-parseInt(h8.style.width))/2);
var f6=Math.max(10,(j0-parseInt(h8.style.height))/2);
if (f7.style.position!="absolute"&&f7.style.position!="relative"){
v6+=f7.offsetLeft;
f6+=f7.offsetTop;
}
h8.style.top=""+f6+"px";
h8.style.left=""+v6+"px";
h8.style.display="block";
}
this.CloseWaitMsg=function (f7){
var h8=document.getElementById(f7.id+"_waitmsg");
if (h8==null)return ;
h8.style.display="none";
this.Focus(f7);
}
this.MouseDown=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var m1=this.GetTarget(event);
var f7=this.GetSpread(m1,true);
var y2=this.GetPageActiveSpread();
if (y2!=null&&y2!=f7&&!this.IsChild(y2,f7)&&!this.IsChild(f7,y2))this.ClearSelection(y2);
if (this.GetViewport(f7)==null)return ;
if (f7!=null&&m1.parentNode!=null&&m1.parentNode.getAttribute("name")==f7.id+"_slideBar"){
if (this.IsChild(m1,this.GetPager1(f7)))
this.activePager=this.GetPager1(f7);
else if (this.IsChild(m1,this.GetPager2(f7)))
this.activePager=this.GetPager2(f7);
if (this.activePager!=null){
this.dragSlideBar=true;
}
return this.CancelDefault(event);
}
if (this.GetOperationMode(f7)=="ReadOnly")return ;
var i9=false;
if (f7!=null)i9=this.IsXHTML(f7);
if (this.a8){
var f6=this.GetCell(m1);
if (f6!=f7.d2){
var m4=this.EndEdit();
if (!m4)return ;
}else 
return ;
}
if (m1==this.GetParent(this.GetViewport(f7))){
if (this.GetTopSpread(y2)!=f7){
if (f7.d2!=null)this.SelectRange(this.GetRowFromCell(f7,f7.d2),this.GetColFromCell(f7,f7.d2),1,1,true);
if (y2!=null&&y2!=f7)this.ClearSelection(y2);
this.SetActiveSpread(event);
}
return ;
}
var y3=(y2==f7);
this.SetActiveSpread(event);
y2=this.GetPageActiveSpread();
if (this.HitCommandBar(m1))return ;
if (event.button==2)return ;
if (this.IsChild(m1,this.GetGroupBar(f7))){
var h4=parseInt(m1.id.replace(f7.id+"_group",""));
if (!isNaN(h4)){
this.dragCol=h4;
this.dragViewCol=this.GetColByKey(f7,h4);
var x0=this.GetMovingCol(f7);
x0.innerHTML=m1.innerHTML;
x0.style.width=""+Math.max(this.GetPreferredCellWidth(f7,m1),80)+"px";
if (f7.getAttribute("DragColumnCssClass")==null)
x0.style.backgroundColor=m1.style.backgroundColor;
x0.style.top="-50px";
x0.style.left="-100px";
this.a7=true;
f7.dragFromGroupbar=true;
this.CancelDefault(event);
return ;
}
}
this.b5=this.GetSizeColumn(f7,m1,event);
if (this.b5!=null){
this.a7=true;
this.b7=this.b8=event.clientX;
if (this.b5.style!=null)this.b5.style.cursor=this.GetResizeCursor(false);
this.b9=m1;
}else {
this.b6=this.GetSizeRow(f7,m1,event);
if (this.b6!=null){
this.a7=true;
this.b7=this.b8=event.clientY;
this.b6.style.cursor=this.GetResizeCursor(true);
this.b9=m1;
}else {
var y4=this.GetCell(m1,null,event);
if (y4==null){
var c5=this.GetCorner(f7);
if (c5!=null&&this.IsChild(m1,c5)){
if (this.GetOperationMode(f7)=="Normal")
this.SelectTable(f7,true);
}
return ;
}
if (y4.parentNode.getAttribute("FpSpread")=="ch"&&this.GetColFromCell(f7,y4)>=this.GetColCount(f7))return ;
if (y4.parentNode.getAttribute("FpSpread")=="rh"&&this.IsChildSpreadRow(f7,this.GetViewport(f7),y4.parentNode.rowIndex))return ;
if (y4.parentNode.getAttribute("FpSpread")=="ch"&&(this.GetOperationMode(f7)=="RowMode"||this.GetOperationMode(f7)=="SingleSelect"||this.GetOperationMode(f7)=="ExtendedSelect")){
if (!f7.allowColMove&&!f7.allowGroup)
return ;
}else {
var m2=this.FireActiveCellChangingEvent(f7,this.GetRowFromCell(f7,y4),this.GetColFromCell(f7,y4));
if (m2)return ;
var t2=this.GetOperationMode(f7);
var e7=this.GetTopSpread(f7);
if (!event.ctrlKey||f7.getAttribute("multiRange")!="true"||e7.getAttribute("hierView")=="true"){
if (t2!="MultiSelect"){
this.ClearSelection(f7);
}
}else {
if (t2!="ExtendedSelect"&&t2!="MultiSelect"){
if (f7.d2!=null)this.PaintSelectedCell(f7,f7.d2,true);
}
}
}
f7.d2=y4;
var h2=f7.d2;
var u2=this.GetParent(this.GetViewport(f7));
if (u2!=null&&!this.IsControl(m1)&&(m1!=null&&m1.tagName!="scrollbar")){
if (this.IsChild(h2,u2)&&h2.offsetLeft+h2.offsetWidth>u2.scrollLeft+u2.clientWidth){
u2.scrollLeft=h2.offsetLeft+h2.offsetWidth-u2.clientWidth;
}
if (this.IsChild(h2,u2)&&h2.offsetTop+h2.offsetHeight>u2.scrollTop+u2.clientHeight&&h2.offsetHeight<u2.clientHeight){
u2.scrollTop=h2.offsetTop+h2.offsetHeight-u2.clientHeight;
}
if (h2.offsetTop<u2.scrollTop){
u2.scrollTop=h2.offsetTop;
}
if (h2.offsetLeft<u2.scrollLeft){
u2.scrollLeft=h2.offsetLeft;
}
this.ScrollView(f7);
}
if (y4.parentNode.getAttribute("FpSpread")!="ch")this.SetActiveRow(f7,this.GetRowKeyFromCell(f7,f7.d2));
if (y4.parentNode.getAttribute("FpSpread")=="rh")
this.SetActiveCol(f7,0);
else {
this.SetActiveCol(f7,this.GetColKeyFromCell(f7,f7.d2));
}
var t2=this.GetOperationMode(f7);
if (f7.d2.parentNode.getAttribute("FpSpread")=="r"){
if (t2=="ExtendedSelect"||t2=="MultiSelect"){
var y5=this.IsRowSelected(f7,this.GetRowFromCell(f7,f7.d2));
if (y5)
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,false,true);
else 
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,true,true);
}
else if (t2=="RowMode"||t2=="SingleSelect")
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,true,true);
else {
this.SelectRange(f7,this.GetRowFromCell(f7,f7.d2),this.GetColFromCell(f7,f7.d2),1,1,true);
}
f7.d6=this.GetRowFromCell(f7,f7.d2);
f7.d7=this.GetColFromCell(f7,f7.d2);
}else if (f7.d2.parentNode.getAttribute("FpSpread")=="ch"){
if (m1.tagName=="INPUT"||m1.tagName=="TEXTAREA"||m1.tagName=="SELECT")
return ;
var q2=this.GetColFromCell(f7,f7.d2);
if (f7.allowColMove||f7.allowGroup)
{
if (t2=="Normal"||t2=="ReadOnly")
this.SelectColumn(f7,q2,1,true);
this.dragCol=parseInt(this.GetSheetColIndex(f7,q2));
this.dragViewCol=q2;
var x0=this.GetMovingCol(f7);
if (f7.getAttribute("DragColumnCssClass")==null)
x0.style.backgroundColor=y4.style.backgroundColor;
x0.style.top="0px";
x0.style.left="-1000px";
x0.style.display="";
x0.innerHTML=y4.innerHTML;
x0.style.width=""+Math.max(this.GetPreferredCellWidth(f7,y4),80)+"px";
x0.style.height=""+y4.offsetHeight+"px";
}else {
if (t2=="Normal"||t2=="ReadOnly"){
this.SelectColumn(f7,q2,1,true);
}
else 
return ;
}
}else if (f7.d2.parentNode.getAttribute("FpSpread")=="rh"){
if (m1.tagName=="INPUT"||m1.tagName=="TEXTAREA"||m1.tagName=="SELECT")
return ;
if (t2=="ExtendedSelect"||t2=="MultiSelect"){
if (this.IsRowSelected(f7,this.GetRowFromCell(f7,f7.d2)))
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,false,true);
else 
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,true,true);
}else {
this.SelectRow(f7,this.GetRowFromCell(f7,f7.d2),1,true);
}
}
if (f7.d2!=null){
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=f7.id;
g0.Row=g0.row=this.GetSheetIndex(f7,this.GetRowFromCell(f7,f7.d2));
g0.Col=g0.col=this.GetColFromCell(f7,f7.d2);
this.FireEvent(f7,g0);
}
f7.d3=f7.d2;
if (f7.d2!=null){
f7.d4=this.GetRowFromCell(f7,f7.d2);
f7.d5=this.GetColFromCell(f7,f7.d2);
}
this.b9=m1;
this.a7=true;
}
}
this.EnableButtons(f7);
if (!this.a8&&this.b6==null&&this.b5==null){
if (f7.d2!=null&&this.IsChild(f7.d2,f7)&&!this.IsHeaderCell(this.GetCell(m1))){
var h9=this.GetEditor(f7.d2);
if (h9!=null){
if (h9.type=="submit")this.SaveData(f7);
this.a8=(h9.type!="button"&&h9.type!="submit");
this.a9=h9;
this.b0=this.GetEditorValue(h9);
h9.focus();
}
}
}
if (!this.IsControl(m1)){
if (f7!=null)this.UpdatePostbackData(f7);
return this.CancelDefault(event);
}
}
this.GetMovingCol=function (f7){
var x0=document.getElementById(f7.id+"movingCol");
if (x0==null){
x0=document.createElement("DIV");
x0.style.display="none";
x0.style.position="absolute";
x0.style.top="0px";
x0.style.left="0px";
x0.id=f7.id+"movingCol";
x0.align="center";
f7.insertBefore(x0,null);
if (f7.getAttribute("DragColumnCssClass")!=null)
x0.className=f7.getAttribute("DragColumnCssClass");
else 
x0.style.border="1px solid black";
x0.style.MozOpacity=0.50;
}
return x0;
}
this.IsControl=function (f6){
return (f6!=null&&(f6.tagName=="INPUT"||f6.tagName=="TEXTAREA"||f6.tagName=="SELECT"||f6.tagName=="OPTION"));
}
this.EnableButtons=function (f7){
var p7=this.GetCellType(f7.d2);
var l2=this.GetSelection(f7);
var l3=l2.lastChild;
var q9=f7.getAttribute("OperationMode");
var y6=q9=="ReadOnly"||q9=="SingleSelect"||p7=="readonly";
if (!y6){
y6=this.AnyReadOnlyCell(f7,l3);
}
if (y6){
var f5=this.GetCmdBtn(f7,"Copy");
this.UpdateCmdBtnState(f5,l3==null);
var f2=this.c0;
f5=this.GetCmdBtn(f7,"Paste");
this.UpdateCmdBtnState(f5,(l3==null||f2==null));
f5=this.GetCmdBtn(f7,"Clear");
this.UpdateCmdBtnState(f5,true);
}else {
var f5=this.GetCmdBtn(f7,"Copy");
this.UpdateCmdBtnState(f5,l3==null);
var f2=this.c0;
f5=this.GetCmdBtn(f7,"Paste");
this.UpdateCmdBtnState(f5,(l3==null||f2==null));
f5=this.GetCmdBtn(f7,"Clear");
this.UpdateCmdBtnState(f5,l3==null);
}
}
this.CellClicked=function (h2){
var f7=this.GetSpread(h2);
if (f7!=null){
this.SaveData(f7);
}
}
this.UpdateCmdBtnState=function (f5,disabled){
if (f5==null)return ;
if (f5.tagName=="INPUT"){
var f6=f5.disabled;
if (f6==disabled)return ;
f5.disabled=disabled;
}else {
var f6=f5.getAttribute("disabled");
if (f6==disabled)return ;
f5.setAttribute("disabled",disabled);
}
if (f5.tagName=="IMG"){
var y7=f5.getAttribute("disabledImg");
if (disabled&&y7!=null&&y7!=""){
if (f5.src.indexOf(y7)<0)f5.src=y7;
}else {
var y8=f5.getAttribute("enabledImg");
if (f5.src.indexOf(y8)<0)f5.src=y8;
}
}
}
this.MouseUp=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var m1=this.GetTarget(event);
var f7=this.GetSpread(m1,true);
if (f7==null&&!this.a7){
return ;
}
if (this.dragSlideBar&&f7!=null)
{
this.dragSlideBar=false;
if (this.activePager!=null){
var k3=this.GetElementById(this.activePager,f7.id+"_slideBar");
var f6=parseInt(k3.style.left);
if (f6<f7.slideLeft)f6=f7.slideLeft;
if (f6>f7.slideRight)f6=f7.slideRight;
k3.style.left=f6+"px";
var k7=parseInt(this.activePager.getAttribute("totalPage"))-1;
var w8=parseInt(((f6-f7.slideLeft)/(f7.slideRight-f7.slideLeft))*k7);
this.activePager=null;
this.GotoPage(f7,w8);
}
return ;
}
if (this.a7&&(this.b5!=null||this.b6!=null)){
if (this.b5!=null)
f7=this.GetSpread(this.b5);
else 
f7=this.GetSpread(this.b6);
}
if (f7==null)return ;
if (this.GetViewport(f7)==null)return ;
var q9=this.GetOperationMode(f7);
if (q9=="ReadOnly")return ;
var h8=true;
if (this.a7){
this.a7=false;
if (this.dragCol!=null&&this.dragCol>=0){
var y9=(this.IsChild(m1,this.GetGroupBar(f7))||m1==this.GetGroupBar(f7));
if (!y9&&this.GetGroupBar(f7)!=null){
var z0=event.clientX;
var z1=event.clientY;
var o2=f7.offsetLeft;
var s4=f7.offsetTop;
var z2=this.GetGroupBar(f7).offsetWidth;
var z3=this.GetGroupBar(f7).offsetHeight;
var n7=window.scrollX;
var n6=window.scrollY;
if (this.GetPager1(f7)!=null)n6-=this.GetPager1(f7).offsetHeight;
y9=(o2<=n7+z0&&n7+z0<=o2+z2&&s4<=n6+z1&&n6+z1<=s4+z3);
}
if (f7.dragFromGroupbar){
if (y9){
if (f7.targetCol>0)
this.Regroup(f7,this.dragCol,parseInt((f7.targetCol+1)/2));
else 
this.Regroup(f7,this.dragCol,f7.targetCol);
}else {
this.Ungroup(f7,this.dragCol,f7.targetCol);
}
}else {
if (y9){
if (f7.allowGroup){
if (f7.targetCol>0)
this.Group(f7,this.dragCol,parseInt((f7.targetCol+1)/2));
else 
this.Group(f7,this.dragCol,f7.targetCol);
}
}else if (f7.allowColMove){
if (f7.targetCol!=null){
var g0=this.CreateEvent("ColumnDragMove");
g0.cancel=false;
g0.col=this.dragViewCol;
this.FireEvent(f7,g0);
if (!g0.cancel){
this.MoveCol(f7,this.dragCol,f7.targetCol);
var g0=this.CreateEvent("ColumnDragMoveCompleted");
g0.col=this.dragViewCol;
this.FireEvent(f7,g0);
}
}
}
}
var x0=this.GetMovingCol(f7);
if (x0!=null)
x0.style.display="none";
this.dragCol=-1;
this.dragViewCol=-1;
var w9=this.GetPosIndicator(f7);
if (w9!=null)
w9.style.display="none";
f7.dragFromGroupbar=false;
f7.targetCol=null;
this.b5=this.b6=null;
}
if (this.b5!=null){
h8=false;
var x8=event.clientX-this.b7;
var x9=parseInt(this.b5.width);
var z4=x9;
if (isNaN(x9))x9=0;
x9+=x8;
if (x9<1)x9=1;
var j4=parseInt(this.b5.getAttribute("index"));
var z5=this.GetColGroup(this.GetViewport(f7));
if (z5!=null&&z5.childNodes.length>0){
z4=parseInt(z5.childNodes[j4].width);
}else {
z4=1;
}
if (this.GetViewport(f7).rules!="rows"){
if (j4==0)z4+=1;
if (j4==parseInt(this.colCount)-1)z4-=1;
}
if (x9!=z4&&event.clientX!=this.b8)
{
this.SetColWidth(f7,j4,x9);
var g0=this.CreateEvent("ColWidthChanged");
g0.col=j4;
g0.width=x9;
this.FireEvent(f7,g0);
}
this.ScrollView(f7);
this.PaintFocusRect(f7);
}else if (this.b6!=null){
h8=false;
var x8=event.clientY-this.b8;
var y1=this.b6.offsetHeight+x8;
if (y1<1){
y1=1;
x8=1-this.b6.offsetHeight;
}
this.b6.style.height=""+y1+"px";
this.b6.style.cursor="auto";
var i0=null;
i0=this.GetViewport(f7);
if (i0.rows.length>=2&&i0.cellSpacing=="0"){
if (this.b6.rowIndex==0)
i0.rows[0].style.height=""+(parseInt(this.b6.style.height)-1)+"px";
else if (this.b6.rowIndex==i0.rows.length-1)
i0.rows[this.b6.rowIndex].style.height=""+(parseInt(this.b6.style.height)+1)+"px";
else 
i0.rows[this.b6.rowIndex].style.height=this.b6.style.height;
}else {
i0.rows[this.b6.rowIndex].style.height=this.b6.style.height;
}
var o1=this.AddRowInfo(f7,this.b6.getAttribute("FpKey"));
if (o1!=null){
this.SetRowHeight(f7,o1,parseInt(this.b6.style.height));
}
if (this.b7!=event.clientY){
var g0=this.CreateEvent("RowHeightChanged");
g0.row=this.GetRowFromCell(f7,this.b6.cells[0]);
g0.height=this.b6.offsetHeight;
this.FireEvent(f7,g0);
}
var i1=this.GetParentSpread(f7);
if (i1!=null)this.UpdateRowHeight(i1,f7);
var e7=this.GetTopSpread(f7);
this.SizeAll(e7);
this.Refresh(e7);
this.ScrollView(f7);
this.PaintFocusRect(f7);
}else {
}
if (this.b9!=null){
this.b9=null;
}
}
if (h8)h8=!this.IsControl(m1);
if (h8&&this.HitCommandBar(m1))return ;
var z6=false;
var l2=this.GetSelection(f7);
if (l2!=null){
var l3=l2.firstChild;
var h5=new this.Range();
if (l3!=null){
h5.row=this.GetRowByKey(f7,l3.getAttribute("row"));
h5.col=this.GetColByKey(f7,l3.getAttribute("col"));
h5.rowCount=parseInt(l3.getAttribute("rowcount"));
h5.colCount=parseInt(l3.getAttribute("colcount"));
}
switch (f7.d8){
case "":
var g7=this.GetViewport(f7).rows;
for (var e9=h5.row;e9<h5.row+h5.rowCount&&e9<g7.length;e9++){
if (g7[e9].cells.length>0&&g7[e9].cells[0].firstChild!=null&&g7[e9].cells[0].firstChild.nodeName!="#text"){
if (g7[e9].cells[0].firstChild.getAttribute("FpSpread")=="Spread"){
z6=true;
break ;
}
}
}
break ;
case "c":
var i0=this.GetViewport(f7);
for (var e9=0;e9<i0.rows.length;e9++){
if (this.IsChildSpreadRow(f7,i0,e9)){
z6=true;
break ;
}
}
break ;
case "r":
var i0=this.GetViewport(f7);
var t5=h5.rowCount;
for (var e9=h5.row;e9<h5.row+t5&&e9<i0.rows.length;e9++){
if (this.IsChildSpreadRow(f7,i0,e9)){
z6=true;
break ;
}
}
}
}
if (z6){
var f5=this.GetCmdBtn(f7,"Copy");
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(f7,"Paste");
this.UpdateCmdBtnState(f5,true);
f5=this.GetCmdBtn(f7,"Clear");
this.UpdateCmdBtnState(f5,true);
}
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null){
f8.style.top=event.clientY-f7.offsetTop;
f8.style.left=event.clientX-f7.offsetLeft;
}
if (h8)this.Focus(f7);
}
this.UpdateRowHeight=function (i1,child){
var k4=child.parentNode;
while (k4!=null){
if (k4.tagName=="TR")break ;
k4=k4.parentNode;
}
var i9=this.IsXHTML(i1);
if (k4!=null){
var e8=k4.rowIndex;
if (this.GetRowHeader(i1)!=null){
var n9=0;
if (this.GetColHeader(child)!=null)n9=this.GetColHeader(child).offsetHeight;
if (this.GetRowHeader(child)!=null)n9+=this.GetRowHeader(child).offsetHeight;
if (!i9)n9-=this.GetViewport(i1).cellSpacing;
if (this.GetViewport(i1).cellSpacing==0){
this.GetRowHeader(i1).rows[e8].style.height=""+(n9+1)+"px";
if (this.GetParentSpread(i1)!=null){
this.GetRowHeader(i1).parentNode.style.height=""+this.GetRowHeader(i1).offsetHeight+"px";
}
}
else 
this.GetRowHeader(i1).rows[e8].style.height=""+(n9+2)+"px";
this.GetViewport(i1).rows[e8].style.height=""+n9+"px";
child.style.height=""+n9+"px";
}
}
var z7=this.GetParentSpread(i1);
if (z7!=null)
this.UpdateRowHeight(z7,i1);
}
this.MouseOut=function (){
if (!this.a7&&this.b5!=null&&this.b5.style!=null)this.b5.style.cursor="auto";
}
this.KeyDown=function (f7,event){
if (window.fpPostOn!=null)return ;
if (!f7.ProcessKeyMap(event))return ;
var h9=false;
if (this.a8&&this.a9!=null){
var z8=this.GetEditor(this.a9);
h9=(z8!=null);
}
if (event.keyCode!=event.DOM_VK_RETURN&&event.keyCode!=event.DOM_VK_TAB&&(this.a8&&!h9)&&this.a9.tagName=="SELECT")return ;
switch (event.keyCode){
case event.DOM_VK_LEFT:
case event.DOM_VK_RIGHT:
if (h9&&this.a9.getAttribute("FpEditor")!="RadioButton"){
this.EndEdit();
}
if (!this.a8){
this.NextCell(f7,event,event.keyCode);
}
break ;
case event.DOM_VK_UP:
case event.DOM_VK_DOWN:
case event.DOM_VK_RETURN:
if (this.a9!=null&&this.a9.tagName=="TEXTAREA")return ;
if (event.keyCode==event.DOM_VK_RETURN)this.CancelDefault(event);
if (this.a8){
var m4=this.EndEdit();
if (!m4)return ;
}
this.NextCell(f7,event,event.keyCode);
var e7=this.GetTopSpread(f7);
var f8=document.getElementById(e7.id+"_textBox");
if (event.DOM_VK_RETURN==event.keyCode)f8.focus();
break ;
case event.DOM_VK_TAB:
if (this.a8){
var m4=this.EndEdit();
if (!m4)return ;
}
var m3=this.GetProcessTab(f7);
var z9=(m3=="true"||m3=="True");
if (z9)this.NextCell(f7,event,event.keyCode);
break ;
case event.DOM_VK_SHIFT:
break ;
case event.DOM_VK_HOME:
case event.DOM_VK_END:
case event.DOM_VK_PAGE_UP:
case event.DOM_VK_PAGE_DOWN:
if (!this.a8){
this.NextCell(f7,event,event.keyCode);
}
break ;
default :
var aa0=window.navigator.userAgent;
var aa1=(aa0.indexOf("Firefox/2.")>=0);
if (aa1){
if (event.keyCode==67&&event.ctrlKey&&(!this.a8||h9))this.Copy(f7);
else if (event.keyCode==86&&event.ctrlKey&&(!this.a8||h9))this.Paste(f7);
else if (event.keyCode==88&&event.ctrlKey&&(!this.a8||h9))this.Clear(f7);
else if (!this.a8&&f7.d2!=null&&!this.IsHeaderCell(f7.d2)&&!event.ctrlKey&&!event.altKey){
this.StartEdit(f7,f7.d2);
}
}else {
if (event.charCode==99&&event.ctrlKey&&(!this.a8||h9))this.Copy(f7);
else if (event.charCode==118&&event.ctrlKey&&(!this.a8||h9))this.Paste(f7);
else if (event.charCode==120&&event.ctrlKey&&(!this.a8||h9))this.Clear(f7);
else if (!this.a8&&f7.d2!=null&&!this.IsHeaderCell(f7.d2)&&!event.ctrlKey&&!event.altKey){
this.StartEdit(f7,f7.d2);
}
}
break ;
}
}
this.GetProcessTab=function (f7){
return f7.getAttribute("ProcessTab");
}
this.ExpandRow=function (f7,i2){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"ExpandView,"+i2,f7);
else 
__doPostBack(q8,"ExpandView,"+i2);
}
this.SortColumn=function (f7,column){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"SortColumn,"+column,f7);
else 
__doPostBack(q8,"SortColumn,"+column);
}
this.Filter=function (event,f7){
var m1=this.GetTarget(event);
var f6=m1.value;
if (m1.tagName=="SELECT")
f6=m1[m1.selectedIndex].text;
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(m1.name,f6,f7);
else 
__doPostBack(m1.name,f6);
}
this.MoveCol=function (f7,from,to){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"MoveCol,"+from+","+to,f7);
else 
__doPostBack(q8,"MoveCol,"+from+","+to);
}
this.Group=function (f7,l1,toCol){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Group,"+l1+","+toCol,f7);
else 
__doPostBack(q8,"Group,"+l1+","+toCol);
}
this.Ungroup=function (f7,l1,toCol){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Ungroup,"+l1+","+toCol,f7);
else 
__doPostBack(q8,"Ungroup,"+l1+","+toCol);
}
this.Regroup=function (f7,fromCol,toCol){
var q8=f7.getAttribute("name");
var u7=(f7.getAttribute("ajax")!="false");
if (u7)
this.SyncData(q8,"Regroup,"+fromCol+","+toCol,f7);
else 
__doPostBack(q8,"Regroup,"+fromCol+","+toCol);
}
this.ProcessData=function (){
try {
var aa2=this;
aa2.removeEventListener("load",the_fpSpread.ProcessData,false);
var m1=window.srcfpspread;
m1=m1.split(":").join("_");
var aa3=window.fpcommand;
var aa4=document;
var aa5=aa4.getElementById(m1+"_buff");
if (aa5==null){
aa5=aa4.createElement("iframe");
aa5.id=m1+"_buff";
aa5.style.display="none";
aa4.body.appendChild(aa5);
}
var f7=aa4.getElementById(m1);
the_fpSpread.CloseWaitMsg(f7);
if (aa5==null)return ;
var aa6=aa2.responseText;
aa5.contentWindow.document.body.innerHTML=aa6;
var m3=aa5.contentWindow.document.getElementById(m1+"_values");
if (m3!=null){
var q7=m3.getElementsByTagName("data")[0];
var l3=q7.firstChild;
the_fpSpread.error=false;
while (l3!=null){
var g9=the_fpSpread.GetRowByKey(f7,l3.getAttribute("r"));
var h1=the_fpSpread.GetColByKey(f7,l3.getAttribute("c"));
var u9=the_fpSpread.GetValue(f7,g9,h1);
if (l3.innerHTML!=u9){
var h8=the_fpSpread.GetFormula(f7,g9,h1);
var i5=the_fpSpread.GetCellByRowCol(f7,g9,h1);
the_fpSpread.SetCellValueFromView(i5,l3.innerHTML,true);
i5.setAttribute("FpFormula",h8);
}
l3=l3.nextSibling;
}
the_fpSpread.ClearCellData(f7);
}else {
the_fpSpread.UpdateSpread(aa4,aa5,m1,aa6,aa3);
}
var u8=the_fpSpread.GetForm(f7);
u8.__EVENTTARGET.value="";
u8.__EVENTARGUMENT.value="";
var u9=aa4.getElementsByName("__VIEWSTATE")[0];
var f6=aa5.contentWindow.document.getElementsByName("__VIEWSTATE")[0];
if (u9!=null&&f6!=null)u9.value=f6.value;
u9=aa4.getElementsByName("__EVENTVALIDATION");
f6=aa5.contentWindow.document.getElementsByName("__EVENTVALIDATION");
if (u9!=null&&f6!=null&&u9.length>0&&f6.length>0)
u9[0].value=f6[0].value;
aa5.contentWindow.document.location="about:blank";
window.fpPostOn=null;
d9=null;
}catch (g0){
window.fpPostOn=null;
d9=null;
}
var f7=the_fpSpread.GetTopSpread(aa4.getElementById(m1));
var g0=the_fpSpread.CreateEvent("CallBackStopped");
g0.command=aa3;
the_fpSpread.FireEvent(f7,g0);
};
this.UpdateSpread=function (aa4,aa5,m1,aa6,aa3){
var f7=the_fpSpread.GetTopSpread(aa4.getElementById(m1));
var p8=aa5.contentWindow.document.getElementById(f7.id);
if (p8!=null){
the_fpSpread.error=(p8.getAttribute("error")=="true");
if (aa3=="LoadOnDemand"&&!the_fpSpread.error){
var aa7=this.GetElementById(f7,f7.id+"_data");
var aa8=this.GetElementById(p8,f7.id+"_data");
if (aa7!=null&&aa8!=null)aa7.setAttribute("data",aa8.getAttribute("data"));
var aa9=p8.getElementsByTagName("style");
if (aa9!=null){
for (var e9=0;e9<aa9.length;e9++){
if (aa9[e9]!=null&&aa9[e9].innerHTML!=null&&aa9[e9].innerHTML.indexOf(f7.id+"msgStyle")<0)
f7.appendChild(aa9[e9].cloneNode(true));
}
}
var ab0=this.GetElementById(f7,f7.id+"_LoadInfo");
var ab1=this.GetElementById(p8,f7.id+"_LoadInfo");
if (ab0!=null&&ab1!=null)ab0.value=ab1.value;
var ab2=false;
var ab3=this.GetElementById(p8,f7.id+"_rowHeader");
if (ab3!=null){
ab3=ab3.firstChild;
ab2=(ab3.rows.length>1);
var i8=this.GetRowHeader(f7);
this.LoadRows(i8,ab3,true);
}
var ab4=this.GetElementById(p8,f7.id+"_viewport");
if (ab4!=null){
ab2=(ab4.rows.length>0);
var e5=this.GetViewport(f7);
this.LoadRows(e5,ab4,false);
}
the_fpSpread.Init(f7);
the_fpSpread.LoadScrollbarState(f7);
the_fpSpread.Focus(f7);
if (ab2)
f7.LoadState=null;
else 
f7.LoadState="complete";
}else {
f7.innerHTML=p8.innerHTML;
the_fpSpread.CopySpreadAttrs(p8,f7);
var ab5=aa5.contentWindow.document.getElementById(f7.id+"_initScript");
eval(ab5.value);
}
}else {
the_fpSpread.error=true;
}
}
this.LoadRows=function (e5,ab4,isHeader){
if (e5==null||ab4==null)return ;
var ab6=e5.tBodies[0];
var t5=ab4.rows.length;
var ab7=null;
if (isHeader){
t5--;
if (ab6.rows.length>0)ab7=ab6.rows[ab6.rows.length-1];
}
for (var e9=0;e9<t5;e9++){
var ab8=ab4.rows[e9].cloneNode(false);
ab6.insertBefore(ab8,ab7);
ab8.innerHTML=ab4.rows[e9].innerHTML;
}
if (!isHeader){
for (var e9=0;e9<ab4.parentNode.childNodes.length;e9++){
var u5=ab4.parentNode.childNodes[e9];
if (u5!=ab4){
e5.parentNode.insertBefore(u5.cloneNode(true),null);
}
}
}
}
this.CopySpreadAttrs=function (v7,dest){
dest.setAttribute("totalRowCount",v7.getAttribute("totalRowCount"));
dest.setAttribute("pageCount",v7.getAttribute("pageCount"));
dest.setAttribute("loadOnDemand",v7.getAttribute("loadOnDemand"));
dest.setAttribute("allowGroup",v7.getAttribute("allowGroup"));
dest.setAttribute("colMove",v7.getAttribute("colMove"));
dest.setAttribute("showFocusRect",v7.getAttribute("showFocusRect"));
dest.setAttribute("FocusBorderColor",v7.getAttribute("FocusBorderColor"));
dest.setAttribute("FocusBorderStyle",v7.getAttribute("FocusBorderStyle"));
dest.setAttribute("FpDefaultEditorID",v7.getAttribute("FpDefaultEditorID"));
dest.setAttribute("hierView",v7.getAttribute("hierView"));
dest.setAttribute("IsNewRow",v7.getAttribute("IsNewRow"));
dest.setAttribute("cmdTop",v7.getAttribute("cmdTop"));
dest.setAttribute("ProcessTab",v7.getAttribute("ProcessTab"));
dest.setAttribute("AcceptFormula",v7.getAttribute("AcceptFormula"));
dest.setAttribute("EditMode",v7.getAttribute("EditMode"));
dest.setAttribute("AllowInsert",v7.getAttribute("AllowInsert"));
dest.setAttribute("AllowDelete",v7.getAttribute("AllowDelete"));
dest.setAttribute("error",v7.getAttribute("error"));
dest.setAttribute("ajax",v7.getAttribute("ajax"));
dest.setAttribute("autoCalc",v7.getAttribute("autoCalc"));
dest.setAttribute("multiRange",v7.getAttribute("multiRange"));
dest.setAttribute("rowFilter",v7.getAttribute("rowFilter"));
dest.setAttribute("OperationMode",v7.getAttribute("OperationMode"));
dest.setAttribute("selectedForeColor",v7.getAttribute("selectedForeColor"));
dest.setAttribute("selectedBackColor",v7.getAttribute("selectedBackColor"));
dest.setAttribute("anchorBackColor",v7.getAttribute("anchorBackColor"));
dest.tabIndex=v7.tabIndex;
if (dest.style!=null&&v7.style!=null){
if (dest.style.width!=v7.style.width)dest.style.width=v7.style.width;
if (dest.style.height!=v7.style.height)dest.style.height=v7.style.height;
if (dest.style.border!=v7.style.border)dest.style.border=v7.style.border;
}
}
this.Clone=function (k2){
var f6=document.createElement(k2.tagName);
f6.id=k2.id;
var h1=k2.firstChild;
while (h1!=null){
var n1=this.Clone(h1);
f6.appendChild(n1);
h1=h1.nextSibling;
}
return f6;
}
this.FireEvent=function (f7,g0){
if (f7==null||g0==null)return ;
var e7=this.GetTopSpread(f7);
if (e7!=null){
g0.spread=f7;
e7.dispatchEvent(g0);
}
}
this.GetForm=function (f7)
{
var h8=f7.parentNode;
while (h8!=null&&h8.tagName!="FORM")h8=h8.parentNode;
return h8;
}
this.SyncData=function (q8,aa3,f7,asyncCallBack){
if (window.fpPostOn!=null){
return ;
}
this.a8=false;
var g0=this.CreateEvent("CallBackStart");
g0.cancel=false;
g0.command=aa3;
if (asyncCallBack==null)asyncCallBack=false;
g0.async=asyncCallBack;
if (f7==null){
var n1=q8.split(":").join("_");
f7=document.getElementById(n1);
}
if (f7!=null){
var e7=this.GetTopSpread(f7);
this.FireEvent(f7,g0);
}
if (g0.cancel){
the_fpSpread.ClearCellData(f7);
return ;
}
if (aa3!=null&&(aa3.indexOf("SelectView,")==0||aa3=="Next"||aa3=="Prev"||aa3.indexOf("Group,")==0||aa3.indexOf("Page,")==0))
f7.LoadState=null;
var ab9=g0.async;
if (ab9){
this.OpenWaitMsg(f7);
}
window.fpPostOn=true;
if (this.error)aa3="update";
try {
var u8=this.GetForm(f7);
if (u8==null)return ;
u8.__EVENTTARGET.value=q8;
u8.__EVENTARGUMENT.value=encodeURIComponent(aa3);
var ac0=u8.action;
var f6;
if (ac0.indexOf("?")>-1){
f6="&";
}
else 
{
f6="?";
}
ac0=ac0+f6;
var f2=this.CollectData(f7);
var aa6="";
var aa2=(window.XMLHttpRequest)?new XMLHttpRequest():new ActiveXObject("Microsoft.XMLHTTP");
if (aa2==null)return ;
aa2.open("POST",ac0,ab9);
aa2.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
if (f7!=null)
window.srcfpspread=f7.id;
else 
window.srcfpspread=q8;
window.fpcommand=aa3;
this.AttachEvent(aa2,"load",the_fpSpread.ProcessData,false);
aa2.send(f2);
}catch (g0){
window.fpPostOn=false;
d9=null;
}
};
this.CollectData=function (f7){
var u8=this.GetForm(f7);
var f6;
var g2="fpcallback=true&";
for (var e9=0;e9<u8.elements.length;e9++){
f6=u8.elements[e9];
var ac1=f6.tagName.toLowerCase();
if (ac1=="input"){
var ac2=f6.type;
if (ac2=="hidden"||ac2=="text"||ac2=="password"||((ac2=="checkbox"||ac2=="radio")&&f6.checked)){
g2+=(f6.name+"="+encodeURIComponent(f6.value)+"&");
}
}else if (ac1=="select"){
if (f6.childNodes!=null){
for (var h7=0;h7<f6.childNodes.length;h7++){
var o0=f6.childNodes[h7];
if (o0!=null&&o0.tagName!=null&&o0.tagName.toLowerCase()=="option"&&o0.selected){
g2+=(f6.name+"="+encodeURIComponent(o0.value)+"&");
}
}
}
}else if (ac1=="textarea"){
g2+=(f6.name+"="+encodeURIComponent(f6.value)+"&");
}
}
return g2;
};
this.ClearCellData=function (f7){
var f2=this.GetData(f7);
var ac3=f2.getElementsByTagName("root")[0];
var f3=ac3.getElementsByTagName("data")[0];
if (f3==null)return null;
if (f7.d9!=null){
var i2=f7.d9.firstChild;
while (i2!=null){
var g9=i2.getAttribute("key");
var ac4=i2.firstChild;
while (ac4!=null){
var h1=ac4.getAttribute("key");
var ac5=f3.firstChild;
while (ac5!=null){
var h3=ac5.getAttribute("key");
if (g9==h3){
var ac6=false;
var ac7=ac5.firstChild;
while (ac7!=null){
var h4=ac7.getAttribute("key");
if (h1==h4){
ac5.removeChild(ac7);
ac6=true;
break ;
}
ac7=ac7.nextSibling;
}
if (ac6)break ;
}
ac5=ac5.nextSibling;
}
ac4=ac4.nextSibling;
}
i2=i2.nextSibling;
}
}
f7.d9=null;
var f5=this.GetCmdBtn(f7,"Cancel");
if (f5!=null)
this.UpdateCmdBtnState(f5,true);
}
this.StorePostData=function (f7){
var f2=this.GetData(f7);
var f3=f2.getElementsByTagName("root")[0];
var w6=f3.getElementsByTagName("data")[0];
if (w6!=null)f7.d9=w6.cloneNode(true);
}
}
function CheckBoxCellType_setFocus(h2){
var h9=h2.getElementsByTagName("INPUT");
if (h9!=null&&h9.length>0&&h9[0].type=="checkbox"){
h9[0].focus();
}
}
function CheckBoxCellType_getCheckBoxEditor(h2){
var h9=h2.getElementsByTagName("INPUT");
if (h9!=null&&h9.length>0&&h9[0].type=="checkbox"){
return h9[0];
}
return null;
}
function CheckBoxCellType_isValid(h2,s6){
if (s6==null)return "";
s6=the_fpSpread.Trim(s6);
if (s6=="")return "";
if (s6.toLowerCase()=="true"||s6.toLowerCase()=="false")
return "";
else 
return "invalid value";
}
function CheckBoxCellType_getValue(t0,f7){
return CheckBoxCellType_getEditorValue(t0,f7);
}
function CheckBoxCellType_getEditorValue(t0,f7){
var h2=the_fpSpread.GetCell(t0);
var h9=CheckBoxCellType_getCheckBoxEditor(h2);
if (h9!=null&&h9.checked){
return "true";
}
return "false";
}
function CheckBoxCellType_setValue(t0,s6){
var h2=the_fpSpread.GetCell(t0);
var h9=CheckBoxCellType_getCheckBoxEditor(h2);
if (h9!=null){
h9.checked=(s6!=null&&s6.toLowerCase()=="true");
return ;
}
}
function IntegerCellType_getValue(t0){
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
var r0=f6.innerHTML;
t0=the_fpSpread.GetCell(t0);
if (t0.getAttribute("FpRef")!=null)t0=document.getElementById(t0.getAttribute("FpRef"));
var ac8=t0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=r0.length;
while (true){
r0=r0.replace(ac8,"");
if (r0.length==r7)break ;
r7=r0.length;
}
if (r0.charAt(0)=='('&&r0.charAt(r0.length-1)==')'){
var ac9=t0.getAttribute("negsign");
if (ac9==null)ac9="-";
r0=ac9+r0.substring(1,r0.length-1);
}
r0=the_fpSpread.ReplaceAll(r0,"&nbsp;"," ");
return r0;
}
function IntegerCellType_isValid(h2,s6){
if (s6==null||s6.length==0)return "";
s6=s6.replace(" ","");
if (s6.length==0)return "";
var ad0=h2;
var ad1=h2.getAttribute("FpRef");
if (ad1!=null)ad0=document.getElementById(ad1);
var ac9=ad0.getAttribute("negsign");
var ad2=ad0.getAttribute("possign");
if (ac9!=null)s6=s6.replace(ac9,"-");
if (ad2!=null)s6=s6.replace(ad2,"+");
if (s6.charAt(s6.length-1)=="-")s6="-"+s6.substring(0,s6.length-1);
var r9=new RegExp("^\\s*[-\\+]?\\d+\\s*$");
var m4=(s6.match(r9)!=null);
if (m4)m4=!isNaN(s6);
if (m4){
var s3=ad0.getAttribute("MinimumValue");
var i4=ad0.getAttribute("MaximumValue");
var s2=parseInt(s6);
if (s3!=null){
s3=parseInt(s3);
m4=(!isNaN(s3)&&s2>=s3);
}
if (m4&&i4!=null){
i4=parseInt(i4);
m4=(!isNaN(i4)&&s2<=i4);
}
}
if (!m4){
if (ad0.getAttribute("error")!=null)
return ad0.getAttribute("error");
else 
return "Integer";
}
return "";
}
function DoubleCellType_isValid(h2,s6){
if (s6==null||s6.length==0)return "";
var ad0=h2;
if (h2.getAttribute("FpRef")!=null)ad0=document.getElementById(h2.getAttribute("FpRef"));
var ad3=ad0.getAttribute("decimalchar");
if (ad3==null)ad3=".";
var ac8=ad0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=s6.length;
while (true){
s6=s6.replace(ac8,"");
if (s6.length==r7)break ;
r7=s6.length;
}
var m4=true;
if (s6.length==0){
m4=false;
}else {
var ac9=ad0.getAttribute("negsign");
var ad2=ad0.getAttribute("possign");
var s3=ad0.getAttribute("MinimumValue");
var i4=ad0.getAttribute("MaximumValue");
m4=the_fpSpread.IsDouble(s6,ad3,ac9,ad2,s3,i4);
}
if (!m4){
if (ad0.getAttribute("error")!=null)
return ad0.getAttribute("error");
else 
return "Double";
}
return "";
}
function DoubleCellType_getValue(t0){
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
var r0=f6.innerHTML;
t0=the_fpSpread.GetCell(t0);
if (t0.getAttribute("FpRef")!=null)t0=document.getElementById(t0.getAttribute("FpRef"));
var ac8=t0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=r0.length;
while (true){
r0=r0.replace(ac8,"");
if (r0.length==r7)break ;
r7=r0.length;
}
if (r0.charAt(0)=='('&&r0.charAt(r0.length-1)==')'){
var ac9=t0.getAttribute("negsign");
if (ac9==null)ac9="-";
r0=ac9+r0.substring(1,r0.length-1);
}
r0=the_fpSpread.ReplaceAll(r0,"&nbsp;"," ");
return r0;
}
function CurrencyCellType_isValid(h2,s6){
if (s6!=null&&s6.length>0){
var ad0=h2;
if (h2.getAttribute("FpRef")!=null)ad0=document.getElementById(h2.getAttribute("FpRef"));
var r6=ad0.getAttribute("currencychar");
if (r6==null)r6="$";
s6=s6.replace(r6,"");
var ac8=ad0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=s6.length;
while (true){
s6=s6.replace(ac8,"");
if (s6.length==r7)break ;
r7=s6.length;
}
var m4=true;
if (s6.length==0){
m4=false;
}else {
var ad3=ad0.getAttribute("decimalchar");
if (ad3==null)ad3=".";
var ac9=ad0.getAttribute("negsign");
var ad2=ad0.getAttribute("possign");
var s3=ad0.getAttribute("MinimumValue");
var i4=ad0.getAttribute("MaximumValue");
m4=the_fpSpread.IsDouble(s6,ad3,ac9,ad2,s3,i4);
}
if (!m4){
if (ad0.getAttribute("error")!=null)
return ad0.getAttribute("error");
else 
return "Currency ("+r6+"100"+ad3+"10) ";
}
}
return "";
}
function CurrencyCellType_getValue(t0){
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
var r0=f6.innerHTML;
t0=the_fpSpread.GetCell(t0);
if (t0.getAttribute("FpRef")!=null)t0=document.getElementById(t0.getAttribute("FpRef"));
var r6=t0.getAttribute("currencychar");
if (r6!=null){
var ad4=document.createElement("SPAN");
ad4.innerHTML=r6;
r6=ad4.innerHTML;
}
if (r6==null)r6="$";
var ac8=t0.getAttribute("groupchar");
if (ac8==null)ac8=",";
r0=r0.replace(r6,"");
var r7=r0.length;
while (true){
r0=r0.replace(ac8,"");
if (r0.length==r7)break ;
r7=r0.length;
}
var ac9=t0.getAttribute("negsign");
if (ac9==null)ac9="-";
if (r0.charAt(0)=='('&&r0.charAt(r0.length-1)==')'){
r0=ac9+r0.substring(1,r0.length-1);
}
r0=the_fpSpread.ReplaceAll(r0,"&nbsp;"," ");
return r0;
}
function RegExpCellType_isValid(h2,s6){
if (s6==null||s6=="")
return "";
var ad0=h2;
if (h2.getAttribute("FpRef")!=null)ad0=document.getElementById(h2.getAttribute("FpRef"));
var ad5=new RegExp(ad0.getAttribute("fpexpression"));
var s0=s6.match(ad5);
var k9=(s0!=null&&s0.length>0&&s6==s0[0]);
if (!k9){
if (ad0.getAttribute("error")!=null)
return ad0.getAttribute("error");
else 
return "invalid";
}
return "";
}
function PercentCellType_getValue(t0){
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
f6=f6.innerHTML;
var h2=the_fpSpread.GetCell(t0);
var ad0=h2;
if (h2.getAttribute("FpRef")!=null)ad0=document.getElementById(h2.getAttribute("FpRef"));
var ad6=ad0.getAttribute("percentchar");
if (ad6==null)ad6="%";
f6=f6.replace(ad6,"");
var ac8=ad0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=f6.length;
while (true){
f6=f6.replace(ac8,"");
if (f6.length==r7)break ;
r7=f6.length;
}
var ac9=ad0.getAttribute("negsign");
var ad2=ad0.getAttribute("possign");
f6=the_fpSpread.ReplaceAll(f6,"&nbsp;"," ");
var g2=f6;
if (ac9!=null)
f6=f6.replace(ac9,"-");
if (ad2!=null)
f6=f6.replace(ad2,"+");
var ad3=ad0.getAttribute("decimalchar");
if (ad3!=null)
f6=f6.replace(ad3,".");
if (!isNaN(f6))
return g2;
else 
return t0.innerHTML;
}
function PercentCellType_setValue(t0,s6){
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
t0=f6;
if (s6!=null&&s6!=""){
var ad0=the_fpSpread.GetCell(t0);
if (ad0.getAttribute("FpRef")!=null)ad0=document.getElementById(ad0.getAttribute("FpRef"));
var ad6=ad0.getAttribute("percentchar");
if (ad6==null)ad6="%";
s6=s6.replace(" ","");
s6=s6.replace(ad6,"");
t0.innerHTML=s6+ad6;
}else {
t0.innerHTML="";
}
}
function PercentCellType_isValid(h2,s6){
if (s6!=null){
var ad0=the_fpSpread.GetCell(h2);
if (ad0.getAttribute("FpRef")!=null)ad0=document.getElementById(ad0.getAttribute("FpRef"));
var ad6=ad0.getAttribute("percentchar");
if (ad6==null)ad6="%";
s6=s6.replace(ad6,"");
var ac8=ad0.getAttribute("groupchar");
if (ac8==null)ac8=",";
var r7=s6.length;
while (true){
s6=s6.replace(ac8,"");
if (s6.length==r7)break ;
r7=s6.length;
}
var ad7=s6;
var ac9=ad0.getAttribute("negsign");
var ad2=ad0.getAttribute("possign");
if (ac9!=null)s6=s6.replace(ac9,"-");
if (ad2!=null)s6=s6.replace(ad2,"+");
var ad3=ad0.getAttribute("decimalchar");
if (ad3!=null)
s6=s6.replace(ad3,".");
var m4=!isNaN(s6);
if (m4){
var ad8=ad0.getAttribute("MinimumValue");
var ad9=ad0.getAttribute("MaximumValue");
if (ad8!=null||ad9!=null){
var s3=parseFloat(ad8);
var i4=parseFloat(ad9);
m4=!isNaN(s3)&&!isNaN(i4);
if (m4){
if (ad3==null)ad3=".";
m4=the_fpSpread.IsDouble(ad7,ad3,ac9,ad2,s3*100,i4*100);
}
}
}
if (!m4){
if (ad0.getAttribute("error")!=null)
return ad0.getAttribute("error");
else 
return "Percent:(ex,10"+ad6+")";
}
}
return "";
}
function ListBoxCellType_getValue(t0){
var f6=t0.getElementsByTagName("TABLE");
if (f6.length>0)
{
var g7=f6[0].rows;
for (var h7=0;h7<g7.length;h7++){
var h2=g7[h7].cells[0];
if (h2.selected=="true")
{
var ae0=h2;
while (ae0.firstChild!=null)ae0=ae0.firstChild;
var ad0=ae0.nodeValue;
return ad0;
}
}
}
return "";
}
function ListBoxCellType_setValue(t0,s6){
var f6=t0.getElementsByTagName("TABLE");
if (f6.length>0)
{
f6[0].style.width=(t0.clientWidth-6)+"px";
var g7=f6[0].rows;
for (var h7=0;h7<g7.length;h7++){
var h2=g7[h7].cells[0];
var ae0=h2;
while (ae0.firstChild!=null)ae0=ae0.firstChild;
var ad0=ae0.nodeValue;
if (ad0==s6){
h2.selected="true";
if (f6[0].parentNode.getAttribute("selectedBackColor")!="undefined")
h2.style.backgroundColor=f6[0].parentNode.getAttribute("selectedBackColor");
if (f6[0].parentNode.getAttribute("selectedForeColor")!="undefined")
h2.style.color=f6[0].parentNode.getAttribute("selectedForeColor");
}else {
h2.style.backgroundColor="";
h2.style.color="";
h2.selected="";
h2.bgColor="";
}
}
}
}
function TextCellType_getValue(t0){
var h2=the_fpSpread.GetCell(t0,true);
if (h2.getAttribute("password")!=null){
if (h2!=null&&h2.getAttribute("value")!=null)
return h2.getAttribute("value");
else 
return "";
}else {
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
if (f6.innerHTML=="&nbsp;")return "";
if (f6!=null)f6=the_fpSpread.HTMLDecode(f6.innerHTML);
f6=the_fpSpread.ReplaceAll(f6,"<br>","\n");
return f6;
}
}
function TextCellType_setValue(t0,s6){
var h2=the_fpSpread.GetCell(t0,true);
if (h2==null)return ;
var f6=t0;
while (f6.firstChild!=null&&f6.firstChild.nodeName!="#text")f6=f6.firstChild;
t0=f6;
if (h2.getAttribute("password")!=null){
if (s6!=null&&s6!=""){
s6=s6.replace(" ","");
t0.innerHTML="";
for (var e9=0;e9<s6.length;e9++)
t0.innerHTML+="*";
h2.setAttribute("value",s6);
}else {
t0.innerHTML="";
h2.setAttribute("value","");
}
}else {
if (s6!=null)s6=the_fpSpread.HTMLEncode(s6);
s6=the_fpSpread.ReplaceAll(s6,"\n","<br>");
t0.innerHTML=s6;
}
}
function TextCellType_setEditorValue(f6,s6){
if (s6!=null)s6=the_fpSpread.HTMLDecode(s6);
f6.value=s6;
}
function RadioButtonListCellType_getValue(t0){
var h2=the_fpSpread.GetCell(t0,true);
if (h2==null)return ;
var ae1=h2.getElementsByTagName("INPUT");
for (var e9=0;e9<ae1.length;e9++){
if (ae1[e9].tagName=="INPUT"&&ae1[e9].checked){
return ae1[e9].value;
}
}
return "";
}
function RadioButtonListCellType_getEditorValue(t0){
return RadioButtonListCellType_getValue(t0);
}
function RadioButtonListCellType_setValue(t0,s6){
var h2=the_fpSpread.GetCell(t0,true);
if (h2==null)return ;
if (s6!=null)s6=the_fpSpread.Trim(s6);
var ae1=h2.getElementsByTagName("INPUT");
for (var e9=0;e9<ae1.length;e9++){
if (ae1[e9].tagName=="INPUT"&&s6==the_fpSpread.Trim(ae1[e9].value)){
ae1[e9].checked=true;
break ;
}else {
if (ae1[e9].checked)ae1[e9].checked=false;
}
}
}
function RadioButtonListCellType_setFocus(t0){
var h2=the_fpSpread.GetCell(t0,true);
if (h2==null)return ;
var h9=h2.getElementsByTagName("INPUT");
if (h9==null)return ;
for (var e9=0;e9<h9.length;e9++){
if (h9[e9].type=="radio"&&h9[e9].checked){
h9[e9].focus();
return ;
}
}
}
