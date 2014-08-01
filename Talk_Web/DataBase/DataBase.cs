using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace Talk_Web.DataBase
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DataBase
    {
        /// <summary>
        /// 将要连接的表名
        /// </summary>
        public string TableName;

        private MySqlConnection conn;

        private MySqlCommand cmd;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">将要连接的表名</param>
        public DataBase(string tableName)
        {
            TableName = tableName;
        }

        public DataBase()
        { 
        
        }

        private void Open()
        {
            try
            {
                const string sql = "server=127.0.0.1;user=root;password=wangyang08;database=talk_web";
                conn = new MySqlConnection(sql);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public int Execute(string sql)
        {
            Open();
            int res=0;
            try
            {
                cmd = new MySqlCommand(sql, conn);
                res=cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            Close();
            return res;
        }

        /// <summary>
        /// GetDataSet  将结果存入到ds中
        /// </summary>
        /// <param name="ds">存放结果的记录集</param>
        /// <param name="tableName">ds中的索引名</param>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public void GetDataSet(DataSet ds, string tableName,string sql)
        {
            Open();
            try
            {
                 
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                adapter.Fill(ds, tableName);
                adapter.Dispose();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            Close();
            //return ds;
        }

        public Object[] GetDataReader(string sql)
        {
            Open();
            
            try
            {

                using (MySqlCommand _cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader dr = _cmd.ExecuteReader();
                    Object[] values;
                    int x = dr.FieldCount;
                    values = new Object[x];
                    dr.Read();
                    dr.GetValues(values);
                    dr.Close();
                    Close();
                    return values;
                }
                
            }
            catch (MySqlException ex)
            {
                Close();
                return null;
            }     
        }

         public void Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}