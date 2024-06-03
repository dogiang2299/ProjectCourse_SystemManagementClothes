using BLL;
using DTO;
using Guna.UI2.WinForms;
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

namespace ProjectCourse_SystemManagementClothes.Subfolder
{
    public partial class CategoryCustomers : Form
    {
        private Customers cus;
        private DTO_CategoryCustomers categoryCustomers = new DTO_CategoryCustomers();
        private BLL_CategoryCustomers customers = new BLL_CategoryCustomers();
        public CategoryCustomers(Customers cus)
        {
            InitializeComponent();
            this.cus = cus;
        }
        // add
        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {
            AddCategoryCustomers addCategoryCustomers = new AddCategoryCustomers(cus);
            addCategoryCustomers.Show();
        }
        public void DisplayCate()
        {
            List<DTO_CategoryCustomers> cuss = customers.GetAll();
            guna2DataGridView1.DataSource = cuss;
            guna2DataGridView1.Columns[0].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Tên danh mục khách hàng";
        }
        private void CategoryCustomers_Load(object sender, EventArgs e)
        {
            DisplayCate();
            guna2TextBox1_search.Visible = false;

        }
        private void SearchCat()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_CategoryCustomers> search = customers.Search(keyword);
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = search;
            guna2DataGridView1.Columns[0].HeaderText = "Mã danh mục khách hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Tên danh mục khách hàng";
            guna2DataGridView1.DataSource = search;
        }
        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            if (keyword.Length >= 1 || string.IsNullOrEmpty(keyword))
            {
                SearchCat();
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            guna2TextBox1_search.Visible = true;
        }
    }
}
