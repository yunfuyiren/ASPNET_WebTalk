using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Talk_Web.DataBase
{
    public class Usr : ObjectData
    {
        public String _username;
        public String _sex;

        public Usr(int id)
        {
            ID = id;
            keyList = new String[6];
            keyList[0] = "id";
            keyList[1] = "username";
            keyList[2] = "password";
            keyList[3] = "sex";
            keyList[4] = "isadmin";
            keyList[5] = "isonline";
            TableName = "user_table";
        }

        public void AddFilter(int ColumnIndex, string value)
        {
            AddFilter(keyList[ColumnIndex], value);
        }

        public void AddFilter(int ColumnIndex, int value)
        {
            AddFilter(keyList[ColumnIndex], value);
        }

        public void AddValue(int ColumnIndex, string value)
        {
            AddValue(keyList[ColumnIndex], value);
        }

        public void AddValue(int ColumnIndex, int value)
        {
            AddValue(keyList[ColumnIndex], value);
        }

        public bool DeleteAllItem()
        {
            /*
            string sql;
            if (filter == null || filter == "")
            {
                //if (DevExpress.XtraEditors.XtraMessageBox.Show("全部删除？", "警告", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                //   return false;
                sql = "select * from " + TableName;
            }
            else
                sql = String.Format("select * from {0} where {1}", TableName, filter);
            DataBase db = new DataBase(TableName);
            using (DataSet ds = new DataSet())
            {
                db.GetDataSet(ds, TableName, sql);
            }
            //String ID;
            //CourseOpt cs = new CourseOpt();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ID = dr[0] as string;
            //    cs.AddFilter(0, ID);
            //    cs.DeleteAllItem();
            //}*/
            DeleteItem();
            return true;
        }

        public bool DelUser(int userId) 
        {
            filter = "";
            AddFilter(0, userId);
            bool temp=DeleteItem();
            
            return temp;
        }

        public bool AddUser(int userid,string username,string PasswordField,string  usersex,int isadmin ,int isonline)
        {
            AddFilter(0, userid);
            bool temp=InsertItem(userid,username, PasswordField, usersex, isadmin,isonline);
            return temp;
        }

        public bool UpdateUser()
        {
            bool temp=UpdateItem();
            return temp;
        }

        public bool UserValid(int id,string psw)
        {
            AddFilter(0, id);
            AddFilter(2, psw);
            using (DataSet ds = new DataSet())
            {
                SelectItem(ds);
                filter = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ID = id;
                    _sex = ds.Tables[0].Rows[0][3].ToString();
                    _username = ds.Tables[0].Rows[0][1].ToString();

                    AddFilter(0, id);
                    AddValue(5, 1);
                    UpdateItem();
                    ///////////////////////////////操作发送消息


                    return true;
                }
                else
                    return false;
            }
        }
    }
}