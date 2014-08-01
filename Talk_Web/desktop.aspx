<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="desktop.aspx.cs" Inherits="Talk_Web.desktop"%>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="Stylesheet" href="desk.css" type="text/css" />
     <title>Desktop - Ext.NET Examples</title>    
<ext:XScript ID="XScript1" runat="server">
    <script type="text/javascript">
        var alignPanels = function () {
           friend_win.getEl().alignTo(Ext.getBody(), "tr", [-505, 5], false)
        };

        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value + "%");
        };
        
        var RequestMethod = function(a,b){
            var w=a.getDesktop();
            var ww=w.createWindow({
                title:"好友操作页面",
                width  : 500,
                height : 300,
                maximizable : true,
                minimizable : true,
                autoLoad : {
                    url  : b,
                    mode : "iframe",
                    showMask : true
                }
            });
            ww.center();
            
        };
        
        var test = function(a,b)
        {
            Ext.net.DirectMethod.request(
            {
                url          : "Talk_WebSer.asmx/chat_win",
                cleanRequest : true,
                json         : true,
                params       :{
                                ss      : a,
                                bb      : b
                              },
                success     :function(result)
                            {
                                if(result=="b")
                                {
                                    Ext.net.Notification.show(
                                    {
                                        title:"Warning",
                                        html:"请选定选择好友！"
                                    }
                                    );
                                    return false;
                                }
                                else
                                {
                                    CCX.show_win(a,b);
                                    addTab(chat_room_tab,a,b,"./Chart.aspx");
                                    return true;
                                }
                            },
                failure      :function()
                              {
                                Ext.net.Notification.show(
                                {
                                title : "Warning",
                                html  : "应用程序请求失败!"
                                });
                    
                                return false;
                               }  
             });
            }
        
        var addTab = function (tabPanel, id,name,url) {
            var tab = tabPanel.getComponent(id);

            if (!tab) {
                tab = tabPanel.add({
                    id: id, 
                    title    : name, 
                    closable : true,                    
                    autoLoad : {
                        showMask : true,
                        url      : url,
                        mode: "iframe",
                        maskMsg  : "Loading " + url + "...",
                       
                         
                    }                     
                });
                /*
                tab.on("activate", function () {
                    var item = MenuPanel1.menu.items.get(id + "_item");
                    
                    if (item) {
                        MenuPanel1.setSelection(item);
                    }
                }, this);*/
            }
            
            tabPanel.setActiveTab(tab);
        };

        var getIndexId=function()
        {
            var pnl=#{chat_room_tab}.getActiveTab().getBody();
            var str=pnl.TextArea1.getValue();
            var date=new Date();
            var dateStr=date.getFullYear()+'/'+date.getMonth()+'/'+date.getDay()+" "+date.toLocaleTimeString();
            str+="\n"+#{txtname}.getValue().trim()+"\t"+dateStr+":\n\t"+#{content}.getValue();
            pnl.TextArea1.setValue(str);
            var x=#{chat_room_tab}.getActiveTab().id;
            #{content}.setValue("");
            return x;
        }
        
        ///////////////删除好友模块
        var test_del=function(a,b)
        {
            CCX.del_friend(a,b);    
        };
        
        var test_update=function(a,b)
        {
            CCX.update_friend(a,b);
        };
        
        ////////////////////////////消息管理模块
        var chat_settle=function()
        {
            chat_search_button.setDisabled(true);
            var x=send_msg_fri_id.getValue().toString().trim();
            var y=send_msg_fri_name.getValue().toString().trim();
            if(x=="" || y=="")
                return;
            test(send_msg_fri_id.getValue().toString().trim(),send_msg_fri_name.getValue().toString().trim());
            send_msg_fri_name.setValue("");
            send_msg_fri_id.setValue("");
           
        };
        
    </script>
    </ext:XScript>
