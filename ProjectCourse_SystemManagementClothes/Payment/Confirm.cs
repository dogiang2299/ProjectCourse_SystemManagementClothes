using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace ProjectCourse_SystemManagementClothes.Subfolder
{
    public partial class Confirm : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_SaleInvoice salesInvoice = new BLL_SaleInvoice();
        private List<Products> selectedProducts = new List<Products>();
        public void UpdateSelectedProducts(List<Products> products)
        {
            selectedProducts = products;
        }
        private void DisplayEmp()
        {
            List<DTO_Employer> products = new List<DTO_Employer>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                con.Open();
                string query = "SELECT MaNhanVien, TenNhanVien  FROM tbl_Quan_Ly_Nhan_Vien";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Employer DTO_Customers = new DTO_Employer()
                    {
                        MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                        TenNhanVien = reader["TenNhanVien"].ToString(),

                    };
                    products.Add(DTO_Customers);
                }
            }
            manhanvien.DataSource = products;
            manhanvien.DisplayMember = "MaNhanVien";
            manhanvien.ValueMember = "MaNhanVien";
        }
        private void DisplayCus()
        {
            List<DTO_Customers> products = new List<DTO_Customers>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                con.Open();
                string query = "SELECT makhachhang, tenkhachhang, ngaysinh, diachi, sdthoai, tendanhmuckhachhang FROM tbl_QuanLyKhachHang";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Customers DTO_Customers = new DTO_Customers()
                    {
                        makhachhang = reader.GetInt32(reader.GetOrdinal("makhachhang")),
                        tenkhachhang = reader["tenkhachhang"].ToString(),
                        ngaysinh = reader["ngaysinh"].ToString(),
                        diachi = reader["diachi"].ToString(),
                        sdthoai = reader["sdthoai"].ToString(),
                        danhmuckhachhang = reader["tendanhmuckhachhang"].ToString(),
                    };
                    products.Add(DTO_Customers);
                }
            }
            makhachhang.DataSource = products;
            makhachhang.DisplayMember = "makhachhang";
            makhachhang.ValueMember = "makhachhang";
        }
        public Confirm()
        {
            InitializeComponent();
            guna2DataGridView1.Columns.Add("Column1", "Mã sản phẩm");
            guna2DataGridView1.Columns.Add("Column2", "Tên sản phẩm");
            guna2DataGridView1.Columns.Add("Column3", "Số lượng tồn kho");
            guna2DataGridView1.Columns.Add("Column4", "Giá bán");
            guna2DataGridView1.Columns.Add("Column5", "Kích thước");
            guna2DataGridView1.Columns.Add("Column6", "Số lượng mua");
            guna2DataGridView1.Columns["Column3"].Visible = false;
            guna2DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            guna2DataGridView1.AllowUserToAddRows = false;
          
            guna2DataGridView1.CellEndEdit += new DataGridViewCellEventHandler(guna2DataGridView1_CellEndEdit);
            DisplayEmp();
        }
        private int tongchinh;
        private void DisplayLable()
        {
            #region số lượng
            int totalQuantity = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Column6"].Value != null)
                {
                    int quantity;
                    if (int.TryParse(row.Cells["Column6"].Value.ToString(), out quantity))
                    {
                        totalQuantity += quantity;
                    }
                }
            }
            guna2TextBox1_soluong.Text = totalQuantity.ToString();
            #endregion

           
           
        }
        public void SetData(string[] data)
        {
            if (data != null && data.Length == 6)
            {
                bool productExists = false;
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.Cells["Column1"].Value != null && row.Cells["Column1"].Value.ToString() == data[0])
                    {
                        row.Cells["Column6"].Value = data[5];
                        productExists = true;
                        break;
                    }
                }
                if (!productExists)
                {
                    guna2DataGridView1.Rows.Add(data); 
                }

                DisplayToTal(); 
            }
            else
            {
                MessageBox.Show("Invalid data received.");
            }
        }
        private int thanhtien = 0;
        private void Confirm_Load(object sender, EventArgs e)
        {
            DisplayLable();
            DisplayCus();
            //DisplayEmp();
        }
        public int tongtienatca;
        #region DisplayToTal
        private void DisplayToTal()
        {
            thanhtien = 0; // Reset tổng số tiền trước khi tính toán lại

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Column4"].Value != null && row.Cells["Column6"].Value != null)
                {
                    int price;
                    int quantity;
                    if (int.TryParse(row.Cells["Column4"].Value.ToString(), out price) &&
                        int.TryParse(row.Cells["Column6"].Value.ToString(), out quantity))
                    {
                        int amount = price * quantity;
                        thanhtien += amount;
                    }
                }
            }
            tongtienatca = thanhtien;
            guna2TextBox6.Text = thanhtien.ToString();
            guna2TextBox13.Text = thanhtien.ToString();
            #region Tổng phụ
            guna2HtmlLabel11.Text = thanhtien.ToString("#,##0") + " VNĐ";
            #endregion
        }
        #endregion
        public string LabelText // Tạo một thuộc tính để lấy giá trị của Label
        {
            get { return guna2HtmlLabel11.Text; }
        }
        private void guna2DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Column6"].Index)
            {
                DisplayToTal();
                DisplayLable();
            }
        }
        #region UP
        private void Up()
        {
            // Initialize variables
            int giamgia = 0;
            int vanchuyen = 0;
            int khuyenmai = 0;

            // Check if the textboxes have values, if not keep the variables as 0
            if (!string.IsNullOrEmpty(guna2TextBox9.Text))
            {
                int.TryParse(guna2TextBox9.Text, out giamgia);
            }

            if (!string.IsNullOrEmpty(guna2TextBox10.Text))
            {
                int.TryParse(guna2TextBox10.Text, out vanchuyen);
            }

            if (!string.IsNullOrEmpty(guna2TextBox11.Text))
            {
                int.TryParse(guna2TextBox11.Text, out khuyenmai);
            }

            // Calculate tongchinh
            int tempTongChinh = thanhtien - (giamgia + khuyenmai) + vanchuyen;
            guna2TextBox13.Text = tempTongChinh.ToString();
        }
        #endregion

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {
            Up();
        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {
            Up();
        }

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {
            Up();
        }

        private void guna2TextBox13_TextChanged(object sender, EventArgs e)
        {
            Up();
        }
        private string mahoadon;
        // tạo hoá đơn
        private void guna2Button4_Click(object sender, EventArgs e)
        {
                // Mã khách hàng và các thông tin khác đã được lấy từ form
                int maKhachHang;
                if (!int.TryParse(makhachhang.SelectedValue.ToString(), out maKhachHang))
                {
                    MessageBox.Show("Mã khách hàng không hợp lệ.");
                    return;
                }
                int maNhanVien;
                if (!int.TryParse(manhanvien.SelectedValue.ToString(), out maNhanVien))
                {
                    MessageBox.Show("Mã nhân viên không hợp lệ.");
                    return;
                }
                string tenKhachHang = tenkhachhang.Text;
                string tenNhanVien = tennhanvien.Text;
                int giamGia = int.TryParse(guna2TextBox9.Text, out giamGia) ? giamGia : 0;
                int vanChuyen = int.TryParse(guna2TextBox10.Text, out vanChuyen) ? vanChuyen : 0;
                int khuyenMai = int.TryParse(guna2TextBox11.Text, out khuyenMai) ? khuyenMai : 0;
                int tongPhu = int.TryParse(guna2TextBox6.Text, out tongPhu) ? tongPhu : 0;
                int tongChinh = int.TryParse(guna2TextBox13.Text, out tongChinh) ? tongChinh : 0;
                DateTime ngayBan = DateTime.Now;

                List<DTO_SaleInvoiceItem> chiTietHoaDon = new List<DTO_SaleInvoiceItem>();
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    int maSP = int.Parse(row.Cells["Column1"].Value.ToString());
                    string tenSP = row.Cells["Column2"].Value.ToString();
                    int soLuongMua = int.Parse(row.Cells["Column6"].Value.ToString());
                    int giaBan = int.Parse(row.Cells["Column4"].Value.ToString());
                    var item = new DTO_SaleInvoiceItem
                    {
                        MaSP = maSP,
                        TenSP = tenSP,
                        SoLuongMua = soLuongMua,
                        GiaBan = giaBan,
                    };

                    chiTietHoaDon.Add(item);
                }

                var hoaDonChinh = new DTO_SaleInvoice
                {
                    MaKhach = maKhachHang,
                    TenKhach = tenKhachHang,
                    MaNV = maNhanVien,
                    TenNV = tenNhanVien,
                    KhuyenMai = khuyenMai,
                    GiamGia = giamGia,
                    VanChuyen = vanChuyen,
                    TongPhu = tongPhu,
                    TongChinh = tongChinh,
                    NgayBan = ngayBan
                };

                salesInvoice.CreateHoaDonBan(hoaDonChinh, chiTietHoaDon);

                guna2MessageDialog1.Show("Hóa đơn bán đã được tạo thành công! Mã hóa đơn: " + hoaDonChinh.MaHoaDonBan);

                Invoice invoiceForm = new Invoice();
                invoiceForm.LoadData(hoaDonChinh); // Hiển thị thông tin hóa đơn trên form Invoice
                invoiceForm.LoadDaGrd(hoaDonChinh.MaHoaDonBan);
                invoiceForm.Show();
           
        }
    
        private bool isUpdating = false;
        private void makhachhang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!isUpdating && makhachhang.SelectedItem is DTO_Customers dTO_Customers)
            {
                isUpdating = true;
                tenkhachhang.Text = dTO_Customers.tenkhachhang;
                sodienthoai.Text = dTO_Customers.sdthoai;
                diachi.Text = dTO_Customers.diachi;
                danhmuckhachhang.Text = dTO_Customers.danhmuckhachhang;
                isUpdating = false;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isUpdating && manhanvien.SelectedItem is DTO_Employer dTO_Customers)
            {
                isUpdating = true;
                tennhanvien.Text = dTO_Customers.TenNhanVien;
                isUpdating = false;
            }
        }
        
        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Prepaymoney prepaymoney = new Prepaymoney(this);
            prepaymoney.LabelTextFromConfirmForm = this.LabelText;
            prepaymoney.Show();
        }
    }
}
