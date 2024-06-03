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
namespace ProjectCourse_SystemManagementClothes.Models
{
    public partial class AddProducts : Form
    {
        private byte[] productImage;
        private BLL_Products bLL = new BLL_Products();
        private Products products = new Products();
        private DTO_Products dTO_employ = new DTO_Products();
        private Sell sell1 = new Sell();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public AddProducts(Products productss)
        {
            InitializeComponent();
            products = productss;
        }
        public AddProducts(Sell sell)
        {
            InitializeComponent();
            this.sell1 = sell;

        }
        private void dissplayCombobox()
        {
            List<DTO_CategoryProducts> pro = new List<DTO_CategoryProducts>();
            string query = "select madanhmuc, tendangmuc from tblQuanLyDanhMucSanPham_VOGUE";
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO_CategoryProducts dTO_CategoryProducts = new DTO_CategoryProducts()
                            {
                                madanhmuc = Convert.ToInt32(reader["madanhmuc"]),
                                tendangmuc = reader["tendangmuc"].ToString()
                            };
                            pro.Add(dTO_CategoryProducts);
                        }
                    }
                }
                danhmc.DataSource = pro;
                danhmc.DisplayMember = "madanhmuc";
                danhmc.ValueMember = "madanhmuc";
            }
        }
        private void guna2Button4_lammoi_Click(object sender, EventArgs e)
        {
            masp.Enabled = true;
            mabar.Enabled = true;
            masp.Text = "";
            mabar.Text = "";
            ten.Text = "";
            giaban.Text = "";
            gianhap.Text = "";
            kthuoc.Text = "";
            donvi.Text = "";
            motas.Text = "";
            sltk.Text = "";
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
        int kiemtramatrungbar(string mabar)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tblQuanLySanPham_VOGUE where mabarcode = @mabarcode";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@mabarcode", mabar);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }

        }
        private void guna2Button1_save_Click(object sender, EventArgs e)
        {
           
            if (kiemtramatrung(masp.Text) == 1 || kiemtramatrungbar(mabar.Text) == 1)
            {
                guna2MessageDialog3.Show("Mã sản phẩm hoặc mã barcode đã tồn tại! Vui lòng nhập mã khác");
            }
            else if((mabar.Text).Length < 13 || (mabar.Text).Length > 13)
            {
                guna2MessageDialog3.Show("Mã barcode phải đủ 13 số");
            }
            else if (mabar.Text == "" || masp.Text == "" || ten.Text == "" || giaban.Text == "" || gianhap.Text == "" || sltk.Text == "")
            {
                guna2MessageDialog2.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }
            else
            {
               
                GeneralMethod();
                bool isCreated = bLL.Create(dTO_employ);

                if (isCreated)
                {
                    guna2MessageDialog5.Show("Dữ liệu được thêm thành công!");
                    products.Display();
                    
                }
                
            }

        }
        public void GeneralMethod()
        {
            int masanpham, giavon, giabans, soluongtonkho;

            // Kiểm tra và chuyển đổi giá trị
            if (!int.TryParse(masp.Text, out masanpham))
            {
                guna2MessageDialog3.Show("Mã sản phẩm không hợp lệ!");
                return;
            }
            if (!int.TryParse(gianhap.Text, out giavon))
            {
                guna2MessageDialog3.Show("Giá nhập không hợp lệ!");
                return;
            }
            if (!int.TryParse(giaban.Text, out giabans))
            {
                guna2MessageDialog3.Show("Giá bán không hợp lệ!");
                return;
            }
            if (!int.TryParse(sltk.Text, out soluongtonkho))
            {
                guna2MessageDialog3.Show("Số lượng tồn kho không hợp lệ!");
                return;
            }

            dTO_employ = new DTO_Products()
            {
                masanpham = masanpham,
                mabarcode = mabar.Text,
                tensanpham = ten.Text,
                giavon = giavon,
                giaban = giabans,
                donvi = donvi.Text,
                soluongtonkho = soluongtonkho,
                mota = motas.Text,
                gannhansanpham = gannhan.Text,
                kichthuoc = kthuoc.Text,
                danhmucsanpham = tendanhmuc.Text,
                tinhtrangsanpham = tinhtrangsp.Text,
                hinhanhsanpham = productImage,
                madanhmuc = Convert.ToInt32(danhmc.Text),

            };
        }
        private void guna2Button2_update_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn sửa thông tin ở sản phẩm này không?");
            if (result == DialogResult.OK)
            {
                int masanpham, giavons, giabans, soluongtonkho;

                // Kiểm tra và chuyển đổi giá trị
                if (!int.TryParse(masp.Text, out masanpham))
                {
                    guna2MessageDialog3.Show("Mã sản phẩm không hợp lệ!");
                    return;
                }
                if (!int.TryParse(gianhap.Text, out giavons))
                {
                    guna2MessageDialog3.Show("Giá nhập không hợp lệ!");
                    return;
                }
                if (!int.TryParse(giaban.Text, out giabans))
                {
                    guna2MessageDialog3.Show("Giá bán không hợp lệ!");
                    return;
                }
                if (!int.TryParse(sltk.Text, out soluongtonkho))
                {
                    guna2MessageDialog3.Show("Số lượng tồn kho không hợp lệ!");
                    return;
                }

                dTO_employ = new DTO_Products()
                {
                    masanpham = masanpham,
                    mabarcode = mabar.Text,
                    tensanpham = ten.Text,
                    giavon = giavons,
                    giaban = giabans,
                    donvi = donvi.Text,
                    soluongtonkho = soluongtonkho,
                    mota = motas.Text,
                    gannhansanpham = gannhan.Text,
                    kichthuoc = kthuoc.Text,
                    danhmucsanpham = tendanhmuc.Text,
                    tinhtrangsanpham = tinhtrangsp.Text,
                    hinhanhsanpham = productImage,
                    madanhmuc = Convert.ToInt32(danhmc.Text),

                };
                bLL.Update(dTO_employ);
                guna2MessageDialog5.Show("Dữ liệu được sửa thành công!");
                products.Display();
                sell1.Display();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn giữ nguyên!");

            }

        }
        private void guna2Button3_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn xoá thông tin ở nhân viên này không?");
            if (result == DialogResult.OK)
            {
                int id = Convert.ToInt32(masp.Text);
                bLL.Delete(id);
                guna2MessageDialog5.Show("Dữ liệu được xoá thành công!");
                products.Display();
            }
            else
            {
                guna2MessageDialog3.Show("Dữ liệu vẫn được giữ nguyên!");
            }
            
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // open file image taianh
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
                        guna2PictureBox3.Image = Image.FromFile(imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi mở tệp hình ảnh: {ex.Message}");
                    }
                }
            }
        }
        public void SetData(string[] texbox, string[] combobox, byte[] hinhAnh)
        {
            mabar.Enabled = false;
            masp.Enabled = false;
            masp.Text = texbox[0];
            mabar.Text = texbox[1];
            ten.Text = texbox[2];
            gianhap.Text = texbox[3];
            giaban.Text = texbox[4];
            donvi.Text = texbox[5];
            sltk.Text = texbox[6];
            motas.Text = texbox[7];
            tendanhmuc.Text = texbox[8];
            gannhan.SelectedItem = combobox[0];
            kthuoc.SelectedItem = combobox[1];
            danhmc.SelectedItem = combobox[2];
            tinhtrangsp.SelectedItem = combobox[3];
            if (hinhAnh != null && hinhAnh.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(hinhAnh))
                {
                    Image image = Image.FromStream(ms);
                    guna2PictureBox3.Image = image;
                }
            }
            else
            {
                guna2PictureBox3.Image = null;
            }

        }
        private void AddProducts_Load(object sender, EventArgs e)
        {
            dissplayCombobox();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void danhmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(danhmc.SelectedItem is DTO_CategoryProducts products)
            {
                tendanhmuc.Text = products.tendangmuc.ToString();
            }
        }
    }
}
