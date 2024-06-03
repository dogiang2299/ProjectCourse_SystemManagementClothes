using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using System.Data;

namespace DAL
{
    public class DAL_Products
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_Products> GetAll()
        {
            using(SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_Products> dTO_Products = new List<DTO_Products>();

                string query = "select * from tblQuanLySanPham_VOGUE";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while(reader.Read()) 
                        {
                            DTO_Products dTO_Products1 = MapDoctorFromDataReader(reader);
                            dTO_Products.Add(dTO_Products1);
                        }
                    
                    }
                }
                return dTO_Products;
            }
        }
        public bool Create(DTO_Products dTO_Products)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "INSERT INTO tblQuanLySanPham_VOGUE(idsanpham , mabarcode , tensapnpham , giagoc , giaban , donvi , soluongtonkho , mota , gannhansanpham , kichthuoc , danhmucsanpham , tinhtrangsanpham , hinhanhsanpham , madanhmuc) " +
                               "VALUES (@idsanpham ,@mabarcode ,@tensapnpham ,@giagoc ,@giaban ,@donvi ,@soluongtonkho ,@mota ,@gannhansanpham ,@kichthuoc ,@danhmucsanpham ,@tinhtrangsanpham ,@hinhanhsanpham ,@madanhmuc)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idsanpham", dTO_Products.masanpham);
                    command.Parameters.AddWithValue("@mabarcode", dTO_Products.mabarcode);
                    command.Parameters.AddWithValue("@tensapnpham", dTO_Products.tensanpham);
                    command.Parameters.AddWithValue("@giagoc", dTO_Products.giavon);
                    command.Parameters.AddWithValue("@giaban", dTO_Products.giaban);
                    command.Parameters.AddWithValue("@donvi", dTO_Products.donvi);
                    command.Parameters.AddWithValue("@soluongtonkho", dTO_Products.soluongtonkho);
                    command.Parameters.AddWithValue("@mota", dTO_Products.mota);
                    command.Parameters.AddWithValue("@gannhansanpham", dTO_Products.gannhansanpham);
                    command.Parameters.AddWithValue("@kichthuoc", dTO_Products.kichthuoc);
                    command.Parameters.AddWithValue("@danhmucsanpham", dTO_Products.danhmucsanpham);
                    command.Parameters.AddWithValue("@tinhtrangsanpham", dTO_Products.tinhtrangsanpham);
                    command.Parameters.AddWithValue("@hinhanhsanpham", dTO_Products.hinhanhsanpham);
                    command.Parameters.AddWithValue("@madanhmuc", dTO_Products.madanhmuc);
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting data: {ex.Message}");
                        return false;
                    }
                }
            }
        }


        public bool Update(DTO_Products dTO_Products)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "UPDATE tblQuanLySanPham_VOGUE SET mabarcode = @mabarcode, tensapnpham = @tensapnpham, giagoc = @giagoc, giaban = @giaban, donvi = @donvi, soluongtonkho = @soluongtonkho, mota = @mota, gannhansanpham = @gannhansanpham,kichthuoc = @kichthuoc, madanhmuc = @madangmuc, danhmucsanpham = @danhmucsanpham, tinhtrangsanpham = @tinhtrangsanpham, hinhanhsanpham = @hinhanhsanpham, madanhmuc = @madanhmuc WHERE idsanpham = @idsanpham";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idsanpham", dTO_Products.masanpham);
                    command.Parameters.AddWithValue("@mabarcode", dTO_Products.mabarcode);
                    command.Parameters.AddWithValue("@tensapnpham", dTO_Products.tensanpham);
                    command.Parameters.AddWithValue("@giagoc", dTO_Products.giavon);
                    command.Parameters.AddWithValue("@giaban", dTO_Products.giaban);
                    command.Parameters.AddWithValue("@donvi", dTO_Products.donvi);
                    command.Parameters.AddWithValue("@soluongtonkho", dTO_Products.soluongtonkho);
                    command.Parameters.AddWithValue("@mota", dTO_Products.mota);
                    command.Parameters.AddWithValue("@gannhansanpham", dTO_Products.gannhansanpham);
                    command.Parameters.AddWithValue("@kichthuoc", dTO_Products.kichthuoc);
                    command.Parameters.AddWithValue("@danhmucsanpham", dTO_Products.danhmucsanpham);
                    command.Parameters.AddWithValue("@tinhtrangsanpham", dTO_Products.tinhtrangsanpham);
                    command.Parameters.AddWithValue("@hinhanhsanpham", dTO_Products.hinhanhsanpham);
                    command.Parameters.AddWithValue("@madanhmuc", dTO_Products.madanhmuc);
                    try
                    {
                        Console.WriteLine("Executing SQL command...");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating data: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public bool Delete(int dto_products)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM tblQuanLySanPham_VOGUE WHERE idsanpham = @idsanpham";

                using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                { 
                    command.Parameters.AddWithValue("@idsanpham", dto_products);
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

        public List<DTO_Products> SearchDoctor(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_Products> DTO_Productss = new List<DTO_Products>();

                // Triển khai logic tìm kiếm SQL ở đây
                string query = "SELECT * FROM tblQuanLySanPham_VOGUE WHERE " +
                    "idsanpham LIKE @keyword OR " +
                    "mabarcode LIKE @keyword OR " +
                    "tensapnpham LIKE @keyword OR " +
                    "giagoc LIKE @keyword OR " +
                    "giaban LIKE @keyword OR " +
                    "donvi LIKE @keyword OR " +
                    "soluongtonkho LIKE @keyword OR " +
                    "mota LIKE @keyword OR " +
                    "kichthuoc LIKE @keyword OR " +
                    "gannhansanpham LIKE @keyword OR " +
                    "danhmucsanpham LIKE @keyword OR " +
                    "tinhtrangsanpham LIKE @keyword";
                    

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_Products products = MapDoctorFromDataReader(reader);
                            DTO_Productss.Add(products);
                        }
                    }
                }

                return DTO_Productss;
            }
        }
        private DTO_Products MapDoctorFromDataReader(SqlDataReader reader)
        {
            DTO_Products dTO_Products1 = new DTO_Products
            {
                masanpham = Convert.ToInt32(reader["idsanpham"]),
                mabarcode = reader["mabarcode"].ToString(),
                tensanpham = reader["tensapnpham"].ToString(),
                giavon = Convert.ToInt32(reader["giagoc"]),
                giaban = Convert.ToInt32(reader["giaban"]),
                donvi = reader["donvi"].ToString(),
                soluongtonkho = Convert.ToInt32(reader["soluongtonkho"]),
                mota = reader["mota"].ToString(),
                gannhansanpham = reader["gannhansanpham"].ToString(),
                kichthuoc = reader["kichthuoc"].ToString(),
                madanhmuc = Convert.ToInt32(reader["madanhmuc"]),
                danhmucsanpham = reader["danhmucsanpham"].ToString(),
                tinhtrangsanpham = reader["tinhtrangsanpham"].ToString(),
                hinhanhsanpham = (reader["hinhanhsanpham"] as byte[]),
            };

            return dTO_Products1;
        }
        public void UpdateInventoryQuantity(int slgmoi, int masp)
        {
            using(SqlConnection conn = new SqlConnection(connectSQL))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tblQuanLySanPham_VOGUE SET soluongtonkho = @soluongtonkho WHERE idsanpham = @idsanpham";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@soluongtonkho", slgmoi);
                    command.Parameters.AddWithValue("@idsanpham", masp);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật sản phẩm: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }    
        }

        public static DataTable ThongKeTonKho()
        {
            string query = "select idsanpham,tensapnpham,soluongtonkho,tinhtrangsanpham from tblQuanLySanPham_VOGUE";
            return ReadData.DocChiTietHoaDon(query);
        }


    }
}
