using System;
using System.Data;
using Ext.Net;
using Talk_Web.DataBase;
namespace Talk_Web
{
    public partial class friend_list : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                TreeNodeLoad();

            }
        }


        [DirectMethod]
        public void TreeNodeLoad()                                   //好友列表刷新，动态添加结点
        {
            TreeNode root = new TreeNode { NodeID = "root", Expanded = true };
            friend_treepanel.Root.Add(root);
            TreeNode node1 = new TreeNode { Expanded = true, NodeID = "familytree", Text = "家人", IconFile = "1.png" };
            root.Nodes.Add(node1);

            TreeNode node2 = new TreeNode { Expanded = true, NodeID = "classmatetree", Text = "同学", IconFile = "1.png" };
            root.Nodes.Add(node2);

            TreeNode node3 = new TreeNode { Expanded = true, NodeID = "otherstree", Text = "其它", IconFile = "1.png" };
            root.Nodes.Add(node3);
            int srcfriend_num = int.Parse(Session["id"].ToString());

            myfriend fri = new myfriend(srcfriend_num);
            using (DataSet ds = new DataSet())
            {
                fri.FindFriend(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                    if (dr[3].ToString().Trim() == "家人")
                    {
                        TreeNode node_t = new TreeNode { NodeID = dr[1].ToString(), Text = dr[2].ToString(), Leaf = true };
                        if (dr[4].ToString().Trim() == "男")
                            node_t.IconFile = "2.png";
                        else
                            node_t.IconFile = "3.png";
                        node1.Nodes.Add(node_t);
                    }
                    else
                        if (dr[3].ToString().Trim() == "同学")
                        {
                            TreeNode node_t = new TreeNode { NodeID = dr[1].ToString(), Text = dr[2].ToString(), Leaf = true };
                            if (dr[4].ToString().Trim() == "男")
                                node_t.IconFile = "2.png";
                            else
                                node_t.IconFile = "3.png";
                            node2.Nodes.Add(node_t);
                        }
                        else
                        {
                            TreeNode node_t = new TreeNode { NodeID = dr[1].ToString(), Text = dr[2].ToString(), Leaf = true };
                            if (dr[4].ToString().Trim() == "男")
                                node_t.IconFile = "2.png";
                            else
                                node_t.IconFile = "3.png";
                            node3.Nodes.Add(node_t);
                        }
            }
            // this.PlaceHolder1.Controls.Add(tree);

        }
    }
}
