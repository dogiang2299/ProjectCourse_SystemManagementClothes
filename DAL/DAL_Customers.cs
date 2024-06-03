using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Customers
    {

        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_Customers> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_Customers> DTO_Customers = new List<DTO_Customers>();
                string query = "select * from tbl_QuanLyKhachHang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_Customers DTO_Customers1 = new DTO_Customers
                            {
                                makhachhang = reader.GetInt32(reader.GetOrdinal("makhachhang")),
                                tenkhachhang = reader.IsDBNull(reader.GetOrdinal("tenkhachhang")) ? string.Empty : reader.GetString(reader.GetOrdinal("tenkhachhang")),
                                ngaysinh = reader.IsDBNull(reader.GetOrdinal("ngaysinh")) ? string.Empty : reader.GetString(reader.GetOrdinal("ngaysinh")),
                                gioitinh = reader.IsDBNull(reader.GetOrdinal("gioitinh")) ? string.Empty : reader.GetString(reader.GetOrdinal("gioitinh")),
                                diachi = reader.IsDBNull(reader.GetOrdinal("diachi")) ? string.Empty : reader.GetString(reader.GetOrdinal("diachi")),
                                sdthoai = reader.IsDBNull(reader.GetOrdinal("sdthoai")) ? string.Empty : reader.GetString(reader.GetOrdinal("sdthoai")),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
                                madanhmuc = reader.GetInt32(reader.GetOrdinal("madanhmuckhachhang")),
                                danhmuckhachhang = reader.IsDBNull(reader.GetOrdinal("tendanhmuckhachhang")) ? string.Empty : reader.GetString(reader.GetOrdinal("tendanhmuckhachhang")),
                            };
                            DTO_Customers.Add(DTO_Customers1);

                        }

                    }
                }
                return DTO_Customers;
            }
        }

        public bool Creat(DTO_Customers DTO_Customers)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "insert into tbl_QuanLyKhachHang(makhachhang,tenkhachhang,ngaysinh,gioitinh,diachi,sdthoai,email,madanhmuckhachhang, tendanhmuckhachhang) values (@makhachhang, @tenkhachhang, @ngaysinh, @gioitinh, @diachi, @sdthoai, @email,@madanhmuckhachhang, @tendanhmuckhachhang)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.AddWithValue("makhachhang", DTO_Customers.makhachhang);
                    command.Parameters.AddWithValue("tenkhachhang", DTO_Customers.tenkhachhang);
                    command.Parameters.AddWithValue("ngaysinh", DTO_Customers.ngaysinh);
                    command.Parameters.AddWithValue("gioitinh", DTO_Customers.gioitinh);
                    command.Parameters.AddWithValue("diachi", DTO_Customers.diachi);
                    command.Parameters.AddWithValue("sdthoai", DTO_Customers.sdthoai);
                    command.Parameters.AddWithValue("email", DTO_Customers.email);
                    command.Parameters.AddWithValue("madanhmuckhachhang", DTO_Customers.madanhmuc);
                    command.Parameters.AddWithValue("tendanhmuckhachhang", DTO_Customers.danhmuckhachhang);

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

        public bool Update(DTO_Customers DTO_Customers)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "update tbl_QuanLyKhachHang set tenkhachhang = @tenkhachhang, ngaysinh = @ngaysinh, gioitinh = @gioitinh, diachi = @diachi,sdthoai = @sdthoai, email = @email, madanhmuckhachhang = @madanhmuckhachhang, tendanhmuckhachhang = @tendanhmuckhachhang  where makhachhang = @makhachhang";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("makhachhang", DTO_Customers.makhachhang);
                    command.Parameters.AddWithValue("tenkhachhang", DTO_Customers.tenkhachhang);
                    command.Parameters.AddWithValue("ngaysinh", DTO_Customers.ngaysinh);
                    command.Parameters.AddWithValue("gioitinh", DTO_Customers.gioitinh);
                    command.Parameters.AddWithValue("diachi", DTO_Customers.diachi);
                    command.Parameters.AddWithValue("sdthoai", DTO_Customers.sdthoai);
                    command.Parameters.AddWithValue("email", DTO_Customers.email);
                    command.Parameters.AddWithValue("madanhmuckhachhang", DTO_Customers.madanhmuc);
                    command.Parameters.AddWithValue("tendanhmuckhachhang", DTO_Customers.danhmuckhachhang);
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

        public bool Delete(int DTO_Customers)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM tbl_QuanLyKhachHang WHERE makhachhang = @makhachhang";

                using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@makhachhang", DTO_Customers);
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

        public List<DTO_Customers> SearchDoctor(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_Customers> DTO_Customerss = new List<DTO_Customers>();

                // Triển khai logic tìm kiếm SQL ở đây
                string query = "SELECT * FROM tbl_QuanLyKhachHang WHERE " +
                    "makhachhang LIKE @keyword OR " +
                    "tenkhachhang LIKE @keyword OR " +
                    "ngaysinh LIKE @keyword OR " +
                    "gioitinh LIKE @keyword OR " +
                    "diachi LIKE @keyword OR " +
                    "sdthoai LIKE @keyword OR " +
                    "email LIKE @keyword OR " +
                    "madanhmuckhachhang LIKE @keyword OR " +
                    "tendanhmuckhachhang LIKE @keyword";
                    


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_Customers products = MapDoctorFromDataReader(reader);
                            DTO_Customerss.Add(products);
                        }
                    }
                }

                return DTO_Customerss;
            }
        }
        private DTO_Customers MapDoctorFromDataReader(SqlDataReader reader)
        {
            DTO_Customers DTO_Customers1 = new DTO_Customers
            {
                makhachhang = reader.GetInt32(reader.GetOrdinal("makhachhang")),
                tenkhachhang = reader.IsDBNull(reader.GetOrdinal("tenkhachhang")) ? string.Empty : reader.GetString(reader.GetOrdinal("tenkhachhang")),
                ngaysinh = reader.IsDBNull(reader.GetOrdinal("ngaysinh")) ? string.Empty : reader.GetString(reader.GetOrdinal("ngaysinh")),
                gioitinh = reader.IsDBNull(reader.GetOrdinal("gioitinh")) ? string.Empty : reader.GetString(reader.GetOrdinal("gioitinh")),
                diachi = reader.IsDBNull(reader.GetOrdinal("diachi")) ? string.Empty : reader.GetString(reader.GetOrdinal("diachi")),
                sdthoai = reader.IsDBNull(reader.GetOrdinal("sdthoai")) ? string.Empty : reader.GetString(reader.GetOrdinal("sdthoai")),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
                madanhmuc = reader.GetInt32(reader.GetOrdinal("madanhmuckhachhang")),
                danhmuckhachhang = reader.IsDBNull(reader.GetOrdinal("tendanhmuckhachhang")) ? string.Empty : reader.GetString(reader.GetOrdinal("tendanhmuckhachhang")),


            };

            return DTO_Customers1;
        }
    }
}
