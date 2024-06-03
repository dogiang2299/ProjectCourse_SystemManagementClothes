using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Supllier
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_Supplier> SearchSuppliers(string keyword)
        {
            List<DTO_Supplier> suppliers = new List<DTO_Supplier>();
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "SELECT manhacc, tennhacc, diachincc, email, sodienthoai FROM tbl_QuanLyNhaCungCap WHERE manhacc LIKE @keyword OR tennhacc LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Supplier supplier = new DTO_Supplier
                    {
                        manhacc = reader["manhacc"].ToString(),
                        tennhacc = reader["tennhacc"].ToString(),
                        diachincc = reader["diachincc"].ToString(),
                        email = reader["email"].ToString(),
                        sodienthoai = reader["sodienthoai"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }
        public bool InsertSupplier(DTO_Supplier supplier)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "INSERT INTO tbl_QuanLyNhaCungCap (manhacc, tennhacc, diachincc, email, sodienthoai) VALUES (@manhacc, @tennhacc, @diachincc, @Email, @Sodienthoai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhacc", supplier.manhacc);
                cmd.Parameters.AddWithValue("@tennhacc", supplier.tennhacc);
                cmd.Parameters.AddWithValue("@diachincc", supplier.diachincc);
                cmd.Parameters.AddWithValue("@Email", supplier.email);
                cmd.Parameters.AddWithValue("@Sodienthoai", supplier.sodienthoai);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdateSupplier(DTO_Supplier supplier)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "UPDATE tbl_QuanLyNhaCungCap SET tennhacc = @tennhacc, diachincc = @diachincc, email = @Email, sodienthoai = @Sodienthoai WHERE manhacc = @manhacc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhacc", supplier.manhacc);
                cmd.Parameters.AddWithValue("@tennhacc", supplier.tennhacc);
                cmd.Parameters.AddWithValue("@diachincc", supplier.diachincc);
                cmd.Parameters.AddWithValue("@Email", supplier.email);
                cmd.Parameters.AddWithValue("@Sodienthoai", supplier.sodienthoai);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteSupplier(string manhacc)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "DELETE FROM tbl_QuanLyNhaCungCap WHERE manhacc = @manhacc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhacc", manhacc);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<DTO_Supplier> GetSuppliers()
        {
            List<DTO_Supplier> suppliers = new List<DTO_Supplier>();
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "SELECT manhacc, tennhacc, diachincc, email, sodienthoai FROM tbl_QuanLyNhaCungCap";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Supplier supplier = new DTO_Supplier
                    {
                        manhacc = reader["manhacc"].ToString(),
                        tennhacc = reader["tennhacc"].ToString(),
                        diachincc = reader["diachincc"].ToString(),
                        email = reader["email"].ToString(),
                        sodienthoai = reader["sodienthoai"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }
    }
}
