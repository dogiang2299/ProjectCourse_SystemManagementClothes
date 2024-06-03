using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_CategoryCustomers
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_CategoryCustomers> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_CategoryCustomers> DTO_CategoryCustomers = new List<DTO_CategoryCustomers>();
                string query = "select * from tblQuanLyDanhMucKhachHang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_CategoryCustomers DTO_CategoryCustomers1 = new DTO_CategoryCustomers
                            {
                                madanhmucKH = Convert.ToInt32(reader["maphanloaikhachhang"]),
                                tendanhmucKH = reader["tenphanloaikhachhang"].ToString(),

                            };
                            DTO_CategoryCustomers.Add(DTO_CategoryCustomers1);

                        }

                    }
                }
                return DTO_CategoryCustomers;
            }
        }
        public bool Creat(DTO_CategoryCustomers DTO_CategoryCustomers)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "insert into tblQuanLyDanhMucKhachHang(maphanloaikhachhang,tenphanloaikhachhang) values (@maphanloaikhachhang, @tenphanloaikhachhang)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("maphanloaikhachhang", DTO_CategoryCustomers.madanhmucKH);
                    command.Parameters.AddWithValue("tenphanloaikhachhang", DTO_CategoryCustomers.tendanhmucKH);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu có ít nhất một dòng bị ảnh hưởng (đã được thêm)                 
                    }
                    catch (Exception ex)
                    {
                        // Ghi log lỗi nếu cần thiết
                        Console.WriteLine($"Error inserting data: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        public bool Update(DTO_CategoryCustomers DTO_CategoryCustomers)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "update tblQuanLyDanhMucKhachHang set tenphanloaikhachhang = @tenphanloaikhachhang where maphanloaikhachhang = @maphanloaikhachhang";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("maphanloaikhachhang", DTO_CategoryCustomers.madanhmucKH);
                    command.Parameters.AddWithValue("tenphanloaikhachhang", DTO_CategoryCustomers.tendanhmucKH);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu có ít nhất một dòng bị ảnh hưởng (đã được thêm)                 
                    }
                    catch (Exception ex)
                    {
                        // Ghi log lỗi nếu cần thiết
                        Console.WriteLine($"Error inserting data: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        public bool Delete(int dto_catecus)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM tblQuanLyDanhMucKhachHang WHERE maphanloaikhachhang = @maphanloaikhachhang";

                using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@maphanloaikhachhang", dto_catecus);
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu có ít nhất một dòng bị ảnh hưởng (đã được xóa)
                    }
                    catch (Exception ex)
                    {
                        // Ghi log lỗi nếu cần thiết
                        Console.WriteLine($"Error deleting data: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public List<DTO_CategoryCustomers> Search(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_CategoryCustomers> DTO_CategoryCustomerss = new List<DTO_CategoryCustomers>();

                // Triển khai logic tìm kiếm SQL ở đây
                string query = "SELECT * FROM tblQuanLyDanhMucKhachHang WHERE " +
                    "maphanloaikhachhang LIKE @keyword OR " +
                    "tenphanloaikhachhang LIKE @keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_CategoryCustomers products = MapDoctorFromDataReader(reader);
                            DTO_CategoryCustomerss.Add(products);
                        }
                    }
                }

                return DTO_CategoryCustomerss;
            }
        }
        public DTO_CategoryCustomers MapDoctorFromDataReader(SqlDataReader reader)
        {
            DTO_CategoryCustomers DTO_CategoryCustomers1 = new DTO_CategoryCustomers
            {
                madanhmucKH = Convert.ToInt32(reader["maphanloaikhachhang"]),
                tendanhmucKH = reader["tenphanloaikhachhang"].ToString(),
            };

            return DTO_CategoryCustomers1;
        }
    }
}
