function MultiPickList3(param1, param2, param3,param4,param5)
{
//************************************************************************************************
//******  Solution:                               Multi Select Pick List function:                              
//******  Platform:                              Microsoft Dynamics CRM 2011 
//******  Last updated date:            25th Apirl 2012
//******  Created by:                         Aroop Vachali, aroopv@gmail.com, Company: Central Equity, Melbourne, Australia
//
//************************************************************************************************
//**** Courtsey: Karen Carvalho (Marketing Manager), Christine Suell (Supervisor), Google and all bloggers (for many 
//                                                                                                                                                                                       of the syntaxes)
//************************************************************************************************

try
{
var fn = arguments.callee.toString().match(/function\s+([^\s\(]+)/); 

if (param1==null || param2==null)
{
alert("Error: Parameter missing. \n Format: <optionset fieldname> ,  <option value text field> ,  [<option header>], [<select color>]   ,   [<deselect color>] ,  \n ["+"Function="+fn[1]+"]"  );
return true;
}

var tparamtype=Xrm.Page.data.entity.attributes.get(param1).getAttributeType();
if (tparamtype!="optionset")
  { alert (param1+"(first parameter) should be of type optionset \n"+"[function="+fn[1]+"]");
    return true;
  }

var tparamtype=Xrm.Page.data.entity.attributes.get(param2).getAttributeType();
if (tparamtype!="memo")
  { alert (param2+"(second parameter) should be of type memo (text with mutiline) \n[function="+fn[1]+"]");
    return true;
  }

var plOptions=param1;      
var plText=param2;    
var plMenuItem="View Selected List";
var SelectedList_orig = crmForm.all[plText];
var FullList=crmForm.all[plOptions];

var SelCtr=-1;
var new_selColor="orange";
var new_deSelColor="white";
if (param4!=null)
    new_selColor=param4;
if (param5!=null)
    new_deSelColor=param5;
   
var SelectedList =SelectedList_orig.value.split("\r\n");
crmForm.all[plText].style.display="none";

if(FullList!=null && SelectedList!=null)
    {
     initColor();
     if (param3!=null && param3!="")
        {
             plMenuItem=param3;
        }
   else
       { 
          plMenuItem=FullList.options[0].text;
           changeColor("grey",0);
       }

     for (var ctr=0; ctr<SelectedList.length;ctr++)
         {
           selCtr=SelectedIndex(SelectedList[ctr]);
             if (selCtr >-1)
               {       
                   changeColor(new_selColor,selCtr);
                }
         }
     }

function SelectedIndex(selectedText)
{
var FullListText;
for (var ctr1=0; ctr1<FullList.options.length;ctr1++)
    {
      FullListText=FullList.options[ctr1].text;
      if ( FullListText==selectedText)
         {
            return ctr1;
         }
     }
return -2;
}

crmForm.all[plOptions].attachEvent('onchange', OnChangePL);

function OnChangePL() 
{
var SelCtr=-1;
var sel=crmForm.all[plOptions].SelectedText;

if (sel==plMenuItem)
return;

SelCtr=SelectedIndex(sel);
var SelColor="grey"; 
SelColor=crmForm.all[plOptions][SelCtr].style.backgroundColor;

if (SelColor==new_selColor)
     {
       changeColor(new_deSelColor,SelCtr);
       saveChanges(sel,selCtr,"del");
    }
else
    {
       changeColor(new_selColor, SelCtr);             
       saveChanges(sel,selCtr,"add");
    }
}

function saveChanges(p_selText,p_SelCtr,p_mode)
{
switch(p_mode)
{
    case "add":
            SelectedList.push(p_selText);
            break;
    case "del":
            for (var ctr2=0;ctr2<SelectedList.length;ctr2++)
                 {
                  if (SelectedList[ctr2]==p_selText)
                       {
                         SelectedList.splice(ctr2,1);
                         break;
                       } 
                  }
              break;
}
Xrm.Page.getAttribute(plText).setValue(SelectedList.join("\r\n"));  
}

function initColor()
{
      for (var ctr3=0; ctr3<FullList.options.length;ctr3++)
           {             changeColor(new_deSelColor, ctr3);   }
}

function changeColor(newColor, newCtr)
{
      crmForm.all[plOptions][newCtr].style.backgroundColor=newColor;
}

}
catch (e)
         {  alert (e.description);}
}
//*************************************END*******************************************************