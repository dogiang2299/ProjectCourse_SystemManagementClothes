using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_CategoryProducts
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public List<DTO_CategoryProducts> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_CategoryProducts> DTO_CategoryProducts = new List<DTO_CategoryProducts>();
                string query = "select * from tblQuanLyDanhMucSanPham_VOGUE";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_CategoryProducts DTO_CategoryProducts1 = new DTO_CategoryProducts
                            {
                                madanhmuc = Convert.ToInt32(reader["madanhmuc"]),
                                tendangmuc = reader["tendangmuc"].ToString(),
                                motadanhmuc = reader["motadanhmuc"].ToString(),
                                soluongsanphamtrongDM = Convert.ToInt32(reader["soluongsanphamtrongDM"]),
                            };
                            DTO_CategoryProducts.Add(DTO_CategoryProducts1);

                        }

                    }
                }
                return DTO_CategoryProducts;
            }
        }
        public bool Creat(DTO_CategoryProducts DTO_CategoryProducts)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "insert into tblQuanLyDanhMucSanPham_VOGUE(madanhmuc,tendangmuc, motadanhmuc, soluongsanphamtrongDM) values (@madanhmuc,@tendangmuc, @motadanhmuc, @soluongsanphamtrongDM)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("madanhmuc", DTO_CategoryProducts.madanhmuc);
                    command.Parameters.AddWithValue("tendangmuc", DTO_CategoryProducts.tendangmuc);
                    command.Parameters.AddWithValue("motadanhmuc", DTO_CategoryProducts.motadanhmuc);
                    command.Parameters.AddWithValue("soluongsanphamtrongDM", DTO_CategoryProducts.soluongsanphamtrongDM);
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
        public bool Update(DTO_CategoryProducts DTO_CategoryProducts)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "update tblQuanLyDanhMucKhachHang set tendangmuc = @tendangmuc, motadanhmuc = @motadanhmuc where madanhmuc = @madanhmuc";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("madanhmuc", DTO_CategoryProducts.madanhmuc);
                    command.Parameters.AddWithValue("tendangmuc", DTO_CategoryProducts.tendangmuc);
                    command.Parameters.AddWithValue("motadanhmuc", DTO_CategoryProducts.motadanhmuc);
                    command.Parameters.AddWithValue("soluongsanphamtrongDM", DTO_CategoryProducts.soluongsanphamtrongDM);
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
                string deleteQuery = "DELETE FROM tblQuanLyDanhMucSanPham_VOGUE WHERE madanhmuc = @madanhmuc";

                using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@madanhmuc", dto_catecus);
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

        public List<DTO_CategoryProducts> Search(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                List<DTO_CategoryProducts> DTO_CategoryProductss = new List<DTO_CategoryProducts>();

                // Triển khai logic tìm kiếm SQL ở đây
                string query = "SELECT * FROM tblQuanLyDanhMucSanPham_VOGUE WHERE " +
                    "madanhmuc LIKE @keyword OR " +
                    "tendangmuc LIKE @keyword OR " +
                    "motadanhmuc LIKE @keyword OR " +
                    "soluongsanphamtrongDM LIKE @keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_CategoryProducts products = MapDoctorFromDataReader(reader);
                            DTO_CategoryProductss.Add(products);
                        }
                    }
                }

                return DTO_CategoryProductss;
            }
        }
        public DTO_CategoryProducts MapDoctorFromDataReader(SqlDataReader reader)
        {
            DTO_CategoryProducts DTO_CategoryProducts1 = new DTO_CategoryProducts
            {
                madanhmuc = Convert.ToInt32(reader["madanhmuc"]),
                tendangmuc = reader["tendangmuc"].ToString(),
                motadanhmuc = reader["motadanhmuc"].ToString(),
                soluongsanphamtrongDM = Convert.ToInt32(reader["soluongsanphamtrongDM"]),
            };
            return DTO_CategoryProducts1;
        }
    }
}
