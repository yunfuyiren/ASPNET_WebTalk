using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Talk_Web
{
    /// <summary>
    /// Talk_WebSer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Talk_WebSer : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession=true)]
        public bool chat_win(string ss,string bb)
        {
            if (ss == null || ss == "familytree" || ss == "root" || ss == "classmatetree" || ss == "otherstree")
                return false;
            this.Session["Friend_ID"] = ss.ToString();
            this.Session["Friend_NAME"] = bb;
            return true;
        }
        
        
    }
}
