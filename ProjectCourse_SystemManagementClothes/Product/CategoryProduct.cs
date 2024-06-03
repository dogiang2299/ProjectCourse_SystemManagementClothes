using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Mainfolder;
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
    public partial class CategoryProduct : Form
    {
        private BLL_CategoryProducts productss = new BLL_CategoryProducts();
        private Products products;
        public CategoryProduct()
        {
            InitializeComponent();
        }
        // add
        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {
            AddCategoryProduct addcategoryproduct = new AddCategoryProduct(products);
            addcategoryproduct.Show();
        }
        public void Display1()
        {
            List<DTO_CategoryProducts> cus = productss.GetAll();
            guna2DataGridView1.DataSource = cus;
            guna2DataGridView1.Columns[0].HeaderText = "Mã danh mục sản phẩm";
            guna2DataGridView1.Columns[1].HeaderText = "Tên danh mục sản phẩm";
            guna2DataGridView1.Columns[2].HeaderText = "Mô tả danh mục";
            guna2DataGridView1.Columns[3].HeaderText = "Số lượng sản phẩm";
        }
        private void CategoryProduct_Load(object sender, EventArgs e)
        {
            Display1();
        }
        private void SearchTextBoxDM()
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            List<DTO_CategoryProducts> search = productss.Search(keyword);
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = search;
            guna2DataGridView1.Columns[0].HeaderText = "Mã danh mục";
            guna2DataGridView1.Columns[1].HeaderText = "Tên danh mục";
            guna2DataGridView1.Columns[2].HeaderText = "Mô tả danh mục";
            guna2DataGridView1.Columns[3].HeaderText = "Số lượng";

            guna2DataGridView1.DataSource = search;
        }
        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchTextBoxDM();
        }
    }
}
