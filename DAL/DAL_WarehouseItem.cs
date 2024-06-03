using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_WarehouseItem
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_WarehouseItem> GetAllChiTietXuatKho(string MaXuatKho)
        {
            List<DTO_WarehouseItem> list = new List<DTO_WarehouseItem>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tblQuanLyChiTietXuatKho WHERE MaXuatKho = @MaXuatKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXuatKho", MaXuatKho);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DTO_WarehouseItem item = new DTO_WarehouseItem
                    {
                        MaChiTietXuatKho = Convert.ToInt32(reader["MaChiTietXuatKho"]),
                        MaXuatKho = reader["MaXuatKho"].ToString(),
                        MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                        TenSanPham = reader["TenSanPham"].ToString(),
                        SoLuongTonKho = Convert.ToInt32(reader["SoLuongTonKho"]),
                        SoLuongXuatKho = Convert.ToInt32(reader["SoLuongXuatKho"]),
                        GiaXuat = Convert.ToInt32(reader["GiaXuat"]),
                    };
                    list.Add(item);
                }
            }

            return list;
        }

        public bool DeleteChiTietXuatKho(int maChiTietXuatKho)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "DELETE FROM tblQuanLyChiTietXuatKho WHERE MaChiTietXuatKho = @MaChiTietXuatKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTietXuatKho", maChiTietXuatKho);
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

    }
}
