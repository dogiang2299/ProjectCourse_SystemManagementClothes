using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Mainfolder;
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
using System.IO;
namespace ProjectCourse_SystemManagementClothes.Product
{
    public partial class QuickCreateProduct : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_Products products = new BLL_Products();
        private Products pros;
        private DTO_Products _Products = new DTO_Products();
        public QuickCreateProduct(Products pro)
        {
            InitializeComponent();
            this.pros = pro;
        }
        int kiemtramatrung(string ma)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tblQuanLySanPham_VOGUE where idsanpham = @idsanpham";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@idsanpham", ma);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }

        }
        // thêm nhanh sản phẩm
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (kiemtramatrung(masp.Text) == 1)
            {
                guna2MessageDialog1.Show("Mã sản phẩm đã tồn tại! Vui lòng nhập mã khác");
            }
           
            else if ( masp.Text == "" || ten.Text == "" || giaban.Text == "" || gianhap.Text == "")
            {
                guna2MessageDialog2.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }
            else if(guna2PictureBox2.Image == null)
            {
                guna2MessageDialog2.Show("Bạn chưa cập nhật ảnh! Vui lòng cập nhật ảnh");
            }
            else
            {

                int masanpham, giavon, giabans;

                // Kiểm tra và chuyển đổi giá trị
                if (!int.TryParse(masp.Text, out masanpham))
                {
                    guna2MessageDialog1.Show("Mã sản phẩm không hợp lệ!");
                    return;
                }
                if (!int.TryParse(gianhap.Text, out giavon))
                {
                    guna2MessageDialog1.Show("Giá nhập không hợp lệ!");
                    return;
                }
                if (!int.TryParse(giaban.Text, out giabans))
                {
                    guna2MessageDialog1.Show("Giá bán không hợp lệ!");
                    return;
                }
                _Products = new DTO_Products()
                {
                    masanpham = masanpham,
                    tensanpham = ten.Text,
                    giavon = giavon,
                    giaban = giabans,
                    hinhanhsanpham = productImage,
                };
                bool isCreated = products.Create(_Products);

                if (isCreated)
                {
                    guna2MessageDialog2.Show("Dữ liệu được thêm thành công!");
                    pros.Display();
                }

            }
        }
        private byte[] productImage;
        // add image
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    try
                    {
                        productImage = File.ReadAllBytes(imagePath);
                        if (productImage.Length == 0)
                        {
                            MessageBox.Show("Dữ liệu hình ảnh không hợp lệ.");
                            return;
                        }
                        guna2PictureBox2.Image = Image.FromFile(imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi mở tệp hình ảnh: {ex.Message}");
                    }
                }
            }
        }
    }
}
