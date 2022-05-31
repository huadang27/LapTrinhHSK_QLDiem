using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLonHSK.Class
{
    class Data
    {
        public SqlConnection getConnect()
        {
            String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            return new SqlConnection(constr);
        }
        public DataTable getTable(String sql)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        SqlDataReader dataReader;
        public List<getUser> getUsers(string sql)
        {
            List<getUser> getusers= new List<getUser>();
            SqlConnection conn = getConnect();
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    getusers.Add(new getUser(dataReader.GetString(0), dataReader.GetString(1),dataReader.GetString(3)));
                    //dataReader.GetString(0), dataReader.GetString(1))
                }    
                conn.Close();
            }
            return getusers;

        }
        public void ExcuteNonQuery(String sql)
        {
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Clone();
            conn.Close();
        }
        public String ExcuteScalar(String sql)
        {
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            String kq = cmd.ExecuteScalar().ToString();
            conn.Close();
            return kq;
        }
    }
}

