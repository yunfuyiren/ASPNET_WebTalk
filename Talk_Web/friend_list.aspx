<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="friend_list.aspx.cs" Inherits="Talk_Web.friend_list" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">

        function getTreeCheckedValue(node) {
            var checkedNodes = node.id;
            if (node.id == "0") { return false; }   //alert("请按左边+号展开节点选择用户!");
            if (node.id.split("#")[0] == "0") { return false; }   //alert("请选择用户!");
            document.getElementById('GetFriendID').value = node.id;
            document.getElementById('GetFriendName').value = node.text;
            returnValue = node.id;
            //document.getElementById("btnClose").click();            
        }


        ///////////////////好友管理模块
        var talking = function () {

            var a = document.getElementById('GetFriendID').value;
            var b = document.getElementById('GetFriendName').value;
            window.parent.test(a, b);
        };

        var del = function () {
            var a = document.getElementById('GetFriendID').value;
            var b = document.getElementById('GetFriendName').value;
            window.parent.test_del(a, b);

        };

        var update = function () {
            var a = document.getElementById('GetFriendID').value;
            var b = document.getElementById('GetFriendName').value;
            window.parent.test_update(a, b);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
        <Items>
             <ext:TreePanel 
                   ID="friend_treepanel" 
                        runat="server" 
                        AutoHeight="false" 
                        Icon="BookOpen" ContextMenuID="cmenu"
                        >
                        <TopBar>
                            <ext:Toolbar ID="Toolbar2" runat="server">
                                <Items>
                                    <ext:Button ID="friend_expand" runat="server" Text="展开所有">
                                        <Listeners>
                                            <Click Handler="#{friend_treepanel}.expandAll();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button ID="friend_collapse" runat="server" Text="闭合所有">
                                        <Listeners>
                                            <Click Handler="#{friend_treepanel}.collapseAll();" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                    

                        <Root>
                        </Root>   
                <%--         <Listeners>
                            <Click Fn="onNodeClick" />
                        </Listeners>
                                 --%>
                         <Listeners>
                         <Click Handler="getTreeCheckedValue(node)" /><Click />             
                        </Listeners>
                   </ext:TreePanel>
                 </Items>
            </ext:Viewport>
            
              <ext:TextField ID="GetFriendID" runat="server" Hidden="true" ></ext:TextField>
                 <ext:TextField ID="GetFriendName" runat="server" Hidden="true" ></ext:TextField>

            <ext:Menu ID="cmenu" runat="server"> 
                   <Items>
                     <ext:MenuItem ID="copyItems" runat="server" Text="选择聊天" Icon="Add">
                       <Listeners>
                             <Click Handler="talking();" />                    
                         </Listeners>    
                      </ext:MenuItem>
                     <ext:MenuItem ID="editItems" runat="server" Text="修改好友" Icon="Anchor">
                      <Listeners>
                            <Click Handler="update();" />
                      </Listeners>
                     </ext:MenuItem>
                     <ext:MenuItem ID="moveItems" runat="server" Text="删除好友" Icon="Delete">
                         <Listeners>
                              <Click Handler="del();" />
                      </Listeners>
                     </ext:MenuItem>
                </Items>
         </ext:Menu>
    </form>
</body>
</html>
