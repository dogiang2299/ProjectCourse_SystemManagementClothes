using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.Models
{
    public partial class AddCategoryProduct : Form
    {
        private Products pro = new Products();
        private BLL_CategoryProducts products = new BLL_CategoryProducts();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public AddCategoryProduct(Products products)
        {
            InitializeComponent();
           // soluongsp.Enabled = false;
            pro = products; 
        }

        private void guna2Button4_lammoi_Click(object sender, EventArgs e)
        {
            madanhmuc.Enabled = true;
            madanhmuc.Text = "";
            tendanhmuc.Text = "";
            mota.Text = "";
            soluongsp.Text = "";
        }
        int kiemtramatrung(string ma)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tblQuanLyDanhMucSanPham_VOGUE where madanhmuc = @madanhmuc";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@madanhmuc", ma);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }
        }
        private void guna2Button1_save_Click(object sender, EventArgs e)
        {
            if (kiemtramatrung(madanhmuc.Text) == 1)
            {
                guna2MessageDialog1.Show("Mã danh mục sản phẩm đã tồn tại! Vui lòng nhập mã danh mục sản phẩm khác");
            }
            else if (madanhmuc.Text == "" || tendanhmuc.Text == "" ||mota.Text == "")
            {
                guna2MessageDialog2.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }

            else
            {
                
                int soluongSanPham;

                if (string.IsNullOrEmpty(soluongsp.Text) || !int.TryParse(soluongsp.Text, out soluongSanPham))
                {
                    soluongSanPham = 0;
                }
                DTO_CategoryProducts dTO_catecus = new DTO_CategoryProducts()
                {
                    madanhmuc = Convert.ToInt32(madanhmuc.Text),
                    tendangmuc = tendanhmuc.Text,
                    motadanhmuc = mota.Text,
                    soluongsanphamtrongDM = soluongSanPham

                };
                products.Create(dTO_catecus);
                guna2MessageDialog5.Show("Dữ liệu được tạo thành công!");
                pro.Display1();
            }

        }
        public void SetData(string[] textbox)
        {
            madanhmuc.Enabled = false;
            madanhmuc.Text = textbox[0];
            tendanhmuc.Text = textbox[1];
            mota.Text = textbox[2];
            soluongsp.Text = textbox[3];
            
        }
        private void guna2Button2_update_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn sửa thông tin ở danh mục sản phẩm này không?");
            if (result == DialogResult.Yes)
            {
                madanhmuc.Enabled = false;
                soluongsp.Enabled = false;
                int makhach = Convert.ToInt32(madanhmuc.Text);
                string tenkhach = tendanhmuc.Text;
                string motas = mota.Text;
                int slg = Convert.ToInt32(soluongsp.Text);
                DTO_CategoryProducts dTO_Customers = new DTO_CategoryProducts()
                {
                    madanhmuc = makhach,
                    tendangmuc = tenkhach,
                    motadanhmuc = motas,
                    soluongsanphamtrongDM = slg

                };
                products.Update(dTO_Customers);
                guna2MessageDialog5.Show("Dữ liệu được thay đổi thành công!");
                pro.Display1();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }

        private void guna2Button3_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn xoá thông tin ở danh mục sản phẩm này không?");
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(madanhmuc.Text);
                products.Delete(id);
                guna2MessageDialog5.Show("Dữ liệu được xoá thành công!");
                pro.Display1();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }

        private void tendanhmuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void mota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void soluongsp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void madanhmuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void soluongsp_TextChanged(object sender, EventArgs e)
        {
            
                string query = "select count(soluongtonkho) from tblQuanLySanPham_VOGUE inner join tblQuanLyDanhMucSanPham_VOGUE on tblQuanLyDanhMucSanPham_VOGUE.madanhmuc = tblQuanLySanPham_VOGUE.madanhmuc where tblQuanLyDanhMucSanPham_VOGUE.madanhmuc = @ma";
                using (SqlConnection conn = new SqlConnection(connectSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", madanhmuc.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                soluongsp.Text = reader[0].ToString();
                            }
                        }
                    }
                }
            

        }
        private void DisplayNumberCount()
        {

        }
        private void AddCategoryProduct_Load(object sender, EventArgs e)
        {
            soluongsp.Enabled = false;
        }
    }
}
