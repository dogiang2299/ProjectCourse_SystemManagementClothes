using BLL;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using ProjectCourse_SystemManagementClothes.Models;
using ProjectCourse_SystemManagementClothes.Warehouse;
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

namespace ProjectCourse_SystemManagementClothes.Subfolder
{
    public partial class ImportBook : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_ImportBook bLL_Import = new BLL_ImportBook();
        private BLL_ImportBookItem bLL_Import_item = new BLL_ImportBookItem();
        private List<DTO_Products> products = new List<DTO_Products>();
        private DTO_ImportBook newImport = new DTO_ImportBook();
        private Products products1 = new Products();
        public ImportBook()
        {
            InitializeComponent();
            guna2Panel1.Visible = true;
            txtser.Visible = false;

        }
        private BLL_Products _Products = new BLL_Products();
        
        public void DisplayImportBook()
        {
            List<DTO_ImportBook> dTO_ImportBooks = bLL_Import.GetAll();
            guna2DataGridView1.DataSource = dTO_ImportBooks;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhập kho";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[5].HeaderText = "Tổng tiền nhập";
            guna2DataGridView1.Columns[6].HeaderText = "Ngày nhập kho";

            // visble


        }
        private void ImportBook_Load(object sender, EventArgs e)
        {
            DisplayImportBook();
        }
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            txtser.Visible = true;
        }
       
        private void guna2Button4_Click_2(object sender, EventArgs e)
        {
            AddImportBook addImportBook = new AddImportBook(this);
            addImportBook.Show();
        }

        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = txtser.Text.Trim(); 
            List<DTO_ImportBook> searchResult = bLL_Import.SearchImportBooks(keyword);
            guna2DataGridView1.DataSource = searchResult;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhập kho";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[5].HeaderText = "Tổng tiền nhập";
            guna2DataGridView1.Columns[6].HeaderText = "Ngày nhập kho";
        }

        private void guna2Button1_today_Click(object sender, EventArgs e)
        {
            List<DTO_ImportBook> dTO_ImportBooks = bLL_Import.GetToDay();
            guna2DataGridView1.DataSource = dTO_ImportBooks;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhập kho";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[5].HeaderText = "Tổng tiền nhập";
            guna2DataGridView1.Columns[6].HeaderText = "Ngày nhập kho";

        }

        private void guna2Button2_thismoth_Click(object sender, EventArgs e)
        {
            List<DTO_ImportBook> dTO_ImportBooks = bLL_Import.GetThismotnh();
            guna2DataGridView1.DataSource = dTO_ImportBooks;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhập kho";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[5].HeaderText = "Tổng tiền nhập";
            guna2DataGridView1.Columns[6].HeaderText = "Ngày nhập kho";
        }

        private void guna2Button3_lastmonth_Click(object sender, EventArgs e)
        {
            List<DTO_ImportBook> dTO_ImportBooks = bLL_Import.GetLastmonth();
            guna2DataGridView1.DataSource = dTO_ImportBooks;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhập kho";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[5].HeaderText = "Tổng tiền nhập";
            guna2DataGridView1.Columns[6].HeaderText = "Ngày nhập kho";
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maNhapKho = guna2DataGridView1.Rows[e.RowIndex].Cells["MaNhapKho"].Value.ToString();
                List<DTO_ImportBookItem> chiTietNhapKhos = bLL_Import_item.GetAllChiTietNhapKho(maNhapKho);
                guna2DataGridView2.DataSource = chiTietNhapKhos;
                guna2DataGridView2.Columns[0].HeaderText = "Mã chi tiết nhập kho";
                guna2DataGridView2.Columns[1].HeaderText = "Mã nhập kho";
                guna2DataGridView2.Columns[2].HeaderText = "Mã sản phẩm";
                guna2DataGridView2.Columns[3].HeaderText = "Tên sản phẩm";
                guna2DataGridView2.Columns[4].HeaderText = "Số lượng tồn kho";
                guna2DataGridView2.Columns[5].HeaderText = "Số lượng nhập kho";
                guna2DataGridView2.Columns[6].HeaderText = "Đơn giá nhập";
            }
        }
        // delete
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog3.Show("Bạn có muốn xoá phiếu nhập kho này không?");
            if (result == DialogResult.Yes)
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    string maNhapKho = guna2DataGridView1.SelectedRows[0].Cells["MaNhapKho"].Value.ToString();
                    bLL_Import.Delete(maNhapKho);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phiếu nhập kho để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
