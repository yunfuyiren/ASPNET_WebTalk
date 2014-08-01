using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ext.Net;
using Talk_Web.DataBase;
namespace Talk_Web
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void cancel(object sender, DirectEventArgs e)
        {
            this.Response.Redirect("Default.aspx");
        }

        [DirectMethod]
        public void Add_User()
        {
            Usr user = new Usr(int.Parse(this.userid.Text));
            bool i;
            //user.AddFilter(0, int.Parse(userid.Text));
            i = user.AddUser(int.Parse(userid.Text), username.Text, PasswordField.Text,RadioGroup1.CheckedItems[0].BoxLabel, 0, 1);

            if (i == true)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "提示",
                    Message = "编辑成功!",
                    Icon = MessageBox.Icon.INFO,
                    Buttons = MessageBox.Button.OK
                });
                Response.Redirect("default.aspx");
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "提示",
                    Message = "编辑失败!该用户已存在",
                    Icon = MessageBox.Icon.INFO,
                    Buttons = MessageBox.Button.OK
                });
            }
        }
    }
}
