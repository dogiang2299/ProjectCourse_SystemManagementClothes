using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_SaleInvoice
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_SaleInvoice> GetAllHoaDonBan()
        {
            List<DTO_SaleInvoice> hoaDonBanList = new List<DTO_SaleInvoice>();

            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                string query = " select * from tblHoaDonBan";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_SaleInvoice hoaDon = new DTO_SaleInvoice();
                    hoaDon.MaHoaDonBan = reader["MaHoaDonBan"].ToString();
                    hoaDon.MaKhach = Convert.ToInt32(reader["MaKhach"]);
                    hoaDon.TenKhach = reader["TenKhach"].ToString();
                    hoaDon.KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]);
                    hoaDon.GiamGia = Convert.ToInt32(reader["GiamGia"]);
                    hoaDon.VanChuyen = Convert.ToInt32(reader["VanChuyen"]);
                    hoaDon.TongPhu = Convert.ToInt32(reader["TongPhu"]);
                    hoaDon.TongChinh = Convert.ToInt32(reader["TongChinh"]);
                    hoaDon.NgayBan = Convert.ToDateTime(reader["NgayBan"]);

                    hoaDonBanList.Add(hoaDon);
                }

                reader.Close();
            }

            return hoaDonBanList;
        }
        public void CreateHoaDonBan(DTO_SaleInvoice hoaDon, List<DTO_SaleInvoiceItem> chiTietHoaDon)
        {
            hoaDon.MaHoaDonBan = GenerateRandomMaHoaDon();

            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Tạo hóa đơn bán
                    string insertHoaDonQuery = "INSERT INTO tblHoaDonBan (MaHoaDonBan, MaKhach, TenKhach, MaNV, TenNV, KhuyenMai, GiamGia, VanChuyen, TongPhu, TongChinh, NgayBan) VALUES (@MaHoaDonBan, @MaKhach, @TenKhach, @MaNV, @TenNV, @KhuyenMai, @GiamGia, @VanChuyen, @TongPhu, @TongChinh, @NgayBan)";
                    SqlCommand insertHoaDonCommand = new SqlCommand(insertHoaDonQuery, connection, transaction);
                    insertHoaDonCommand.Parameters.AddWithValue("@MaHoaDonBan", hoaDon.MaHoaDonBan);
                    insertHoaDonCommand.Parameters.AddWithValue("@MaKhach", hoaDon.MaKhach);
                    insertHoaDonCommand.Parameters.AddWithValue("@TenKhach", hoaDon.TenKhach);
                    insertHoaDonCommand.Parameters.AddWithValue("@MaNV", hoaDon.MaNV);
                    insertHoaDonCommand.Parameters.AddWithValue("@TenNV", hoaDon.TenNV);
                    insertHoaDonCommand.Parameters.AddWithValue("@KhuyenMai", hoaDon.KhuyenMai);
                    insertHoaDonCommand.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                    insertHoaDonCommand.Parameters.AddWithValue("@VanChuyen", hoaDon.VanChuyen);
                    insertHoaDonCommand.Parameters.AddWithValue("@TongPhu", hoaDon.TongPhu);
                    insertHoaDonCommand.Parameters.AddWithValue("@TongChinh", hoaDon.TongChinh);
                    insertHoaDonCommand.Parameters.AddWithValue("@NgayBan", hoaDon.NgayBan);

                    int hoaDonResult = insertHoaDonCommand.ExecuteNonQuery();
                    if (hoaDonResult <= 0)
                    {
                        throw new Exception("Failed to insert into tblHoaDonBan.");
                    }

                    // Thêm các chi tiết hóa đơn
                    foreach (var item in chiTietHoaDon)
                    {
                        string insertChiTietQuery = "INSERT INTO tblChiTietHoaDonBan (MaHoaDonBan, MaSP, TenSP, SoLuongMua, GiaBan) VALUES (@MaHoaDonBan, @MaSP, @TenSP, @SoLuongMua, @GiaBan)";
                        SqlCommand insertChiTietCommand = new SqlCommand(insertChiTietQuery, connection, transaction);
                        insertChiTietCommand.Parameters.AddWithValue("@MaHoaDonBan", hoaDon.MaHoaDonBan);
                        insertChiTietCommand.Parameters.AddWithValue("@MaSP", item.MaSP);
                        insertChiTietCommand.Parameters.AddWithValue("@TenSP", item.TenSP);
                        insertChiTietCommand.Parameters.AddWithValue("@SoLuongMua", item.SoLuongMua);
                        insertChiTietCommand.Parameters.AddWithValue("@GiaBan", item.GiaBan);

                        int chiTietResult = insertChiTietCommand.ExecuteNonQuery();
                        if (chiTietResult <= 0)
                        {
                            throw new Exception("Failed to insert into tblChiTietHoaDonBan.");
                        }

                        // Cập nhật số lượng tồn kho
                        string updateQuery = "UPDATE tblQuanLySanPham_VOGUE SET soluongtonkho = soluongtonkho - @SoLuongMua WHERE idsanpham = @MaSP";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
                        updateCommand.Parameters.AddWithValue("@SoLuongMua", item.SoLuongMua);
                        updateCommand.Parameters.AddWithValue("@MaSP", item.MaSP);

                        int updateResult = updateCommand.ExecuteNonQuery();
                        if (updateResult <= 0)
                        {
                            throw new Exception("Failed to update tblQuanLySanPham_VOGUE.");
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Ghi lại lỗi vào nhật ký hoặc hiển thị thông báo lỗi chi tiết
                    Console.WriteLine("Error: " + ex.Message);
                    throw new Exception("Error creating invoice: " + ex.Message);
                }
            }
        }


        private string GenerateRandomMaHoaDon()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<DTO_SaleInvoice> GetToday()
        {
            List<DTO_SaleInvoice> hoaDonBanList = new List<DTO_SaleInvoice>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryToday = "SELECT * FROM tblHoaDonBan WHERE CONVERT(date, NgayBan) = CONVERT(date, GETDATE())";
                SqlCommand cmd = new SqlCommand(queryToday, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_SaleInvoice hoaDon = new DTO_SaleInvoice();
                        hoaDon.MaHoaDonBan = reader["MaHoaDonBan"].ToString();
                        hoaDon.MaKhach = Convert.ToInt32(reader["MaKhach"]);
                        hoaDon.TenKhach = reader["TenKhach"].ToString();
                        hoaDon.KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]);
                        hoaDon.GiamGia = Convert.ToInt32(reader["GiamGia"]);
                        hoaDon.VanChuyen = Convert.ToInt32(reader["VanChuyen"]);
                        hoaDon.TongPhu = Convert.ToInt32(reader["TongPhu"]);
                        hoaDon.TongChinh = Convert.ToInt32(reader["TongChinh"]);
                        hoaDon.NgayBan = Convert.ToDateTime(reader["NgayBan"]);

                        hoaDonBanList.Add(hoaDon);
                    }
                }
            }

            return hoaDonBanList;
        }

        public List<DTO_SaleInvoice> GetMonth()
        {
            List<DTO_SaleInvoice> hoaDonBanList = new List<DTO_SaleInvoice>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryThisMonth = "SELECT * FROM tblHoaDonBan WHERE YEAR(NgayBan) = YEAR(GETDATE()) AND MONTH(NgayBan) = MONTH(GETDATE())";
                SqlCommand cmd = new SqlCommand(queryThisMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_SaleInvoice hoaDon = new DTO_SaleInvoice();
                        hoaDon.MaHoaDonBan = reader["MaHoaDonBan"].ToString();
                        hoaDon.MaKhach = Convert.ToInt32(reader["MaKhach"]);
                        hoaDon.TenKhach = reader["TenKhach"].ToString();
                        hoaDon.KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]);
                        hoaDon.GiamGia = Convert.ToInt32(reader["GiamGia"]);
                        hoaDon.VanChuyen = Convert.ToInt32(reader["VanChuyen"]);
                        hoaDon.TongPhu = Convert.ToInt32(reader["TongPhu"]);
                        hoaDon.TongChinh = Convert.ToInt32(reader["TongChinh"]);
                        hoaDon.NgayBan = Convert.ToDateTime(reader["NgayBan"]);

                        hoaDonBanList.Add(hoaDon);
                    }
                }
            }

            return hoaDonBanList;
        }

        public List<DTO_SaleInvoice> GetLastMonth()
        {
            List<DTO_SaleInvoice> importBooks = new List<DTO_SaleInvoice>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryLastMonth = "SELECT * FROM tblHoaDonBan WHERE YEAR(NgayBan) = YEAR(DATEADD(month, -1, GETDATE())) AND MONTH(NgayBan) = MONTH(DATEADD(month, -1, GETDATE()))";
                SqlCommand cmd = new SqlCommand(queryLastMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_SaleInvoice hoaDon = new DTO_SaleInvoice();
                        hoaDon.MaHoaDonBan = reader["MaHoaDonBan"].ToString();
                        hoaDon.MaKhach = Convert.ToInt32(reader["MaKhach"]);
                        hoaDon.TenKhach = reader["TenKhach"].ToString();
                        hoaDon.KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]);
                        hoaDon.GiamGia = Convert.ToInt32(reader["GiamGia"]);
                        hoaDon.VanChuyen = Convert.ToInt32(reader["VanChuyen"]);
                        hoaDon.TongPhu = Convert.ToInt32(reader["TongPhu"]);
                        hoaDon.TongChinh = Convert.ToInt32(reader["TongChinh"]);
                        hoaDon.NgayBan = Convert.ToDateTime(reader["NgayBan"]);

                        importBooks.Add(hoaDon);
                    }
                }
            }

            return importBooks;
        }
    }



}

