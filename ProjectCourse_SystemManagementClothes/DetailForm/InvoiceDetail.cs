using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.DetailForm
{
    public partial class InvoiceDetail : Form
    {
        private BLL_SaleInvoiceItem BLL_SaleInvoiceItem = new BLL_SaleInvoiceItem();
        private BLL_SaleInvoice invoice = new BLL_SaleInvoice();
        public InvoiceDetail()
        {
            InitializeComponent();
        }
        private void DisplayInvoice()
        {
            List<DTO_SaleInvoice> dTO_SaleInvoices = invoice.GetAll();
            guna2DataGridView1.DataSource = dTO_SaleInvoices;
            guna2DataGridView1.Columns[0].HeaderText = "Mã hoá đơn";//
            guna2DataGridView1.Columns[1].HeaderText = "Mã khách hàng";
            guna2DataGridView1.Columns[2].HeaderText = "Tên khách hàng";//
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhân viên";
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[5].HeaderText = "Khuyến mãi";//
            guna2DataGridView1.Columns[6].HeaderText = "Giảm giá";//
            guna2DataGridView1.Columns[7].HeaderText = "Vận chuyển";//
            guna2DataGridView1.Columns[8].HeaderText = "Tổng phụ";//
            guna2DataGridView1.Columns[9].HeaderText = "Tổng chính";//
            guna2DataGridView1.Columns[10].HeaderText = "Ngày bán";//

            // visible
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[4].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
        }
        private void InvoiceDetail_Load(object sender, EventArgs e)
        {
            guna2ShadowPanel1.Visible = false;
            DisplayInvoice();
            DisplayDetail();
        }
        private void DisplayDetail()
        {
            if(guna2DataGridView2.SelectedRows.Count > 0)
            {
                guna2DataGridView2.Columns[0].HeaderText = "Mã chi tiết hoá đơn";
                guna2DataGridView2.Columns[1].HeaderText = "Mã hoá đơn";
                guna2DataGridView2.Columns[2].HeaderText = "Mã sản phẩm";
                guna2DataGridView2.Columns[3].HeaderText = "Tên sản phẩm";
                guna2DataGridView2.Columns[4].HeaderText = "Số lượng mua";
                guna2DataGridView2.Columns[5].HeaderText = "Giá bán";
                guna2DataGridView2.Columns[0].Visible = false;
            }    
           
        }
        public void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ShadowPanel1.Visible = true;
            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
            if (e.RowIndex >= 0)
            {
                string maHoaDon = guna2DataGridView1.Rows[e.RowIndex].Cells["MaHoaDonBan"].Value.ToString();
                List<DTO_SaleInvoiceItem> details = BLL_SaleInvoiceItem.GetAll(maHoaDon);
                guna2DataGridView2.DataSource = details;
                DisplayDetail();
                // mã hđ
                guna2HtmlLabel2.Text = selectedRow.Cells[0].Value.ToString();
                // mã kh
                guna2DataGridView1.Columns[1].Visible = false;
                // tên kh
                guna2HtmlLabel5.Text = selectedRow.Cells[2].Value.ToString();
                guna2DataGridView1.Columns[3].Visible = false;
                guna2DataGridView1.Columns[4].Visible = false;
                // khuyến mãi
                guna2HtmlLabel13.Text = selectedRow.Cells[5].Value.ToString();
                // giảm giá
                guna2HtmlLabel15.Text = selectedRow.Cells[6].Value.ToString();
                // phí vchuyen
                guna2HtmlLabel14.Text = selectedRow.Cells[7].Value.ToString();
                // tổng phụ
                guna2HtmlLabel8.Text = selectedRow.Cells[8].Value.ToString();
                // ngày chính
                guna2HtmlLabel17.Text = selectedRow.Cells[9].Value.ToString();
                // ngày bán
                guna2HtmlLabel3.Text = selectedRow.Cells[10].Value.ToString();
            }
        }
    }
}
