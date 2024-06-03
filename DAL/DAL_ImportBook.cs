using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ImportBook
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";

        public List<DTO_ImportBook> GetAll()
        {
            List<DTO_ImportBook> danhSachNhapKho = new List<DTO_ImportBook>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tblQuanLyNhapKho";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DTO_ImportBook nhapKho = new DTO_ImportBook
                    {
                        MaNhapKho = reader["MaNhapKho"].ToString(),
                        MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]),
                        TenNhanVien = reader["TenNhanVien"].ToString(),
                        MaNhaCungCap = reader["MaNhaCungCap"].ToString(),
                        TenNhaCungCap = reader["TenNhaCungCap"].ToString(),
                        TongTienNhap = Convert.ToInt32(reader["TongTienNhap"]),
                        NgayNhapKho = Convert.ToDateTime(reader["NgayNhapKho"])
                    };

                    danhSachNhapKho.Add(nhapKho);
                }
            }

            return danhSachNhapKho;
        }
        public void DeleteWarehouse(string MaNhapKho)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Delete Warehouse chiTiets first
                    string deletechiTietsQuery = "DELETE FROM tblQuanLyChiTietNhapKho WHERE MaNhapKho = @MaNhapKho";
                    SqlCommand deletechiTietsCmd = new SqlCommand(deletechiTietsQuery, conn, transaction);
                    deletechiTietsCmd.Parameters.AddWithValue("@MaNhapKho", MaNhapKho);
                    deletechiTietsCmd.ExecuteNonQuery();

                    // Delete Warehouse
                    string deleteWarehouseQuery = "DELETE FROM tblQuanLyNhapKho WHERE MaNhapKho = @MaNhapKho";
                    SqlCommand deleteWarehouseCmd = new SqlCommand(deleteWarehouseQuery, conn, transaction);
                    deleteWarehouseCmd.Parameters.AddWithValue("@MaNhapKho", MaNhapKho);
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
        public bool UpdateProductPriceAndQuantity(int maSanPham, int newPrice, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Cập nhật giá sản phẩm trong bảng tblQuanLySanPham_VOGUE
                    string queryUpdatePrice = "UPDATE tblQuanLySanPham_VOGUE SET giagoc = @NewPrice WHERE idsanpham = @idsanpham";
                    SqlCommand cmdUpdatePrice = new SqlCommand(queryUpdatePrice, conn, transaction);
                    cmdUpdatePrice.Parameters.AddWithValue("@idsanpham", maSanPham);
                    cmdUpdatePrice.Parameters.AddWithValue("@NewPrice", newPrice);
                    cmdUpdatePrice.ExecuteNonQuery();

                    // Cập nhật số lượng nhập trong bảng tblQuanLyChiTietNhapKho
                    string queryUpdateQuantity = "UPDATE tblQuanLyChiTietNhapKho SET SoLuongNhapKho = @NewQuantity WHERE MaSanPham = @MaSanPham";
                    SqlCommand cmdUpdateQuantity = new SqlCommand(queryUpdateQuantity, conn, transaction);
                    cmdUpdateQuantity.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmdUpdateQuantity.Parameters.AddWithValue("@NewQuantity", newQuantity);
                    cmdUpdateQuantity.ExecuteNonQuery();

                    // Cập nhật số lượng tồn kho trong bảng tblQuanLySanPham_VOGUE
                    string queryUpdateStock = "UPDATE tblQuanLySanPham_VOGUE SET soluongtonkho = soluongtonkho + @NewQuantity WHERE idsanpham = @idsanpham";
                    SqlCommand cmdUpdateStock = new SqlCommand(queryUpdateStock, conn, transaction);
                    cmdUpdateStock.Parameters.AddWithValue("@idsanpham", maSanPham);
                    cmdUpdateStock.Parameters.AddWithValue("@NewQuantity", newQuantity);
                    cmdUpdateStock.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public List<DTO_ImportBook> SearchImportBooks(string keyword)
        {
            List<DTO_ImportBook> importBooks = new List<DTO_ImportBook>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tblQuanLyNhapKho WHERE MaNhapKho LIKE @Keyword OR TenNhanVien LIKE @Keyword OR TenNhaCungCap LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ImportBook importBook = new DTO_ImportBook();
                        importBook.MaNhapKho = reader["MaNhapKho"].ToString();
                        importBook.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                        importBook.TenNhanVien = reader["TenNhanVien"].ToString();
                        importBook.MaNhaCungCap = reader["MaNhaCungCap"].ToString();
                        importBook.TenNhaCungCap = reader["TenNhaCungCap"].ToString();
                        importBook.TongTienNhap = Convert.ToInt32(reader["TongTienNhap"]);
                        importBook.NgayNhapKho = Convert.ToDateTime(reader["NgayNhapKho"]);

                        importBooks.Add(importBook);
                    }
                }
            }

            return importBooks;
        }
        public OperationResult Add(DTO_ImportBook nhapKho, List<DTO_ImportBookItem> chiTietNhapKhos)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    nhapKho.MaNhapKho = GenerateMaNhapKho();

                    //// Bật IDENTITY_INSERT
                    //SqlCommand cmdEnableIdentityInsert = new SqlCommand("SET IDENTITY_INSERT tblQuanLyNhapKho ON", conn, transaction);
                    //cmdEnableIdentityInsert.ExecuteNonQuery();

                    string queryNhapKho = "INSERT INTO tblQuanLyNhapKho (MaNhapKho, MaNhanVien, TenNhanVien, MaNhaCungCap, TenNhaCungCap, TongTienNhap, NgayNhapKho) VALUES (@MaNhapKho, @MaNhanVien, @TenNhanVien, @MaNhaCungCap, @TenNhaCungCap, @TongTienNhap, @NgayNhapKho)";
                    SqlCommand cmdNhapKho = new SqlCommand(queryNhapKho, conn, transaction);
                    cmdNhapKho.Parameters.AddWithValue("@MaNhapKho", nhapKho.MaNhapKho);
                    cmdNhapKho.Parameters.AddWithValue("@MaNhanVien", nhapKho.MaNhanVien);
                    cmdNhapKho.Parameters.AddWithValue("@TenNhanVien", nhapKho.TenNhanVien);
                    cmdNhapKho.Parameters.AddWithValue("@MaNhaCungCap", nhapKho.MaNhaCungCap);
                    cmdNhapKho.Parameters.AddWithValue("@TenNhaCungCap", nhapKho.TenNhaCungCap);
                    cmdNhapKho.Parameters.AddWithValue("@TongTienNhap", nhapKho.TongTienNhap);
                    cmdNhapKho.Parameters.AddWithValue("@NgayNhapKho", nhapKho.NgayNhapKho);
                    int hoaDonResult = cmdNhapKho.ExecuteNonQuery();
                    if (hoaDonResult <= 0)
                    {
                        throw new Exception("Failed to insert into tblQuanLyNhapKho.");
                    }

                    // Tắt IDENTITY_INSERT
                    //SqlCommand cmdDisableIdentityInsert = new SqlCommand("SET IDENTITY_INSERT tblQuanLyNhapKho OFF", conn, transaction);
                    //cmdDisableIdentityInsert.ExecuteNonQuery();

                    foreach (var chiTiet in chiTietNhapKhos)
                    {
                        string queryChiTietNhapKho = "INSERT INTO tblQuanLyChiTietNhapKho ( MaNhapKho, MaSanPham, TenSanPham, SoLuongTonKho, SoLuongNhapKho, DonGiaNhap) VALUES ( @MaNhapKho, @MaSanPham, @TenSanPham, @SoLuongTonKho, @SoLuongNhapKho, @DonGiaNhap)";
                        SqlCommand cmdChiTietNhapKho = new SqlCommand(queryChiTietNhapKho, conn, transaction);
                        cmdChiTietNhapKho.Parameters.AddWithValue("@MaNhapKho", nhapKho.MaNhapKho);  // Sử dụng MaNhapKho từ DTO_ImportBook
                        cmdChiTietNhapKho.Parameters.AddWithValue("@MaSanPham", chiTiet.MaSanPham);
                        cmdChiTietNhapKho.Parameters.AddWithValue("@TenSanPham", chiTiet.TenSanPham);
                        cmdChiTietNhapKho.Parameters.AddWithValue("@SoLuongTonKho", chiTiet.SoLuongTonKho);
                        cmdChiTietNhapKho.Parameters.AddWithValue("@SoLuongNhapKho", chiTiet.SoLuongNhapKho);
                        cmdChiTietNhapKho.Parameters.AddWithValue("@DonGiaNhap", chiTiet.DonGiaNhap);
                        cmdChiTietNhapKho.ExecuteNonQuery();

                        string queryUpdateSanPham = "UPDATE tblQuanLySanPham_VOGUE SET soluongtonkho = soluongtonkho + @SoLuongNhapKho WHERE idsanpham = @MaSanPham";
                        SqlCommand cmdUpdateSanPham = new SqlCommand(queryUpdateSanPham, conn, transaction);
                        cmdUpdateSanPham.Parameters.AddWithValue("@SoLuongNhapKho", chiTiet.SoLuongNhapKho);
                        cmdUpdateSanPham.Parameters.AddWithValue("@MaSanPham", chiTiet.MaSanPham);
                        cmdUpdateSanPham.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return new OperationResult { Success = true, ErrorMessage = string.Empty };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new OperationResult { Success = false, ErrorMessage = ex.Message };
                }
            }
        }

        public string GenerateMaNhapKho()
        {
            const string chars = "AQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public List<DTO_ImportBook> GetToday()
        {
            List<DTO_ImportBook> importBooks = new List<DTO_ImportBook>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryToday = "SELECT * FROM tblQuanLyNhapKho WHERE CONVERT(date, NgayNhapKho) = CONVERT(date, GETDATE())";
                SqlCommand cmd = new SqlCommand(queryToday, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ImportBook importBook = new DTO_ImportBook();
                        importBook.MaNhapKho = reader["MaNhapKho"].ToString();
                        importBook.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                        importBook.TenNhanVien = reader["TenNhanVien"].ToString();
                        importBook.MaNhaCungCap = reader["MaNhaCungCap"].ToString();
                        importBook.TenNhaCungCap = reader["TenNhaCungCap"].ToString();
                        importBook.TongTienNhap = Convert.ToInt32(reader["TongTienNhap"]);
                        importBook.NgayNhapKho = Convert.ToDateTime(reader["NgayNhapKho"]);

                        importBooks.Add(importBook);
                    }
                }
            }

            return importBooks;
        }

        public List<DTO_ImportBook> GetMonth()
        {
            List<DTO_ImportBook> importBooks = new List<DTO_ImportBook>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryThisMonth = "SELECT * FROM tblQuanLyNhapKho WHERE YEAR(NgayNhapKho) = YEAR(GETDATE()) AND MONTH(NgayNhapKho) = MONTH(GETDATE())";
                SqlCommand cmd = new SqlCommand(queryThisMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ImportBook importBook = new DTO_ImportBook();
                        importBook.MaNhapKho = reader["MaNhapKho"].ToString();
                        importBook.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                        importBook.TenNhanVien = reader["TenNhanVien"].ToString();
                        importBook.MaNhaCungCap = reader["MaNhaCungCap"].ToString();
                        importBook.TenNhaCungCap = reader["TenNhaCungCap"].ToString();
                        importBook.TongTienNhap = Convert.ToInt32(reader["TongTienNhap"]);
                        importBook.NgayNhapKho = Convert.ToDateTime(reader["NgayNhapKho"]);

                        importBooks.Add(importBook);
                    }
                }
            }

            return importBooks;
        }

        public List<DTO_ImportBook> GetLastMonth()
        {
            List<DTO_ImportBook> importBooks = new List<DTO_ImportBook>();

            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string queryLastMonth = "SELECT * FROM tblQuanLyNhapKho WHERE YEAR(NgayNhapKho) = YEAR(DATEADD(month, -1, GETDATE())) AND MONTH(NgayNhapKho) = MONTH(DATEADD(month, -1, GETDATE()))";
                SqlCommand cmd = new SqlCommand(queryLastMonth, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ImportBook importBook = new DTO_ImportBook();
                        importBook.MaNhapKho = reader["MaNhapKho"].ToString();
                        importBook.MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
                        importBook.TenNhanVien = reader["TenNhanVien"].ToString();
                        importBook.MaNhaCungCap = reader["MaNhaCungCap"].ToString();
                        importBook.TenNhaCungCap = reader["TenNhaCungCap"].ToString();
                        importBook.TongTienNhap = Convert.ToInt32(reader["TongTienNhap"]);
                        importBook.NgayNhapKho = Convert.ToDateTime(reader["NgayNhapKho"]);

                        importBooks.Add(importBook);
                    }
                }
            }

            return importBooks;
        }
    }
}
