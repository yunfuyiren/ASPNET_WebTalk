using System;

namespace Talk_Web.DataBase
{
    public struct MESSAGE
    {
        public int src_userid;
        public int des_userid;
        public string msg_time;
        public int msg_isseen;
        public string msg_text;
        public int type;
    }
}
