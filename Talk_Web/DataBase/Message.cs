using System;
using System.Data;

namespace Talk_Web.DataBase
{
    public class Message : ObjectData
    {
        private readonly DataSet ds;
        private int pos;
        private readonly string msgSql;
        private readonly int friend_Id;
        public Message(int id, int friend_id)
        {
            msgSql = String.Format("select * from message_table where msg_isseen=0 and src_userid={0} and des_userid={1} and type=0", friend_id, id);
            pos = 0;
            ds = new DataSet();
            ID = id;
            friend_Id = friend_id;
            keyList = new String[6];
            keyList[0] = "src_userid";
            keyList[1] = "des_userid";
            keyList[2] = "msg_time";
            keyList[3] = "msg_isseen";
            keyList[4] = "msg_text";
            keyList[5] = "type";
            TableName = "message_table";
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

        public MESSAGE RecvMessage()
        {
            MESSAGE msg = new MESSAGE { msg_isseen = 0, des_userid = 0, msg_text = "", src_userid = 0, type = 0, msg_time = "" };
            if (ds.Tables.Count == 0 ||ds.Tables[0].Rows.Count==0)
            {
                DataBase db = new DataBase(TableName);

                db.GetDataSet(ds, TableName, msgSql);
                pos = 0;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Clear();
                msg.type = -1;
                return msg;
            }

            msg.src_userid = Convert.ToInt32(ds.Tables[0].Rows[pos][0]);
            msg.des_userid = Convert.ToInt32(ds.Tables[0].Rows[pos][1]);
            msg.msg_time = Convert.ToString(ds.Tables[0].Rows[pos][2]);
            msg.msg_isseen = 0;
            msg.msg_text = Convert.ToString(ds.Tables[0].Rows[pos][4]);
            msg.type = Convert.ToInt32(ds.Tables[0].Rows[pos][5]);
            filter = "";
            values = "";
            AddFilter(0, msg.src_userid);
            AddFilter(1, msg.des_userid);
            AddFilter(2, msg.msg_time);
            AddValue(3, 1);
            AddFilter(4, msg.msg_text);
            AddFilter(5, msg.type);
            UpdateItem();
            pos++;
            if (pos >= ds.Tables[0].Rows.Count)
                ds.Clear();
            return msg;
        }

        //添加好友提醒
        public DataSet RecvAllAlert(DataSet ds)
        {
            ds.Clear();
            String sql = String.Format("select * from message_table where msg_isseen=0 and des_userid={0} and type>0", ID);
            DataBase db = new DataBase(TableName);
            db.GetDataSet(ds, TableName, sql);
            return ds;
        }

        //添加消息提醒
        public DataSet RecvAllMsg(DataSet ds)
        {
            ds.Clear();
            String sql = String.Format("select * from message_table where msg_isseen=0 and des_userid={0} and type=0", ID);
            DataBase db = new DataBase(TableName);
            db.GetDataSet(ds, TableName, sql);
            return ds;
        }

        public bool SendMessage(MESSAGE msg)
        {
            AddFilter(0, -1);
            bool temp = InsertItem(msg.src_userid, msg.des_userid, msg.msg_time, 0, msg.msg_text, msg.type);
            return temp;
        }

        protected bool Insert_my_Item(params object[] args)
        {
            //if (filter == null || filter == "")
            //{

            int temp = GetCount();
            if (temp > 0)
            {
                return false;
            }
            //}
            String sql = String.Format("Insert Into {0} Values (", TableName);
            foreach (object obj in args)
            {
                if (obj.GetType() == typeof(int))
                    sql += Convert.ToInt32(obj) + ",";
                else
                    sql += "'" + obj as string + "',";
            }
            int index = sql.Length - 1;
            sql = sql.Remove(index);
            sql += ")";
            DataBase db = new DataBase(TableName);
            if (db.Execute(sql) > 0)
                return true;
            else
                return false;
        }

        ///////////////////////修改消息
        public bool UpdateMessage()
        {
            bool temp = UpdateItem();
            return temp;
        }

        public bool DelMessage()
        {
            bool res = DeleteItem();
            return res;

        }
    }
}