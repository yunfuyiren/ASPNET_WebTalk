using System;
using System.Data;
using System.Web.UI;
using Ext.Net;
using Talk_Web.DataBase;
using System.Collections.Generic;

namespace Talk_Web
{
 /*   class friend_list
    {
        public int fri_id;
        public int fri_time;
        public in
    }*/
    public partial class desktop : Page
    {

        //public static int msg_tlk_count;                //对话消息数
       // static List<>
        public static List<int> friend_tabpanel = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest&& Session["id"].ToString() != "")
            {
                friend_chat_id.Text = "0";
                friend_chat_name.Text = "";
                msg_fri_count.Text = "0";
                desk_id.Text = "";
                desk_sex.Text = "";
                desk_username.Text = "";
                desk_userpassword.Text = "";
                if (!IsPostBack && Session["id"].ToString() != "")
                {
                    // Usr user = new Usr(int.Parse(this.Session["id"].ToString()));
                    desk_id.Text = Session["id"].ToString();
                    desk_username.Text = Session["username"].ToString();
                    desk_sex.Text = Session["sex"].ToString();
                    desk_userpassword.Text = Session["password"].ToString();
                    txtid.Text = desk_id.Text;
                    txtname.Text = desk_username.Text;
                    txtpassword.Text = desk_userpassword.Text;
                    txtsex.Text = desk_sex.Text;
                  
                }
                else
                {
                    Response.Redirect("Default.apsx");
                }
            }
        }

        protected void Logout_Click(object sender, DirectEventArgs e)
        {
            Usr user = new Usr(Convert.ToInt32(desk_id.Text.Trim()));

            user.AddFilter(0, Convert.ToInt32(desk_id.Text.Trim()));
            user.AddValue(5, 0);

            bool i;
            try
            {
                i = user.UpdateUser();
                this.Session.RemoveAll();
            }
            catch (Exception a)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "注销失败！",
                    Message = a.Message,
                    Icon = MessageBox.Icon.INFO,
                    Buttons = MessageBox.Button.OK
                });
            }
            Response.Redirect("Default.aspx");
        }

        //*********************个人信息模块
        //&&&&&&&&&&&&&&&/个人信息设置模块隐藏
        [DirectMethod]
        public void SearchCustomer()
        {
            user_information.Hide();
            //  return customer;
        }

        //&&&&&&&&&&&&&&&修改个人信息模块
        [DirectMethod]
        public void ChangeCustomer()
        {
            updateuser_window.Show();
        }

        [DirectMethod]
        public void Update_user()
        {
            Usr user = new Usr(Convert.ToInt32(desk_id.Text.Trim()));
            desk_username.Text = update_username.Text;
            desk_userpassword.Text = update_userpassword.Text;
            desk_sex.Text = RadioGroup1.CheckedItems[0].BoxLabel;
            user.AddFilter(0, Convert.ToInt32(desk_id.Text.Trim()));
            user.AddValue(1, desk_username.Text);
            user.AddValue(2, desk_userpassword.Text);
            user.AddValue(3, desk_sex.Text);
            bool i;
            try
            {
                i = user.UpdateUser();
            }
            catch (Exception a)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "错误提示",
                    Message = a.Message,
                    Icon = MessageBox.Icon.INFO,
                    Buttons = MessageBox.Button.OK
                });
            }
            X.Msg.Notify("提示", "修改成功！").Show();
            txtname.Text = update_username.Text;
            txtpassword.Text = update_userpassword.Text;
            txtsex.Text = RadioGroup1.CheckedItems[0].BoxLabel;
            updateuser_window.Hide();

            //Response.Redirect("Default.aspx");
        }

        [DirectMethod]
        public void update_cancel()
        {
            updateuser_window.Hide();
            update_username.Text = "";
            update_userpassword.Text = "";
            confirm_updatepassword.Text = "";
            G1R1.Checked = true;
        }

        //*************************************
        //&&&&&&&&&&&&&&&&&&&&&&&聊天模块

        [DirectMethod]
        public void show_win(string ss,string bb)
        {
            friend_chat_id.Text = ss.Trim();
            friend_chat_name.Text = bb;
            chat_window.Show();
        }

        protected void send_msg(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["send_txt"] == null)
                return;
            if (e.ExtraParams["Index_ID"] == "chat_room")
                return;
            Message msg = new Message(Convert.ToInt32(desk_id.Text.Trim()), Convert.ToInt32(e.ExtraParams["Index_ID"].Trim()));
            MESSAGE send_msg = new MESSAGE { src_userid = Convert.ToInt32(desk_id.Text.Trim()), des_userid = Convert.ToInt32(e.ExtraParams["Index_ID"].Trim()), msg_isseen = 0, msg_text = e.ExtraParams["send_txt"], msg_time = DateTime.Now.ToString(), type = 0 };
            bool res=msg.SendMessage(send_msg);
            content.SetValue("");
            if (!res)
            {
                X.Msg.Notify("ERROR", "消息发送失败！").Show();
            }
           
        }

        [DirectMethod]
        public void cancel_msg()
        {
            content.Text = "";
            chat_window.Hide();
        }

        public void RecvMessage(object sender, DirectEventArgs e)
        {
            bool y = true;
            bool z = true;
            int temp;
            String temp1 = req_friend_id.Text + req_msg_type.Text;
            msg_fri_count.Text = "0";
            String temp2 = send_msg_fri_id.Text;
            Message MsgCtl = new Message(Convert.ToInt32(desk_id.Text.Trim()), 0);
            using (DataSet ds = new DataSet())
            {
                MsgCtl.RecvAllAlert(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (int.Parse(dr[5].ToString()) == 3)   //好友申请消息
                        z = false;
                    if (int.Parse(dr[5].ToString()) == 4)   //好友确认消息
                        z = false;
                    temp = Convert.ToInt32(msg_fri_count.Text.Trim());
                    temp++;
                    msg_fri_count.Text = temp.ToString();
                    if (temp != 0)
                        friend_msg_button.Text = String.Format("({0})", temp);
                    string[] ss = dr[4].ToString().Split('$');
                    req_friend_name.Text = ss[0];
                    if(ss.Length>1)
                        req_friend_sex.Text = ss[1];
                    req_friend_id.Text = dr[0].ToString();
                    req_msg_type.Text = dr[5].ToString();
                }
            }

            if (!z && temp1 != req_friend_id.Text + req_msg_type.Text)
            {
                friend_msg_button.Disabled = false;
                X.Msg.Notify("提示", "好友信息待处理").Show();
            }
            ////////////////////////////////////////////////////////////////////////chat_room_tab.Items[0].Id
            using (DataSet ds2 = new DataSet())
            {
                MsgCtl.RecvAllMsg(ds2);
                foreach (DataRow dr1 in ds2.Tables[0].Rows)
                {
                    foreach (Panel tpnl in chat_room_tab.Items)
                        if (tpnl.ID == (dr1[0].ToString()))
                            continue;
                    y = false;
                    // msg_tlk_count++;             
                    //if(msg_tlk_count!=0)
                    // chat_search_button.Text = "("+msg_tlk_count+")";
                    send_msg_fri_id.Text = dr1[0].ToString();
                    //////////////以下部分是为获得聊天好友名而调用数据库
                    myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
                    using (DataSet ds3 = new DataSet())
                    {
                        fri.FindFriend(ds3, Convert.ToInt32(send_msg_fri_id.Text.Trim()));
                        foreach (DataRow dr2 in ds3.Tables[0].Rows)
                            send_msg_fri_name.Text = dr2[2].ToString();
                    }

                    //MsgCtl.AddFilter(0, int.Parse(dr1[0].ToString()));
                    //MsgCtl.AddFilter(1, int.Parse(dr1[1].ToString()));
                    //MsgCtl.AddFilter(2, dr1[2].ToString());
                    //MsgCtl.AddValue(3, 1);
                    //MsgCtl.UpdateMessage();
                }
            }
            if (!y && temp2 != send_msg_fri_id.Text)
            {
                chat_search_button.Disabled = false;
                X.Msg.Notify("提示", "有新消息").Show();
            }

        }

        //////////////////////////////////////////////////////////////好友添加申请模块
        [DirectMethod]
        public void add_friend()
        {
            add_friend_window.Show();
        }
        [DirectMethod]
        public void add()
        {

            myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
            if (fri.RequestFriend(int.Parse(add_friend_num.Text), desk_username.Text, desk_sex.Text))
                X.Msg.Notify("提示", "好友申请已发出").Show();

            else
                X.Msg.Notify("提示", "好友申请发送失败").Show();
            add_friend_window.Hide();
            add_friend_name.Text = "";
            add_friend_num.Text = "";

        }

        //////////////////////////////////////////////////////////好友添加确认模块
        [DirectMethod]
        public void DoConfirm()
        {
            int temp = Convert.ToInt32(msg_fri_count.Text.Trim());
            temp--;
            msg_fri_count.Text = temp.ToString();

            if (temp <= 0)
            {
                friend_msg_button.Text = "";
                friend_msg_button.Disabled = true;
            }
            else
                friend_msg_button.Text = String.Format("({0})", temp);
            string ss = string.Format("{0}向您提出好友申请，是否同意?", req_friend_name.Text);
            X.Msg.Confirm("提示", ss, new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "CCX.DoYes()",
                    Text = "是"
                },
                No = new MessageBoxButtonConfig
                {
                    Handler = "CCX.DoNo()",
                    Text = "否"
                }
            }).Show();
        }

        [DirectMethod]
        public void DoYes()
        {
            choose_fri_group_win.Show();
            
           // ClientScript.RegisterStartupScript(typeof(string), "js", "update_tree()", true);
        }

        [DirectMethod]
        public static void DoNo()
        {
            X.Msg.Notify("提示", "您已拒绝好友申请").Show();
        }

        [DirectMethod]
        public void choose_fri_group()
        {
            myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
            fri.AddFilter(0, Convert.ToInt32(desk_id.Text.Trim()));
            fri.AddFilter(1, req_friend_id.Text);
            if (fri.AddFriend(Convert.ToInt32(desk_id.Text.Trim()), int.Parse(req_friend_id.Text), req_friend_name.Text, ComboBox3.SelectedItem.Value, req_friend_sex.Text))
            {
                X.Msg.Notify("提示", "增加好友成功").Show();
                fri.AgreeFriend(int.Parse(req_friend_id.Text), desk_username.Text, desk_sex.Text);
                friend_win.Reload();
                
            }
            else
                X.Msg.Notify("提示", "增加好友失败或好友已经存在").Show();
            Message MsgCtl = new Message(0,0);
            MsgCtl.AddFilter(0, int.Parse(req_friend_id.Text));
            MsgCtl.AddFilter(1, int.Parse(desk_id.Text.Trim()));
            MsgCtl.AddFilter(5, 3);
            MsgCtl.AddValue(3, 1);
            MsgCtl.DelMessage();
            ComboBox3.SelectedIndex=0;
            req_friend_id.Text = "";
            req_friend_sex.Text = "";
            req_friend_name.Text = "";
            req_msg_type.Text = "";
            choose_fri_group_win.Hide();
        }


        //////////////////////////////////////////////////好友删除模块
        [DirectMethod]
        public static void del_friend(string aa, string bb)
        {
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(aa), "aa is null or empty.");
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(bb), "bb is null or empty.");

            X.Msg.Confirm("提示", "确定删除好友" + bb, new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    Handler = String.Format("CCX.del_friend_data({0})", aa),
                    Text = "是"
                },
                No = new MessageBoxButtonConfig
                {
                    Text = "否"
                }
            }).Show();
        }

        [DirectMethod]
        public void del_friend_data(string ss)
        {
            myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
            fri.DeleteFriend(int.Parse(ss));
            friend_win.Reload();
        }

        ///////////////////////////////////////////////////////////////////////修改好友
        [DirectMethod]
        public void update_friend(string aa, string bb)
        {
            GetFriendID.Text = aa;
            update_fri_window.Show();
        }
        [DirectMethod]
        public void update_fri_data()
        {
            myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
            fri.AddFilter(1, int.Parse(GetFriendID.Text));
            fri.AddValue(2, update_fri_name.Text);
            fri.AddValue(3, ComboBox2.SelectedItem.Value);
            if (fri.UpdateFriend())
            {
                X.Msg.Notify("提示", "修改好友成功").Show();
                friend_win.Reload();
            }
            else
                X.Msg.Notify("提示", "修改好友失败").Show();
            update_fri_window.Hide();
            update_fri_name.Text = "";
            ComboBox2.SelectedIndex = 0;    
        }

        ///////////////////////////////好友消息提醒函数

        [DirectMethod]
        public void msg_fri_manage()
        {
            
            int temp = Convert.ToInt32(msg_fri_count.Text.Trim());
            if (temp == 0)
            {
                X.Msg.Notify("提示", "暂时无消息").Show();
                return;
            }
            if(int.Parse(req_msg_type.Text)==3)
                DoConfirm();
            if (int.Parse(req_msg_type.Text)== 4)
                Do_request_confirm();
        }

        public void Do_request_confirm()
        {
            int temp = Convert.ToInt32(msg_fri_count.Text.Trim());
            temp--;
            msg_fri_count.Text = temp.ToString();
            friend_msg_button.Disabled = true;
            if (temp <= 0)
                friend_msg_button.Text = "";
            else
                friend_msg_button.Text = String.Format("({0})", temp);
            X.Msg.Alert("提示", String.Format("收到来自{0}的好友确认，同意添加您为好友", req_friend_name.Text), "confirm_choose_fri_group_win.show();").Show();
            
            
        }

        [DirectMethod]
        public void confirm_choose_fri_group()
        {
            myfriend fri = new myfriend(Convert.ToInt32(desk_id.Text.Trim()));
            fri.AddFilter(0, Convert.ToInt32(desk_id.Text.Trim()));
            fri.AddFilter(1, req_friend_id.Text);
            if (fri.AddFriend(Convert.ToInt32(desk_id.Text.Trim()), int.Parse(req_friend_id.Text), req_friend_name.Text, ComboBox1.SelectedItem.Value, req_friend_sex.Text))
            {
                X.Msg.Notify("提示", "增加好友成功").Show();
                friend_win.Reload();
            }
            else
                X.Msg.Notify("提示", "增加好友失败或好友已经存在").Show();

            Message MsgCtl = new Message(0,0);
            MsgCtl.AddFilter(0, int.Parse(req_friend_id.Text));
            MsgCtl.AddFilter(1, int.Parse(desk_id.Text.Trim()));
            MsgCtl.AddFilter(5, 4);
            MsgCtl.AddValue(3, 1);
            MsgCtl.DelMessage();
            ComboBox3.SelectedIndex = 0; ;
            req_friend_id.Text = "";
            req_friend_sex.Text = "";
            req_friend_name.Text = "";
            req_msg_type.Text = "";
            confirm_choose_fri_group_win.Hide();
        }
        ////////////////////////////////////对话消息提醒函数
    }
}