using System;
using System.Web.UI;
using Ext.Net;
using Talk_Web.DataBase;

namespace Talk_Web
{
    public partial class Chart : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                

                TextArea1.Text = "";
            }
        }

        protected void GetMessage(object sender, DirectEventArgs e)
        {
            int Friend_ID = Convert.ToInt32(Session["Friend_ID"]);
            String Friend_NAME = Session["Friend_NAME"].ToString();
            Message msg = new Message(Convert.ToInt32(Session["id"]), Friend_ID);
            //TextArea1.Text = DateTime.Now.ToString();
            MESSAGE recvmsg;
            while (true)
            {
                if (msg == null)
                    return;
                recvmsg=msg.RecvMessage();
                //String i = recvmsg.msg_text;
                //
                switch (recvmsg.type)
                { 
                    case 0:                     //普通消息
                        //X.Msg.Notify("提示", recvmsg.msg_text.Trim()).Show();
                        TextArea1.Text += String.Format("\n{0}\t{1}:\n {2}", Friend_NAME, recvmsg.msg_time.Trim(), recvmsg.msg_text.Trim());
                        break;
                    case 1:                     //朋友上线提示
                        break;
                    case 2:                     //朋友下线提示
                        break;
                    case 3:                     //好友请求
                        break;
                    case 4:                     //好友请求成功
                        break;
                    default:                    //其他 表示未获得消息
                        return;
                }
            }
             
        }

        
    }
}