using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Warehouse
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public OperationResultWare Add(DTO_Warehouse warehouse, List<DTO_WarehouseItem> warehousechiTiets)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    warehouse.MaXuatKho = GenerateMaXuatKho();

                    //// Bật IDENTITY_INSERT
                    //SqlCommand cmdEnableIdentityInsert = new SqlCommand("SET IDENTITY_INSERT tblQuanLyXuatKho ON", conn, transaction);
                    //cmdEnableIdentityInsert.ExecuteNonQuery();

                    string warehouseQuery = "INSERT INTO tblQuanLyXuatKho (MaXuatKho, MaNV, TenNV, MaKH, TenKH, TongTienXuat, NgayXuatKho) VALUES (@MaXuatKho, @MaNV, @TenNV, @MaKH, @TenKH, @TongTienXuat, @NgayXuatKho)";
                    SqlCommand warehouseCmd = new SqlCommand(warehouseQuery, conn, transaction);
                    warehouseCmd.Parameters.AddWithValue("@MaXuatKho", warehouse.MaXuatKho);
                    warehouseCmd.Parameters.AddWithValue("@MaNV", warehouse.MaNV);
                    warehouseCmd.Parameters.AddWithValue("@TenNV", warehouse.TenNV);
                    warehouseCmd.Parameters.AddWithValue("@MaKH", warehouse.MaKH);
                    warehouseCmd.Parameters.AddWithValue("@TenKH", warehouse.TenKH);
                    warehouseCmd.Parameters.AddWithValue("@TongTienXuat", warehouse.TongTienXuat);
                    warehouseCmd.Parameters.AddWithValue("@NgayXuatKho", warehouse.NgayXuatKho);
                    warehouseCmd.ExecuteNonQuery();
                    int hoaDonResult = warehouseCmd.ExecuteNonQuery();
                    if (hoaDonResult <= 0)
                    {
                        throw new Exception("Failed to insert into tblQuanLyXuatKho.");
                    }

                    // Tắt IDENTITY_INSERT
                    //SqlCommand cmdDisableIdentityInsert = new SqlCommand("SET IDENTITY_INSERT tblQuanLyXuatKho OFF", conn, transaction);
                    //cmdDisableIdentityInsert.ExecuteNonQuery();

                    foreach (var chiTiet in warehousechiTiets)
                    {
                        string chiTietQuery = "INSERT INTO tblQuanLyChiTietXuatKho (MaXuatKho, MaSanPham, TenSanPham, SoLuongTonKho, SoLuongXuatKho, GiaXuat) VALUES ( @MaXuatKho, @MaSanPham, @TenSanPham, @SoLuongTonKho, @SoLuongXuatKho, @GiaXuat)";
                        SqlCommand chiTietCmd = new SqlCommand(chiTietQuery, conn, transaction);
                        chiTietCmd.Parameters.AddWithValue("@MaXuatKho", chiTiet.MaXuatKho);
                        chiTietCmd.Parameters.AddWithValue("@MaSanPham", chiTiet.MaSanPham);
                        chiTietCmd.Parameters.AddWithValue("@TenSanPham", chiTiet.TenSanPham);
                        chiTietCmd.Parameters.AddWithValue("@SoLuongTonKho", chiTiet.SoLuongTonKho);
                        chiTietCmd.Parameters.AddWithValue("@SoLuongXuatKho", chiTiet.SoLuongXuatKho);
                        chiTietCmd.Parameters.AddWithValue("@GiaXuat", chiTiet.GiaXuat);
                        chiTietCmd.ExecuteNonQuery();

                        string queryUpdateSanPham = "UPDATE tblQuanLySanPham_VOGUE SET soluongtonkho = soluongtonkho - @SoLuongXuatKho WHERE idsanpham = @MaSanPham";
                        SqlCommand cmdUpdateSanPham = new SqlCommand(queryUpdateSanPham, conn, transaction);
                        cmdUpdateSanPham.Parameters.AddWithValue("@SoLuongXuatKho", chiTiet.SoLuongXuatKho);
                        cmdUpdateSanPham.Parameters.AddWithValue("@MaSanPham", chiTiet.MaSanPham);
                        cmdUpdateSanPham.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return new OperationResultWare { Success = true, ErrorMessage = string.Empty };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new OperationResultWare { Success = false, ErrorMessage = ex.Message };
                }
            }
        }
        public string GenerateMaXuatKho()
        {
            const string chars = "AQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public List<DTO_Warehouse> GetAllWarehouses()
        {
   
            List<DTO_Warehouse> warehouses = new List<DTO_Warehouse>();
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                string query = "SELECT * FROM tblQuanLyXuatKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Warehouse warehouse = new DTO_Warehouse
                    {
                        MaXuatKho = reader["MaXuatKho"].ToString(),
                        MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                        TenNV = reader["TenNV"].ToString(),
                        MaKH = reader["MaKH"] != DBNull.Value ? Convert.ToInt32(reader["MaKH"]) : 0,
                        TenKH = reader["TenKH"].ToString(),
                        TongTienXuat = reader["TongTienXuat"] != DBNull.Value ? Convert.ToInt32(reader["TongTienXuat"]) : 0,
                        NgayXuatKho = reader["NgayXuatKho"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuatKho"]) : DateTime.MinValue
                    };
                    warehouses.Add(warehouse);
                }
            }
            return warehouses;
        }
        public List<DTO_Warehouse> SearchWareBooks(string keyword)
        {
            List<DTO_Warehouse> wareBooks = new List<DTO_Warehouse>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tblQuanLyXuatKho WHERE MaXuatKho LIKE @Keyword OR TenNV LIKE @Keyword OR TenKH LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_Warehouse warehouse = new DTO_Warehouse
                        {
                            MaXuatKho = reader["MaXuatKho"].ToString(),
                            MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                            TenNV = reader["TenNV"].ToString(),
                            MaKH = reader["MaKH"] != DBNull.Value ? Convert.ToInt32(reader["MaKH"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            TongTienXuat = reader["TongTienXuat"] != DBNull.Value ? Convert.ToInt32(reader["TongTienXuat"]) : 0,
                            NgayXuatKho = reader["NgayXuatKho"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuatKho"]) : DateTime.MinValue
                        };
                        wareBooks.Add(warehouse);

                    }
                }
            }

            return wareBooks;
        }
        public void DeleteWarehouse(string maXuatKho)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Delete Warehouse chiTiets first
                    string deletechiTietsQuery = "DELETE FROM tblQuanLyChiTietXuatKho WHERE MaXuatKho = @MaXuatKho";
                    SqlCommand deletechiTietsCmd = new SqlCommand(deletechiTietsQuery, conn, transaction);
                    deletechiTietsCmd.Parameters.AddWithValue("@MaXuatKho", maXuatKho);
                    deletechiTietsCmd.ExecuteNonQuery();

                    // Delete Warehouse
                    string deleteWarehouseQuery = "DELETE FROM tblQuanLyXuatKho WHERE MaXuatKho = @MaXuatKho";
                    SqlCommand deleteWarehouseCmd = new SqlCommand(deleteWarehouseQuery, conn, transaction);
                    deleteWarehouseCmd.Parameters.AddWithValue("@MaXuatKho", maXuatKho);
                    deleteWarehouseCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<DTO_Warehouse> GetToday()
        {
            List<DTO_Warehouse> wareBooks = new List<DTO_Warehouse>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryToday = "SELECT * FROM tblQuanLyXuatKho WHERE CONVERT(date, NgayNhapKho) = CONVERT(date, GETDATE())";
                SqlCommand cmd = new SqlCommand(queryToday, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_Warehouse warehouse = new DTO_Warehouse
                        {
                            MaXuatKho = reader["MaXuatKho"].ToString(),
                            MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                            TenNV = reader["TenNV"].ToString(),
                            MaKH = reader["MaKH"] != DBNull.Value ? Convert.ToInt32(reader["MaKH"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            TongTienXuat = reader["TongTienXuat"] != DBNull.Value ? Convert.ToInt32(reader["TongTienXuat"]) : 0,
                            NgayXuatKho = reader["NgayXuatKho"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuatKho"]) : DateTime.MinValue
                        };
                        wareBooks.Add(warehouse);
                    }
                }
            }

            return wareBooks;
        }

        public List<DTO_Warehouse> GetMonth()
        {
            List<DTO_Warehouse> wareBooks = new List<DTO_Warehouse>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryThisMonth = "SELECT * FROM tblQuanLyXuatKho WHERE YEAR(NgayNhapKho) = YEAR(GETDATE()) AND MONTH(NgayNhapKho) = MONTH(GETDATE())";
                SqlCommand cmd = new SqlCommand(queryThisMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_Warehouse warehouse = new DTO_Warehouse
                        {
                            MaXuatKho = reader["MaXuatKho"].ToString(),
                            MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                            TenNV = reader["TenNV"].ToString(),
                            MaKH = reader["MaKH"] != DBNull.Value ? Convert.ToInt32(reader["MaKH"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            TongTienXuat = reader["TongTienXuat"] != DBNull.Value ? Convert.ToInt32(reader["TongTienXuat"]) : 0,
                            NgayXuatKho = reader["NgayXuatKho"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuatKho"]) : DateTime.MinValue
                        };
                        wareBooks.Add(warehouse);
                    }
                }
            }

            return wareBooks;
        }

        public List<DTO_Warehouse> GetLastMonth()
        {
            List<DTO_Warehouse> wareBooks = new List<DTO_Warehouse>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryLastMonth = "SELECT * FROM tblQuanLyXuatKho WHERE YEAR(NgayNhapKho) = YEAR(DATEADD(month, -1, GETDATE())) AND MONTH(NgayNhapKho) = MONTH(DATEADD(month, -1, GETDATE()))";
                SqlCommand cmd = new SqlCommand(queryLastMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DTO_Warehouse warehouse = new DTO_Warehouse
                    {
                        MaXuatKho = reader["MaXuatKho"].ToString(),
                        MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                        TenNV = reader["TenNV"].ToString(),
                        MaKH = reader["MaKH"] != DBNull.Value ? Convert.ToInt32(reader["MaKH"]) : 0,
                        TenKH = reader["TenKH"].ToString(),
                        TongTienXuat = reader["TongTienXuat"] != DBNull.Value ? Convert.ToInt32(reader["TongTienXuat"]) : 0,
                        NgayXuatKho = reader["NgayXuatKho"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuatKho"]) : DateTime.MinValue
                    };
                    wareBooks.Add(warehouse);
                }
            }

            return wareBooks;
        }
    }
}
