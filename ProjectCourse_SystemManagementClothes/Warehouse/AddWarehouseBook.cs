using BLL;
using DTO;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using ProjectCourse_SystemManagementClothes.Subfolder;
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
using static Guna.UI2.Native.WinApi;

namespace ProjectCourse_SystemManagementClothes.Warehouse
{
    public partial class AddWarehouseBook : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_Warehouse warehouse = new BLL_Warehouse();
        private WarehouseBook book = new WarehouseBook();
        private DTO_Products products = new DTO_Products();
        private BLL_Products _Products = new BLL_Products();

        public AddWarehouseBook()
        {
            InitializeComponent();
        }
        // tạo mới
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tenkhachhang.Text = "";
            tennhanvien.Text = "";
            soluongtonkho.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            soluongxuatkho.Text = "";
        }
        public void DisplayImport()
        {
            List<DTO_Products> emp = _Products.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";//
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";//
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";//
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";//
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";//
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";//
            guna2DataGridView1.Columns[13].HeaderText = "Mã danh mục sản phẩm";//
            // VISIBLE
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[9].Visible = false;
            guna2DataGridView1.Columns[12].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
            guna2DataGridView1.Columns[11].Visible = false;
            guna2DataGridView1.Columns[13].Visible = false;
        }
        // xuất kho
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soluongxuatkho.Text) || string.IsNullOrEmpty(tenkhachhang.Text) || string.IsNullOrEmpty(makhachhang.Text) || string.IsNullOrEmpty(guna2TextBox4.Text))
            {
                guna2MessageDialog1.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
                return;
            }

            try
            {
                if (!int.TryParse(guna2TextBox2.Text, out int maSanPham))
                {
                    throw new FormatException("Mã sản phẩm không đúng định dạng.");
                }
                if (!int.TryParse(guna2TextBox4.Text, out int donGiaXuat))
                {
                    throw new FormatException("Đơn giá xuất không đúng định dạng.");
                }
                if (!int.TryParse(soluongtonkho.Text, out int soluongTonkho))
                {
                    throw new FormatException("Số lượng tồn kho không đúng định dạng.");
                }
                if (!int.TryParse(soluongxuatkho.Text, out int soLuongXuatKho))
                {
                    throw new FormatException("Số lượng xuất kho không đúng định dạng.");
                }
                if (!int.TryParse(guna2HtmlLabel6.Text, out int tongtienXuat))
                {
                    throw new FormatException("Tổng tiền xuất không đúng định dạng.");
                }

                if (manhanviens.SelectedValue == null || !int.TryParse(manhanviens.SelectedValue.ToString(), out int maNV))
                {
                    throw new FormatException("Mã nhân viên không đúng định dạng hoặc không được chọn.");
                }

                if (makhachhang.SelectedValue == null || !int.TryParse(makhachhang.SelectedValue.ToString(), out int maKH))
                {
                    throw new FormatException("Mã khách hàng không đúng định dạng hoặc không được chọn.");
                }

                string tenSanPham = guna2TextBox3.Text;
                List<DTO_WarehouseItem> chiTietHoaDon = new List<DTO_WarehouseItem>
        {
            new DTO_WarehouseItem
            {
                MaSanPham = maSanPham,
                TenSanPham = tenSanPham,
                SoLuongTonKho = soluongTonkho,
                SoLuongXuatKho = soLuongXuatKho,
                GiaXuat = donGiaXuat,
            }
        };

                var XuatKhochinh = new DTO_Warehouse
                {
                    MaNV = maNV,
                    TenNV = tennhanvien.Text,
                    MaKH = maKH,
                    TenKH = tenkhachhang.Text,
                    TongTienXuat = tongtienXuat,
                    NgayXuatKho = DateTime.Now
                };

                var result = warehouse.Add(XuatKhochinh, chiTietHoaDon);

                if (result.Success)
                {
                    DialogResult dialogResult = guna2MessageDialog2.Show("Thêm nhập kho thành công! Mã phiếu nhập là: " + XuatKhochinh.MaXuatKho);
                    if (dialogResult == DialogResult.OK)
                    {
                        book.Warehouse();
                        DisplayImport();
                    }
                }
                else
                {
                    MessageBox.Show("Thêm nhập kho thất bại: " + result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho các trường giá và số lượng. " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
        private void DisplayEmployer()
        {
            List<DTO_Employer> employees = new List<DTO_Employer>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                try
                {
                    con.Open();
                    string query = "SELECT MaNhanVien, TenNhanVien FROM tbl_Quan_Ly_Nhan_Vien";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DTO_Employer dTO_Employer = new DTO_Employer()
                        {
                            MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                            TenNhanVien = reader["TenNhanVien"].ToString()
                        };
                        employees.Add(dTO_Employer);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message);
                }
            }
            if (employees.Count > 0)
            {
                manhanviens.DataSource = employees;
                manhanviens.DisplayMember = "MaNhanVien";
                manhanviens.ValueMember = "MaNhanVien";
                manhanviens.Refresh();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhân viên để hiển thị.");
            }
        }
        private void DisplayCus()
        {
            List<DTO_Customers> customers = new List<DTO_Customers>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                try
                {
                    con.Open();
                    string query = "SELECT makhachhang, tenkhachhang FROM tbl_QuanLyKhachHang";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DTO_Customers customer = new DTO_Customers()
                        {
                            makhachhang = reader.GetInt32(reader.GetOrdinal("makhachhang")),
                            tenkhachhang = reader["tenkhachhang"].ToString()
                        };
                        customers.Add(customer);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message);
                }
            }

            if (customers.Count > 0)
            {
                makhachhang.DataSource = customers;
                makhachhang.DisplayMember = "makhachhang"; // Hiển thị mã khách hàng
                makhachhang.ValueMember = "makhachhang"; // Giá trị là mã khách hàng
                makhachhang.Refresh();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng để hiển thị.");
            }
        }

        private void makhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (makhachhang.SelectedItem is DTO_Customers customer)
            {
                tenkhachhang.Text = customer.tenkhachhang;
            }
        }


        private void manhanviens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manhanviens.SelectedItem is DTO_Employer employer)
            {
                tennhanvien.Text = employer.TenNhanVien.ToString();
            }
        }

        private void AddWarehouseBook_Load(object sender, EventArgs e)
        {
            DisplayCus();
            DisplayEmployer();
            DisplayImport();
        }
        private int thanhtiens = 0;
        private void Up()
        {
            int soluongsauxuat;
            int giaxuat;
            if (int.TryParse(guna2TextBox4.Text, out giaxuat) && int.TryParse(soluongxuatkho.Text, out soluongsauxuat))
            {
                thanhtiens = giaxuat * soluongsauxuat;
            }
            else
            {
                thanhtiens = 0;
            }
            guna2HtmlLabel6.Text = thanhtiens.ToString();
        }
        private void soluongxuatkho_TextChanged(object sender, EventArgs e)
        {
            Up();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                guna2TextBox2.Enabled = false;
                guna2TextBox3.Enabled = false;
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                guna2TextBox2.Text = selectedRow.Cells[0].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells[2].Value.ToString();
                guna2TextBox4.Text = selectedRow.Cells[4].Value.ToString();
                soluongtonkho.Text = selectedRow.Cells[6].Value.ToString();
            }
        }
    }
}
