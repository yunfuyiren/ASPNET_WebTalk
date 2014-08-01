<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="Talk_Web.Chart" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" Theme="Gray" runat="server"  DirectMethodNamespace="CX">
        
    </ext:ResourceManager>
    <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
        <Items>
            <ext:TextArea ID="TextArea1" runat="server" ReadOnly="true">
            </ext:TextArea>
        </Items>
    </ext:Viewport>
    <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task 
                    TaskID="TaskManager1"
                    Interval="1500" 
                    OnStart=""
                    OnStop="">
                      <DirectEvents>
                        <Update OnEvent="GetMessage"  >

                        </Update>
                       </DirectEvents>
			
			
                                  
                </ext:Task>
                
            </Tasks>

        </ext:TaskManager>
    </form>
</body>
</html>
