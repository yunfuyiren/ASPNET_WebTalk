using System;
using System.Data;

namespace Talk_Web.DataBase
{
    public class myfriend:ObjectData
    {
         public myfriend(int id)
        {
            ID = id;
            keyList = new String[5];
            keyList[0] = "srcfriend_id";
            keyList[1]="friend_id";
            keyList[2] = "friend_name";
            keyList[3] = "friend_group";
            keyList[4] = "friend_sex";
           //  keyList[5]="friend_isonl"
            TableName="friend_table";
        }

        public void AddFilter(int ColumnIndex,string value)
        {
            AddFilter(keyList[ColumnIndex], value);
        }

        public void AddFilter(int ColumnIndex, int value)
        {
            AddFilter(keyList[ColumnIndex], value);
        }

        public void AddValue(int ColumnIndex, string value)
        {
            AddValue(keyList[ColumnIndex],value);
        }

        public void AddValue(int ColumnIndex, int value)
        {
            AddValue(keyList[ColumnIndex], value);
        }

        public bool AddFriend(int srcfriend_id,int friend_id,String friend_name,String friend_group,String friend_sex)
        {
            bool res=InsertItem(srcfriend_id, friend_id, friend_name,friend_group, friend_sex);
            return res;
        }

        public bool DeleteFriend(int friend_id)
        {
            filter = "";
            AddFilter(0, ID);
            AddFilter(1, friend_id);
            bool res=DeleteItem();
            return res;
        }

        public bool FindFriend(DataSet ds,int friend_id)
        {
            filter="";
            AddFilter(0, ID);
            AddFilter(1, friend_id);
            SelectItem(ds);
            return true;
        }

        public bool FindFriend(DataSet ds)
        {
            filter = "";
            AddFilter(0, ID);
            SelectItem(ds);
            return true;
        }

        public void LogOut_Msg(int friend_id)        //向好友发送离线提醒
        {
            Message msg = new Message(ID, friend_id);
            MESSAGE sendmsg = new MESSAGE { msg_isseen = 0, src_userid = ID, des_userid = friend_id, msg_text = "", msg_time = DateTime.Now.ToString(), type = 2 };
            msg.SendMessage(sendmsg);
        }

        public void LogIn_Msg(int friend_id)            //向好友发送登陆提醒
        {
            Message msg = new Message(ID, friend_id);
            MESSAGE sendmsg = new MESSAGE { msg_isseen = 0, src_userid = ID, des_userid = friend_id, msg_text = "", msg_time = DateTime.Now.ToString(), type = 1 };
            msg.SendMessage(sendmsg);
        }

        public bool RequestFriend(int friend_id,String my_name,String my_sex)        //向friend_id 申请好友
        {
            Usr user = new Usr(friend_id);
            user.AddFilter(0, friend_id);
            int res=user.GetCount();
            if (res == 0)
                return false;
            Message msg = new Message(ID, friend_id);
            MESSAGE sendmsg = new MESSAGE { msg_isseen = 0, src_userid = ID, des_userid = friend_id, msg_text = String.Format("{0}${1}", my_name, my_sex), msg_time = DateTime.Now.ToString(), type = 3 };
            ///////////////////修改
            return msg.SendMessage(sendmsg);
        }

        public bool AgreeFriend(int friend_id,String my_name,String my_sex)      //同意friend_id的好友请求
        {
            Message msg = new Message(ID, friend_id);
            MESSAGE sendmsg = new MESSAGE { msg_isseen = 0, src_userid = ID, des_userid = friend_id, msg_text = String.Format("{0}${1}", my_name, my_sex), msg_time = DateTime.Now.ToString(), type = 4 };
            msg.SendMessage(sendmsg);
            return true;
        }

        ///////////////update friend
        public bool UpdateFriend()
        {
            bool temp = UpdateItem();
            return temp;
        }
    }
}