using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Mainfolder;
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
    public partial class WarehouseBook : Form
    {
        private BLL_Products _Products = new BLL_Products();
        private List<DTO_Products> products = new List<DTO_Products>();
        private BLL_Warehouse bLL = new BLL_Warehouse();
        private BLL_WarehouseItem item = new BLL_WarehouseItem();
        private DTO_Warehouse newImport = new DTO_Warehouse();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public WarehouseBook()
        {
            InitializeComponent();
           
        }
        public void Warehouse()
        {
            List<DTO_Warehouse> emp = bLL.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã xuất hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[4].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[5].HeaderText = "Ngày xuất kho";
        }



        private void WarehouseBook_Load(object sender, EventArgs e)
        {
            txtser.Enabled = false;
            Warehouse();
        }
      
        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {
            txtser.Enabled = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddWarehouseBook addWarehouseBook = new AddWarehouseBook();
            addWarehouseBook.Show();
        }

        private void txtser_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = txtser.Text.Trim();
            List<DTO_Warehouse> DTO_Warehouses = bLL.SearchImportBooks(keyword);
            guna2DataGridView1.DataSource = DTO_Warehouses;
            guna2DataGridView1.Columns[0].HeaderText = "Mã xuất hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[4].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[5].HeaderText = "Ngày xuất kho";

        }

        private void guna2Button1_today_Click(object sender, EventArgs e)
        {
            List<DTO_Warehouse> DTO_Warehouses = bLL.GetToDay();
            guna2DataGridView1.DataSource = DTO_Warehouses;
            guna2DataGridView1.Columns[0].HeaderText = "Mã xuất hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[4].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[5].HeaderText = "Ngày xuất kho";
        }

        private void guna2Button2_thismoth_Click(object sender, EventArgs e)
        {
            List<DTO_Warehouse> DTO_Warehouses = bLL.GetThismotnh();
            guna2DataGridView1.DataSource = DTO_Warehouses;
            guna2DataGridView1.Columns[0].HeaderText = "Mã xuất hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[4].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[5].HeaderText = "Ngày xuất kho";
        }

        private void guna2Button3_lastmonth_Click(object sender, EventArgs e)
        {
            List<DTO_Warehouse> DTO_Warehouses = bLL.GetLastmonth();
            guna2DataGridView1.DataSource = DTO_Warehouses;
            guna2DataGridView1.Columns[0].HeaderText = "Mã xuất hàng";
            guna2DataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[2].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[3].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[4].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[5].HeaderText = "Ngày xuất kho";
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog3.Show("Bạn có muốn xoá phiếu xuất kho này không?");
            if (result == DialogResult.Yes)
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    string maNhapKho = guna2DataGridView1.SelectedRows[0].Cells["MaXuatKho"].Value.ToString();
                    bLL.Delete(maNhapKho);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phiếu nhập kho để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maNhapKho = guna2DataGridView1.Rows[e.RowIndex].Cells["MaNhapKho"].Value.ToString();
                List<DTO_WarehouseItem> chiTietNhapKhos = item.GetAllChiTietXuatKho(maNhapKho);
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
    }
}
    


