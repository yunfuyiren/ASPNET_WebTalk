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
using System.Data.SqlClient;
using Talk_Web.DataBase;
namespace Talk_Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            bool i;
            Usr user = new Usr(int.Parse(txtUsername.Text));
             i=user.UserValid(int.Parse(txtUsername.Text), txtPassword.Text);
             if (txtUsername.Text == "" || txtPassword.Text == "")
             {
                   Response.Write("<script>alert('验证不能为空！')</script>");
              }
          
             else if (i==true)
             {
                 this.Session["id"] = txtUsername.Text;
                 this.Session["username"] = user._username;
                 this.Session["sex"] = user._sex;
                 this.Session["password"] = txtPassword.Text;
                 //  this.Session["sex"] = sm["sex"].ToString();
                   //this.Session["isadmin"] = sm["isadmin"].ToString();
                   /*     sm.Close();         //关闭SqlDataReader，只读数据库
                        con.Close();  //关闭数据库连接*/
                   Response.Redirect("Desktop.aspx");
              }
             else             
            {
               X.Msg.Notify("提示", "密码错误或您还未注册，请注册").Show();
            }

            // Then user send to application
        }

        [DirectMethod]
        public void Button2_Click()
        {
            Response.Redirect("register.aspx");
        }
    }
}
