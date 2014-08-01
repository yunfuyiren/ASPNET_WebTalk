using System;
using System.Data;

namespace Talk_Web.DataBase
{
    public class ObjectData
    {
        public string TableName;
        protected int ID;
        public string[] keyList;
        protected string filter;
        protected string values;

        public ObjectData()
        {
            keyList = null;
            filter = null;
            values = null;
        }

        protected void AddFilter(string key, string value)
        {
            if (value == "" || value == null)
                return;
            if (filter == "" || filter == null)
            {
                filter = String.Format("{0}='{1}'", key, value);
                // filter = String.Format("{0}.{1}='{2}' ", TableName, key, value);
            }
            else
            {
                filter += String.Format(" and {0}='{1}'", key, value);
                //filter += String.Format("and {0}.{1}='{2}' ", TableName, key, value);
            }
        }

        protected void AddFilter(string key, int value)
        {
            //if (value == "" || value == null)
            //    return;
            if (filter == "" || filter == null)
            {
                filter = String.Format("{0}={1}", key, value);
                // filter = String.Format("{0}.{1}='{2}' ", TableName, key, value);
            }
            else
            {
                filter += String.Format(" and {0}={1}", key, value);
                //filter += String.Format("and {0}.{1}='{2}' ", TableName, key, value);
            }
        }

        protected void AddValue(string key, string value)
        {
            if (value == "" || value == null)
                return;
            if (values == "" || values == null)
            {
                values = String.Format("{0}='{1}'", key, value);
            }
            else
            {
                values += String.Format(" ,{0}='{1}'", key, value);
            }
        }

        protected void AddValue(string key, int value)
        {
            //if (value == "" || value == null)
            //    return;
            if (values == "" || values == null)
            {
                values = String.Format("{0}={1}", key, value);
            }
            else
            {
                values += String.Format(" ,{0}={1}", key, value);
            }
        }
        protected bool DeleteItem()
        {
            string sql;
            if (filter == null || filter == "")
            {
                sql = "delete from " + TableName;

            }else
                sql = String.Format("delete from {0} where {1}", TableName, filter);
            DataBase db = new DataBase(TableName);
            if (db.Execute(sql) > 0)
                return true;
            else
                return false;
        }

        protected bool InsertItem(params object[] args)
        {
            if (filter != null && filter != "")
            {
                
                int temp=GetCount();
                if (temp > 0)
                {
                    return false;
                }
            }
            String sql = String.Format("Insert Into {0} Values (", TableName);
            foreach (object obj in args)
            {
                if (obj.GetType() == typeof(int))
                    sql += Convert.ToInt32(obj)+ ",";
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

       

        protected void SelectItem(DataSet ds)
        {
            string sql;
            if (filter == null || filter == "")
                sql = "select * from " + TableName;
            else
                sql = String.Format("select * from {0} where {1}", TableName, filter);
            DataBase db = new DataBase(TableName);
            db.GetDataSet(ds, TableName, sql);
        }

        public int GetCount()
        {
            string sql;
            if (filter == null || filter == "")
                sql = "select count(*) from " + TableName;
            else
                sql = String.Format("select count(*) from {0} where {1}",TableName,filter);

            DataBase db = new DataBase(TableName);
             
            Object []val=db.GetDataReader(sql);
            int temp = Convert.ToInt32(val[0]);
            return temp;
        }

        public int GetCount(String sql)
        {
            DataBase db = new DataBase(TableName);

            Object[] val = db.GetDataReader(sql);
            int temp = Convert.ToInt32(val[0]);
            return temp;
        }

        public Object[] GetFirstRecord()
        {
            string sql;
            if (filter == null || filter == "")
                sql = "select count(*) from " + TableName;
            else
                sql = String.Format("select count(*) from {0} where {1}", TableName, filter);

            DataBase db = new DataBase(TableName);

            Object[] val = db.GetDataReader(sql);
            return val;
        }

        public Object[] GetFirstRecord(String sql)
        {
            DataBase db = new DataBase(TableName);

            Object[] val = db.GetDataReader(sql);
            return val;
        }
        protected bool UpdateItem()
        {
            String sql = String.Format("Update {0} set {1} where {2}", TableName, values, filter);
            DataBase db = new DataBase(TableName);
            if (db.Execute(sql) > 0)
                return true;
            else
                return false;
        }
    }
}