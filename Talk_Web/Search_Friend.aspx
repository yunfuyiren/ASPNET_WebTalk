<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Friend.aspx.cs" Inherits="Talk_Web.Search_Friend" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查找好友</title>
</head>
<body>
     <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="CompanyX" />
     
     <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
     <Items>
        <ext:FormPanel ID="Panel1"
            runat="server"
            Frame="true" 
            Padding="5" AnchorHorizontal="100%"
           >
            <Items>
                <ext:TextField ID="user_num" runat="server" FieldLabel="用户号" AnchorHorizontal="100%" AllowBlank="False"></ext:TextField>
                <ext:TextField ID="user_name" runat="server" FieldLabel="用户名" AnchorHorizontal="100%"></ext:TextField>
            </Items>
            <Buttons>
                <ext:Button ID="search_button" runat="server" Text="查找">
                    <Listeners>
                        <Click Handler="CompanyX.add();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:FormPanel>
        </Items>
      </ext:Viewport>
</body>
</html>
