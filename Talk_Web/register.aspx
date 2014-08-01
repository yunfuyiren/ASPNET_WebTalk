<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Talk_Web.register" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>注册页面</title>
      <style type="text/css">
        .icon-exclamation {
            padding-left: 25px !important;
            background: url(/icons/exclamation-png/ext.axd) no-repeat 3px 3px !important;
        }
        
        .icon-accept {
            padding-left: 25px !important;
            background: url(/icons/accept-png/ext.axd) no-repeat 3px 3px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" Theme="Gray" runat="server" DirectMethodNamespace="X" />
       
      <center>
                <ext:Window 
                    ID="Window1" 
                    runat="server" 
                    Closable="false"
                    Resizable="false"
                    AutoHeight="true"
                    
                    Title="用户注册表"
                    Draggable="false"
                    Width="350"
                    Modal="true"
                    Padding="8"
                    Layout="Form">
                   <Items>
                    
                    <ext:Panel ID="Panel1" runat="server" Border="false" Header="false" ColumnWidth=".7" Layout="Form" LabelAlign="Left">
                        <Defaults>
                            <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                            <ext:Parameter Name="MsgTarget" Value="side" />
                        </Defaults>
                        <Items>
                            <ext:NumberField ID="userid" runat="server" FieldLabel="用户号" AnchorHorizontal="92%" />
                            <ext:TextField ID="username" runat="server" FieldLabel="用户名" AnchorHorizontal="92%" />
                            
                            <ext:RadioGroup ID="RadioGroup1" runat="server"  FieldLabel="性别" AnchorHorizontal="92%">
                                  <Items>
                                        <ext:Radio ID="G1R1" runat="server" BoxLabel="男" Checked="true" />
                                        <ext:Radio ID="G1R2" runat="server" BoxLabel="女"  />
                                  </Items>
                            </ext:RadioGroup> 
                           
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel2" runat="server" Border="false" Layout="Form" ColumnWidth=".5" LabelAlign="Left">
                        <Defaults>
                            <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                        </Defaults>
                        <Items>
                          <ext:TextField 
                            ID="PasswordField" 
                            runat="server"                    
                            FieldLabel="密码"
                            InputType="Password"
                             MsgTarget="Side"
                             AnchorHorizontal="92%">                            
                        </ext:TextField>
                       
                        <ext:TextField ID="ConfirmPasswordField" 
                            runat="server"                     
                            Vtype="password"
                            FieldLabel="确认密码"
                            InputType="Password"
                            MsgTarget="Side"
                             AnchorHorizontal="92%">     
                            <CustomConfig>
                                <ext:ConfigItem Name="initialPassField" Value="#{PasswordField}" Mode="Value" />
                            </CustomConfig>                      
                         </ext:TextField>     
                        
                        </Items>
                    </ext:Panel>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="Save">
                        <Listeners>
                            <Click Handler="X.Add_User();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="Button2" runat="server" Text="Cancel" >
                        <DirectEvents>
                            <Click OnEvent="cancel"></Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                </ext:Window >
  </center>

    </form>
</body>
</html>
