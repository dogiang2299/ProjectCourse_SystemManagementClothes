using BLL;
using ProjectCourse_SystemManagementClothes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using ProjectCourse_SystemManagementClothes.DetailForm;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace ProjectCourse_SystemManagementClothes.Subfolder
{
    public partial class Customers : Form
    {
        private ArrangeCus arrangeCus1;
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        BLL_Customers _Customers = new BLL_Customers();
        BLL_CategoryCustomers categoryCustomers = new BLL_CategoryCustomers();
        public Customers()
        {
            InitializeComponent();
            guna2TextBox1_search.Visible = false;
            arrangeCus1 = new ArrangeCus(this);
        }
      
        public void Display()
        {
            List<DTO_Customers> cuss = _Customers.GetAll();
            guna2DataGridView1.DataSource = cuss;
            guna2DataGridView1.Columns[0].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[2].HeaderText = "Ngày sinh";
            guna2DataGridView1.Columns[3].HeaderText = "Giới tính";
            guna2DataGridView1.Columns[4].HeaderText = "Địa chỉ";
            guna2DataGridView1.Columns[5].HeaderText = "Số điện thoại";
            guna2DataGridView1.Columns[6].HeaderText = "Email";
            guna2DataGridView1.Columns[7].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView1.Columns[8].HeaderText = "Danh mục khách hàng";

        }
        private void ShowDanhMuc()
        {
            guna2Panel2.Visible = true;
            guna2DataGridView2.Visible = true;
            DisplayCate();
        }
        private void ShowKH()
        {
            guna2Panel2_add.Visible = true;
            guna2DataGridView1.Visible = true;
            guna2Panel2.Visible = false;
            guna2DataGridView2.Visible = false;
            Display();
        }
        private void Customers_Load(object sender, EventArgs e)
        {
            ShowDanhMuc();
            ShowKH();
        }
        public void DisplayCate()
        {
            List<DTO_CategoryCustomers> cuss = categoryCustomers.GetAll();
            guna2DataGridView2.DataSource = cuss;
            guna2DataGridView2.Columns[0].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView2.Columns[1].HeaderText = "Tên danh mục khách hàng";
        }
        // nhomkhachhang
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowDanhMuc();
        }
        // khachhang
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ShowKH();
        }
        // thêm khách hàng
        private void guna2ImageButton1_search_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1_search.Visible == false)
            {
                guna2TextBox1_search.Visible = true;
                
            }

        }
       
        // addcus
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            AddCustomers addCustomers = new AddCustomers(this);
            addCustomers.Show();
            addCustomers.BringToFront();
            Display();

        }
        // addcate
        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            AddCategoryCustomers customers = new AddCategoryCustomers(this);
            customers.Show();
            DisplayCate();
        }
        // hiển thị textbox ở form AddCus
      
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < guna2DataGridView1.Columns.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                    string[] textbox = new string[8];
                    for (int i = 0; i < 8; i++)
                    {
                        textbox[i] = selectedRow.Cells[i].Value.ToString();
                    }
                    string combobox = selectedRow.Cells[8].Value.ToString();
                    AddCustomers formAdd = new AddCustomers(this);
                    formAdd.SetData(textbox, combobox);
                    formAdd.Show();
                    formAdd.BringToFront();
                    formAdd.Focus();
                }
                
                 }
            }
        private void SearchCus()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_Customers> search = _Customers.Search(keyword);
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = search;
            guna2DataGridView1.Columns[0].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[2].HeaderText = "Ngày sinh";
            guna2DataGridView1.Columns[3].HeaderText = "Giới tính";
            guna2DataGridView1.Columns[4].HeaderText = "Địa chỉ";
            guna2DataGridView1.Columns[5].HeaderText = "Số điện thoại";
            guna2DataGridView1.Columns[6].HeaderText = "Email";
            guna2DataGridView1.Columns[7].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView1.Columns[8].HeaderText = "Danh mục khách hàng";
            guna2DataGridView1.DataSource = search;
        }
        private void SearchCat()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_CategoryCustomers> search = categoryCustomers.Search(keyword);
            guna2DataGridView2.DataSource = null;
            guna2DataGridView2.DataSource = search;
            guna2DataGridView2.Columns[0].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView2.Columns[1].HeaderText = "Tên danh mục khách hàng";
            guna2DataGridView2.DataSource = search;
        }
        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            if(keyword.Length >= 1 || string.IsNullOrEmpty(keyword))
            {
                SearchCus();
                SearchCat();
            }  
            
        }

        private void guna2ImageButton2_arrange_Click(object sender, EventArgs e)
        {
            ArrangeCus arrangeCus = new ArrangeCus(this);
            arrangeCus.Show();
            arrangeCus.BringToFront();
            arrangeCus.Focus();
        }

        public void SortAndDisplayData(string sortOrder)
        {
            string query = "";
            if (sortOrder == "Từ A  -   Z")
            {
                query = "SELECT * FROM tbl_QuanLyKhachHang ORDER BY tenkhachhang ASC";
            }
            else if (sortOrder == "Từ Z  -   A")
            {
                query = "SELECT * FROM tbl_QuanLyKhachHang ORDER BY tenkhachhang DESC";
            }
            else if (sortOrder == "Theo ngày tạo khách hàng")
            {
                query = "SELECT * FROM tbl_QuanLyKhachHang ORDER BY ngaytao ASC"; // Adjust column name if needed
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

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < guna2DataGridView1.Columns.Count)
            {
                
                if (e.ColumnIndex == 1)
                {
                    DataGridViewRow selectedRow = guna2DataGridView2.Rows[e.RowIndex];
                    string[] textbox = new string[2];
                    for (int i = 0; i < 2; i++)
                    {
                        textbox[i] = selectedRow.Cells[i].Value.ToString();
                    }
                    AddCategoryCustomers formAdd = new AddCategoryCustomers(this);
                    formAdd.SetData(textbox);
                    formAdd.Show();
                    formAdd.BringToFront();
                    formAdd.Focus();
                }

            }
        }
    }
}
