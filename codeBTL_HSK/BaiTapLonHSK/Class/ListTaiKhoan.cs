using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLonHSK.Class
{
    class ListTaiKhoan
    {
        public ListTaiKhoan()
        { }
        public SqlConnection getConnect()
        {
            String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            return new SqlConnection(constr);
        }
        SqlDataReader dataReader;
        public List<getUser> getUsers(string sql)
        {

            List<getUser> getusers = new List<getUser>();
            SqlConnection conn = getConnect();
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    getusers.Add(new getUser(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(3)));
                    //dataReader.GetString(0), dataReader.GetString(1))
                }
                conn.Close();
            }
            return getusers;

        }

        public void ThemTK (string sql)
        {
            SqlConnection conn = getConnect();
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
