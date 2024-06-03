using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Models;
using ProjectCourse_SystemManagementClothes.Product;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.Mainfolder
{
    public partial class Sell : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_Products _Products = new BLL_Products();
        private List<DTO_Products> _pro = new List<DTO_Products>();
        private Products products = new Products();
        private List<Products> produc;
        
        public Sell(Products pro)
        {
            products = pro;
        }
        public Sell(List<Products> produs)
        {
            produc = produs;
        }
        public Sell()
        {
            InitializeComponent();
            guna2Panel5.Visible = false;
        }
     
        public void Display()
        {
            List<DTO_Products> emp = _Products.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";//
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";//
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";//
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";//
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";
            guna2DataGridView1.Columns[13].HeaderText = "Mã danh mục";
            // VISIBLE
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
            guna2DataGridView1.Columns[12].Visible = false;
            guna2DataGridView1.Columns[13].Visible = false;
        }

        private void Sell_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Visible = false;
            Display();
            products.Display();
        }
        private void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox5.Text = "";
            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
            if(e.RowIndex >= 0)
            {
                guna2Panel5.Visible = true;
                // mã
                label13_1.Text = selectedRow.Cells[0].Value.ToString();
                guna2TextBox2.Text = selectedRow.Cells[0].Value.ToString();
                // barcode
                label23_2.Text = selectedRow.Cells[1].Value.ToString();
                // tên
                label5_3.Text = selectedRow.Cells[2].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells[2].Value.ToString();
                // giá nhập
                label17_5.Text = selectedRow.Cells[3].Value.ToString();
                // giá bán
                label16_4.Text = selectedRow.Cells[4].Value.ToString();
                guna2TextBox6.Text = selectedRow.Cells[4].Value.ToString();
                // đơn vị
                label18_6.Text = selectedRow.Cells[5].Value.ToString();
                // số tồn
                label22_10.Text = selectedRow.Cells[6].Value.ToString();
                guna2TextBox4.Text = selectedRow.Cells[6].Value.ToString();
                // mô tra
                label10_11.Text = selectedRow.Cells[7].Value.ToString();
                // gắn nhãn
                label19_7.Text = selectedRow.Cells[8].Value.ToString();
                // kích thước
                guna2TextBox7.Text = selectedRow.Cells[9].Value.ToString();
                // danh mục
                label20_8.Text = selectedRow.Cells[10].Value.ToString();
                // tình trạng
                label21_9.Text = selectedRow.Cells[11].Value.ToString();
                // hình ảnh
                if (selectedRow.Cells[12].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])selectedRow.Cells[12].Value;
                    Image image = ConvertBytesToImage(imageBytes);
                    guna2PictureBox2.Image = image;
                }
                
            }
          
        }
        private Image ConvertBytesToImage(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return Image.FromStream(memoryStream);
            }
        }
        //private int thanhtiens = 0;
        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            //int soluongtonkho;
            //int giaban;
            //int soluongban;
            //if (int.TryParse(guna2TextBox4.Text, out soluongtonkho) && int.TryParse(guna2TextBox6.Text, out giaban))
            //{
            //    if (int.TryParse(guna2TextBox5.Text, out soluongban))
            //    {
            //        if(soluongtonkho > soluongban)
            //        {
            //            thanhtiens = giaban    idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc,tinhtrangsanpham, hinhanhsanpham   soluongban;
            //        }
            //        else if(soluongtonkho < soluongban)
            //        {
            //            guna2MessageDialog1.Show("Không đủ để thực hiện giao dịch","Lỗi!");
            //        }
            //    }
            //    else
            //    {
            //        thanhtiens = 0;
            //    }
            //}

            //else
            //{
            //    MessageBox.Show("Giá trị tồn kho không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //guna2TextBox7.Text = thanhtiens.ToString();
        }
        // Confirm
        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            int soluongton;
            int soluongban;
            if(int.TryParse(guna2TextBox4.Text, out soluongton) && (int.TryParse(guna2TextBox5.Text, out soluongban)))
            {
                if(soluongton < soluongban)
                {
                    guna2MessageDialog1.Show("Số lượng sản phẩm không đủ để thực hiện. Vui lòng nhập thêm ở kho", "Lỗi!");
                }  
                else
                {
                    string[] productData = new string[6];
                    productData[0] = guna2TextBox2.Text; // mã
                    productData[1] = guna2TextBox3.Text;// tên
                    productData[2] = guna2TextBox4.Text;//sltk
                    productData[3] = guna2TextBox6.Text;// giá bán
                    productData[4] = guna2TextBox7.Text;// kích thước
                    productData[5] = guna2TextBox5.Text;// sl mua
                    
                    Confirm confirmForm = (Confirm)Application.OpenForms["Confirm"];
                    if (confirmForm == null)
                    {
                        confirmForm = new Confirm();
                    }

                    confirmForm.SetData(productData);
                    confirmForm.UpdateSelectedProducts(produc);
                    confirmForm.Show();
                }
            }
           
            //this.Close();
        }
        // add pro
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddProducts addProducts = new AddProducts(products);
            addProducts.Show();
            
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            if(guna2TextBox1.Visible == false)
            {
                guna2TextBox1.Visible = true;
            }    
        }
        private void DisplaySearch()
        {
            string keyword = guna2TextBox1.Text.Trim().ToLower();
            List<DTO_Products> search = _Products.Search(keyword);
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = search;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";//
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";//
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";//
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";//
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";//
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";//
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";
            guna2DataGridView1.Columns[13].HeaderText = "Mã danh mục";//

            // VISIBLE
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
            guna2DataGridView1.Columns[12].Visible = false;
            guna2DataGridView1.Columns[13].Visible = false;
        }
            
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = guna2TextBox1.Text.Trim().ToLower();
            if (keyword.Length >= 1 || string.IsNullOrEmpty(keyword))
            {
                DisplaySearch();
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            QuickCreateProduct quickCreate = new QuickCreateProduct(products);
            quickCreate.Show();
        }

        private void arrange_Click(object sender, EventArgs e)
        {
            ArrangePro arrangePro = new ArrangePro(this);
            arrangePro.Show();
        }

        public void SortAndDisplayData(string sortOrder)
        {
            string query = "";

            if (sortOrder == "Từ A - Z")
            {
                query = "SELECT idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc, hinhanhsanpham FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham ASC ";
              
            }
            else if (sortOrder == "Từ Z - A")
            {
                query = "SELECT idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc, hinhanhsanpham FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC ";
               
            }
            else if (sortOrder == "Mới nhất")
            {
                query = "SELECT idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc,ngaytao, hinhanhsanpham FROM tblQuanLySanPham_VOGUE ORDER BY ngaytao DESC ";
                
            }
            else if (sortOrder == "Sản phẩm bán chạy")
            {
                query = "select idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc,tinhtrangsanpham, SoLuongMua as [Số lượng bán] ,hinhanhsanpham  from tblQuanLySanPham_VOGUE inner join tblChiTietHoaDonBan on tblChiTietHoaDonBan.MaSP = tblQuanLySanPham_VOGUE.idsanpham order by SoLuongMua desc";
                
            }
            else if (sortOrder == "Còn hàng từ cao đến thấp")
            {
                query = "select idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc, soluongtonkho, hinhanhsanpham FROM tblQuanLySanPham_VOGUE ORDER BY soluongtonkho DESC";
                
            }
            else if (sortOrder == "Còn hàng từ thấp đến cao")
            {
                query = "select idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, kichthuoc, soluongtonkho, hinhanhsanpham FROM tblQuanLySanPham_VOGUE ORDER BY soluongtonkho ASC";
                
            }
            else if (sortOrder == "Giá từ cao đến thấp")
            {
                query = "select idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, soluongtonkho, kichthuoc,tinhtrangsanpham, hinhanhsanpham from tblQuanLySanPham_VOGUE ORDER BY giaban DESC ";
                
            }
            else if (sortOrder == "Giá từ thấp đến cao")
            {
                query = "select idsanpham, tensapnpham,tblQuanLySanPham_VOGUE.giaban, gannhansanpham, soluongtonkho, kichthuoc,tinhtrangsanpham, hinhanhsanpham from tblQuanLySanPham_VOGUE ORDER BY giaban ASC ";
               
            }
            else
            {
                MessageBox.Show("Thứ tự sắp xếp không hợp lệ.");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
