<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Talk_Web._Default"%>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ext:ResourceManager ID="ResourceManager1" Theme="Gray" runat="server" DirectMethodNamespace="X" /> 

          <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="135"
            Icon="Lock" 
            Title="Login"
            Draggable="false"
            Width="280"
            Modal="true"
            Padding="8"
            Layout="Form">
            <Items>
                <ext:TextField 
                    ID="txtUsername" 
                    runat="server" 
                    FieldLabel="用户号" 
                    AllowBlank="false"
                    
                    
                    AnchorHorizontal="98%"
                    BlankText="Your username is required."
                    />
                <ext:TextField 
                    ID="txtPassword" 
                    runat="server" 
                    InputType="Password" 
                    FieldLabel="密码" 
                    AllowBlank="false" 
                     AnchorHorizontal="98%"
                    BlankText="Your password is required."
                    />
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="登录" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="Button1_Click">
                            <EventMask ShowMask="true" Msg="Verifying..." MinDelay="1000" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                
                <ext:LinkButton ID="Button2" runat="server" Text="注册">
                    <Listeners>
                        <Click Handler="X.Button2_Click();" />
                    </Listeners>
                </ext:LinkButton>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>
