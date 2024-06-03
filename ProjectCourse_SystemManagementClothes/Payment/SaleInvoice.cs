using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.Order
{
    public partial class SaleInvoice : Form
    {
        private BLL_SaleInvoice invoice = new BLL_SaleInvoice();
        private BLL_SaleInvoiceItem item = new BLL_SaleInvoiceItem();
        public SaleInvoice()
        {
            InitializeComponent();
        }
        private void DisplayInvoice()
        {
            List<DTO_SaleInvoice> dTO_Sale = invoice.GetAll();
            guna2DataGridView1.DataSource = dTO_Sale;
            guna2DataGridView1.Columns[0].HeaderText = "Mã hoá đơn bán";
            guna2DataGridView1.Columns[1].HeaderText = "Mã khách hàng";//
            guna2DataGridView1.Columns[2].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhân viên";//
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[5].HeaderText = "Khuyến mãi";//
            guna2DataGridView1.Columns[6].HeaderText = "Giảm giá";//
            guna2DataGridView1.Columns[7].HeaderText = "Vận chuyển";//
            guna2DataGridView1.Columns[8].HeaderText = "Tổng phụ";
            guna2DataGridView1.Columns[9].HeaderText = "Tổng chính";
            guna2DataGridView1.Columns[10].HeaderText = "Ngày bán";//

            // visble
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;

        }
       
        private void SaleInvoice_Load(object sender, EventArgs e)
        {
            DisplayInvoice();
            guna2Panel3.Visible = false;
        }
        public void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                guna2Panel3.Visible = true;
                string mahoadon = guna2DataGridView1.Rows[e.RowIndex].Cells["MaHoaDonBan"].Value.ToString();
                List<DTO_SaleInvoiceItem> chiTietNhapKhos = item.GetAll(mahoadon);
                guna2DataGridView2.DataSource = chiTietNhapKhos;
                guna2DataGridView2.Columns[0].HeaderText = "Mã chi tiết hoá đơn";
                guna2DataGridView2.Columns[1].HeaderText = "Mã hoá đơn";
                guna2DataGridView2.Columns[2].HeaderText = "Mã sản phẩm";
                guna2DataGridView2.Columns[3].HeaderText = "Tên sản phẩm";
                guna2DataGridView2.Columns[4].HeaderText = "Số lượng mua";
                guna2DataGridView2.Columns[5].HeaderText = "Đơn giá";
                // vis
                guna2DataGridView2.Columns[1].Visible = false;

                string mahoadonban = guna2DataGridView1.Rows[e.RowIndex].Cells["MaHoaDonBan"].Value.ToString();
                string tenkhachhang = guna2DataGridView1.Rows[e.RowIndex].Cells["TenKhach"].Value.ToString();
                int tongPhu = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["TongPhu"].Value);
                int vanchuyen = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["VanChuyen"].Value);
                int ggia = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["GiamGia"].Value);
                int km = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["KhuyenMai"].Value);
                int tchinh = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["TongChinh"].Value);
                DateTime ngayban = Convert.ToDateTime(guna2DataGridView1.Rows[e.RowIndex].Cells["NgayBan"].Value);
                guna2HtmlLabel4.Text =  mahoadonban;
                guna2HtmlLabel5.Text = tenkhachhang;
                guna2HtmlLabel15.Text = tongPhu.ToString();
                guna2HtmlLabel17.Text = vanchuyen.ToString();
                guna2HtmlLabel18.Text =  ggia.ToString();
                guna2HtmlLabel16.Text =  km.ToString();
                guna2HtmlLabel14.Text =  tchinh.ToString("N0") + " VNĐ";
                guna2HtmlLabel8.Text = ngayban.ToString("dd/MM/yyyy"); ;

            }
        }

        private void guna2Button1_today_Click(object sender, EventArgs e)
        {
            List<DTO_SaleInvoice> dTO_Sale = invoice.GetToDay();
            guna2DataGridView1.DataSource = dTO_Sale;
            guna2DataGridView1.Columns[0].HeaderText = "Mã hoá đơn bán";
            guna2DataGridView1.Columns[1].HeaderText = "Mã khách hàng";//
            guna2DataGridView1.Columns[2].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhân viên";//
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[5].HeaderText = "Khuyến mãi";//
            guna2DataGridView1.Columns[6].HeaderText = "Giảm giá";//
            guna2DataGridView1.Columns[7].HeaderText = "Vận chuyển";//
            guna2DataGridView1.Columns[8].HeaderText = "Tổng phụ";
            guna2DataGridView1.Columns[9].HeaderText = "Tổng chính";
            guna2DataGridView1.Columns[10].HeaderText = "Ngày bán";//

            // visble
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
        }

        private void guna2Button2_thismoth_Click(object sender, EventArgs e)
        {
            List<DTO_SaleInvoice> dTO_Sale = invoice.GetThismotnh();
            guna2DataGridView1.DataSource = dTO_Sale;
            guna2DataGridView1.Columns[0].HeaderText = "Mã hoá đơn bán";
            guna2DataGridView1.Columns[1].HeaderText = "Mã khách hàng";//
            guna2DataGridView1.Columns[2].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhân viên";//
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[5].HeaderText = "Khuyến mãi";//
            guna2DataGridView1.Columns[6].HeaderText = "Giảm giá";//
            guna2DataGridView1.Columns[7].HeaderText = "Vận chuyển";//
            guna2DataGridView1.Columns[8].HeaderText = "Tổng phụ";
            guna2DataGridView1.Columns[9].HeaderText = "Tổng chính";
            guna2DataGridView1.Columns[10].HeaderText = "Ngày bán";//

            // visble
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
        }

        private void guna2Button3_lastmonth_Click(object sender, EventArgs e)
        {
            List<DTO_SaleInvoice> dTO_Sale = invoice.GetLastmonth();
            guna2DataGridView1.DataSource = dTO_Sale;
            guna2DataGridView1.Columns[0].HeaderText = "Mã hoá đơn bán";
            guna2DataGridView1.Columns[1].HeaderText = "Mã khách hàng";//
            guna2DataGridView1.Columns[2].HeaderText = "Tên khách hàng";
            guna2DataGridView1.Columns[3].HeaderText = "Mã nhân viên";//
            guna2DataGridView1.Columns[4].HeaderText = "Tên nhân viên";
            guna2DataGridView1.Columns[5].HeaderText = "Khuyến mãi";//
            guna2DataGridView1.Columns[6].HeaderText = "Giảm giá";//
            guna2DataGridView1.Columns[7].HeaderText = "Vận chuyển";//
            guna2DataGridView1.Columns[8].HeaderText = "Tổng phụ";
            guna2DataGridView1.Columns[9].HeaderText = "Tổng chính";
            guna2DataGridView1.Columns[10].HeaderText = "Ngày bán";//

            // visble
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[3].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[6].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }
    }
}
