using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DAL_Employer
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_Employer> GetAllNhanVien()
        {
            List<DTO_Employer> nhanViens = new List<DTO_Employer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Quan_Ly_Nhan_Vien", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Employer nhanVien = new DTO_Employer();
                    nhanVien.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                    nhanVien.TenNhanVien = reader["TenNhanVien"].ToString();
                    nhanVien.SoDienThoai = reader["SoDienThoai"].ToString();
                    nhanVien.NgaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                    nhanVien.GioiTinh = reader["GioiTinh"].ToString();
                    nhanVien.DiaChi = reader["DiaChi"].ToString();
                    nhanVien.Email = reader["Email"].ToString();
                    nhanVien.TenChucVu = reader["TenChucVu"].ToString();
                    nhanViens.Add(nhanVien);
                }
            }

            return nhanViens;
        }

        public void AddNhanVien(DTO_Employer nhanVien)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Quan_Ly_Nhan_Vien VALUES (@TenNhanVien, @SoDienThoai, @NgaySinh, @GioiTinh, @DiaChi, @Email, @TenChucVu)", connection);
                cmd.Parameters.AddWithValue("@TenNhanVien", nhanVien.TenNhanVien);
                cmd.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
                cmd.Parameters.AddWithValue("@NgaySinh", nhanVien.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", nhanVien.GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
                cmd.Parameters.AddWithValue("@Email", nhanVien.Email);
                cmd.Parameters.AddWithValue("@TenChucVu", nhanVien.TenChucVu);
                cmd.ExecuteNonQuery();
            }
        }
       
        public void UpdateNhanVien(DTO_Employer nhanVien)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE tbl_Quan_Ly_Nhan_Vien SET TenNhanVien = @TenNhanVien, SoDienThoai = @SoDienThoai, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi, Email = @Email, TenChucVu = @TenChucVu WHERE MaNhanVien = @MaNhanVien", connection);
                cmd.Parameters.AddWithValue("@MaNhanVien", nhanVien.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNhanVien", nhanVien.TenNhanVien);
                cmd.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
                cmd.Parameters.AddWithValue("@NgaySinh", nhanVien.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", nhanVien.GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
                cmd.Parameters.AddWithValue("@Email", nhanVien.Email);
                cmd.Parameters.AddWithValue("@TenChucVu", nhanVien.TenChucVu);
                cmd.ExecuteNonQuery();
            }
        }

        public List<DTO_Employer> SearchNhanVien(string keyword)
        {
            List<DTO_Employer> nhanViens = new List<DTO_Employer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Quan_Ly_Nhan_Vien WHERE TenNhanVien LIKE @Keyword OR SoDienThoai LIKE @Keyword OR Email LIKE @Keyword", connection);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Employer nhanVien = new DTO_Employer();
                    nhanVien.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                    nhanVien.TenNhanVien = reader["TenNhanVien"].ToString();
                    nhanVien.SoDienThoai = reader["SoDienThoai"].ToString();
                    nhanVien.NgaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                    nhanVien.GioiTinh = reader["GioiTinh"].ToString();
                    nhanVien.DiaChi = reader["DiaChi"].ToString();
                    nhanVien.Email = reader["Email"].ToString();
                    nhanVien.TenChucVu = reader["TenChucVu"].ToString();
                    nhanViens.Add(nhanVien);
                }
            }

            return nhanViens;
        }
        public void DeleteNhanVien(int maNhanVien)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_Quan_Ly_Nhan_Vien WHERE MaNhanVien = @MaNhanVien", connection);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

