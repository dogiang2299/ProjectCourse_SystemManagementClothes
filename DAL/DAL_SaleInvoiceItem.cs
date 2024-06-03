using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_SaleInvoiceItem
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_SaleInvoiceItem> GetChiTietHoaDonBan(string maHoaDonBan)
        {
            List<DTO_SaleInvoiceItem> chiTietList = new List<DTO_SaleInvoiceItem>();

            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                string query = "SELECT * FROM tblChiTietHoaDonBan WHERE MaHoaDonBan = @MaHoaDonBan";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaHoaDonBan", maHoaDonBan);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_SaleInvoiceItem chiTiet = new DTO_SaleInvoiceItem();
                    chiTiet.MaChiTietHoaDonBan = Convert.ToInt32(reader["MaChiTietHoaDonBan"]);
                    chiTiet.MaHoaDonBan = reader["MaHoaDonBan"].ToString();
                    chiTiet.MaSP = Convert.ToInt32(reader["MaSP"]);
                    chiTiet.TenSP = reader["TenSP"].ToString();
                    chiTiet.SoLuongMua = Convert.ToInt32(reader["SoLuongMua"]);
                    chiTiet.GiaBan = Convert.ToInt32(reader["GiaBan"]);

                    chiTietList.Add(chiTiet);
                }

                reader.Close();
            }

            return chiTietList;
        }
    // deletechitiethoadonban
        }
}