</head>
<body>
<form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" Theme="Gray" runat="server" DirectMethodNamespace="CCX">
            <Listeners>
          
            </Listeners>
        </ext:ResourceManager>

        <ext:Desktop 
            ID="MyDesktop" 
            runat="server" 
            BackgroundColor="Black" 
            ShortcutTextColor="White" 
            Wallpaper="desktop.jpg">
            <StartButton Text="Start" IconCls="start-button" />
            <%-- NOTE: Body Controls must be added to a container with position:absolute --%>
            <Content>
        
             <ext:Window 
                    ID="friend_win" 
                    runat="server" 
                    Title="聊天好友列表"
                    Cls="desktopEl" 
                    Padding="0"
                    Border="false"
                    Collapsible="true" Height="485"
                    Closable="False">
                    <AutoLoad Url="friend_list.aspx" Mode="IFrame"></AutoLoad>
                    <BottomBar>                                          
                            <ext:ToolBar ID="friend_toolbar" runat="server">
                                <Items>
                                    <ext:Button ID="friend_add_button" runat="server" ToolTip="添加好友" Icon="Add">
                                        <Listeners>
                                            <Click Handler="CCX.add_friend();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                    <ext:Button ID="friend_msg_button" runat="server"  Disabled="true" ToolTip="消息管理器" Icon="BellSilver">
                                         <Listeners>
                                           <Click Handler="CCX.msg_fri_manage()" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarSeparator />
                                    <ext:Button ID="chat_search_button" runat="server" Disabled="true" ToolTip="对话管理器" Icon="User">
                                        <Listeners>
                                            <Click Handler="chat_settle();" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:ToolBar>
                        </BottomBar>
               </ext:Window>         <%--桌面右上角的panel --%>
            </Content>
            <Modules>
                <ext:DesktopModule ModuleID="DesktopModule1" WindowID="user_information" AutoRun="false">
                    <Launcher ID="Launcher1" runat="server" Text="个人信息设置" Icon="Add">
                    </Launcher>
                    
                </ext:DesktopModule>
                
            </Modules>                              <%--点击开始后的几个图标 决定三个窗口的运行情况 --%>
            
            <Shortcuts>
                <ext:DesktopShortcut ModuleID="DesktopModule1" Text="个人信息设置" IconCls="shortcut-icon icon-user48">
                </ext:DesktopShortcut>
            </Shortcuts>                         <%--桌面小图标 --%>
            
            
            <StartMenu Width="400" Height="400" ToolsWidth="227" Title="Start Menu">
                <ToolItems>
                    <ext:MenuItem Text="设置" Icon="Wrench">
                        <Listeners>
                            <Click Handler="Ext.Msg.alert('Message', 'Settings Clicked');" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="退出登录" Icon="Disconnect">
                        <DirectEvents>
                            <Click OnEvent="Logout_Click">
                                <EventMask ShowMask="true" Msg="Good Bye..." MinDelay="1000" />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>                 
                    <ext:MenuSeparator />                 
                    
                </ToolItems>                  <%--setting项和logout项 --%>
                
                <Items>
                    <ext:MenuItem ID="MenuItem1" runat="server" Text="All" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="Menu1" runat="server">                             
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>                          <%--文件夹下的几项 --%>
            </StartMenu>
        </ext:Desktop>
        
        <ext:DesktopWindow 
            ID="user_information" 
            runat="server" 
            Title="个人信息" 
            InitCenter="false"
            Icon="User" 
            Padding="5"
            Width="300"
            Height="200"
            PageX="100" 
            PageY="25"
            Layout="Form" AutoDestroy="False" Disabled="False">
            <Items>           
                <ext:NumberField ID="txtid" runat="server" FieldLabel="用户号"   AnchorHorizontal="100%" LabelWidth="10" ReadOnly="true" />
                <ext:TextField ID="txtname" runat="server" FieldLabel="用户名"  AnchorHorizontal="100%" ReadOnly="true"/>
                <ext:TextField ID="txtsex" runat="server" FieldLabel="用户性别"  AnchorHorizontal="100%" ReadOnly="true" />
                <ext:TextField ID="txtpassword" runat="server" FieldLabel="用户密码" AnchorHorizontal="100%" ReadOnly="true"/>            
            </Items>
            <Buttons>
                <ext:Button ID="Search_Customer" runat="server" Text="确定">
                    <Listeners>
                        <Click Handler="CCX.SearchCustomer( );" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Change_Customer" runat="server" Text="修改">
                    <Listeners>
                        <Click Handler="CCX.ChangeCustomer();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:DesktopWindow>
        
        <ext:Window
         ID="updateuser_window" 
            runat="server" 
            Title="修改个人信息表" 
            Width="300" 
            Height="210"
            Padding="10" 
            Resizable="false" 
            Closable="false"
            Layout="Fit" Hidden="True">
            <Items>
                <ext:FormPanel 
                    ID="FormPanel1" 
                    runat="server" 
                    Border="false" 
                    BodyStyle="background-color:transparent;"
                    Layout="Form">
                    <Items>
                        <ext:TextField 
                            ID="update_username" 
                            runat="server" 
                            AllowBlank="false"
                            FieldLabel="用户名" 
                            AnchorHorizontal="100%"
                            />
                            <ext:RadioGroup ID="RadioGroup1" runat="server"  FieldLabel="性别" AnchorHorizontal="100%" >
                                  <Items>
                                        <ext:Radio ID="G1R1" runat="server" BoxLabel="男" Checked="true" />
                                        <ext:Radio ID="G1R2" runat="server" BoxLabel="女"  />
                                  </Items>
                            </ext:RadioGroup> 
                         <ext:TextField 
                            ID="update_userpassword" 
                            runat="server"                    
                            FieldLabel="密码"
                            InputType="Password"
                            AnchorHorizontal="100%">                            
                        </ext:TextField>
                        <ext:TextField ID="confirm_updatepassword" 
                            runat="server"                     
                            Vtype="password"
                            FieldLabel="确认密码"
                            InputType="Password"
                            AnchorHorizontal="100%">     
                            <CustomConfig>
                                <ext:ConfigItem Name="initialPassField" Value="#{PasswordField}" Mode="Value" />
                            </CustomConfig>                      
                        </ext:TextField>  
                   </Items>
                </ext:FormPanel>
               </Items>
                <BottomBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:Button ID="updateuser_save" runat="server" Text="发送" Icon="Disk">
                                <Listeners>
                                    <Click Handler="CCX.Update_user();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="updateuser_cancel" runat="server" Text="取消" Icon="Decline">
                                <Listeners>
                                    <Click Handler="CCX.update_cancel();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar> 
                </BottomBar>
        </ext:Window>    
            
       <ext:Window ID="chat_window" 
            runat="server"
            Title="聊天框"
            Icon="Application"
            
            Width="600"
            Height="500"
            Border="false"
             Hidden="true"
             Collapsible="true">
             <Items>
                <ext:BorderLayout runat="server" ID="chat_BorderLayout">    
                <West>
                        <ext:MenuPanel ID="MenuPanel1" Hidden="true" runat="server" Width="200">
                            <Menu id="Menu2" runat="server">
                                <Items>
                                    <ext:MenuItem ID="idClt_item" runat="server" Text="Ext.Net">
                                    
                                    </ext:MenuItem>
                             
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                    </West>          
                   <Center>
                        <ext:TabPanel ID="chat_room_tab" 
                                       IDMode="Explicit" 
                                       runat="server" 
                                       ResizeTabs="true"
                                       EnableTabScroll="true"
                                       >
                            <Items>
                                <ext:Panel runat="server" ID="chat_room" Title="聊天大厅">

                                </ext:Panel>
                            </Items>
                         </ext:TabPanel>
                        
                   </Center>
                     <South  MinHeight="150" CollapseMode="Mini"  Split="true">
                     <ext:Panel 
                            runat="server"                             
                            Height="150" 
                            CtCls="south-panel"
                            Layout="fit"
                            Border="false"
                            >
                            <Items>
                                    <ext:TextArea ID="content" runat="server" LabelPad="0" Margins="0" >
                                    </ext:TextArea>             
                           </Items>
                           <Buttons>
                                <ext:Button ID="send_button" runat="server" Text="发送" Icon="Disk">
                                    <DirectEvents>
                                        <Click OnEvent="send_msg">
                                            <ExtraParams >
                                                <ext:Parameter Name="send_txt" Value="content.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="Index_ID" Value="getIndexId()" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>  
                                <ext:Button ID="cancel_button" runat="server" Text="取消" Icon="Decline">
                                    <Listeners>
                                        <Click Handler="CCX.cancel_msg();" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>                        
                      </ext:Panel>
                    </South>

                </ext:BorderLayout>
             </Items>
             </ext:Window>
        <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task 
                    TaskID="TaskManager1"
                    Interval="10000"
                    OnStart=""
                    OnStop="">
                    <DirectEvents >
                        <Update OnEvent="RecvMessage" Failure="">
                        
                        </Update>
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
        
            <!--好友管理模块-->
           <ext:Window ID="add_friend_window"
                    runat="server"
                     Height="150"
                     Width="300"
                     BodyStyle="background-color: #fff;" 
                     Padding="5"
			Resizable="false"
                     Collapsible="true" 
                     Modal="true"
                     Hidden="true"
                     con="Application"
                      Layout="Fit"
                     Title="添加好友">
                   <Items>
                    <ext:FormPanel ID="FormPanel2"
                        runat="server"
                        Frame="true" 
                        
                        Padding="5" AnchorHorizontal="100%"
                       >
                        <Items>
                            <ext:NumberField ID="add_friend_num" AnchorHorizontal="95%" runat="server" FieldLabel="用户号" AllowBlank="False"></ext:NumberField>
                            <ext:TextField ID="add_friend_name" AnchorHorizontal="95%" runat="server" FieldLabel="用户名" ></ext:TextField>
                        </Items>
                        <Buttons>
                            <ext:Button ID="add_button" runat="server" Text="添加">
                                <Listeners>
                                    <Click Handler="CCX.add();" />
                                </Listeners>
                            </ext:Button>
                        </Buttons>
                    </ext:FormPanel>
                 </Items>
            </ext:Window>
            
            <ext:TextField ID="req_friend_id" runat="server" Hidden="true"></ext:TextField>
            <ext:TextField ID="req_friend_name" runat="server" Hidden="true"></ext:TextField>
            <ext:TextField ID="req_friend_sex" runat="server" Hidden="true"></ext:TextField>
            <ext:TextField ID="req_msg_type" runat="server" Hidden="true"></ext:TextField>
            <ext:NumberField ID="send_msg_fri_id" runat="server" Hidden="true"></ext:NumberField>
            <ext:TextField ID="send_msg_fri_name" runat="server" Hidden="true"></ext:TextField>
            
            <ext:Window ID="choose_fri_group_win" Title="选择组别" Height="150" Width="150" runat="server" Frame="true" Hidden="true">
                <Items>
                    <ext:ComboBox ID="ComboBox3" runat="server" AnchorHorizontal="98%" FieldLabel="好友组别" LabelWidth="70" SelectedIndex="0">
                        <Items>
                            <ext:ListItem Text="家人" Value="家人"  />
                            <ext:ListItem Text="同学" Value="同学" />
                            <ext:ListItem Text="其它" Value="其它" />             
                        </Items>
                    </ext:ComboBox>
                </Items>
                <Buttons>
                    <ext:Button ID="button1" runat="server" Text="确定">
                        <Listeners>
                            <Click Handler="CCX.choose_fri_group();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            
            <ext:Window ID="confirm_choose_fri_group_win" Title="选择组别" Resizable="false" Height="135" Width="235" runat="server" Padding="8" Frame="true" Hidden="true">
                <Items>
                    <ext:ComboBox ID="ComboBox1" runat="server" AnchorHorizontal="98%" FieldLabel="好友组别" LabelWidth="70" SelectedIndex="0">
                        <Items>
                            <ext:ListItem Text="家人" Value="家人"  />
                            <ext:ListItem Text="同学" Value="同学" />
                            <ext:ListItem Text="其它" Value="其它" />             
                        </Items>
                    </ext:ComboBox>
                </Items>
                <Buttons>
                    <ext:Button ID="button3" runat="server" Text="确定">
                        <Listeners>
                            <Click Handler="CCX.confirm_choose_fri_group();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
             <ext:Window ID="update_fri_window" Title="修改好友" Resizable="false" Height="135" Width="235" Padding="8" runat="server" Frame="true" Hidden="true">
                <Items>
                <ext:ToolbarSeparator />
                    <ext:TextField ID="update_fri_name" AnchorHorizontal="98%" FieldLabel="好友备注名" LabelWidth="70"  AllowBlank="false" runat="server">
                    </ext:TextField>
                    <ext:ComboBox ID="ComboBox2" runat="server" AnchorHorizontal="98%" FieldLabel="好友组别" LabelWidth="70" SelectedIndex="0">
                        <Items>
                            <ext:ListItem Text="家人" Value="家人"  />
                            <ext:ListItem Text="同学" Value="同学" />
                            <ext:ListItem Text="其它" Value="其它" />             
                        </Items>
                    </ext:ComboBox>
                </Items>
                <Buttons >
                    <ext:Button ID="button2" runat="server" Text="确定">
                        <Listeners>
                            <Click Handler="CCX.update_fri_data();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            
            <ext:TextField ID="GetFriendID" runat="server" Hidden="true"></ext:TextField>
  
            <ext:TextField ID="desk_id" runat="server" Hidden="true"/>
            <ext:TextField ID="desk_sex" runat="server" Hidden="true"/>
            <ext:TextField ID="desk_username" runat="server" Hidden="true"/>
            <ext:TextField ID="desk_userpassword" runat="server" Hidden="true"/>
            <ext:TextField ID="friend_chat_id" runat="server" Hidden="true"/>
            <ext:TextField ID="friend_chat_name" runat="server" Hidden="true"/>
            <ext:TextField ID="msg_fri_count" runat="server" Hidden="true"/>
   </form> 
    </body>
</html>
