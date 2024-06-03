using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Models;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
namespace ProjectCourse_SystemManagementClothes.Mainfolder
{
    public partial class Employer : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        BLL_Employer _employer = new BLL_Employer();
        public Employer()
        {
            InitializeComponent();
            guna2TextBox1_search.Visible = false;
        }
        public void Display()
        {
            
            List<DTO_Employer> emp = _employer.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[1].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Số điện thoại";
            guna2DataGridView1.Columns[3].HeaderText = "Ngày sinh";
            guna2DataGridView1.Columns[4].HeaderText = "Giới tính";
            guna2DataGridView1.Columns[5].HeaderText = "Địa chỉ";
            guna2DataGridView1.Columns[6].HeaderText = "Email";
            guna2DataGridView1.Columns[7].HeaderText = "Tên chức vụ";

        }

        private void Employer_Load(object sender, EventArgs e)
        {
                Display();
          
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            
            if(guna2TextBox1_search.Visible == false)
            {
                guna2TextBox1_search.Visible = true;
            }    
        }

        private void guna2ImageButton1_add_Click(object sender, EventArgs e)
        {
            AddEmployer addEmployer = new AddEmployer(this);
            addEmployer.Show();
            addEmployer.BringToFront();
            Display();
        }

        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = guna2TextBox1_search.Text.Trim().ToLower();
            if (keyword.Length >= 1 || string.IsNullOrEmpty(keyword))
            {
                List<DTO_Employer> search = _employer.Search(keyword);
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = search;
                guna2DataGridView1.Columns[0].HeaderText = "Mã nhân viên";
                guna2DataGridView1.Columns[1].HeaderText = "Tên nhân viên";
                guna2DataGridView1.Columns[2].HeaderText = "Số điện thoại";
                guna2DataGridView1.Columns[3].HeaderText = "Ngày sinh";
                guna2DataGridView1.Columns[4].HeaderText = "Giới tính";
                guna2DataGridView1.Columns[5].HeaderText = "Địa chỉ";
                guna2DataGridView1.Columns[6].HeaderText = "Email";
                guna2DataGridView1.Columns[7].HeaderText = "Tên chức vụ";
                guna2DataGridView1.DataSource = search;
            }
            else
            {
                Display();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                string[] textbox = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    textbox[i] = selectedRow.Cells[i].Value.ToString();
                }
                string[] comboboxes = new string[1];
                comboboxes[0] = selectedRow.Cells[7].Value.ToString();
                AddEmployer formAdd = new AddEmployer(this);
                formAdd.SetData(textbox, comboboxes);
                formAdd.Show();
                formAdd.BringToFront();
                formAdd.Focus();

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
    }
}
