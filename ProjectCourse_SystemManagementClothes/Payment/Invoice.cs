using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using ProjectCourse_SystemManagementClothes.DetailForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace ProjectCourse_SystemManagementClothes.Subfolder
{
    public partial class Invoice : Form
    {
        private BLL_SaleInvoiceItem invoiceItem = new BLL_SaleInvoiceItem();
        private BLL_SaleInvoice invoice1;

        private List<DTO_SaleInvoiceItem> items = new List<DTO_SaleInvoiceItem>();
        public Invoice()
        {
            InitializeComponent();
            
        }
        public Invoice(string mahoadon)
        {
            invoice1 = new BLL_SaleInvoice();
            InitializeComponent();
            LoadDaGrd(mahoadon);
        }
        private DTO_SaleInvoice invoice = new DTO_SaleInvoice();
      
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";   
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            var result = confirm.ShowDialog();

            // Nếu người dùng chọn "Yes" trong hộp thoại xác nhận, đóng cả form chính và form xác nhận
            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form chính
                confirm.Close(); // Đóng form xác nhận
            }
        }
        public void LoadData(DTO_SaleInvoice invoice)
        {
            guna2HtmlLabel30.Text = invoice.MaHoaDonBan;
            guna2HtmlLabel6.Text = invoice.TenKhach;
           
            guna2HtmlLabel9.Text = invoice.NgayBan.ToString();
            guna2HtmlLabel24.Text = invoice.TongPhu.ToString();
            guna2HtmlLabel25.Text = invoice.GiamGia.ToString();
            guna2HtmlLabel26.Text = invoice.KhuyenMai.ToString();
            guna2HtmlLabel27.Text = invoice.VanChuyen.ToString();
            guna2HtmlLabel28.Text = invoice.TongChinh.ToString();

        }
        public void LoadDaGrd(string mahoadon)
        {
            try
            {
                List<DTO_SaleInvoiceItem> invoiceItems = invoiceItem.GetAll(mahoadon);

                guna2DataGridView1.DataSource = invoiceItems;

                // Thiết lập tiêu đề các cột
                guna2DataGridView1.Columns[0].HeaderText = "Mã chi tiết hoá đơn bán";
                guna2DataGridView1.Columns[1].HeaderText = "Mã hoá đơn bán";
                guna2DataGridView1.Columns[2].HeaderText = "Mã sản phẩm";
                guna2DataGridView1.Columns[3].HeaderText = "Tên sản phẩm";
                guna2DataGridView1.Columns[4].HeaderText = "Số lượng mua";
                guna2DataGridView1.Columns[5].HeaderText = "Giá bán";
                guna2DataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã có lỗi xảy ra khi tải dữ liệu: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Show();
        }
    }

}
