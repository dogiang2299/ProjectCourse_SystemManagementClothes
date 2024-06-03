using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReadData
    {
        private static string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public static DataTable DocChiTietHoaDon(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
