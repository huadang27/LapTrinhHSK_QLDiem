using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLonHSK.Class
{
    class Login
    {
       getUser U = new getUser();
        Data da = new Data();
        public DataTable getUser(String condition)
        {
            DataTable dt = null;
            String sql = "Select * from TaiKhoan where " + condition;
            dt = da.getTable(sql);
            return dt;
        }

    }
}
