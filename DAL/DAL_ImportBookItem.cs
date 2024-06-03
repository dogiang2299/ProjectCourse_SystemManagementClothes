using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ImportBookItem
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_ImportBookItem> GetAllChiTietNhapKho(string maNhapKho)
        {
            List<DTO_ImportBookItem> danhSachChiTietNhapKho = new List<DTO_ImportBookItem>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tblQuanLyChiTietNhapKho WHERE MaNhapKho = @MaNhapKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhapKho", maNhapKho);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DTO_ImportBookItem chiTietNhapKho = new DTO_ImportBookItem
                    {
                        MaChiTietNhapKho = Convert.ToInt32(reader["MaChiTietNhapKho"]),
                        MaNhapKho = reader["MaNhapKho"].ToString(),
                        MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                        TenSanPham = reader["TenSanPham"].ToString(),
                        SoLuongTonKho = Convert.ToInt32(reader["SoLuongTonKho"]),
                        SoLuongNhapKho = Convert.ToInt32(reader["SoLuongNhapKho"]),
                        DonGiaNhap = Convert.ToInt32(reader["DonGiaNhap"])
                    };

                    danhSachChiTietNhapKho.Add(chiTietNhapKho);
                }
            }

            return danhSachChiTietNhapKho;
        }

        public bool DeleteChiTietNhapKho(int maChiTietNhapKho)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "DELETE FROM tblQuanLyChiTietNhapKho WHERE MaChiTietNhapKho = @MaChiTietNhapKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietNhapKho", maChiTietNhapKho);
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


    }
}
