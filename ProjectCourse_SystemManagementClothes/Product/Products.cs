using BLL;
using DTO;
using Irony.Parsing;
using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Models;
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

namespace ProjectCourse_SystemManagementClothes.Mainfolder
{
    public partial class Products : Form
    {
       // private Sell sell = new Sell();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_Products _Products = new BLL_Products();
        private BLL_CategoryProducts categoryProducts = new BLL_CategoryProducts();
        public Products()
        {
            InitializeComponent();
            guna2TextBox1_search.Visible = false;
        }

        private void Products_Load(object sender, EventArgs e)
        {
            Display1();
            Display();
            TrangThaiBanDau();
        }

        private void guna2ImageButton1_search_Click(object sender, EventArgs e)
        {
            if(guna2TextBox1_search.Visible == false)
            {
                guna2TextBox1_search.Visible = true;
            }    
        }

        private void guna2ImageButton4_addproduct_Click(object sender, EventArgs e)
        {
            AddProducts addProducts = new AddProducts(this);
            addProducts.ShowDialog();
        }
        public void Display1()
        {
            List<DTO_CategoryProducts> cus = categoryProducts.GetAll();
            guna2DataGridView2.DataSource = cus;
            guna2DataGridView2.Columns[0].HeaderText = "Mã danh mục sản phẩm";
            guna2DataGridView2.Columns[1].HeaderText = "Tên danh mục sản phẩm";
            guna2DataGridView2.Columns[2].HeaderText = "Mô tả danh mục";
            guna2DataGridView2.Columns[3].HeaderText = "Số lượng sản phẩm";
        }
        public void Display()
        {
            List<DTO_Products> emp = _Products.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";
            guna2DataGridView1.Columns[13].HeaderText = "Mã mục sản phẩm";
            guna2DataGridView1.Columns[13].Visible = false;


        }
        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            AddCategoryProduct addCategoryProduct = new AddCategoryProduct(this);
            addCategoryProduct.Show();
        }
        private void SearchTextBoxDM()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_CategoryProducts> search = categoryProducts.Search(keyword);
            guna2DataGridView2.DataSource = null;
            guna2DataGridView2.DataSource = search;
            guna2DataGridView2.Columns[0].HeaderText = "Mã danh mục";
            guna2DataGridView2.Columns[1].HeaderText = "Tên danh mục";
            guna2DataGridView2.Columns[2].HeaderText = "Mô tả danh mục";
            guna2DataGridView2.Columns[3].HeaderText = "Số lượng";
            
            guna2DataGridView2.DataSource = search;
        }
        private void SearchTextBoxSP()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_Products> search = _Products.Search(keyword);
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = search;
            Display();
            guna2DataGridView1.DataSource = search;
        }
        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            if (keyword.Length >= 1 || string.IsNullOrEmpty(keyword))
            {
                SearchTextBoxDM();
                SearchTextBoxSP();
            }
           
        }
        private void TrangThaiBanDau()
        {
            guna2Panel3.Visible = true;
            guna2DataGridView1.Visible = true;
            guna2Panel4.Visible = false;
            guna2DataGridView2.Visible = false;
            Display();
        }
        private void ShowSanPham()
        {
            guna2Panel3.Visible = true;
            guna2Panel4.Visible = false;
            guna2DataGridView1.Visible = true;
            guna2DataGridView2.Visible = false;
            Display();
        }
        private void ShowDanhMuc()
        {
            guna2Panel4.Visible = true;
            guna2DataGridView2.Visible = true;
            Display1();
        }
        // danh mucj
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowDanhMuc();
        }
        // sản phẩm
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ShowSanPham();
        }
       
        // danh muc
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView2.Rows[e.RowIndex];
                string[] textbox = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    textbox[i] = selectedRow.Cells[i].Value.ToString();
                }
                AddCategoryProduct formAdd = new AddCategoryProduct(this);
                formAdd.SetData(textbox);
                formAdd.Show();
                formAdd.Focus();
                formAdd.BringToFront();

            }
        }
      
        // san pham
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                string[] textbox = new string[9];
                for (int i = 0; i < 9; i++)
                {
                    textbox[i] = selectedRow.Cells[i].Value.ToString();
                }
                string[] combobox = new string[4];
                combobox[0] = selectedRow.Cells[9].Value?.ToString() ?? string.Empty;
                combobox[1] = selectedRow.Cells[10].Value?.ToString() ?? string.Empty;
                combobox[2] = selectedRow.Cells[11].Value?.ToString() ?? string.Empty;
                combobox[3] = selectedRow.Cells[13].Value?.ToString() ?? string.Empty;
                byte[] hinhAnh = null;
                if (selectedRow.Cells[12].Value != DBNull.Value)
                {
                    hinhAnh = (byte[])selectedRow.Cells[12].Value;
                }
                AddProducts formAdd = new AddProducts(this);
                formAdd.SetData(textbox, combobox, hinhAnh);
                formAdd.Show();
                formAdd.Focus();
                formAdd.BringToFront();
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.Value != null)
            {
                DataGridViewImageCell cell = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;
                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }
        // arrange
        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            ArrangePro arrangeCus = new ArrangePro(this);
            arrangeCus.Show();
            arrangeCus.BringToFront();
            arrangeCus.Focus();
        }
        public void SortAndDisplayData(string sortOrder)
        {
            string query = "";
            if (sortOrder == "Từ A  -   Z")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham ASC";
            }
            else if (sortOrder == "Từ Z  -   A")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }
            else if (sortOrder == "Mới nhất")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }
            else if (sortOrder == "Sản phẩm bán chạy")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            } // 
            else if (sortOrder == "Còn hàng từ cao đến thấp")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }
            else if (sortOrder == "Còn hàng từ thấp đến cao")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }
            else if (sortOrder == "Giá từ cao đến thấp")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }
            else if (sortOrder == "Giá từ thấp đến cao")
            {
                query = "SELECT * FROM tblQuanLySanPham_VOGUE ORDER BY tensapnpham DESC";
            }

            if (!string.IsNullOrEmpty(query))
            {
                using (SqlConnection conn = new SqlConnection(connectSQL))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    guna2DataGridView1.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Không thể tạo truy vấn. Vui lòng chọn một loại sắp xếp hợp lệ.");
            }
        }

    }
}
